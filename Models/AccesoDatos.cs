using System.Data;
using System.Data.SqlClient;


namespace Clinica_Dental.Models
{
    public class AccesoDatos
    {
        private readonly string _conexion;

        public AccesoDatos(IConfiguration configuracion)
        {
            _conexion = configuracion.GetConnectionString("Conexion");
        }

        // Insertar pasiente con el procedimiento almacenado sp_Insertar_Paciente
        public void AgregarPaciente(Paciente nuevoPaciente)
        {
            using (SqlConnection con = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Insertar_Paciente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cedula", nuevoPaciente.Cedula);
                    cmd.Parameters.AddWithValue("@nombre", nuevoPaciente.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", nuevoPaciente.Apellido);
                    cmd.Parameters.AddWithValue("@telefono", nuevoPaciente.Telefono);
                    cmd.Parameters.AddWithValue("@correo_electronico", nuevoPaciente.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@direccion", nuevoPaciente.Direccion);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", nuevoPaciente.FechaNacimiento);

                    // Parámetro OUTPUT opcional
                    SqlParameter mensajeParam = new SqlParameter("@mensaje", SqlDbType.VarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(mensajeParam);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    
                    string mensaje = mensajeParam.Value.ToString();
                }
            }
        }

        // Actualizar pasiente con el procedimiento almacenado sp_Actualizar_Paciente
        public bool ActualizarPaciente(Paciente paciente, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            using (SqlConnection con = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Actualizar_Paciente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_paciente", paciente.IdPaciente);
                    cmd.Parameters.AddWithValue("@cedula", paciente.Cedula);
                    cmd.Parameters.AddWithValue("@nombre", paciente.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", paciente.Apellido);
                    cmd.Parameters.AddWithValue("@telefono", paciente.Telefono);
                    cmd.Parameters.AddWithValue("@correo_electronico", paciente.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@direccion", paciente.Direccion);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", paciente.FechaNacimiento);

                    SqlParameter mensajeParam = new SqlParameter("@mensaje", SqlDbType.VarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(mensajeParam);

                    con.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    mensaje = mensajeParam.Value.ToString();
                    resultado = filasAfectadas > 0;
                }
            }
            return resultado;
        }

        // Eliminar pasiente con el procedimiento almacenado sp_Eliminar_Paciente
        public bool EliminarPaciente(int idPaciente, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            using (SqlConnection con = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Eliminar_Paciente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_paciente", idPaciente);

                    SqlParameter mensajeParam = new SqlParameter("@mensaje", SqlDbType.VarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(mensajeParam);

                    con.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    mensaje = mensajeParam.Value.ToString();
                    resultado = filasAfectadas > 0;
                }
            }
            return resultado;
        }

        // Optener pasiente con cedula (sp_Obtener_Paciente_Por_Cedula)
        public Paciente ObtenerPacientePorCedula(string cedula)
        {
            Paciente paciente = null;
            using (SqlConnection con = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Obtener_Paciente_Por_Cedula", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cedula", cedula);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            paciente = new Paciente
                            {
                                IdPaciente = Convert.ToInt32(reader["id_paciente"]),
                                Cedula = reader["cedula"].ToString(),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Telefono = reader["telefono"].ToString(),
                                CorreoElectronico = reader["correo_electronico"].ToString(),
                                Direccion = reader["direccion"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"])
                            };
                        }
                    }
                }
            }
            return paciente;
        }

        // Insertar cita sp_Insertar_Cita
        public void InsertarCita(Cita cita)
        {
            using (SqlConnection con = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Insertar_Cita", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_paciente", cita.IdPaciente);
                    cmd.Parameters.AddWithValue("@fecha_hora", cita.FechaHora);
                    cmd.Parameters.AddWithValue("@motivo_consulta", cita.MotivoConsulta);
                    cmd.Parameters.AddWithValue("@estado", cita.Estado);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Obtener cita sp_Obtener_Citas_Del_Dia
        public DataTable ObtenerCitasDelDia(DateTime fecha)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Obtener_Citas_Del_Dia", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fecha", fecha.Date);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}

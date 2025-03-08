using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration; // Asegúrate de tener la referencia a Microsoft.Extensions.Configuration

namespace Clinica_Dental.Models
{
    public class AccesoDatos
    {
        private readonly string _conexion;

        // Constructor que recibe IConfiguration para obtener la cadena de conexión
        public AccesoDatos(IConfiguration configuracion)
        {
            _conexion = configuracion.GetConnectionString("Conexion");
        }

        // Método que busca crear un nuevo paciente
        public void AgregarPaciente(Pacientes nuevoPaciente)
        {
            using (SqlConnection con = new SqlConnection(_conexion))
            {
                try
                {
                    string query = "Exec sp_Insertar_Paciente @cedula, @nombre, @apellido, @telefono, @correo_electronico, @direccion, @fecha_nacimiento, @adicionado_por";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@cedula", nuevoPaciente.Cedula);
                        cmd.Parameters.AddWithValue("@nombre", nuevoPaciente.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", nuevoPaciente.Apellido);
                        cmd.Parameters.AddWithValue("@telefono", nuevoPaciente.Telefono);
                        cmd.Parameters.AddWithValue("@correo_electronico", nuevoPaciente.Correo_electronico);
                        cmd.Parameters.AddWithValue("@direccion", nuevoPaciente.Direccion);
                        cmd.Parameters.AddWithValue("@fecha_nacimiento", nuevoPaciente.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@adicionado_por", nuevoPaciente.AdicionadoPor);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar el paciente: " + ex.Message);
                }
            }
        }
    }
}

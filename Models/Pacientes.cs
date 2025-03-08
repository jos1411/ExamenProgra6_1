namespace Clinica_Dental.Models
{
    public class Pacientes
    {
        public int Id_paciente { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo_electronico { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
        public DateTime? Fecha_modificacion { get; set; }
        public string Modificado_por { get; set; }
    }
}

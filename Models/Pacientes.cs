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
        public System.DateTime Fecha_nacimiento { get; set; }
        public System.DateTime Fecha_adicion { get; set; }
        public string Adicionado_por { get; set; }
        public System.DateTime? Fecha_modificacion { get; set; }
        public string Modificado_por { get; set; }
    }
}

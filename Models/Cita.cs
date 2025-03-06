namespace Clinica_Dental.Models
{
    public class Cita
    {
        public int Id_cita { get; set; }
        public int Id_paciente { get; set; }
        public System.DateTime Fecha_hora { get; set; }
        public string Motivo_consulta { get; set; }
        public string Estado { get; set; }
        public System.DateTime Fecha_adicion { get; set; }
        public string Adicionado_por { get; set; }
        public System.DateTime? Fecha_modificacion { get; set; }
        public string Modificado_por { get; set; }
    }
}

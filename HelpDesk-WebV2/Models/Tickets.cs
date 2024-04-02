namespace HelpDesk_WebV2.Models
{
    public class Tickets
    {
        public int n_ticket { get; set; }
        public DateTime fech_registro { get; set; }
        public DateTime? fech_termino { get; set; }
        public string usu_registro { get; set; }
        public DateTime hora_registro { get; set; }
        public DateTime? hora_termino { get; set; }
        public string estado { get; set; }
    }
}

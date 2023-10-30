namespace API.Models.Dtos
{
    public class BranchDto
    {
        public int id { get; set; } = 0;
        public int code { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public string identification { get; set; }
        public DateTime dateCreate { get; set; }
        public int idCurrency { get; set; }
    }
}

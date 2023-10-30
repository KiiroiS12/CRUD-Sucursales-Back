namespace API.Models.Dtos
{
    public class GeneralResponse
    {
        public string title { get; set; } = default!;
        public string message { get; set; } = default!;
        public int status { get; set; }
        public object data { get; set; } = default!;
    }
}

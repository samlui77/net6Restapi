namespace ytRESTfulAPI.Models
{
    public record Documents
    {
        public int Id { get; set; }
        public string DocNo { get; set; }
        public string DocType { get; set; } = String.Empty;


    }
}

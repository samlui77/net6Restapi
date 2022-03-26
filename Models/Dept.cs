using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ytRESTfulAPI.Models
{
    public class Dept
    {
        internal int id;

        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("compId")]
        public int CompId { get; set; }
        [JsonPropertyName("cname")]
        public string? CName { get; set; }
        [JsonPropertyName("ename")]
        public string EName { get; set; } = String.Empty;

    }
}

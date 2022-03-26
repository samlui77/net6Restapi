using System.ComponentModel.DataAnnotations;

namespace ytRESTfulAPI.Models
{
    public class ST_USER
    {
        [Key]
        public int UserId { get; set; }
        public int EmpId { get; set; }
        public int CompId { get; set; }
        public string LoginName { get; set; }

    }
}

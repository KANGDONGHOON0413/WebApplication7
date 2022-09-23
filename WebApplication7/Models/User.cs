using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class User
    {
        [Key]
        public int UserNo { get; set; }


        [Required(ErrorMessage ="Id 칸이 비어있습니다")]
        public string UserID { get; set; }


        [Required]
        public string UserPW { get; set; }


        [Required]
        public string UserPhone { get; set; }
    }
}

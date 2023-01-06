

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Reservation_System_.Models.ViewModels
{
    public class LoginViewModel
    {
        [Key]
        [DisplayName("Adres email")]
        [Required]
        public string EmailProvider { get; set; }
        [DisplayName("Hasło")]
        [DataType(DataType.Password)]
        [Required]
        public string PasswordProvider { get; set; }
    }
}

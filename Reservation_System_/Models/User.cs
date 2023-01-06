using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Reservation_System_.Models
{
    public class User
    {
        public int id { get; set; }
        [DisplayName("Imię i nazwisko")]
        public string name { get; set; }
        [DisplayName("Adres e-mail")]
        [EmailAddress]
        [EmailValid]
        public string email { get; set; }
        [DisplayName("Hasło")]
        [DataType(DataType.Password)]
        [PasswordValidation]
        public string passwordHashcode { get; set; }

    }
}

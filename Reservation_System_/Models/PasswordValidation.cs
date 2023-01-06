using Reservation_System_.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Reservation_System_.Models
{
    public class PasswordValidation : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var _context = (ApplicationDbContext)validationContext
                               .GetService(typeof(ApplicationDbContext));
            var inputPassword = (string)value;

            var regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (inputPassword != null && regex.IsMatch(inputPassword))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    "Hasło powinno mieć minimum 8 znaków i składać się z:   " +
                    "- przynajmniej jednej wielkiej litery" +
                    "- przynajmniej jednej małej litery" +
                    "- przynajmniej jednej cyfry" +
                    "- przynajmniej jednego znaku specjalnego");
            }
        }
    }
}

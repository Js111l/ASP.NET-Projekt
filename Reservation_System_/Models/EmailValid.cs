using Reservation_System_.Data;
using System.ComponentModel.DataAnnotations;

namespace Reservation_System_.Models
{
    public class EmailValid : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _context = (ApplicationDbContext)validationContext
                            .GetService(typeof(ApplicationDbContext));

            string userEmail = (string)value;

            if (_context.User.Any(x => x.email.Equals(userEmail)))
            {
                return new ValidationResult("Wybrany adres email jest już zajęty!");
            }
            else
            {
                return ValidationResult.Success;
            }

        }
    }

}

using Newtonsoft.Json.Linq;
using Reservation_System_.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Reservation_System_.Models
{
    public class StartDateValid : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var _context = (ApplicationDbContext)validationContext
                             .GetService(typeof(ApplicationDbContext));

            DateTime startDate = Convert.ToDateTime(value);
            var reservationDates = _context.Reservation.Where(x => x.StartReservation.Day.Equals(startDate.Day)).ToArray();
            foreach (var item in reservationDates)
            {
                if (item.StartReservation <= startDate && startDate <= item.EndReservation)
                {
                    return new ValidationResult(" Wybrany termin jest zajęty  ");
                }
            }

            if (startDate.Hour <= new DateTime(2022, 1, 1, 20, 00, 00).Hour
                && startDate.Hour >= new DateTime(2022, 1, 1, 8, 00, 00).Hour
                )
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(" Godziny rezerwacji: 8:00 - 20:00 ");
            }
        }
    }
}

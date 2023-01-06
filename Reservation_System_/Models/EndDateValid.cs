using Newtonsoft.Json.Linq;
using Reservation_System_.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;


namespace Reservation_System_.Models
{
    public class EndDateValid : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _context = (ApplicationDbContext)validationContext
                            .GetService(typeof(ApplicationDbContext));


            var endDate = Convert.ToDateTime(value);
            var reservationDates = _context.Reservation.ToArray();


            foreach (var item in reservationDates)
            {
                if (item.StartReservation <= endDate && endDate <= item.EndReservation)
                {
                    return new ValidationResult(" Wybrany termin jest zajęty");
                }
            }


            if (endDate.Hour <= new DateTime(2023, 12, 12, 20, 00, 00).Hour
                 && endDate.Hour >= new DateTime(2023, 12, 12, 8, 00, 00).Hour
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


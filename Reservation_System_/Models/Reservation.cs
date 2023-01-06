using Reservation_System_.Data;
using Reservation_System_.Models;
using System.ComponentModel;

namespace System_rezerwacji_sal.Models
{
    public class Reservation
    {

        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Numer sali")]
        public int RoomId { get; set; }
        [DisplayName("Zarezerwowane dla:")]
        public string Name { get; set; }

        [DisplayName("Rezerwacja od")]
        [StartDateValid]
        public DateTime StartReservation { get; set; }

        public DateTime _EndReservation;


        [DisplayName("Rezerwacja do")]
        [EndDateValid]
        public DateTime EndReservation
        {
            get
            {
                return _EndReservation;
            }
            set
            {
                value = StartReservation.AddHours(value.Hour);
                _EndReservation = value;
            }
        }


    }
}

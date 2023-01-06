using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System_rezerwacji_sal.Models
{
    public class Room
    {

        public int Id { get; set; }

        [DisplayName("Numer sali")]
        public int number { get; set; }

        [DisplayName("Liczba miejsc")]
        public int numberOfSeats { get; set; }

        [DisplayName("Projektor")]
        public bool hasProjector { get; set; }

        public string _roomType { get; set; }

        [DisplayName("Typ sali")]
        public string roomType
        {
            get
            {
                var returnVal = _roomType.Equals("lab") ? "Laboratorium" : "Sala wykładowa";
                return returnVal;
            }
            set
            {
                _roomType = value;
            }
        }

        [DisplayName("Klimatyzacja")]
        public bool hasAirConditioner { get; set; }

        public Room()
        {

        }
        public Room(int id, int number, int numberOfSeats, bool hasProjector, string roomType, bool hasAirConditioner)
        {
            Id = id;
            this.number = number;
            this.numberOfSeats = numberOfSeats;
            this.hasProjector = hasProjector;
            this.roomType = roomType;
            this.hasAirConditioner = hasAirConditioner;
        }

    }
}

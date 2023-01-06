using Microsoft.AspNetCore.Mvc.Rendering;

namespace Reservation_System_.Models.ViewModels
{
    public class RoomViewModel
    {
        public int selectedNumber { get; set; }
        public string selectedType { get; set; }
        public string optionProjector { get; set; }
        public string optionAirCond { get; set; }

        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public List<SelectListItem> selectSeats { get; set; }
        public List<SelectListItem> selectType { get; set; }
        public List<SelectListItem> selectAirConditioner { get; set; }
        public List<SelectListItem> selectProjector { get; set; }
    }
}

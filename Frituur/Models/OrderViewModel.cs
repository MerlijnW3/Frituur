using Microsoft.AspNetCore.Mvc.Rendering;

namespace Frituur.Models
{
    public class OrderViewModel
    {
        public int UserId { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
        public List<int> SelectedProductIds { get; set; }
        public List<int> Quantities { get; set; } = new List<int>();
    }
}

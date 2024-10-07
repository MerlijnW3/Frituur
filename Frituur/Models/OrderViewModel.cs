using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Frituur.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public List<int> SelectedProductIds { get; set; }
        public List<SelectListItem> Products { get; set; }
        public DateTime OrderDate { get; set; }

        public OrderViewModel()
        {
            SelectedProductIds = new List<int>();
            Products = new List<SelectListItem>();
        }
    }
}

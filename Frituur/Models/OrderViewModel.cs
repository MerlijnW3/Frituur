using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Frituur.Models
{
    public class OrderViewModel
    {
        [Required]
        public int UserId { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; } = new List<SelectListItem>();

        [Required]
        public List<int> SelectedProductIds { get; set; } = new List<int>();

        [Required]
        public List<int> Quantities { get; set; } = new List<int>();
    }
}

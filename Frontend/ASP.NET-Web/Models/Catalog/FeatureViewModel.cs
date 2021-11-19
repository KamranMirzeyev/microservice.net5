using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Web.Models.Catalog
{
    public class FeatureViewModel
    {
        [Display(Name = "Kurs süre")]
        public int Duration { get; set; }
    }
}

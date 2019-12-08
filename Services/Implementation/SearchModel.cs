using Database.Entities;
using Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.Implementation
{
    public class SearchModel
    {
        public IEnumerable<Currency> Currencies { get; set; }
        public IEnumerable<Iata> IataCodes { get; set; }
        public List<OfferItem> Offers { get; set; }
        public string LocalStoragekey { get; set; }

        [Required(ErrorMessage = "Please enter Currency Code.")]
        [StringLength(3)]
        [Display(Name = "Currency Code")]
        public string CurrencyCode { get; set; }

        [Required(ErrorMessage = "Please enter Origin Airport.")]
        [Display(Name = "Origin Airport")]
        public string OriginCode { get; set; }

        [Required(ErrorMessage = "Please enter Destination Airport.")]
        [Display(Name = "Destination Airport")]
        public string DestinationCode { get; set; }

        [Required(ErrorMessage = "Please enter Departure Date.")]
        [Display(Name = "Departure Date")]
        public string DepartureDate { get; set; }

        [Required(ErrorMessage = "Please enter Return Date.")]
        [Display(Name = "Return Date")]
        public string ReturnDate { get; set; }

        [Required(ErrorMessage = "Please enter number of Passengers.")]
        [Display(Name = "Passengers")]
        public string Passengers { get; set; }
    }
}

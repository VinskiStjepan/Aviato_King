using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Implementation
{
    public partial class AmadeusJson
    {
        [JsonProperty("data")]
        public List<Data> data { get; set; }

        public List<OfferItem> GetAllOffers()
        {
            List<OfferItem> Offers = new List<OfferItem>();
            foreach (Data dataItem in data)
            {
                foreach (OfferItem offer in dataItem.OfferItems)
                {
                    Offers.Add(offer);
                }
            }
            return Offers;
        }
    }

    public partial class Data
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("offerItems")]
        public List<OfferItem> OfferItems { get; set; }
    }

    public partial class OfferItem
    {
        [JsonProperty("services")]
        public List<Service> Services { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("pricePerAdult")]
        public Price PricePerAdult { get; set; }
    }

    public partial class Price
    {
        [JsonProperty("total")]
        public string Total { get; set; }

        [JsonProperty("totalTaxes")]
        public string TotalTaxes { get; set; }
    }

    public partial class Service
    {
        [JsonProperty("segments")]
        public List<Segment> Segments { get; set; }
    }

    public partial class Segment
    {
        [JsonProperty("flightSegment")]
        public FlightSegment FlightSegment { get; set; }
    }

    public partial class FlightSegment
    {
        [JsonProperty("departure")]
        public Arrival Departure { get; set; }

        [JsonProperty("arrival")]
        public Arrival Arrival { get; set; }

        [JsonProperty("carrierCode")]
        public string CarrierCode { get; set; }

        [JsonProperty("operating")]
        public Operating Operating { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }
    }

    public partial class Arrival
    {
        [JsonProperty("iataCode")]
        public string IataCode { get; set; }

        [JsonProperty("terminal")]
        public string Terminal { get; set; }

        [JsonProperty("at")]
        public string At { get; set; }
    }

    public partial class Operating
    {
        [JsonProperty("carrierCode")]
        public string CarrierCode { get; set; }

        [JsonProperty("number")]
        public String Number { get; set; }
    }
}

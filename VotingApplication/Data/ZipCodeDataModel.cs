using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApplication
{
    public class ZipCodeDataModel
    {
        [Key]
        public int Zip { get; set; }

        [Required]
        [MaxLength(64)]
        public string PrimaryCity { get; set; }

        [Required]
        [MaxLength(2)]
        public string State { get; set; }

        [Required]
        [MaxLength(64)]
        public string County { get; set; }

        [Required]
        [MaxLength(64)]
        public string Timezone { get; set; }

        [Required]
        [MaxLength(2)]
        public string Country { get; set; }

        [Required]
        [Column(TypeName = "numeric(8,3)")]
        public decimal Latitude { get; set; }

        [Required]
        [Column(TypeName = "numeric(8,3)")]
        public decimal Longitude { get; set; }

        [Required]
        [MaxLength(512)]
        public string Geometry { get; set; }

        public ICollection<ZipCodeFillsDistrict> District { get; set; }

        public ICollection<VoterDemographicsDataModel> Residents { get; set; }
    }

    #region Used to seralize the datamodel to Json
    public class ZipCodeFeatureCollection
    {
        public readonly string type = "FeatureCollection";
        public ICollection<ZipCodeFeature> features = new List<ZipCodeFeature>();
    }

    public class ZipCodeFeature
    {
        public readonly string type = "Feature";
        public Properties properties;
        public Geometry geometry;
    }

    public class Properties
    {
        public string PrimaryCity;
        public string State;
        public string County;
        public string Timezone;
        public string Country;
        public string Latitude;
        public string Longitude;

        public Properties(ZipCodeDataModel data)
        {
            PrimaryCity = data.PrimaryCity;
            State = data.State;
            County = data.County;
            Timezone = data.Timezone;
            Country = data.Country;
            Latitude = data.Latitude.ToString();
            Longitude = data.Longitude.ToString();
        }
    }

    public class Geometry
    {
        public readonly string type = "Polygon";
        public JArray coordinates = new JArray();

        public Geometry(ZipCodeDataModel data)
        {
            coordinates.Add((JArray)JsonConvert.DeserializeObject(data.Geometry));
            var first = coordinates.First.First;
            ((JArray)coordinates.First).Add(first);
        }
    }
    #endregion
}

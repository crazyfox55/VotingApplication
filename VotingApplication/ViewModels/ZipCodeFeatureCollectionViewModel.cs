using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.ViewModels
{
    public class ZipCodeFeatureCollectionViewModel
    {
        public ZipCodeFeatureCollection ZipCodes { get; set; }
        public string JsonFeatureCollection => JsonConvert.SerializeObject(ZipCodes);
    }
    
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
}

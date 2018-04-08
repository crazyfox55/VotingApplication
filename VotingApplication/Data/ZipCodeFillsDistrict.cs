using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication
{
    //DataModel naming convention not used. Database takes name of class as bridge table name
    public class ZipCodeFillsDistrict
    {
        public int ZipCode { get; set; }
        public int DistrictId { get; set; }

        public ZipCodeDataModel Zip { get; set; }
        public DistrictDataModel District { get; set; }

    }
}

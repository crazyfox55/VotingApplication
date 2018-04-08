using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication
{
    //DataModel naming convention not used. Database takes name of class as bridge table name
    public class DistrictFillsRegion
    {
        public int DistrictId { get; set; }
        public int RegionId { get; set; }
        
        public DistrictDataModel District { get; set; }
        public RegionDataModel Region { get; set; }
    }
}

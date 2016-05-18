using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WorkPlaceInfo
    {
        public Guid ID { get; set; }
        public string PlaceName { get; set; }
        public string PlaceCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}

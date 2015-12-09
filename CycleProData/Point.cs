using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclePro.Models
{
    public class Point
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public double? Elevation { get; set; }
        //public int HeartRate { get; set }
        public int HeartRate { get; set; }
        public DateTime Time { get; set; }
    }
}

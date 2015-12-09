using System.Collections.Generic;
using System.Xml.Linq;

namespace CyclePro.Models
{
    public partial class Route
    {
        public enum DocumentNsType
        {
            Gpx = 1
        }

        public XDocument Document { get; set; }
        public List<Point> Points { get; set; }
    }
}

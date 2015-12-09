using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclePro.Models
{
    public partial class Route
    {
        public List<Point> RemovePoint(string lat, string lon)
        {
            this.Points.RemoveAll(p => p.Latitude == lat && p.Longitude == lon);
            return this.Points;
        }
    }
}

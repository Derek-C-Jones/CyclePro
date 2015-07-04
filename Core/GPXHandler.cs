using CyclePro.Core.Exceptions;
using CyclePro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CyclePro.Core
{
    public class GPXHandler : XMLHandlerBase
    {
        public string documentNamespace = "http://www.topografix.com/GPX/1/1";

        public override XDocument LoadDocument(string location, LoadType type)
        {
            XDocument doc = new XDocument();
            try
            {
                if (type == LoadType.Disk)
                {
                    doc = loadFromDisk(location);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidInputFileException(ex);
            }
            return doc;
        }

        public override Route CreateRoute(XDocument doc)
        {
            Route route = new Route();
            route.Document = doc;
            route.Points = LoadPoints(doc);
            return route;
        }

        public override List<Point> LoadPoints(XDocument doc)
        {
            List<Point> result = new List<Point>();
            XNamespace ns = documentNamespace;

            var entries = from entry in doc.Descendants(ns + "trk")
                          select new
                          {
                              Points = (
                                  from trackpoint in entry.Descendants(ns + "trkpt")
                                  select new CyclePro.Models.Point
                                  {
                                      Latitude = Convert.ToDouble(trackpoint.Attribute("lat").Value),
                                      Longitude = Convert.ToDouble(trackpoint.Attribute("lon").Value),
                                      Elevation = Convert.ToDouble(trackpoint.Element(ns + "ele") != null ? trackpoint.Element(ns + "ele").Value : null),
                                      Time = DateTime.Parse(trackpoint.Element(ns + "time") != null ? trackpoint.Element(ns + "time").Value : null)
                                  }
                              )
                          };

            entries.ToList();

            if (entries.Count() > 1)
            {
                throw new InvalidInputFileException("The input file that you specified contains more than one trk element. Only a single trk element per input file is currently supported.");
            }
            else if (entries.Count() == 1)
            {
                if (entries.ElementAt(0).Points != null)
                {
                    result = entries.ElementAt(0).Points.ToList();
                }
            }
            return result;
        }

    }
}

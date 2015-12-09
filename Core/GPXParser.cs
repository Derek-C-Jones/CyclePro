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
    public class GPXParser : XMLParserBase, IXMLParser
    {
        public string gpxNamespace = "http://www.topografix.com/GPX/1/1";
        public string gpxtpxNamespace = "http://www.garmin.com/xmlschemas/TrackPointExtension/v1";

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
            XNamespace gpxNamespace = this.gpxNamespace;
            XNamespace gpxtpxNamespace = this.gpxtpxNamespace;

            var test = from entry in doc.Descendants(gpxNamespace + "trk")
                          select new
                          {
                              Points = (
                                  from trackpoint in entry.Descendants(gpxNamespace + "trkpt")
                                  select new 
                                  {
                                      Elevation = Convert.ToDouble(trackpoint.Element(gpxNamespace + "ele") != null ? trackpoint.Element(gpxNamespace + "ele").Value : null),
                                      hasHrExtension = trackpoint.Descendants(gpxNamespace + "extensions").Any() && trackpoint.Element(gpxNamespace + "extensions").Descendants(gpxtpxNamespace + "TrackPointExtension").Any() && trackpoint.Element(gpxNamespace + "extensions").Element(gpxtpxNamespace + "TrackPointExtension").Descendants(gpxtpxNamespace + "hr").Any(),
                                      HeartRate = trackpoint.Element(gpxNamespace + "extensions")
                                  })
                          };

            var entries = from entry in doc.Descendants(gpxNamespace + "trk")
                          select new
                          {
                              Points = (
                                  from trackpoint in entry.Descendants(gpxNamespace + "trkpt")
                                  select new CyclePro.Models.Point
                                  {
                                      Latitude = trackpoint.Attribute("lat").Value,
                                      Longitude = trackpoint.Attribute("lon").Value,
                                      Elevation = Convert.ToDouble(trackpoint.Element(gpxNamespace + "ele") != null ? trackpoint.Element(gpxNamespace + "ele").Value : null),
                                      Time = DateTime.Parse(trackpoint.Element(gpxNamespace + "time") != null ? trackpoint.Element(gpxNamespace + "time").Value : null),
                                      HeartRate = Convert.ToInt32(trackpoint.Descendants(gpxNamespace + "extensions").Any() && trackpoint.Element(gpxNamespace + "extensions").Descendants(gpxtpxNamespace + "TrackPointExtension").Any() && trackpoint.Element(gpxNamespace + "extensions").Element(gpxtpxNamespace + "TrackPointExtension").Descendants(gpxtpxNamespace + "hr").Any() == true ? trackpoint.Element(gpxNamespace + "extensions").Elements(gpxtpxNamespace + "TrackPointExtension").Elements(gpxtpxNamespace + "hr").FirstOrDefault().Value : "0")
                                    })
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

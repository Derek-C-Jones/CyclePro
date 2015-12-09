using CyclePro.Core;
using CyclePro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CyclePro.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            GPXParser gpxHandler = new GPXParser();
            XDocument doc = gpxHandler.LoadDocument("ShortAfternoonRide.gpx", XMLParserBase.LoadType.Disk);
            Console.WriteLine(doc.ToString());
            Route testRoute = gpxHandler.CreateRoute(doc);
            testRoute.RemovePoint("-25.8286530", "28.2942930");
            Console.ReadKey();
        }
    }
}

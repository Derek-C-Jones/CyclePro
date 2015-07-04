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
            GPXHandler gpxHandler = new GPXHandler();
            XDocument doc = gpxHandler.LoadDocument("2015062711500.gpx", CyclePro.Core.XMLHandlerBase.LoadType.Disk);
            Console.WriteLine(doc.ToString());
            Route testRout = gpxHandler.CreateRoute(doc);
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CyclePro.Core.Exceptions;
using CyclePro.Models;

namespace CyclePro.Core
{
    public abstract class XMLHandlerBase
    {
        public enum LoadType 
        {
            Disk = 1
        }

        public abstract XDocument LoadDocument(string location, LoadType type);

        public abstract Route CreateRoute(XDocument doc);        

        public abstract List<Point> LoadPoints(XDocument doc);

        protected XDocument loadFromDisk(string location) 
        {
            XDocument kmlDoc = new XDocument();
            kmlDoc = XDocument.Load(location);
            return kmlDoc;
        }
    }
}

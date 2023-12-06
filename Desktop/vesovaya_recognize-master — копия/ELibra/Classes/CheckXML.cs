using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ELibra.Classes
{
    class CheckXML
    {

        public static void RemoveExpiredDate()
        {
            if (File.Exists("data.xml"))
            {
                XDocument xDoc = XDocument.Load("data.xml");
                try
                {
                    CreateDate(xDoc, "point1");
                    CreateDate(xDoc, "point2");
                    CreateDate(xDoc, "numOrder");
                    CreateDate(xDoc, "posOrder");

                    xDoc.Descendants("point1").Where(x => (DateTime.Parse(x.Attribute("date").Value).AddDays(45).Date < DateTime.Now)).Remove();
                    xDoc.Descendants("point2").Where(x => (DateTime.Parse(x.Attribute("date").Value).AddDays(45).Date < DateTime.Now)).Remove();
                    xDoc.Descendants("numOrder").Where(x => (DateTime.Parse(x.Attribute("date").Value).AddDays(45).Date < DateTime.Now)).Remove();
                    xDoc.Descendants("posOrder").Where(x => (DateTime.Parse(x.Attribute("date").Value).AddDays(45).Date < DateTime.Now)).Remove();

                    xDoc.Save("data.xml");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }                
            }
        }

        private static void CreateDate(XDocument xDoc, string attribute)
        {
            foreach (var xElem in xDoc.Descendants(attribute))
            {
                if (xElem.Attribute("date") == null)
                {
                    xElem.Add(new XAttribute("date", DateTime.Now.Date.ToString()));
                }
            }
        }
    }
}

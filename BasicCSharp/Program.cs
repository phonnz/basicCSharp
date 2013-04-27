using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace BasicCSharp
{
    /**
     * 27-04-2013
     * phonnz
     * 
     *Basic rss reading > processing > displaying proccessed data
     *Using System.Xml.Linq, Linq
     */
    class Program
    {
        static void Main(string[] args)
        {
           //Here you can change your rss or create a string dictionary to load more than one feeds 
           string urlfeed = "http://ep00.epimg.net/rss/internacional/portada.xml" ;

           XDocument feed = XDocument.Load(urlfeed);

            //Obtaining nodes to organize them in a object list
           var feedData = (from item in feed.Descendants("channel").Descendants("item")
                           select new
                           {
                               Title = item.Element("title").Value,
                               Description = item.Element("description").Value,
                               Cont = item.Element("description").Value,
                               Link = item.Element("link").Value,
                               PubDate = item.Element("pubDate").Value

                           }
                           ).ToList();


           var byDate = (from bd in feedData orderby bd.PubDate ascending select bd).ToList();
           var byTitle = (from t in feedData orderby t.Title ascending select t).ToList();
           var fromMX = (from m in feedData where m.Title.Contains("México") || m.Description.Contains("México") || m.Cont.Contains("México") select m).ToList();

           Console.WriteLine("*** Sorted by date ***");
           foreach (var i in byDate)
           { 
               string toPrint = i.PubDate.ToString();
               Console.WriteLine(toPrint);
           }
           string str = Console.ReadLine();
           Console.WriteLine("*** Sorted by title ***");
           foreach (var i in byTitle)
           {
               string toPrint = i.Title ;
               Console.WriteLine(toPrint);
           }
           str = Console.ReadLine();
           Console.WriteLine("*** Contains the word México ***");
           if (fromMX.Count() > 0)
           {
               foreach (var i in fromMX)
               {
                   string toPrint = i.Title + " - " + i.Description.ToString();
                   Console.WriteLine(toPrint);
               }
           }
           else { Console.WriteLine("We don't have news from México"); }
           str = Console.ReadLine();
        }
    }
}

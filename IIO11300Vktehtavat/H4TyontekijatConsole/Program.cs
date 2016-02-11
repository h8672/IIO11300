using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace H4TyontekijatConsole
{
    class Program
    {
        static void ReadWorkersFromXML(string filename)
        {
            try
            {

                //Tutkitaan onko tiedostoa olemassa
                if (System.IO.File.Exists(filename))
                {
                    //Luetaan XML-tiedosto XMLDocument-olioon
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(filename);
                    //Haetaan kaikki työntekijä-elementit XPathilla
                    XmlNodeList xnl = xmldoc.SelectNodes("/tyontekijat/tyontekija");
                    XmlNode xn; //Edustaa yksittäistä noodia
                    XmlNodeList xnl2; //Edustaa yksittäistä noodia
                    //Loop nodelista läpi
                    for (int i = 0; i < xnl.Count; i++)
                    {
                        xn = xnl.Item(i);
                        Console.WriteLine(xn.InnerText);
                        xnl2 = xn.ChildNodes;
                        for (int j = 0; j < xnl2.Count; j++)
                        {
                            Console.WriteLine(xnl2.Item(j).InnerText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        static void CalculateSalarySumFromXML(string filename)
        {
            try
            {
                //ReadWorkerksFromXML("d:\\Työntekijät.xml")
                //Tutkitaan onko tiedostoa olemassa
                if (System.IO.File.Exists(filename))
                {
                    //Luetaan XML-tiedosto XMLDocument-olioon
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(filename);
                    //Haetaan kaikkien vakituisten työntekijöitten palkka-elementit XPathilla
                    XmlNodeList xnl = xmldoc.SelectNodes("/tyontekijat/tyontekija[tyosuhde='vakituinen']/palkka");
                    //Loopitetaan nodelista läpi
                    int yht = 0;
                    XmlNode xn; //Edustaa yksittäistä noodia
                    XmlNodeList xnl2; //Edustaa yksittäistä noodia
                    //Loop nodelista läpi
                    for (int i = 0; i < xnl.Count; i++)
                    {
                        yht += Convert.ToInt32(xnl.Item(i).InnerText);
                    }
                    Console.WriteLine(string.Format("Vakituisia on {0} ja heidän palkat yhteensä {1}", xnl.Count, yht));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        static void Main(string[] args)
        {
            try
            {
                //ReadWorkersFromXML("d:\\Työntekijät.xml");
                CalculateSalarySumFromXML("d:\\Työntekijät.xml");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}

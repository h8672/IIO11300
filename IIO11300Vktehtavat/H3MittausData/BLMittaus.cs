using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAMK.IT.IIO11300
{
    [Serializable]
    public class MittausData
    {
        #region PROPERTIES
        private string kello;
        public string Kello
        {
            get { return kello; }
            set { kello = value; }
        }
        private string mittaus;

        public string Mittaus
        {
            get { return mittaus; }
            set { mittaus = value; }
        }
        #endregion
        #region CONSTRUCTORS
        //luokalle tehdään kaksi konstruktoria
        public MittausData()
        {
            kello = "0:00";
            mittaus = "empty";
        }
        public MittausData(string klo, string mdata)
        {
            this.kello = klo;
            this.mittaus = mdata;
        }
        #endregion
        #region METHODS
        //ylikirjoitetaan ToString
        public override string ToString()
        {
            //return base.ToString();
            return kello + "=" + mittaus;
        }
        //Tiedoston käsittely metodit
        public static void SaveToFile(string filename, List<MittausData> data)
        {
            StreamWriter sw;
            //Tutkitaan onko tiedosto jo tehty
            if (File.Exists(filename))
            {
                //Liitetään olemassaolevaan tiedostoon
                sw = File.AppendText(filename);
            }
            else
            {
                //Luodaan uusi
                sw = File.CreateText(filename);
            }
            //Kirjoitus
            foreach (var item in data)
            {
                sw.WriteLine(item);
            }
            sw.Close();
        }
        public static void SaveToFileV2(string filename, List<MittausData> data)
        {
            //Luodaan uusi tai kirjoitetaan olemassaolevaan tiedostoon
            StreamWriter sw = File.AppendText(filename);
            //Kirjoitus
            foreach (var item in data)
            {
                sw.WriteLine(item);
            }
            sw.Close();
        }
        public static List<MittausData> ReadFromFile(string filename)
        {
            try
            {
                List<MittausData> luetut;
                if (File.Exists(filename))
                {
                    //Luetaan tekstitiedosto ja muutetaan se Mittausdata-olioiksi
                    MittausData md;
                    luetut = new List<MittausData>();
                    string rivi = "";
                    StreamReader sr = File.OpenText(filename);
                    rivi = sr.ReadLine();
                    while((rivi = sr.ReadLine()) != null)
                    {
                        //Etsii joka riviltä tulosta, joka sisältää yhtäsuuruusmerkin
                        if(rivi.Length > 3 && rivi.Contains("="))
                        {
                            string[] split = rivi.Split(new char[] { '=' });
                            //alimerkkijonoista luodaan olio
                            md = new MittausData(split[0], split[1]);
                            luetut.Add(md);
                        }
                    }
                    sr.Close();
                    return luetut;
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

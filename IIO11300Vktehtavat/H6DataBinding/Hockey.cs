using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6DataBinding
{
    public static class TestHockeyBench
    {
        public static List<HockeyPlayer> Get3Players()
        {
            List<HockeyPlayer> players = new List<HockeyPlayer>();
            players.Add(new HockeyPlayer("Teemu Selänne", "8"));
            players.Add(new HockeyPlayer("Jarkko Immonen", "26"));
            players.Add(new HockeyPlayer("Ville Peltonen", "16"));
            return players;
        }
    }
    public class HockeyPlayer : INotifyPropertyChanged
    {
        private string name;
        private string number;

        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public string Name {
            get {
                return name;
            }
            set
            {
                name = value;
                Notify("Name");
                Notify("NameAndNumber");
            }
        }
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                Notify("Number");
                Notify("NameAndNumber");
            }
        }
        public string NameAndNumber
        {
            get
            {
                return name + "#" + number;
            }
        }

        public HockeyPlayer()
        {
            name = "UnnamedSoldier";
            Number = "666";
        }
        public HockeyPlayer(string name, string number)
        {
            this.name = name;
            this.number = number;
        }
    }

    public class HockeyTeam
    {
        #region PROPERTIES
        //HUOM! public field ei kelpaa XAMLin bindauksessa, pitää olla Property
        public string Name { get; set; }
        public string City { get; set; }
        #endregion
        #region CONSTRUCTORS
        public HockeyTeam()
        {
            Name = "";
            City = "none";
        }
        public HockeyTeam(string teamName, string city)
        {
            Name = teamName;
            City = city;
        }
        public override string ToString()
        {
            return Name + "@" + City;
        }
        #endregion
    }

    public class HockeyLeague
    {
        //Perustetaan SMLiiga
        List<HockeyTeam> teams = new List<HockeyTeam>();

        public HockeyLeague()
        {
            teams.Add(new HockeyTeam("Ilves", "Tampere"));
            teams.Add(new HockeyTeam("JYP", "Jyväskylä"));
            teams.Add(new HockeyTeam("HIFK", "Helsinki"));
            teams.Add(new HockeyTeam("Kärpät", "Oulu"));
            teams.Add(new HockeyTeam("Kalpa", "Kuopio"));
        }

        //Metodi joka palauttaa liigan joukkueet
        public List<HockeyTeam> GetTeams()
        {
            return teams;
        }

    }
}

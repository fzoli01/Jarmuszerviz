using LsCostume_GUI.Models;
using LsCostume_GUI.Moduls;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LsCostume_GUI
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();

            string sql = $"SELECT COUNT(*) FROM `alkalmazottak` WHERE Rang!='admin'";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                szereloszam.Text = msdr[0].ToString();

            }
            msdr.Close();
            con.Close();

            sql = "SELECT COUNT(*) FROM `javitasok` WHERE elkeszult=0;";
            con.Open();
            msqlc = new MySqlCommand(sql, con);
            msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                munkaszam.Text = msdr[0].ToString()+ " munka";
            }
            msdr.Close();
            con.Close();

        }
        public List<Alkalmazottak> alkalmazottakList=new List<Alkalmazottak>();
        public List<Alkatreszek> alkatreszekList=new List<Alkatreszek>();
        public List<Ugyfelek> ugyfelekList=new List<Ugyfelek>();
        public List<Javitas> JavitasList=new List<Javitas>();
        public List<Jarmuvek> JarmuvekList=new List<Jarmuvek>();
        public List<IdopontFoglalas> IdopontList=new List<IdopontFoglalas>();
        public string ValasztottTabla = "";

        public void AlkalmazottakToltes()
        {
            alkalmazottakList.Clear();
            ListView.View = null;
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn { Header = "Név", DisplayMemberBinding = new Binding("Nev") });
            gridView.Columns.Add(new GridViewColumn { Header = "Telefonszám", DisplayMemberBinding = new Binding("Telefonszam") });
            gridView.Columns.Add(new GridViewColumn { Header = "E-mail", DisplayMemberBinding = new Binding("Email") });

            ListView.View = gridView;


            DashboardPanel.Visibility = Visibility.Collapsed;
            ListView.Visibility = Visibility.Visible;


            string sql = $"select * from alkalmazottak";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                if (msdr["Rang"].ToString() != "Admin")
                {
                    alkalmazottakList.Add(new Alkalmazottak()
                    {
                        AlkalmazottID = msdr.GetInt32(0),
                        Nev = msdr["Nev"].ToString(),
                        Telefonszam = msdr["Telefonszam"].ToString(),
                        Email = msdr["Email"].ToString()
                    });
                }

            }
            msdr.Close();
            con.Close();

            ListView.ItemsSource = alkalmazottakList;
            ListView.SelectedIndex = -1;
        }
        public void JavitasokToltes()
        {
            JavitasList.Clear();
            ListView.View = null;
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn { Header = "Dátum", DisplayMemberBinding = new Binding("Datum") });
            gridView.Columns.Add(new GridViewColumn { Header = "Leirás", DisplayMemberBinding = new Binding("Leiras") });
            gridView.Columns.Add(new GridViewColumn { Header = "Szerelő neve", DisplayMemberBinding = new Binding("Alkalmazott.Nev") });
            gridView.Columns.Add(new GridViewColumn { Header = "Alvázszám", DisplayMemberBinding = new Binding("JarmuID") });
            gridView.Columns.Add(new GridViewColumn { Header = "Állapot", DisplayMemberBinding = new Binding("Elkeszult") });

            ListView.View = gridView;


            DashboardPanel.Visibility = Visibility.Collapsed;
            ListView.Visibility = Visibility.Visible;

            string sql = $"SELECT JavitasID, Datum, Leiras, alkalmazottak.Nev, jarmuvek.Alvazszam, elkeszult FROM javitasok LEFT JOIN Alkalmazottak  ON javitasok.AlkalmazottID = alkalmazottak.AlkalmazottID LEFT JOIN Jarmuvek  ON javitasok.JarmuID = jarmuvek.Alvazszam;";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                JavitasList.Add(new Javitas(msdr.GetBoolean(5),msdr.GetDateTime(1))
                {
                    JavitasID = msdr.GetInt32(0),
                    Leiras = msdr["Leiras"].ToString(),
                    Alkalmazott = new Alkalmazottak() { Nev = msdr["Nev"] == DBNull.Value ? "Törölt alkalmazott" : msdr["Nev"].ToString() },
                    JarmuID = msdr["Alvazszam"] == DBNull.Value ? "Törölt jármű" : msdr["Alvazszam"].ToString()

                });



            }
            msdr.Close();
            con.Close();

            ListView.ItemsSource = JavitasList;
            ListView.SelectedIndex = -1;
        }
        public void AlkatreszekToltes()
        {
            alkatreszekList.Clear();
            ListView.View = null;
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn { Header = "Név", DisplayMemberBinding = new Binding("AlkatreszNev") });
            gridView.Columns.Add(new GridViewColumn { Header = "Cikkszám", DisplayMemberBinding = new Binding("Cikkszam") });
            gridView.Columns.Add(new GridViewColumn { Header = "Ár", DisplayMemberBinding = new Binding("Ar") });

            ListView.View = gridView;


            DashboardPanel.Visibility = Visibility.Collapsed;
            ListView.Visibility = Visibility.Visible;

            string sql = $"select * from alkatreszek";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                alkatreszekList.Add(new Alkatreszek()
                {
                    AlkatreszID = msdr.GetInt32(0),
                    AlkatreszNev = msdr["AlkatreszNev"].ToString(),
                    Cikkszam = msdr["Cikkszam"].ToString(),
                    Ar = double.Parse(msdr["Ar"].ToString())
                });

            }
            msdr.Close();
            con.Close();

            ListView.ItemsSource = alkatreszekList;
            ListView.SelectedIndex = -1;
        }
        public void UgyfelekToltes()
        {
            ugyfelekList.Clear();
            ListView.View = null;
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn { Header = "Név", DisplayMemberBinding = new Binding("Nev") });
            gridView.Columns.Add(new GridViewColumn { Header = "Telefonszám", DisplayMemberBinding = new Binding("Telefonszam") });
            gridView.Columns.Add(new GridViewColumn { Header = "E-mail", DisplayMemberBinding = new Binding("Email") });
            gridView.Columns.Add(new GridViewColumn { Header = "Cim", DisplayMemberBinding = new Binding("Cim") });

            ListView.View = gridView;


            DashboardPanel.Visibility = Visibility.Collapsed;
            ListView.Visibility = Visibility.Visible;

            string sql = $"select * from ugyfelek";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                ugyfelekList.Add(new Ugyfelek()
                {
                    UgyfelID = msdr.GetInt32(0),
                    Nev = msdr["Nev"].ToString(),
                    Telefonszam = msdr["Telefonszam"].ToString(),
                    Email = msdr["Email"].ToString(),
                    Cim = msdr["Cim"].ToString()
                });

            }
            msdr.Close();
            con.Close();

            ListView.ItemsSource = ugyfelekList;
            ListView.SelectedIndex = -1;
        }
        public void JarmuvekToltes()
        {

            JarmuvekList.Clear();
            ListView.View = null;
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn { Header = "Alvázszám", DisplayMemberBinding = new Binding("Alvazszam") });
            gridView.Columns.Add(new GridViewColumn { Header = "Márka", DisplayMemberBinding = new Binding("Marka") });
            gridView.Columns.Add(new GridViewColumn { Header = "Tipus", DisplayMemberBinding = new Binding("Tipus") });
            gridView.Columns.Add(new GridViewColumn { Header = "Évjárat", DisplayMemberBinding = new Binding("Evjarat") });
            gridView.Columns.Add(new GridViewColumn { Header = "Tulajdonos", DisplayMemberBinding = new Binding("Tulajdonos.Nev") });

            ListView.View = gridView;


            DashboardPanel.Visibility = Visibility.Collapsed;
            ListView.Visibility = Visibility.Visible;

            string sql = $"SELECT Alvazszam,Marka,Tipus,Evjarat,ugyfelek.Nev  FROM `jarmuvek` LEFT JOIN ugyfelek ON jarmuvek.UgyfelID = ugyfelek.UgyfelID;";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                JarmuvekList.Add(new Jarmuvek()
                {
                    Alvazszam = msdr["Alvazszam"].ToString(),
                    Marka = msdr["Marka"].ToString(),
                    Tipus = msdr["Tipus"].ToString(),
                    Evjarat = msdr.GetInt32(3),
                    Tulajdonos = new Ugyfelek() { Nev = msdr["Nev"] == DBNull.Value ? "Törölt ügyfel" : msdr["Nev"].ToString() }
                });

            }
            msdr.Close();
            con.Close();

            ListView.ItemsSource = JarmuvekList;
            ListView.SelectedIndex = -1;
        }

        public void IdopontokToltes()
        {
            IdopontList.Clear();
            ListView.View = null;
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn { Header = "Dátum", DisplayMemberBinding = new Binding("Datum") });
            gridView.Columns.Add(new GridViewColumn { Header = "Megjegyzes", DisplayMemberBinding = new Binding("Megjegyzes") });
            gridView.Columns.Add(new GridViewColumn { Header = "Ügyfél neve", DisplayMemberBinding = new Binding("Ugyfel.Nev") });
           

            ListView.View = gridView;


            DashboardPanel.Visibility = Visibility.Collapsed;
            ListView.Visibility = Visibility.Visible;

            string sql = $"SELECT IdoPontID,datum,megjegyzes,idopontfoglalasok.UgyfelID,Nev  FROM `idopontfoglalasok` LEFT JOIN ugyfelek ON idopontfoglalasok.UgyfelID = ugyfelek.UgyfelID;";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                IdopontList.Add(new IdopontFoglalas(msdr.GetDateTime(1))
                {
                    IdoPontID = msdr.GetInt32(0),
                    Megjegyzes = msdr["Megjegyzes"].ToString(),
                    UgyfelId = msdr.GetInt32(3),
                    Ugyfel = new Ugyfelek() { Nev = msdr["Nev"] == DBNull.Value ? "Törölt ügyfel" : msdr["Nev"].ToString() }
                });

            }
            msdr.Close();
            con.Close();

            ListView.ItemsSource = IdopontList;
            ListView.SelectedIndex = -1;
        }

        public void NevekComboToltes()
        {
            NevekCombo.Items.Clear();
            NevekCombo.Items.Add("összes ügyfél");
            NevekCombo.SelectedIndex = 0;
            string sql = $"select * from ugyfelek";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                NevekCombo.Items.Add(msdr["Nev"].ToString());

            }
            msdr.Close();
            con.Close();
        }
        public void KezdoAdatokToltes()
        {
            string sql = $"SELECT COUNT(*) FROM `alkalmazottak` WHERE Rang!='admin'";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                szereloszam.Text = msdr[0].ToString();

            }
            msdr.Close();
            con.Close();

            sql = "SELECT COUNT(*) FROM `javitasok` WHERE elkeszult=0;";
            con.Open();
            msqlc = new MySqlCommand(sql, con);
            msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                munkaszam.Text = msdr[0].ToString() + " munka";
            }
            msdr.Close();
            con.Close();
        }

        private void AlkalmazottakButton_Click(object sender, RoutedEventArgs e)
        { 
            AlkalmazottakToltes();
            NevekCombo.Visibility = Visibility.Collapsed;
            ValasztottTabla = "alkalmazottak";
            TorlesButton.Visibility = Visibility.Visible;
            MododsitoGombok.Visibility = Visibility.Visible;
            UjadatGomb.Visibility = Visibility.Visible;
            ModositasGomb.Visibility = Visibility.Visible;
        }
        

        private void MunkakButton_Click(object sender, RoutedEventArgs e)
        {
            NevekCombo.Visibility = Visibility.Collapsed;
            JavitasokToltes();
            ValasztottTabla = "javitasok";
            TorlesButton.Visibility = Visibility.Collapsed;
            MododsitoGombok.Visibility = Visibility.Visible;
            ModositasGomb.Visibility = Visibility.Collapsed;
            NevekCombo.Visibility= Visibility.Collapsed;
            TorlesButton.Visibility= Visibility.Collapsed;
            
        }

        private void AlkatreszekButton_Click(object sender, RoutedEventArgs e)
        {
            NevekCombo.Visibility = Visibility.Collapsed;
            AlkatreszekToltes();
            ValasztottTabla = "alkatreszek";
            TorlesButton.Visibility = Visibility.Collapsed;
            UjadatGomb.Visibility= Visibility.Visible;
            ModositasGomb.Visibility=Visibility.Visible;
            
        }

        private void UgyfelekButton_Click(object sender, RoutedEventArgs e)
        {
            NevekCombo.Visibility = Visibility.Collapsed;
            UgyfelekToltes();
            ValasztottTabla = "ugyfelek";
            TorlesButton.Visibility = Visibility.Visible;
            MododsitoGombok.Visibility = Visibility.Visible;
            UjadatGomb.Visibility = Visibility.Visible;
            ModositasGomb.Visibility = Visibility.Visible;
        }

        private void JarmuvekButton_Click(object sender, RoutedEventArgs e)
        {
            NevekCombo.Visibility = Visibility.Visible;
            NevekComboToltes();
            JarmuvekToltes();
            ValasztottTabla = "jarmuvek";
            TorlesButton.Visibility = Visibility.Visible;
            MododsitoGombok.Visibility = Visibility.Visible;
            UjadatGomb.Visibility = Visibility.Visible;
            ModositasGomb.Visibility = Visibility.Visible;
        }

        private void IdopontButton_Click(object sender, RoutedEventArgs e)
        {
            NevekCombo.Visibility = Visibility.Collapsed;
            IdopontokToltes();
            ValasztottTabla = "idopontfoglalasok";
            MododsitoGombok.Visibility = Visibility.Visible;
            UjadatGomb.Visibility = Visibility.Visible;
            ModositasGomb.Visibility = Visibility.Visible;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MododsitoGombok.Visibility=Visibility.Collapsed;
            DashboardPanel.Visibility = Visibility.Visible;
            ListView.Visibility = Visibility.Collapsed;
            ListView.ItemsSource = null;
           KezdoAdatokToltes();

        }
        public bool kijelentkezik=false;
        private void KijelentkezesButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan ki akarsz jelentkezni?", "Kijelentkezés", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                kijelentkezik = true;
                Bejelentkezes bejelentkezes = new Bejelentkezes();
                this.Close();
                bejelentkezes.Show();


            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!kijelentkezik)
            {
                MessageBoxResult result = MessageBox.Show("Biztosan bezárod az alkalmazást?", "Bezárás", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No) e.Cancel = true;
                
            }
           
        }
        List<string> MezokGeneralasra = new List<string>();
        private void UjAdatButton_Click(object sender, RoutedEventArgs e)
        {
            
            switch (ValasztottTabla)
            {
                case "alkalmazottak":
                    MezokGeneralasra.Clear();
                    MezokGeneralasra=new List<string> { "Név" , "Telefon", "Email" };
                    break;

                case "alkatreszek":
                    MezokGeneralasra.Clear();
                    MezokGeneralasra = new List<string> { "Név", "Cikkszám", "Ár" };
                    break;

                case "ugyfelek":
                    MezokGeneralasra.Clear();
                    MezokGeneralasra = new List<string> { "Név", "Telefon", "Email", "Cím" };
                    break;

                case "jarmuvek":
                    MezokGeneralasra.Clear();
                    MezokGeneralasra = new List<string> { "Alvazszám", "Márka", "Tipus", "evjarat","Tulajdonos" };
                    break;
                case "idopontfoglalasok":
                    MezokGeneralasra.Clear();
                    MezokGeneralasra = new List<string> { "Dátum", "Megjegyzés", "Ügyfél" };
                    break;
                case "javitasok":
                    MezokGeneralasra.Clear();
                    MezokGeneralasra = new List<string> { "Alvázszám", "Szerelő Neve", "Dátum", "Leírás",   };
                    break;
            }
            UjAdat ujAdat = new UjAdat(MezokGeneralasra, ValasztottTabla);
            this.IsEnabled = false;
            ujAdat.Show();
            ujAdat.Closed += (s, a) => {
                this.IsEnabled = true;
                MododsitoGombok.Visibility = Visibility.Collapsed;
                DashboardPanel.Visibility = Visibility.Visible;
                ListView.Visibility = Visibility.Collapsed;
                KezdoAdatokToltes();
            };

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedIndex >-1)
            {
                string sql = "";
                switch (ValasztottTabla)
                {
                    case "alkalmazottak":
                         sql = $"UPDATE `javitasok` SET `AlkalmazottID`=null WHERE AlkalmazottID={((Alkalmazottak)ListView.SelectedItem).AlkalmazottID} ; DELETE FROM `alkalmazottak` WHERE AlkalmazottID={((Alkalmazottak)ListView.SelectedItem).AlkalmazottID};";
                        break;
                    case "ugyfelek":
                        sql = $"UPDATE `jarmuvek` SET UgyfelID = null WHERE UgyfelID = { ((Ugyfelek)ListView.SelectedItem).UgyfelID }; UPDATE `idopontfoglalasok`SET UgyfelID = null WHERE UgyfelID={((Ugyfelek)ListView.SelectedItem).UgyfelID}; DELETE FROM `ugyfelek` WHERE UgyfelID = {((Ugyfelek)ListView.SelectedItem).UgyfelID}; ";
                        break;
                    case "jarmuvek":
                        sql = $"UPDATE `javitasok` SET JarmuID = null WHERE JarmuID = '{ ((Jarmuvek)ListView.SelectedItem).Alvazszam }'; DELETE FROM `jarmuvek` WHERE Alvazszam = '{ ((Jarmuvek)ListView.SelectedItem).Alvazszam }'; ";
                        break;
                    case "idopontfoglalasok":
                        sql = $"DELETE FROM `idopontfoglalasok` WHERE IdoPontID = '{((IdopontFoglalas)ListView.SelectedItem).IdoPontID}'; ";
                        break;
                }
                
                MySqlConnection con = new MySqlConnection(App.kapcsolat);
                con.Open();
                MySqlCommand msqlc = new MySqlCommand(sql, con);
                msqlc.ExecuteNonQuery();
                con.Close();
                switch (ValasztottTabla)
                {
                    case "alkalmazottak":
                        AlkalmazottakToltes();
                        break;
                    case "ugyfelek":
                        UgyfelekToltes();
                        break;
                    case "jarmuvek":
                        JarmuvekToltes();
                        break;
                    case "idopontfoglalasok":
                        IdopontokToltes();
                        break;
                }
            }
            else MessageBox.Show("Nincs kiválasztott elem!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }

       

        
        public List<Jarmuvek> ValasztottUgyfel= new List<Jarmuvek>();
        private void NevekCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValasztottUgyfel.Clear();
            ListView.ItemsSource=null;
            if(NevekCombo.SelectedIndex == 0)
            {
                JarmuvekToltes();
            }
            else
            {
                string sql = $"SELECT Alvazszam,Marka,Tipus,Evjarat,jarmuvek.UgyfelID,ugyfelek.Nev FROM `jarmuvek` INNER JOIN ugyfelek ON jarmuvek.UgyfelID = ugyfelek.UgyfelID WHERE Nev='{NevekCombo.SelectedItem}';";
                MySqlConnection con = new MySqlConnection(App.kapcsolat);
                con.Open();
                MySqlCommand msqlc = new MySqlCommand(sql, con);
                MySqlDataReader msdr = msqlc.ExecuteReader();
                while (msdr.Read())
                {
                    ValasztottUgyfel.Add(new Jarmuvek()
                    {
                        Alvazszam = msdr["Alvazszam"].ToString(),
                        Marka = msdr["Marka"].ToString(),
                        Tipus = msdr["Tipus"].ToString(),
                        Evjarat = msdr.GetInt32(3),
                        UgyfelID = msdr.GetInt32(4),
                        Tulajdonos = new Ugyfelek() { Nev = msdr["Nev"].ToString() }
                    });

                }
                msdr.Close();
                con.Close();

                ListView.ItemsSource=ValasztottUgyfel;
            }
        }

        public List<string> ValasztottElem=new List<string>();
        private void ModositasButton_Click(object sender, RoutedEventArgs e)
        {
            if(ListView.SelectedIndex > -1)
            {
                switch (ValasztottTabla)
                {
                    case "alkalmazottak":
                        MezokGeneralasra.Clear();
                        MezokGeneralasra = new List<string> { "Név", "Telefon", "Email" };
                        ValasztottElem.Clear();
                        var kivalasztottAlkalmazott = (Alkalmazottak)ListView.SelectedItem;
                        ValasztottElem = new List<string>() 
                        {
                            kivalasztottAlkalmazott.Nev,
                            kivalasztottAlkalmazott.Telefonszam,
                            kivalasztottAlkalmazott.Email
                        };
                        break;

                    case "alkatreszek":
                        MezokGeneralasra.Clear();
                        MezokGeneralasra = new List<string> { "Név", "Cikkszám", "Ár" };
                        ValasztottElem.Clear();
                        var kivalasztottAlkatresz = (Alkatreszek)ListView.SelectedItem;
                        ValasztottElem = new List<string>()
                        {
                            kivalasztottAlkatresz.AlkatreszNev,
                            kivalasztottAlkatresz.Cikkszam,
                            kivalasztottAlkatresz.Ar.ToString()
                        };
                        break;

                    case "ugyfelek":
                        MezokGeneralasra.Clear();
                        MezokGeneralasra = new List<string> { "Név", "Telefon", "Email", "Cím" };
                        ValasztottElem.Clear();
                        var KivalasztottUgyfel=(Ugyfelek)ListView.SelectedItem;
                        ValasztottElem = new List<string>()
                        {
                            KivalasztottUgyfel.Nev,
                            KivalasztottUgyfel.Telefonszam,
                            KivalasztottUgyfel.Email,
                            KivalasztottUgyfel.Cim
                        };
                        break;

                    case "jarmuvek":
                        MezokGeneralasra.Clear();
                        MezokGeneralasra = new List<string> { "Alvazszám", "Márka", "Tipus", "evjarat", "Tulajdonos" };
                        ValasztottElem.Clear();
                        var KivalasztottJarmu=(Jarmuvek)ListView.SelectedItem;
                        ValasztottElem = new List<string>()
                        {
                            KivalasztottJarmu.Alvazszam,
                            KivalasztottJarmu.Marka,
                            KivalasztottJarmu.Tipus,
                            KivalasztottJarmu.Evjarat.ToString(),
                            KivalasztottJarmu.Tulajdonos.Nev
                        };
                        break;
                    case "idopontfoglalasok":
                        MezokGeneralasra.Clear();
                        MezokGeneralasra = new List<string> { "Dátum", "Megjegyzés", "Ugyfel" };
                        var KivalasztottJIdopont = (IdopontFoglalas)ListView.SelectedItem;
                        ValasztottElem = new List<string>()
                        {
                            KivalasztottJIdopont.Datum.ToString(),
                            KivalasztottJIdopont.Megjegyzes,
                            KivalasztottJIdopont.Ugyfel.Nev,
                            KivalasztottJIdopont.IdoPontID.ToString(),
                        };
                        break;
                }

                ModositasWindow modositas = new ModositasWindow(MezokGeneralasra, ValasztottTabla, ValasztottElem);
                this.IsEnabled = false;
                modositas.Show();
                modositas.Closed += (s, a) => {
                    this.IsEnabled = true;
                    MododsitoGombok.Visibility = Visibility.Collapsed;
                    DashboardPanel.Visibility = Visibility.Visible;
                    ListView.Visibility = Visibility.Collapsed;
                    KezdoAdatokToltes();
                };
            }
            else MessageBox.Show("Nincs kiválasztott elem!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

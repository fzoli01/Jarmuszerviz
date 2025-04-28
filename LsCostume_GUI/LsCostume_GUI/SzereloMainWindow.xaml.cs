using LsCostume_GUI.Models;
using LsCostume_GUI.Moduls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for SzereloMainWindow.xaml
    /// </summary>
    public partial class SzereloMainWindow : Window
    {
        public string SzereloNeve;
        public SzereloMainWindow(string nev)
        {
            InitializeComponent();
            SzereloNeve = nev;
            Sznev.Text = SzereloNeve;
            JavitasokToltes();
            AlkatreszekComboToltes();
        }
        public List<Javitas> JavitasList = new List<Javitas>();
        public List<FelhasznaltAlkatresz> FelhasznaltAlkatreszList = new List<FelhasznaltAlkatresz>();
        public Alkalmazottak Szerelo;
        public bool kijelentkezik = false;
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
        public void JavitasokToltes()
        {
            ListView.ItemsSource = null;
            JavitasList.Clear();
            string sql = $"SELECT JavitasID, Datum, Leiras, jarmuvek.Alvazszam, Koltseg, elkeszult FROM javitasok LEFT JOIN Alkalmazottak  ON javitasok.AlkalmazottID = alkalmazottak.AlkalmazottID LEFT JOIN Jarmuvek  ON javitasok.JarmuID = jarmuvek.Alvazszam WHERE Javitasok.AlkalmazottID=(SELECT alkalmazottak.AlkalmazottID FROM alkalmazottak WHERE alkalmazottak.Nev='{SzereloNeve}');;";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                JavitasList.Add(new Javitas(msdr.GetBoolean(5), msdr.GetDateTime(1))
                {
                    JavitasID = msdr.GetInt32(0),
                    Leiras = msdr["Leiras"].ToString(),
                    Koltseg=msdr.GetDouble(4),
                    JarmuID = msdr["Alvazszam"] == DBNull.Value ? "Törölt jármű" : msdr["Alvazszam"].ToString()

                });



            }
            msdr.Close();
            con.Close();
            AlkatreszPanel.Visibility = Visibility.Collapsed;
            ProfilModositasPanel.Visibility = Visibility.Collapsed;
            ListView.ItemsSource = JavitasList;
            ListView.SelectedIndex = -1;
        }
        public void FelhasznaltAlkatreszToltes()
        {
            FelhasznaltAlkatreszList.Clear();
            ListViewAlkatreszek.ItemsSource = null;
            if (ListView.SelectedIndex > -1)
            {
                string sql = $"SELECT felhasznaltalkatreszek.AlkatreszID, alkatreszek.AlkatreszNev, Mennyiseg  FROM felhasznaltalkatreszek INNER JOIN alkatreszek ON felhasznaltalkatreszek.AlkatreszID = alkatreszek.AlkatreszID WHERE JavitasID={((Javitas)ListView.SelectedItem).JavitasID};";
                MySqlConnection con = new MySqlConnection(App.kapcsolat);
                con.Open();
                MySqlCommand msqlc = new MySqlCommand(sql, con);
                MySqlDataReader msdr = msqlc.ExecuteReader();
                while (msdr.Read())
                {
                    FelhasznaltAlkatreszList.Add(new FelhasznaltAlkatresz()
                    {
                        AlkatreszID = msdr.GetInt32(0),
                        Alkatresz = new Alkatreszek() { AlkatreszNev = msdr[1].ToString() },
                        Mennyiseg = msdr.GetInt32(2)


                    });



                }
                msdr.Close();
                con.Close();
                ListViewAlkatreszek.ItemsSource = FelhasznaltAlkatreszList;
                ListViewAlkatreszek.SelectedIndex = -1;
            }
        }
        public void AlkatreszekComboToltes()
        {
            string sql = "SELECT * FROM `alkatreszek`;";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                AlkatreszNeveBox.Items.Add(msdr["AlkatreszNev"].ToString());                
            }
            msdr.Close();
            con.Close();
        }
        private void kijelentkezsButton_Click(object sender, RoutedEventArgs e)
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
        private void FrissitesButton_Click(object sender, RoutedEventArgs e)
        {
            JavitasokToltes();
        }
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedIndex > -1)
            {
                if (((Javitas)ListView.SelectedItem).Elkeszult != "Kész")
                {
                    DolgozttOrakWindow dolgozttOrakWindow=new DolgozttOrakWindow(((Javitas)ListView.SelectedItem).JavitasID);
                    dolgozttOrakWindow.Show();
                    this.IsEnabled = false;
                    dolgozttOrakWindow.Closed += (s, a) => {
                        this.IsEnabled = true;
                        JavitasokToltes();
                    };
                    
                }
                else MessageBox.Show("A Javitást már befejezted!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else MessageBox.Show("Nincs kiválasztott elem!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }

      

        private void AlkatreszNeveBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AlkatreszNeveBox.SelectedIndex > -1 && DarabszamBox.Text != "") MentesButton.IsEnabled = true;
            else MentesButton.IsEnabled = false;
        }

        private void DarabszamBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AlkatreszNeveBox.SelectedIndex > -1 && DarabszamBox.Text != "") MentesButton.IsEnabled = true;
            else MentesButton.IsEnabled = false;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AlkatreszPanel.Visibility = Visibility.Visible;
            ProfilModositasPanel.Visibility = Visibility.Collapsed;
            FelhasznaltAlkatreszToltes();
            
        }

        

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NevBox.Text != "" && TelefonBox.Text != "" && EmailBox.Text != "") ProflModositasaButton.IsEnabled = true;
            else ProflModositasaButton.IsEnabled = false;
        }

        private void ProfilModositasButton_Click(object sender, RoutedEventArgs e)
        {
            ProfilModositasPanel.Visibility = Visibility.Visible;
            string sql = $"SELECT * FROM `alkalmazottak` WHERE Nev='{SzereloNeve}';";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();
            while (msdr.Read())
            {
                Szerelo = new Alkalmazottak()
                {
                    AlkalmazottID = msdr.GetInt32(0),
                    Nev = msdr["Nev"].ToString(),
                    Telefonszam = msdr["Telefonszam"].ToString(),
                    Email = msdr["Email"].ToString()
                };
            }
            msdr.Close();
            con.Close();
            NevBox.Text = Szerelo.Nev;
            TelefonBox.Text=Szerelo.Telefonszam;
            EmailBox.Text=Szerelo.Email;
        }

        private void ProflModositasaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = $"UPDATE `alkalmazottak` SET `Nev`='{NevBox.Text}',`Telefonszam`='{TelefonBox.Text}',`Email`='{EmailBox.Text}' WHERE Nev='{SzereloNeve}';";
                MySqlConnection con = new MySqlConnection(App.kapcsolat);
                con.Open();
                MySqlCommand msqlc = new MySqlCommand(sql, con);
                msqlc.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("A változtatás sikeres volt");
                ProfilModositasPanel.Visibility = Visibility.Collapsed;
            }
            catch { MessageBox.Show("A változtatás nem sikerült", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error); ProfilModositasPanel.Visibility = Visibility.Collapsed; }
            
        }

        private void MentesButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = $"INSERT INTO `felhasznaltalkatreszek`(`JavitasID`, `AlkatreszID`, `Mennyiseg`) VALUES ('{((Javitas)ListView.SelectedItem).JavitasID}',(SELECT AlkatreszID FROM alkatreszek WHERE AlkatreszNev='{AlkatreszNeveBox.SelectedItem}'),'{DarabszamBox.Text}'); UPDATE javitasok INNER JOIN felhasznaltalkatreszek ON javitasok.JavitasID = felhasznaltalkatreszek.JavitasID INNER JOIN alkatreszek ON felhasznaltalkatreszek.AlkatreszID = alkatreszek.AlkatreszID SET javitasok.Koltseg = javitasok.Koltseg + (alkatreszek.Ar * felhasznaltalkatreszek.Mennyiseg) WHERE felhasznaltalkatreszek.JavitasID = {((Javitas)ListView.SelectedItem).JavitasID} AND felhasznaltalkatreszek.AlkatreszID = (SELECT alkatreszek.AlkatreszID FROM alkatreszek WHERE alkatreszek.AlkatreszNev='{AlkatreszNeveBox.SelectedItem}');";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            msqlc.ExecuteNonQuery();
            con.Close();

            AlkatreszNeveBox.SelectedIndex=-1;
            DarabszamBox.Text = "";
            FelhasznaltAlkatreszToltes();
            JavitasokToltes();
        }
    }
}

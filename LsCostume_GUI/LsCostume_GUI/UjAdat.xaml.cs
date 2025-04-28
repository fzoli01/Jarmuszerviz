using LsCostume_GUI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LsCostume_GUI
{
    /// <summary>
    /// Interaction logic for UjAdat.xaml
    /// </summary>
    public partial class UjAdat : Window
    {
        public string ValasztottTabla;
        public UjAdat(List<string> mezok, string valasztottTabla)
        {
            InitializeComponent();
            ValasztottTabla = valasztottTabla;
            MezokGeneralasa(mezok);
           
        }
        
        public void MezokGeneralasa(List<string> mezok)
        {
            int db = 0;
            foreach (string item in mezok)
            {
                Alap.Children.Add(new TextBlock
                {
                    Text = item,
                    Foreground = Brushes.White,

                });
                TextBox tbox= new TextBox();
                tbox.Name = "tbox" + db;
                Alap.Children.Add(tbox);
                this.RegisterName(tbox.Name, tbox);
                

                db++;

            }
        }
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private void MentesButton_Click(object sender, RoutedEventArgs e)
        {

            string sql = "";
            try
            {
                switch (ValasztottTabla)
                {
                    //case "alkalmazottak":
                    //    byte[] passwordHash, passwordSalt;
                    //    CreatePasswordHash("a", out byte[] hash, out byte[] salt);
                    //    passwordHash = hash;
                    //    passwordSalt = salt;

                    //    sql = $"INSERT INTO `alkalmazottak`(`Jelszo`, `Jelszo_hash`, `Rang`, `Nev`, `Telefonszam`, `Email`) VALUES ('{passwordSalt}','{passwordHash}','szerelő','{((TextBox)this.FindName("tbox0"))?.Text}','{((TextBox)this.FindName("tbox1"))?.Text}','{((TextBox)this.FindName("tbox2"))?.Text}')";
                    //    break;
                    case "alkalmazottak":
                        byte[] passwordHash, passwordSalt;
                        CreatePasswordHash("Jelszo123", out passwordHash, out passwordSalt);

                        sql = @"INSERT INTO alkalmazottak 
                            (Jelszo, Jelszo_hash, Rang, Nev, Telefonszam, Email) 
                            VALUES (@Jelszo, @JelszoHash, @Rang, @Nev, @Telefonszam, @Email)";
                        MySqlConnection con = new MySqlConnection(App.kapcsolat);
                        con.Open();

                        using (var command = new MySqlCommand(sql, con))
                        {
                            command.Parameters.Add("@Jelszo", MySqlDbType.Blob).Value = passwordSalt;
                            command.Parameters.Add("@JelszoHash", MySqlDbType.Blob).Value = passwordHash;
                            command.Parameters.Add("@Rang", MySqlDbType.VarChar).Value = "szerelő";
                            command.Parameters.Add("@Nev", MySqlDbType.VarChar).Value = ((TextBox)this.FindName("tbox0"))?.Text;
                            command.Parameters.Add("@Telefonszam", MySqlDbType.VarChar).Value = ((TextBox)this.FindName("tbox1"))?.Text;
                            command.Parameters.Add("@Email", MySqlDbType.VarChar).Value = ((TextBox)this.FindName("tbox2"))?.Text;
                            command.ExecuteNonQuery();
                        }
                        con.Close();
                        MessageBox.Show("Adat Felvétel sikerült!");
                        this.Close();
                        break;

                    case "alkatreszek":
                        sql = $"INSERT INTO `alkatreszek`( `AlkatreszNev`, `Cikkszam`, `Ar`) VALUES ('{((TextBox)this.FindName("tbox0"))?.Text}','{((TextBox)this.FindName("tbox1"))?.Text}','{double.Parse(((TextBox)this.FindName("tbox2"))?.Text)}')";
                        break;

                    case "ugyfelek":
                        sql = $"INSERT INTO `ugyfelek`(`Nev`, `Telefonszam`, `Email`, `Cim`) VALUES ('{((TextBox)this.FindName("tbox0"))?.Text}','{((TextBox)this.FindName("tbox1"))?.Text}','{((TextBox)this.FindName("tbox2"))?.Text}','{((TextBox)this.FindName("tbox3"))?.Text}')";
                        break;

                    case "jarmuvek":
                        sql = $"INSERT INTO Jarmuvek (Alvazszam, Marka, Tipus, Evjarat, UgyfelID) VALUES ('{((TextBox)this.FindName("tbox0"))?.Text}', '{((TextBox)this.FindName("tbox1"))?.Text}', '{((TextBox)this.FindName("tbox2"))?.Text}', '{int.Parse(((TextBox)this.FindName("tbox3"))?.Text)}', (SELECT UgyfelID FROM Ugyfelek WHERE Nev = '{((TextBox)this.FindName("tbox4"))?.Text}'));";
                        break;
                    case "idopontfoglalasok":
                        string JoDatumForma = DateTime.Parse(((TextBox)this.FindName("tbox0"))?.Text).ToString("yyyy-MM-dd HH:mm:ss");
                        sql = $"INSERT INTO idopontfoglalasok (datum, megjegyzes, UgyfelID) VALUES ('{JoDatumForma}', '{((TextBox)this.FindName("tbox1"))?.Text}', (SELECT UgyfelID FROM Ugyfelek WHERE Nev = '{((TextBox)this.FindName("tbox2"))?.Text}'));";
                        break;
                    case "javitasok":
                        JoDatumForma = DateTime.Parse(((TextBox)this.FindName("tbox2"))?.Text).ToString("yyyy-MM-dd HH:mm:ss");
                        sql = $"INSERT INTO `javitasok`(`JarmuID`, `AlkalmazottID`, `Datum`, `Leiras`, `elkeszult`) VALUES ('{((TextBox)this.FindName("tbox0"))?.Text}', (SELECT AlkalmazottID FROM alkalmazottak WHERE Nev = '{((TextBox)this.FindName("tbox1"))?.Text}'), '{JoDatumForma}','{((TextBox)this.FindName("tbox3"))?.Text}', '0');";
                        break;
                }
            }
            catch
            {

            }
            try
            {
                MySqlConnection con = new MySqlConnection(App.kapcsolat);
                con.Open();
                MySqlCommand msqlc = new MySqlCommand(sql, con);
                msqlc.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Adat Felvétel sikerült!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Hibas adatok", "HIBA", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}

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
    /// Interaction logic for Bejelentkezes.xaml
    /// </summary>
    public partial class Bejelentkezes : Window
    {
        public List<Alkalmazottak> alkalmazottakList = new List<Alkalmazottak>();
        public Bejelentkezes()
        {
            InitializeComponent();
            LoginButton.Foreground = new SolidColorBrush(Colors.Black);
        }

        

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash); 
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            alkalmazottakList.Clear();
            string sql = $"SELECT * FROM alkalmazottak";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            MySqlDataReader msdr = msqlc.ExecuteReader();

            while (msdr.Read())
            {
                alkalmazottakList.Add(new Alkalmazottak()
                {
                    AlkalmazottID = msdr.GetInt32(0),
                    Jelszo = (byte[])msdr["Jelszo"],
                    JelszoHash = (byte[])msdr["Jelszo_hash"],
                    Rang = msdr["Rang"].ToString(),
                    Nev = msdr["Nev"].ToString(),
                    Telefonszam = msdr["Telefonszam"].ToString(),
                    Email = msdr["Email"].ToString()
                });
            }

            msdr.Close();
            con.Close();

            var user = alkalmazottakList.FirstOrDefault(a => a.Email == Emailbox.Text);

            if (user == null)
            {
                MessageBox.Show("Hibás E-mail!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                pwdbox.Password = "";
                Emailbox.Text = "";
                
            }

            try
            {
                if (VerifyPasswordHash(pwdbox.Password, user.JelszoHash, user.Jelszo))
                {
                    if (user.Rang == "Admin")
                    {
                        AdminMainWindow adminMainWindow = new AdminMainWindow();
                        adminMainWindow.Show();
                        Close();
                    }
                    else
                    {
                        SzereloMainWindow szereloMainWindow = new SzereloMainWindow(user.Nev);
                        szereloMainWindow.Show();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Hibás jelszó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    pwdbox.Password = "";
                    Emailbox.Text = "";
                }
            }
            catch { }
            
        }

        private void ButtonEnable(object sender, TextChangedEventArgs e)
        {
            if (Emailbox.Text != "" && pwdbox.Password != "") { LoginButton.IsEnabled = true; LoginButton.Foreground = new SolidColorBrush(Colors.White); }
            else { LoginButton.IsEnabled = false; LoginButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000")); }
        }

        private void Pwdbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Emailbox.Text != "" && pwdbox.Password != "") { LoginButton.IsEnabled = true; LoginButton.Foreground = new SolidColorBrush(Colors.White); }
            else { LoginButton.IsEnabled = false; LoginButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF000000")); }
        }
    }
}

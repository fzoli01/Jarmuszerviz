using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for ModositasWindow.xaml
    /// </summary>
    public partial class ModositasWindow : Window
    {
        public string ValasztottTabla;
        public List<string> Adatok=new List<string>();
        public ModositasWindow(List<string> mezok, string tabla, List<string> adatok)
        {
            InitializeComponent();
            ValasztottTabla = tabla;
            MezokGeneralasa(mezok, adatok);
            foreach(string item in adatok)
            {
                Adatok.Add(item);
            }
        }
        public void MezokGeneralasa(List<string> mezok, List<string> adatok)
        {
            int db = 0;
            foreach (string item in mezok)
            {
                Alap.Children.Add(new TextBlock
                {
                    Text = item,
                    Foreground = Brushes.White,

                });
                TextBox tbox = new TextBox();
                tbox.Name = "tbox" + db;
                tbox.Text = adatok[db];
                Alap.Children.Add(tbox);
                this.RegisterName(tbox.Name, tbox);
                db++;

            }
        }

        private void MentesButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "";
            try
            {
                switch (ValasztottTabla)
                {
                    case "alkalmazottak":
                        sql = $"UPDATE `alkalmazottak` SET `Nev`='{((TextBox)this.FindName("tbox0"))?.Text}',`Telefonszam`='{((TextBox)this.FindName("tbox1"))?.Text}',`Email`='{((TextBox)this.FindName("tbox2"))?.Text}' WHERE Nev='{Adatok[0]}'";
                        break;

                    case "alkatreszek":
                        sql = $"UPDATE `alkatreszek` SET `AlkatreszNev`='{((TextBox)this.FindName("tbox0"))?.Text}',`Cikkszam`='{((TextBox)this.FindName("tbox1"))?.Text}',`Ar`='{double.Parse(((TextBox)this.FindName("tbox2"))?.Text)}' WHERE AlkatreszNev='{Adatok[0]}'";
                        break;

                    case "ugyfelek":
                        sql = $"UPDATE `ugyfelek` SET `Nev`='{((TextBox)this.FindName("tbox0"))?.Text}',`Telefonszam`='{((TextBox)this.FindName("tbox1"))?.Text}',`Email`='{((TextBox)this.FindName("tbox2"))?.Text}', `Cim`='{((TextBox)this.FindName("tbox3"))?.Text}' WHERE Nev='{Adatok[0]}'";
                        break;

                    case "jarmuvek":
                        sql = $"UPDATE `jarmuvek` SET `Alvazszam`='{((TextBox)this.FindName("tbox0"))?.Text}',`Marka`='{((TextBox)this.FindName("tbox1"))?.Text}',`Tipus`='{((TextBox)this.FindName("tbox2"))?.Text}',`Evjarat`='{int.Parse(((TextBox)this.FindName("tbox3"))?.Text)}',`UgyfelID`=(SELECT UgyfelID FROM ugyfelek WHERE Nev = '{((TextBox)this.FindName("tbox4"))?.Text}') WHERE Alvazszam='{Adatok[0]}';";
                        break;
                    case "idopontfoglalasok":
                        string JoDatumForma = DateTime.Parse(((TextBox)this.FindName("tbox0"))?.Text).ToString("yyyy-MM-dd HH:mm:ss");
                        sql = $"UPDATE `idopontfoglalasok` SET `datum`='{JoDatumForma}',`megjegyzes`='{((TextBox)this.FindName("tbox1"))?.Text}',`UgyfelID`=(SELECT UgyfelID FROM ugyfelek WHERE Nev= '{((TextBox)this.FindName("tbox2"))?.Text}') WHERE IdoPontID={Adatok[3]};";
                        break;

                }
            }
            catch { }

            try
            {
                MySqlConnection con = new MySqlConnection(App.kapcsolat);
                con.Open();
                MySqlCommand msqlc = new MySqlCommand(sql, con);
                msqlc.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Adat modosítása sikerült!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Hibas adatok", "HIBA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

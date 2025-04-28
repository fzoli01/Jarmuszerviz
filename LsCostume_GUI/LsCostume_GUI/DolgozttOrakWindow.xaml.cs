using LsCostume_GUI.Models;
using MySql.Data.MySqlClient;
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
    /// Interaction logic for DolgozttOrakWindow.xaml
    /// </summary>
    public partial class DolgozttOrakWindow : Window
    {
        public int JavitasID;
        public DolgozttOrakWindow(int Id)
        {
            InitializeComponent();
            JavitasID = Id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sql = $"UPDATE `javitasok` SET `Koltseg`= Koltseg + ({DolgozottOrakSzama.Text}*3500), `elkeszult`=true WHERE JavitasID={JavitasID};";
            MySqlConnection con = new MySqlConnection(App.kapcsolat);
            con.Open();
            MySqlCommand msqlc = new MySqlCommand(sql, con);
            msqlc.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Elkészültél a Javítással", "Figyelem", MessageBoxButton.OK);
            this.Close();
        }
    }
}

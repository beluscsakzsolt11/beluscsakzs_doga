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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace beluscsakzs_doga
{
    public partial class MainWindow : Window
    {
        MySqlConnection kapcs = new MySqlConnection("server=localhost;database=beluscsakzs;uid=root;password=''");
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lbAdatok.Items.Clear();
            var sql = "SELECT * FROM beluscsakzs.filmek";
            MySqlCommand parancs = new MySqlCommand(sql, kapcs);
            kapcs.Open();
            MySqlDataReader olvaso = parancs.ExecuteReader();
            while (olvaso.Read())
            {
                lbAdatok.Items.Add(olvaso["filmazon"].ToString() + ";" + olvaso["cim"].ToString() + ";" + olvaso["ev"].ToString() + ";" + olvaso["szines"].ToString() + ";" + olvaso["mufaj"].ToString() + ";" + olvaso["hossz"].ToString());
            }
            kapcs.Close();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var sql = $"UPDATE beluscsakzs.filmek SET cim='{tb1.Text}', ev={tb2.Text}, szines = '{tb3.Text}', mufaj = '{tb4.Text}', hossz = {tb5.Text} WHERE filmazon = '{lbFilmAzon.Content}'";
            MySqlCommand parancs = new MySqlCommand(sql, kapcs);
            kapcs.Open();
            try
            {
                parancs.ExecuteNonQuery();
                MessageBox.Show("Sikeres módosítás!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
            }
            finally
            {
                kapcs.Close();
            }

        }

        private void lbAdatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbAdatok.SelectedItem != null)
            {
                var sor = lbAdatok.SelectedItem.ToString().Split(';');
                lbFilmAzon.Content = sor[0]; // Filmazon
                tb1.Text = sor[1];
                tb2.Text = sor[2];
                tb3.Text = sor[3];
                tb4.Text = sor[4];
                tb5.Text = sor[5];


            }
        }
    }
}


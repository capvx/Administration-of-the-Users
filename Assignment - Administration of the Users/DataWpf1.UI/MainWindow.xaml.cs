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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace wpfloginscreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con;
        DataTable dt;

        public MainWindow()
        {
            InitializeComponent();

            //Connect your access database
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            BindGrid();
        }

        //Display records in grid
        private void BindGrid()
        {
            SqlCommand cmd = new SqlCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from [ADMIN_1]";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            gvData.ItemsSource = dt.AsDataView();

            if (dt.Rows.Count > 0)
            {
                lblCount.Visibility = System.Windows.Visibility.Hidden;
                gvData.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                lblCount.Visibility = System.Windows.Visibility.Visible;
                gvData.Visibility = System.Windows.Visibility.Hidden;
            }

        }

        //Add records in grid
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;

            if (txtId.Text != "")
            {
                if (txtId.IsEnabled == true)
                {
                    cmd.CommandText = "insert into [ADMIN_1](Id,Username,Password,IsAdmin) Values(" + txtId.Text + ",'" + txtUsername.Text + "','" + txtPassword.Text + "','" + txtIsAdmin.Text + "')";
                    cmd.ExecuteNonQuery();
                    BindGrid();
                    MessageBox.Show("korisnik je uspjesno dodat...");
                    ClearAll();
                }
                else
                {
                    cmd.CommandText = "update [ADMIN_1] set Username='" + txtUsername.Text + "',Password='" + txtPassword.Text + "',IsAdmin='" + txtIsAdmin.Text + "' where Id=" + txtId.Text;
                    cmd.ExecuteNonQuery();
                    BindGrid();
                    MessageBox.Show("korisnicki podaci su azurirani...");
                    ClearAll();
                }
            }
            else
            {
                MessageBox.Show("molimo dodajte Id.......");
            }
        }

        //Clear all records from controls
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            txtId.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtIsAdmin.Text = "";
            btnAdd.Content = "Add";
            txtId.IsEnabled = true;
        }

        //Edit records
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (gvData.SelectedItems.Count > 0)
            {
                DataRowView row = (DataRowView)gvData.SelectedItems[0];
                txtId.Text = row["Id"].ToString();
                txtUsername.Text = row["Username"].ToString();
                txtPassword.Text = row["Password"].ToString();
                txtIsAdmin.Text = row["IsAdmin"].ToString();
                txtId.IsEnabled = false;
                btnAdd.Content = "Update";
            }
            else
            {
                MessageBox.Show("molimo izaberite korisnika iz liste...");
            }
        }

        //Delete records from grid
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gvData.SelectedItems.Count > 0)
            {
                DataRowView row = (DataRowView)gvData.SelectedItems[0];

                SqlCommand cmd = new SqlCommand();
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.Connection = con;
                cmd.CommandText = "delete from [ADMIN_1] where Id=" + row["Id"].ToString();
                cmd.ExecuteNonQuery();
                BindGrid();
                MessageBox.Show("korisnik uspjesno izbrisan...");
                ClearAll();
            }
            else
            {
                MessageBox.Show("molimo izaberite korisnika iz liste...");
            }
        }

        //Exit
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
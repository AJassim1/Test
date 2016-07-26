using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Exampledt
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        string connectionstring;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // build the connection to the database
            // can get the link of the database from APP.config
            connectionstring = ConfigurationManager.ConnectionStrings["Exampledt.Properties.Settings.Database1ConnectionString"].ConnectionString;

            PopulateResipes();
            PopulateAllIngredent();
        }

        private void PopulateResipes() 
        {

            using (connection = new SqlConnection(connectionstring))
            using (SqlDataAdapter adapter =new SqlDataAdapter("SELECT * FROM RESIPE",connection))
            {
                DataTable rispeTable = new DataTable();
                adapter.Fill(rispeTable);

                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Id";
                listBox1.DataSource = rispeTable;
            }
        }

        private void PopulateAllIngredent()
        {

            using (connection = new SqlConnection(connectionstring))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM ingraden", connection))
            {
                DataTable ingradenTable = new DataTable();
                adapter.Fill(ingradenTable);

                listBox3.DisplayMember = "Name";
                listBox3.ValueMember = "Id";
                listBox3.DataSource = ingradenTable;
            }
        }

        private void PopulateIngradent()
        {
            string query = "select a.Name from ingraden a " +
                          "inner join recipeIngreden b on a.Id=b.ingradenId " +
                          "where b.ResipeId=@ResipeId ";
            using (connection = new SqlConnection(connectionstring))
            using (SqlCommand command = new SqlCommand(query,connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@ResipeId",listBox1.SelectedValue);
                DataTable ingraentTable = new DataTable();
                adapter.Fill(ingraentTable);

                listBox2.DisplayMember = "Name";
                listBox2.ValueMember = "Id";
                listBox2.DataSource = ingraentTable;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateIngradent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string query = "insert into Resipe values (@ResipeName , 40 ,'test')";
            using (connection = new SqlConnection(connectionstring))
            using (SqlCommand command = new SqlCommand(query,connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@ResipeName", textBox1.Text);
                command.ExecuteNonQuery();
            }

            PopulateResipes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "update Resipe set Name = @ResipeName where Id= @ResipeId";
            using (connection = new SqlConnection(connectionstring))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@ResipeName", textBox1.Text);
                command.Parameters.AddWithValue("@ResipeId", listBox1.SelectedValue);
                command.ExecuteNonQuery();
            }

            PopulateResipes();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "insert into recipeIngreden values (@ResipeId ,@IngradenId)";
            using (connection = new SqlConnection(connectionstring))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@ResipeId", listBox1.SelectedValue);
                command.Parameters.AddWithValue("@IngradenId", listBox3.SelectedValue);
                command.ExecuteNonQuery();
            }

            PopulateIngradent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
namespace Arab_Bank
{
    public partial class Admin : Form
    {
        SQLiteConnection m_dbConnection;
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            Refresh_Data();
        }

        private void Refresh_Data()
        {
            m_dbConnection = new SQLiteConnection("Data Source=Arab_Bank.sqlite;Version=3;");
            m_dbConnection.Open();
            string sql = "SELECT ID, Name, Balance FROM Account";
            SQLiteDataAdapter da = new SQLiteDataAdapter(sql, m_dbConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Please make sure to click on the first column to select!", "Error doing Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=Arab_Bank.sqlite;Version=3;");
                m_dbConnection.Open();
                string sql = "DELETE FROM Account WHERE ID=" + textBox3.Text;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                textBox1.Text = textBox2.Text = textBox3.Text = "";
                Refresh_Data();
                MessageBox.Show("Deleted Successfully! ^_^", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("Please make sure that ID is exist in the table!", "Error doing Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox4.Text == "")
                MessageBox.Show("Please make sure that all values are filled!", "Error doing Insert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    m_dbConnection = new SQLiteConnection("Data Source=Arab_Bank.sqlite;Version=3;");
                    m_dbConnection.Open();
                    string sql = "INSERT INTO Account(Name, Password, Balance) VALUES('" + textBox1.Text + "', '" + textBox4.Text + "', " + textBox2.Text + ");";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    textBox1.Text = textBox2.Text = textBox3.Text = "";
                    Refresh_Data();
                    MessageBox.Show("Added Successfully! ^_^", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Please make sure that all values are filled!", "Error doing Insert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            string details = "You are currently logging in as Admin\non " + date;
            MessageBox.Show(details);
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader("customers.txt");
                string customer = "";
                while (!reader.EndOfStream)
                {
                    customer = reader.ReadLine();
                    string[] s = customer.Split(',');

                    m_dbConnection = new SQLiteConnection("Data Source=Arab_Bank.sqlite;Version=3;");
                    m_dbConnection.Open();
                    string sql = "INSERT INTO Account(Name, Password, Balance) VALUES('" + s[0] + "', '" + s[1] + "', " + s[2] + ");";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    textBox1.Text = textBox2.Text = textBox3.Text = "";
                    Refresh_Data();
                }
                MessageBox.Show("Imported Successfully! ^_^", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Import operation has failed!", "Error doing Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter writer = new StreamWriter("account.txt");
                writer.AutoFlush = true; //It forces the text file to be re-writable
                m_dbConnection = new SQLiteConnection("Data Source=Arab_Bank.sqlite;Version=3;");
                m_dbConnection.Open();

                string sql = "SELECT * FROM Account WHERE ID=" + textBox5.Text;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                string result = "Name: " + reader["Name"].ToString() + "\n" + "Password: " + reader["Password"].ToString() + "\n" + "Balance: $" + reader["Balance"].ToString();
                writer.WriteLine(result);
                MessageBox.Show("Exported Successfully! ^_^", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Export operation has failed, make sure to enter a valid ID!", "Error doing Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

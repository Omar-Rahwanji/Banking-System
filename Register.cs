using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Arab_Bank
{
    public partial class Register : Form
    {
        SQLiteConnection m_dbConnection;
        public Register()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || int.Parse(textBox3.Text) < 0)  
            {
                MessageBox.Show("Please fill out all the text boxes with proper values!", "Error registering", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Account acc = new Account(textBox1.Text, textBox2.Text, int.Parse(textBox3.Text));
                string sql = "INSERT INTO Account (Name, Password, Balance) VALUES('" + acc.Name + "', '" + acc.Password + "', " + acc.Balance + ");";
                SQLiteCommand comm = new SQLiteCommand(sql, m_dbConnection);
                comm.ExecuteNonQuery();
                this.Close();
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            m_dbConnection = new SQLiteConnection("Data Source=Arab_Bank.sqlite;Version=3;");
            m_dbConnection.Open();
        }
    }
}

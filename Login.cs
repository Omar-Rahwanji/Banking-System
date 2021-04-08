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
    public partial class Login : Form
    {
        public static int idNum;
        SQLiteConnection m_dbConnection;
        Register reg;
        Welcome welc;
        Admin adm;
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_dbConnection = new SQLiteConnection("Data Source=Arab_Bank.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "2021")
            {
                m_dbConnection.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
                adm = new Admin();
                adm.Show();
                m_dbConnection = new SQLiteConnection("Data Source=Arab_Bank.sqlite;Version=3;");
                m_dbConnection.Open();
            }
            else
            {
                Account acc = new Account(textBox1.Text, textBox2.Text);
                m_dbConnection = new SQLiteConnection("Data Source=Arab_Bank.sqlite;Version=3;");
                m_dbConnection.Open();
                string sql = "select * from Account where Name = '" + acc.Name + "' AND Password = '" + acc.Password + "';";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                string result = "";
   
                result += reader["Name"].ToString() + reader["Password"].ToString();

                if (result == "")
                    MessageBox.Show("Wrong username or password", "Error logging in", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    idNum = int.Parse(reader["ID"].ToString());

                    m_dbConnection.Close();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox1.Focus();
                    welc = new Welcome();
                    welc.Show();
                }
            }    
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reg = new Register();
            reg.Show();
            m_dbConnection = new SQLiteConnection("Data Source=Arab_Bank.sqlite;Version=3;");
            m_dbConnection.Open();

        }
    }
}

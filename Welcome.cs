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
    public partial class Welcome : Form
    {
        SQLiteConnection m_dbConnection;
        public Welcome()
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=Arab_Bank.sqlite;Version=3;");
                m_dbConnection.Open();
                string sql = "SELECT Name, ID, Balance from Account  WHERE ID = " + Login.idNum + ";";

                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                label2.Text = reader["Name"].ToString().ToUpper();
                label5.Text = "#" + reader["ID"].ToString();
                label4.Text = "$" + reader["Balance"].ToString();

            }
            catch
            {
                MessageBox.Show("Connecting to the database has failed!", "Error db connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            string details = "You are currently logging in as " + label2.Text + "\non " + date;
            MessageBox.Show(details);
        }

    
        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT Balance from Account  WHERE ID = " + Login.idNum;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                int balance = int.Parse(reader["Balance"].ToString());
                if (textBox1.Text == "")
                    MessageBox.Show("Cannot do the operation, deposit value should not be empty!", "Error doing deposit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    if (int.Parse(textBox1.Text) <= 0)
                    MessageBox.Show("Cannot do the operation, The deposit value should be greater than zero!", "Error doing Deposit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    int newBalance = balance + int.Parse(textBox1.Text);
                    sql = "UPDATE Account SET Balance=" + newBalance + " WHERE ID=" + Login.idNum + ";";
                    SQLiteCommand comm = new SQLiteCommand(sql, m_dbConnection);
                    comm.ExecuteNonQuery();

                    sql = "SELECT Balance from Account  WHERE ID = " + Login.idNum + ";";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    reader = command.ExecuteReader();
                    label4.Text = "$" + reader["Balance"].ToString();
                }
            }
            catch
            {
                MessageBox.Show("Connecting to the database has failed!", "Error db connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "SELECT Balance from Account  WHERE ID = " + Login.idNum;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            int balance = int.Parse(reader["Balance"].ToString());
            if (textBox2.Text == "")
                MessageBox.Show("Cannot do the operation, withdraw value should not be empty!", "Error doing Withdraw", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                if (int.Parse(textBox2.Text) <= 0)
                    MessageBox.Show("Cannot do the operation, The withdrawal value should be greater than zero!", "Error doing Withdraw", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    if (balance >= int.Parse(textBox2.Text))
                    {
                        int newBalance = balance - int.Parse(textBox2.Text);
                        sql = "UPDATE Account SET Balance=" + newBalance + " WHERE ID=" + Login.idNum + ";";
                        SQLiteCommand comm = new SQLiteCommand(sql, m_dbConnection);
                        comm.ExecuteNonQuery();
                        sql = "SELECT Balance from Account  WHERE ID = " + Login.idNum + ";";
                        command = new SQLiteCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();
                        label4.Text = "$" + reader["Balance"].ToString();
                    }
                    else
                        MessageBox.Show("Cannot do the operation, your Balance is lower than the withdrawal value!", "Error doing Withdraw", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "")
                MessageBox.Show("Cannot do the operation, Amount value and Account ID should not be empty!", "Error doing Transfer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string sql = "SELECT Balance from Account  WHERE ID = " + Login.idNum + ";";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                if (int.Parse(textBox3.Text) > int.Parse(reader["Balance"].ToString()))
                    MessageBox.Show("Cannot do the operation, your Balance is lower than the transfer value!", "Error doing Transfer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    if(int.Parse(textBox3.Text)<=0)
                        MessageBox.Show("Cannot do the operation, The transfer value should be greater than zero!", "Error doing Transfer", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    else
                    {
                        sql = "SELECT ID from Account WHERE ID =" + textBox4.Text;
                        command = new SQLiteCommand(sql, m_dbConnection);
                        reader = command.ExecuteReader();
                        if (textBox4.Text != reader["ID"].ToString() || int.Parse(textBox4.Text) <= 0) 
                            MessageBox.Show("Cannot do the operation, The Account ID does not exist!", "Error doing Transfer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            sql = "SELECT Balance from Account  WHERE ID = " + textBox4.Text + ";";
                            command = new SQLiteCommand(sql, m_dbConnection);
                            reader = command.ExecuteReader();
                            int newBalance1 = int.Parse(reader["Balance"].ToString()) + int.Parse(textBox3.Text);
                            sql = "UPDATE Account SET Balance=" + newBalance1 + " WHERE ID=" + textBox4.Text + ";";
                            SQLiteCommand comm = new SQLiteCommand(sql, m_dbConnection);
                            comm.ExecuteNonQuery();

                            sql = "SELECT Balance from Account  WHERE ID = " + Login.idNum + ";";
                            command = new SQLiteCommand(sql, m_dbConnection);
                            reader = command.ExecuteReader();
                            int newBalance2 = int.Parse(reader["Balance"].ToString()) - int.Parse(textBox3.Text);
                            sql = "UPDATE Account SET Balance=" + newBalance2 + " WHERE ID=" + Login.idNum + ";";
                            comm = new SQLiteCommand(sql, m_dbConnection);
                            comm.ExecuteNonQuery();
                            label4.Text = "$" + newBalance2.ToString();
                        }
                }
            }
        }
    }
}

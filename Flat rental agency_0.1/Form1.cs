using System;
using SD = System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace Flat_rental_agency_0._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private SqlConnection sqlConnection = new SqlConnection(@"Data Source=WIN-DPLVCALQJN2; Initial Catalog= Rental agency for flats; Integrated Security=True");
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        private string idроль;

        public void openConnection()
        {
            if (sqlConnection.State == SD.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void closeConnection()
        {
            if (sqlConnection.State == SD.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Авторизация пользователя
        {
            string Login = textBox1.Text.ToString();
            string Password = textBox2.Text.ToString();

            openConnection();

            if (Proverka(Login, Password) == true)
            {
                if (idроль == "1")
                {
                    Form4 frm = new Form4();
                    frm.Show();   
                }
                else if (idроль == "2")
                {
                    Form5 frm = new Form5();
                    frm.Show();
                    
                }
            }
            else
            {
                MessageBox.Show("Вы ввели неверный логин или пароль");
            }
            
            closeConnection();
        }
        private Boolean Proverka(string login, string pass) // Проверка введенных данных с БД
        {
            DataTable table = new DataTable();
            string Login = login;
            string Password = pass;

            Password = CreateMD5(Password);

            string commandString = $"select idроль, Логин, Пароль from Пользователь where Логин='{Login}' and Пароль='{Password}'";

            SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(table);

            if (table.Rows.Count > 0)
            {

                idроль  = table.Rows[0].ItemArray[0].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string CreateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }
    }
}


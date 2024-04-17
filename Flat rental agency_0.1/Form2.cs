using System;
using SD = System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Flat_rental_agency_0._1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private SqlConnection sqlConnection = new SqlConnection(@"Data Source=WIN-DPLVCALQJN2; Initial Catalog= Rental agency for flats; Integrated Security=True");
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

        public void openConnection()
        {
            if (sqlConnection.State == SD.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void closeConnection()
        {
            if (sqlConnection.State == SD.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string LoginReg = textBox1.Text.ToString();
            string PasswordReg1 = textBox2.Text.ToString();
            string PasswordReg2 = textBox5.Text.ToString();
            string Name = textBox3.Text.ToString();
            string lastName = textBox4.Text.ToString();
            string number = textBox6.Text.ToString();

            if (PasswordReg1 == PasswordReg2)
            {
                    openConnection();

                    if (Proverka(LoginReg, PasswordReg1) == true)
                    {
                        MessageBox.Show("Такой аккаун уже есть", "Ошибка");
                    }
                    else if (Proverka(LoginReg, PasswordReg1) == false)
                    {

                        PasswordReg1 = CreateMD5(PasswordReg1); //преобразуем хэш из массива в строку, состоящую из шестнадцатеричных символов в верхнем регистре

                        string commandString = $"insert into Пользователь(Логин, Пароль) values('{LoginReg}', '{PasswordReg1}')";
                        string commandString2 = $"insert into Пользователь(Имя, Фамилия, Номер телефона) values('{Name}', '{lastName}', '{number}')";
                        SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                        SqlCommand sqlCommand2 = new SqlCommand(commandString2, sqlConnection);

                        if (sqlCommand.ExecuteNonQuery() == 1 || sqlCommand2.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Акаунт был создан", "Усех");

                        }
                        else
                        {
                            MessageBox.Show("Акаунт не создан", "Ошибка");
                        }
                    }
            }
            else
            {
                MessageBox.Show("Вы ввели неодинаковые пароли", "Ошибка");
            }

            closeConnection();

            Form1 frm = new Form1();
            frm.Show();
        }

        private Boolean Proverka(string log, string pass)
        {
            DataTable table = new DataTable();
            string Login = log;
            string Password = pass;

            Password = CreateMD5(Password);

            string commandString = $"select Логин, Пароль from Пользователь where Логин='{Login}' and Пароль='{Password}'";

            SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(table);

            if (table.Rows.Count > 0)
            {
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
        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

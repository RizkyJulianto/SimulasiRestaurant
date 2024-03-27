using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulasiRestaurant
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection conn = Properti.conn;

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } else 
                {

                    SqlCommand cmd = new SqlCommand("SELECT COUNT (*) FROM Employee WHERE email = @emailbelumada", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@emailbelumada", textBox1.Text);

                    int employee = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if(employee == 0) {
                        MessageBox.Show("Employee tidak dapat ditemukan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;

                    }


                    SqlCommand command = new SqlCommand("SELECT * FROM Employee WHERE email = @email AND password = @password", conn);
                    command.CommandType = CommandType.Text;
                    conn.Open();
                    command.Parameters.AddWithValue("email", textBox1.Text);
                    command.Parameters.AddWithValue("password",(Properti.enkripsi(textBox2.Text)));
                    SqlDataReader dr = command.ExecuteReader();
                    if(dr.Read())
                    {
                        string position = dr["position"].ToString();
                        Properti.employeeid = Convert.ToInt32(dr["employeeid"].ToString());
                        MainForm mf = new MainForm(position);
                        mf.Show();
                        this.Hide();
                    } else
                    {
                        MessageBox.Show("Login gagal periksa kembali email dan password anda", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    conn.Close();

                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            } else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (Properti.validasi(this.Controls))
                    {
                        MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {

                        SqlCommand cmd = new SqlCommand("SELECT COUNT (*) FROM Employee WHERE email = @emailbelumada", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@emailbelumada", textBox1.Text);

                        int employee = (int)cmd.ExecuteScalar();
                        conn.Close();
                        if (employee == 0)
                        {
                            MessageBox.Show("Employee tidak dapat ditemukan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;

                        }


                        SqlCommand command = new SqlCommand("SELECT * FROM Employee WHERE email = @email AND password = @password", conn);
                        command.CommandType = CommandType.Text;
                        conn.Open();
                        command.Parameters.AddWithValue("email", textBox1.Text);
                        command.Parameters.AddWithValue("password", (Properti.enkripsi(textBox2.Text)));
                        SqlDataReader dr = command.ExecuteReader();
                        if (dr.Read())
                        {
                            string position = dr["position"].ToString();
                            Properti.employeeid = Convert.ToInt32(dr["employeeid"].ToString());
                            MainForm mf = new MainForm(position);
                            mf.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Login gagal periksa kembali email dan password anda", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        conn.Close();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

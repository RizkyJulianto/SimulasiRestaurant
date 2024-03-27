using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulasiRestaurant
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        SqlConnection conn = Properti.conn;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(Properti.validasi(this.Controls))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } else if(textBox2.Text != textBox1.Text) 
                {
                    MessageBox.Show("Password tidak sama", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } else
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Employee SET password = @password WHERE employeeid = @employeeid", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@employeeid", Properti.employeeid);
                    cmd.Parameters.AddWithValue("@password", Properti.enkripsi(textBox1.Text));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Password berhasil dirubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox1.PasswordChar = '\0';
                textBox2.PasswordChar = '\0';
            } else
            {
                textBox1.PasswordChar = '*';
                textBox2.PasswordChar = '*';
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulasiRestaurant
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            tampildata();
        }

        SqlConnection conn = Properti.conn;

        private void tampildata()
        {
           SqlCommand cmd  = new SqlCommand("SELECT * FROM Employee", conn);
           cmd.CommandType  = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dataReader = cmd.ExecuteReader();
            dt.Load(dataReader);
            conn.Close();
            dataGridView1.DataSource = dt;
            comboBox1.Items.Add("ADMIN");
            comboBox1.Items.Add("KASIR");
            comboBox1.Items.Add("CHEF");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menambahkan data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(konfirmasi == DialogResult.Yes)
            {

                try
                {
                    if (Properti.validasi(this.Controls))
                    {
                        MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (!Properti.Email(textBox2.Text))
                    {
                        MessageBox.Show("Inputan email tidak valid ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (!Properti.Number(textBox4.Text))
                    {
                        MessageBox.Show("Inputan handphone tidak valid ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Employee VALUES(@name,@email,@password,@handphone,@position)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@email", textBox2.Text);
                        cmd.Parameters.AddWithValue("@password", (Properti.enkripsi(textBox3.Text)));
                        cmd.Parameters.AddWithValue("@handphone", textBox4.Text);
                        cmd.Parameters.AddWithValue("@position", comboBox1.SelectedItem);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        tampildata();
                        MessageBox.Show("Data berhasil ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
                try
                {
                    var row = dataGridView1.CurrentRow;
                    int employeeid = Convert.ToInt32(row.Cells["employeeid"].Value.ToString());

                    if (Properti.validasi(this.Controls))
                    {
                        MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (!Properti.Email(textBox2.Text))
                    {
                        MessageBox.Show("Inputan email tidak valid ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (!Properti.Number(textBox4.Text))
                    {
                        MessageBox.Show("Inputan handphone tidak valid ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        var konfirmasi = MessageBox.Show("Apakah anda yakin ingin mengubah data ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (konfirmasi == DialogResult.Yes)
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE Employee SET name = @name, email = @email, password = @password, handphone = @handphone , position = @position WHERE employeeid = @employeeid", conn);
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            cmd.Parameters.AddWithValue("@employeeid", employeeid);
                            cmd.Parameters.AddWithValue("@employeeid", employeeid);
                            cmd.Parameters.AddWithValue("@name", textBox1.Text);
                            cmd.Parameters.AddWithValue("@email", textBox2.Text);
                            cmd.Parameters.AddWithValue("@password", (Properti.enkripsi(textBox3.Text)));
                            cmd.Parameters.AddWithValue("@handphone", textBox4.Text);
                            cmd.Parameters.AddWithValue("@position", comboBox1.SelectedItem);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            tampildata();
                            MessageBox.Show("Data berhasil diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clear();

                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menghapus data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(konfirmasi == DialogResult.Yes)
            {
                var row = dataGridView1.CurrentRow;
                int employeeid = Convert.ToInt32(row.Cells["employeeid"].Value.ToString());
                SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE employeeid = @employeeid", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@employeeid", employeeid);
                cmd.ExecuteNonQuery();
                conn.Close();
                tampildata();
                MessageBox.Show("Data berhasil dihapus", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["password"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["handphone"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["position"].Value.ToString();
        }
    }
}

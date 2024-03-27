using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulasiRestaurant
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            tampildata();
        }

        SqlConnection conn = Properti.conn;

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Menu", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dataReader = cmd.ExecuteReader();
            dt.Load(dataReader);
            conn.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["photo"].Visible = false;
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
        }
        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls,textBox4))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (!Properti.Number(textBox2.Text))
                {
                    MessageBox.Show("Inputan price harus angka ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Menu VALUES(@name,@price,@photo)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@price", textBox2.Text);
                    if(!string.IsNullOrEmpty(pictureBox1.ImageLocation))
                    {
                        string filename = Path.GetFileName(pictureBox1.ImageLocation);
                        cmd.Parameters.AddWithValue("@photo", filename);
                    } else
                    {
                        cmd.Parameters.AddWithValue("@photo", DBNull.Value);
                    }

                    conn.Open();
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properti.validasi(this.Controls, textBox4))
                {
                    MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (!Properti.Number(textBox2.Text))
                {
                    MessageBox.Show("Inputan price harus angka ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    var row = dataGridView1.CurrentRow;
                    int menuid = Convert.ToInt32(row.Cells["menuid"].Value.ToString());
                    SqlCommand cmd = new SqlCommand("UPDATE Menu SET name = @name, price = @price , photo = @photo WHERE menuid = @menuid", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@menuid", menuid);
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@price", textBox2.Text);
                    if (!string.IsNullOrEmpty(pictureBox1.ImageLocation))
                    {
                        string filename = Path.GetFileName(pictureBox1.ImageLocation);
                        cmd.Parameters.AddWithValue("@photo", filename);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@photo", DBNull.Value);
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    tampildata();
                    MessageBox.Show("Data berhasil diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
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
            if (konfirmasi == DialogResult.Yes)
            {
                var row = dataGridView1.CurrentRow;
                int menuid = Convert.ToInt32(row.Cells["menuid"].Value.ToString());
                SqlCommand cmd = new SqlCommand("DELETE FROM Menu WHERE menuid = @menuid", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@menuid", menuid);
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
            textBox2.Text = dataGridView1.CurrentRow.Cells["price"].Value.ToString();
            string filename = dataGridView1.CurrentRow.Cells["photo"].Value.ToString();

            textBox4.Text = filename;

            if(!string.IsNullOrWhiteSpace(filename))
            {
                string imagepath = Path.Combine(@"C:\Users\Saya\Pictures\Saved Pictures", filename);
                pictureBox1.ImageLocation = imagepath;
            } else
            {
                pictureBox1.Image = null;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog()
            {
                Filter = "Image Files(*.png;*.*jpg;*.*jpeg)|*.png;*.*jpg;*.*jpeg",
                Title = "Pilih gambar"
            };

            if(od.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = od.FileName;
            }
        }
    }
}

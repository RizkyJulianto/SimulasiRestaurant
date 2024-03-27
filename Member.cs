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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SimulasiRestaurant
{
    public partial class Member : Form
    {
        public Member()
        {
            InitializeComponent();
            tampildata();
        }

        SqlConnection conn = Properti.conn;

        private void tampildata()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Member", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader dataReader = cmd.ExecuteReader();
            dt.Load(dataReader);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
        }

        private void Member_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Inputan handphone harus angka ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Member VALUES(@name,@email,@handphone,@joindate)", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@email", textBox2.Text);
                    cmd.Parameters.AddWithValue("@handphone", textBox4.Text);
                    cmd.Parameters.AddWithValue("@joindate", DateTime.Now);
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

                    var row = dataGridView1.CurrentRow;
                    int memberid = Convert.ToInt32(row.Cells["memberid"].Value.ToString());
                    SqlCommand cmd = new SqlCommand("UPDATE Member SET name = @name, email = @email, handphone = @handphone , joindate = @joindate WHERE memberid = @memberid", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@memberid", memberid);
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@email", textBox2.Text);
                    cmd.Parameters.AddWithValue("@handphone", textBox4.Text);
                    cmd.Parameters.AddWithValue("@joindate", DateTime.Now);
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
                int memberid = Convert.ToInt32(row.Cells["memberid"].Value.ToString());
                SqlCommand cmd = new SqlCommand("DELETE FROM Member WHERE memberid = @memberid", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@memberid", memberid);
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
            textBox4.Text = dataGridView1.CurrentRow.Cells["handphone"].Value.ToString();
        }
    }
}

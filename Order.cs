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
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
            tampildata();
            dataGridView2.Columns.Add("Menu", "Menu");
            dataGridView2.Columns.Add("Qty", "Qty");
            dataGridView2.Columns.Add("Price", "Price");
            dataGridView2.Columns.Add("Total", "Total");
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
            dataGridView2.Rows.Clear();
            pictureBox1.Image = null;
        }

        private void Order_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["price"].Value.ToString();
            string filename = dataGridView1.CurrentRow.Cells["photo"].Value.ToString();

            if(!string.IsNullOrEmpty(filename) )
            {
                string imagepath = Path.Combine(@"C:\Users\Saya\Pictures\Saved Pictures", filename);
                pictureBox1.ImageLocation = imagepath;
            } else
            {
                pictureBox1.Image = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Properti.validasi(this.Controls))
            {
                MessageBox.Show("Inputan tidak boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return;
            } else
            {
                string name = textBox1.Text;
                int qty = Convert.ToInt32(textBox2.Text);
                int price = Convert.ToInt32(textBox4.Text);
                int total = price * qty;

                dataGridView2.Rows.Add(name,qty,price,total);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand headorder = new SqlCommand("INSERT INTO Headorder VALUES(@employeeid,@memberid,@date,@payment,@bank)", conn);
                headorder.CommandType = CommandType.Text;
                conn.Open();
                headorder.Parameters.AddWithValue("@employeeid", Properti.employeeid);
                headorder.Parameters.AddWithValue("@memberid", "1");
                headorder.Parameters.AddWithValue("@date", DateTime.Now);
                headorder.Parameters.AddWithValue("@payment", "-");
                headorder.Parameters.AddWithValue("@bank", "-");
                headorder.ExecuteNonQuery();
                conn.Close();

                SqlCommand order = new SqlCommand("SELECT MAX(orderid) FROM Headorder", conn);
                order.CommandType = CommandType.Text;
                conn.Open();

                int orderid = (int)order.ExecuteScalar();
                conn.Close();


                SqlCommand menu = new SqlCommand("SELECT menuid FROM Menu WHERE name = @name", conn);
                menu.CommandType = CommandType.Text;
                menu.Parameters.AddWithValue("@name", textBox1.Text);
                conn.Open();

                int meenuid = (int)menu.ExecuteScalar();
                conn.Close();


                SqlCommand cmd = new SqlCommand("INSERT INTO Detailorder VALUES(@orderid,@menuid,@qty,@price,@status)", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@orderid", orderid);
                cmd.Parameters.AddWithValue("@menuid", meenuid);
                cmd.Parameters.AddWithValue("@qty", textBox2.Text);
                cmd.Parameters.AddWithValue("@price", textBox4.Text);
                cmd.Parameters.AddWithValue("@status", "PENDING");
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Data berhasil ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin membatalkan pesanan?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(konfirmasi == DialogResult.Yes)
            {
                dataGridView2.Rows.Clear();
                clear();
            }
      
        }
    }
}

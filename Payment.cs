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
    public partial class Payment : Form
    {

        public int selectorderid
        {  
            get { return Convert.ToInt32(comboBox1.SelectedItem.ToString()); }
        }




        public Payment()
        {
            InitializeComponent();
            SqlCommand cmd = new SqlCommand("SELECT orderid FROM Headorder ", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            comboBox1.Items.Clear();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["orderid"].ToString());
            }

            conn.Close();

            comboBox2.Items.Add("TUNAI");
            comboBox2.Items.Add("DEBITCARD");
        }

        SqlConnection conn = Properti.conn;

        private void Payment_Load(object sender, EventArgs e)
        {
            `   
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int orderid = Convert.ToInt32(comboBox1.SelectedItem.ToString());



            SqlCommand cmd = new SqlCommand("SELECT Menu.name AS Menu, Detailorder.qty AS Qty, Detailorder.price AS Price " + " FROM Detailorder " + " INNER JOIN Menu ON Detailorder.menuid = Menu.menuid " +  " WHERE orderid = @orderid ", conn);

            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.Parameters.AddWithValue("@orderid", orderid);
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();

            dt.Columns.Add("Total", typeof(int));
            foreach (DataRow row in dt.Rows)
            {
                int qty = Convert.ToInt32(row["qty"].ToString());
                int price = Convert.ToInt32(row["price"].ToString());
                int total = price * qty;

                row["Total"] = total;
            }
            dataGridView1.DataSource = dt;
            MunculTotal();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    flowLayoutPanel1.Controls.Clear();
                    flowLayoutPanel1.Controls.Add(new Tunai(MunculTotal(), this));
                    break;
                case 1:
                    flowLayoutPanel1.Controls.Clear();
                    flowLayoutPanel1.Controls.Add(new Bank(this));
                    break;
                default:
                    flowLayoutPanel1.Controls.Clear();
                    break;
            }
        }

        private decimal MunculTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                total += Convert.ToDecimal(row.Cells["  "].Value);
            }

            label4.Text = "Total : " + total.ToString();
            return total;
        }

    
    }
}

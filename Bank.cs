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
    public partial class Bank : UserControl
    {
        private Payment payment;

        public Bank(Payment payment)
        {
            InitializeComponent();
            this.payment = payment;

            comboBox1.Items.Add("BCA");
            comboBox1.Items.Add("BRI");
            comboBox1.Items.Add("BNI");
        }
        SqlConnection conn = Properti.conn;

        private void Bank_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int orderid = payment.selectorderid;
                SqlCommand cmd = new SqlCommand("UPDATE Headorder SET payment = @payment, bank = @bank WHERE orderid = @orderid", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@orderid", orderid);
                cmd.Parameters.AddWithValue("@payment", "BANK");
                cmd.Parameters.AddWithValue("@bank", comboBox1.SelectedItem);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Data berhasil diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

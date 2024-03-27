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
    public partial class Tunai : UserControl
    {
        private Payment payment;
        private decimal total;

        public Tunai(decimal total,Payment payment)
        {
            InitializeComponent();
            this.payment = payment;
            this.total = total;
            textBox2.TextChanged += TextBox2_TextChanged;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox2.Text, out decimal bayar))
            {

                decimal kembali = bayar - total;
                textBox1.Text = kembali.ToString();
            }
        }

        SqlConnection conn = Properti.conn;

        private void DebitCard_Load(object sender, EventArgs e)
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
                cmd.Parameters.AddWithValue("@payment", "TUNAI");
                cmd.Parameters.AddWithValue("@bank", "-");
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Data berhasil diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}

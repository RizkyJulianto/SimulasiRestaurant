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
    public partial class Vieworder : Form
    {
        public Vieworder()
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


            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.HeaderText = "Status";
            combo.Name = "Status";
            combo.DataPropertyName = "Status";
            combo.DataSource = new string[] { "COOKING", "PENDING", "DELIVERY" };
            dataGridView1.Columns.Add("menu", "menu");
            dataGridView1.Columns.Add("qty", "qty");
            dataGridView1.Columns.Add(combo);
      
        }

        SqlConnection conn = Properti.conn;

        private void Vieworder_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int orderid = Convert.ToInt32(comboBox1.SelectedItem);

            SqlCommand cmd = new SqlCommand("SELECT Detailorder.detailid, Menu.name, Detailorder.qty, Detailorder.status " + " FROM Detailorder " + " INNER JOIN Menu ON Detailorder.menuid = Menu.menuid " + " WHERE orderid = @orderid ", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.Parameters.AddWithValue("@orderid", orderid);
            DataTable dataTable = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dataTable.Load(dr);
            conn.Close();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

           
                DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
                combo.HeaderText = "Status";
                combo.Name = "Status";
                combo.DataPropertyName = "Status";
                combo.DataSource = new string[] { "COOKING", "PENDING", "DELIVERY" };
                dataGridView1.Columns.Add("menu", "menu");
                dataGridView1.Columns.Add("qty", "qty");
                dataGridView1.Columns.Add("detailid", "detailid");
                 dataGridView1.Columns["detailid"].Visible = false;
                dataGridView1.Columns.Add(combo);

                foreach (DataRow row in dataTable.Rows)
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["menu"].Value = row["name"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["qty"].Value = row["qty"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Status"].Value = row["status"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["detailid"].Value = row["detailid"].ToString();
                }

            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if(e.Control is ComboBox comboBox)
            {
                comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                var row = dataGridView1.CurrentRow;
                int detailid = Convert.ToInt32(row.Cells["detailid"].Value.ToString());
                string status = comboBox.SelectedItem.ToString();

                SqlCommand cmd = new SqlCommand("UPDATE Detailorder SET status = @status WHERE detailid = @detailid", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@detailid", detailid);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Data berhasil diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }
    }
}

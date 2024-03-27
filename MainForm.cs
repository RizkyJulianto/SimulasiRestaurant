using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SimulasiRestaurant
{
    public partial class MainForm : Form
    {
        public MainForm(string position)
        {
            InitializeComponent();
            if (position == "ADMIN")
            {
                lOGINToolStripMenuItem.Enabled = false;
            }
            else if (position == "KASIR")
            {
                lOGINToolStripMenuItem.Enabled = false;
                dATAToolStripMenuItem.Enabled = false;
                cHEFToolStripMenuItem.Enabled = false;
                rEPORTToolStripMenuItem.Enabled = false;
            }
            else if (position == "CHEF")
            {
                lOGINToolStripMenuItem.Enabled = false;
                dATAToolStripMenuItem.Enabled = false;
                tRANSACTIONToolStripMenuItem.Enabled = false;
                rEPORTToolStripMenuItem.Enabled = false;
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void lOGOUYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin logout", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Hide();
            }
        }

        private void eXITToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin keluar", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                Application.Exit();

            }
        }

    

        private void kARYAWANToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.ShowDialog();
        }

        private void mEMBERToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Member member = new Member();
            member.ShowDialog();
        }

        private void mENUToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.ShowDialog();
        }

        private void vIEWORDERToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Vieworder vo = new Vieworder();
            vo.ShowDialog();
        }

        private void oRDERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.ShowDialog();
        }

        private void pAYMENTToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Payment payment = new Payment();
            payment.ShowDialog();
        }

        private void rEPORTToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Income income = new Income();
            income.ShowDialog();
        }

        private void cHANGEPASSWORDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword cp = new ChangePassword();
            cp.ShowDialog();
        }
    }
}

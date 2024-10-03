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

namespace MailBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=Predator;Initial Catalog=MailBox;Integrated Security=True;TrustServerCertificate=True");

        void list()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from TblMails",conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        void clean()
        {
            txtName.Clear();
            txtSurname.Clear();
            txtMail.Clear();
            mskPhone.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into TblMails (Name,Surname,Phone,Mail) values(@p1,@p2,@p3,@p4)", conn);
            cmd.Parameters.AddWithValue("@p1", txtName.Text);
            cmd.Parameters.AddWithValue("@p2", txtSurname.Text);
            cmd.Parameters.AddWithValue("@p3", mskPhone.Text);
            cmd.Parameters.AddWithValue("@p4", txtMail.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            list();
            clean();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("update TblMails set Phone=@p1,Mail=@p2 where Id=@p3", conn);
            cmd.Parameters.AddWithValue("@p1", mskPhone.Text);
            cmd.Parameters.AddWithValue("@p2", txtMail.Text);
            cmd.Parameters.AddWithValue("@p3", lblId.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            list();
            clean();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from TblMails where Id=@p1", conn);
            cmd.Parameters.AddWithValue("@p1", lblId.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            list();
            clean();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            mskPhone.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }
    }
}

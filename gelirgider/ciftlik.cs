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

namespace gelirgider
{
    public partial class ciftlik : Form
    {
        public ciftlik()
        {
            InitializeComponent();
        }

        private void ciftlik_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From ciftlikk", Sql.connection);
            DataSet ds = new DataSet();
            Sql.CheckConnection(Sql.connection);
            da.Fill(ds, "Çiftlik");
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void RefreshPage()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From ciftlikk", Sql.connection);
            DataSet ds = new DataSet();
            Sql.CheckConnection(Sql.connection);
            da.Fill(ds, "Çiftlik");
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Sql.CheckConnection(Sql.connection);
            try
            {
                string query = "INSERT INTO ciftlikk (su, gubre, tohum, ilaclama, ekstra, tutar) " +
                    "VALUES (@su, @gubre, @tohum, @ilaclama,  @ekstra, @tutar)";

                using (SqlCommand command = new SqlCommand(query, Sql.connection))
                {
                    command.Parameters.AddWithValue("@su", textBox1.Text);
                    command.Parameters.AddWithValue("@gubre", textBox2.Text);
                    command.Parameters.AddWithValue("@tohum", textBox3.Text);
                    command.Parameters.AddWithValue("@ilaclama", textBox4.Text);
                    command.Parameters.AddWithValue("@ekstra", textBox5.Text);


                    int tutar = Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox4.Text)
                    + Convert.ToInt32(textBox5.Text);
                    command.Parameters.AddWithValue("@tutar", tutar.ToString());
                    command.ExecuteNonQuery();

                    MessageBox.Show("Veri başarıyla kaydedildi.");
                    RefreshPage();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Veri kaydedilemedi !!" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sql.CheckConnection(Sql.connection);

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                int selectedDataID = Convert.ToInt32(selectedRow.Cells["id"].Value);

                string sqlQuery = "DELETE FROM ciftlikk WHERE id = @id";


                try
                {
                    SqlCommand command = new SqlCommand(sqlQuery, Sql.connection);
                    command.Parameters.AddWithValue("@id", selectedDataID);


                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        dataGridView1.Rows.Remove(selectedRow);
                        MessageBox.Show("Veri başarıyla silindi.");
                    }
                    else
                    {
                        MessageBox.Show("Veri silinemedi.");
                    }
                    RefreshPage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri Silinemedi: " + ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Lütfen tablodan bir satır seçiniz !");
            }
        }

        private void ciftlik_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}

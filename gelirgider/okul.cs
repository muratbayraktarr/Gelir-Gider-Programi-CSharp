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
    public partial class okul : Form
    {
        public okul()
        {
            InitializeComponent();
        }

        private void okul_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From okul", Sql.connection);
            DataSet ds = new DataSet();
            Sql.CheckConnection(Sql.connection);
            da.Fill(ds, "Okul");
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void RefreshPage()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From okul", Sql.connection);
            DataSet ds = new DataSet();
            Sql.CheckConnection(Sql.connection);
            da.Fill(ds, "Okul");
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Sql.CheckConnection(Sql.connection);
            try
            {
                string query = "INSERT INTO okul (elektrik, su, dogalgaz, telefon, malzemeler, personelodeme, ekstra, tutar) " +
                    "VALUES (@elektrik, @su, @dogalgaz, @telefon, @malzemeler, @personelodeme, @ekstra, @tutar)";

                using (SqlCommand command = new SqlCommand(query, Sql.connection))
                {
                    command.Parameters.AddWithValue("@elektrik", textBox1.Text);
                    command.Parameters.AddWithValue("@su", textBox2.Text);
                    command.Parameters.AddWithValue("@dogalgaz", textBox3.Text);
                    command.Parameters.AddWithValue("@telefon", textBox4.Text);
                    command.Parameters.AddWithValue("@malzemeler", textBox5.Text);
                    command.Parameters.AddWithValue("@personelodeme", textBox6.Text);
                    command.Parameters.AddWithValue("@ekstra", textBox7.Text);


                    int tutar = Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox4.Text)
                        + Convert.ToInt32(textBox5.Text) + Convert.ToInt32(textBox6.Text) + Convert.ToInt32(textBox7.Text);
                

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

                string sqlQuery = "DELETE FROM ev WHERE id = @id";


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

        private void okul_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}

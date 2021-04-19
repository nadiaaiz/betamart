using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDproduct
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TextSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(textSearch.Text))
                {
                    this.dataproductTableAdapter.Fill(this.appData.dataproduct);
                    dataproductBindingSource.DataSource = this.appData.dataproduct;
                    //dataGridView.DataSource = datasiswahakimBindingSource;
                }
                else
                {
                    var query = from o in this.appData.dataproduct
                                where o.FullName.Contains(textSearch.Text) || o.KodeBarang == textSearch.Text || o.Merk == textSearch.Text || o.Keterangan.Contains(textSearch.Text)
                                select o;
                    dataproductBindingSource.DataSource = query.ToList();
                    //dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    dataproductBindingSource.RemoveCurrent();
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox.Image = Image.FromFile(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                textName.Focus();
                this.appData.dataproduct.AdddataproductRow(this.appData.dataproduct.NewdataproductRow());
                dataproductBindingSource.MoveFirst();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataproductBindingSource.ResetBindings(false);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                textName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataproductBindingSource.ResetBindings(false);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            dataproductBindingSource.ResetBindings(false);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dataproductBindingSource.EndEdit();
                dataproductTableAdapter.Update(this.appData.dataproduct);
                panel.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataproductBindingSource.ResetBindings(false);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.dataproduct' table. You can move, or remove it, as needed.
            this.dataproductTableAdapter.Fill(this.appData.dataproduct);
            dataproductBindingSource.DataSource = this.appData.dataproduct;

        }
    }
}

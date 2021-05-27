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

namespace QuizMahasiswa
{
    public partial class MasterBarang015 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9RPMM7S;Initial Catalog=QuizMahasiswa;Integrated Security=True;");
        public MasterBarang015()
        {
            InitializeComponent();
        }
        DataClasses1DataContext db = new DataClasses1DataContext();
        private void btnSave_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            string item = txtItem.Text;
            int harga = int.Parse(txtHarga.Text);
            int stok = int.Parse(txtStok.Text);
            string supplier = txtSupplier.Text;
            var data = new tbl_barang
            {
                id_barang = id,
                nama_barang = item,
                harga = harga,
                stok = stok,
                nama_supplier = supplier
            };
            db.tbl_barangs.InsertOnSubmit(data);
            db.SubmitChanges();
            MessageBox.Show("Save Successfully");
            txtHarga.Clear();
            txtItem.Clear();
            txtStok.Clear();
            txtSupplier.Clear();
            LoadData();
        }

        void LoadData()
        {
            var st = from tb in db.tbl_barangs select tb;
            dt.DataSource = st;
        }

        private void MasterBarang015_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select isnull(max (cast (id_barang as int)), 0) +1 from tbl_barang", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            txtID.Text = dt.Rows[0][0].ToString();
            LoadData();
        }
    }
}

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
using System.Data.Common;

namespace AdoNet_V3
{
    public partial class FrmDC : Form
    {
        SqlConnection ctn = new SqlConnection(@"Data Source=.;Initial Catalog=Projet_GS;Integrated Security=True");

        SqlDataAdapter da;
        DataSet ds;
        BindingSource Tbiding = new BindingSource();

        int pos = -1;
        int i;

        public FrmDC()
        {
            InitializeComponent();
        }

        private void FrmDC_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from tblGS", ctn);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Auteurs");

            actualiser();

            //Tbiding.DataSource = ds.Tables["Auteurs"];
            //txtID.DataBindings.Add(new Binding("Text", Tbiding, "ID"));
            //txtNom.DataBindings.Add(new Binding("Text", Tbiding, "Nom"));
            //txtAuteur.DataBindings.Add(new Binding("Text", Tbiding, "Auteur"));
            //txtnbrpage.DataBindings.Add(new Binding("Text", Tbiding, "nbrPage"));
            //txtprix.DataBindings.Add(new Binding("Text", Tbiding, "Prix"));
        }
        private void actualiser()  //Hakka Fonction wla Code Ghadi yb9a Yt3aoud Kandirha f blassa waheda  
        {
            this.dgv.Rows.Clear();
            dgv.DataSource = ds.Tables["Auteurs"];
        }
        private void chemp()
        {
            txtID.Text = "";
            txtNom.Text = "";
            txtAuteur.Text = "";
            txtnbrpage.Text = "";
            txtprix.Text = "";
        }
       
        private void test()
        {

            SqlCommand cmd = new SqlCommand("select * from tblGS where ID='" + txtID.Text + "'", ctn);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Auteurs");
         


        }

        private void btnAjoute_Click(object sender, EventArgs e)
        {
            ds.Tables["Auteurs"].Rows.Add(txtID.Text, txtNom.Text, txtAuteur.Text, txtnbrpage.Text,txtprix.Text);
            MessageBox.Show("Ajouter bien effected");
            chemp();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            //enregistrer sur bd :
            SqlCommandBuilder bld = new SqlCommandBuilder(da);
            bld.GetUpdateCommand();
            da.Update(ds.Tables["Auteurs"]);
            chemp();
        }

        private void btnModifie_Click(object sender, EventArgs e)
        {
            for (i = 0; i < ds.Tables["Auteurs"].Rows.Count; i++)
            {
                if (txtID.Text == ds.Tables["Auteurs"].Rows[i][0].ToString())
                {
                    pos = i;
                    break;
                }

            }
            ds.Tables["Auteurs"].Rows[pos][1] = txtNom.Text;
            ds.Tables["Auteurs"].Rows[pos][2] = txtAuteur.Text;
            ds.Tables["Auteurs"].Rows[pos][3] = txtnbrpage.Text;
            ds.Tables["Auteurs"].Rows[pos][4] = txtprix.Text;
            MessageBox.Show("modefication effected");
            chemp();
        }

        private void btnsupprimer_Click(object sender, EventArgs e)
        {
            for (i = 0; i < ds.Tables["Auteurs"].Rows.Count; i++)
            {
                if (txtID.Text == ds.Tables["Auteurs"].Rows[i][0].ToString())
                {
                    pos = i;
                    break;
                }

            }
            ds.Tables["Auteurs"].Rows[pos].Delete();
            MessageBox.Show("Bien supprimer");
            chemp();
        }

        private void btnRecherche_Click(object sender, EventArgs e)
        {
            //hadi Jedida Ya3ni fach tcllike 3la btn otkounkon khaouia ki ytla3 lik icon ki ygol lik 3ameriha 
            errorProvider1.Clear();
            if(txtID.Text == "")
            {
                errorProvider1.SetError(txtID, "Entre ID");
                return;
            }
            //Hadi Methoude Hya fash Kankon dayerin code na9ess wla 3arf ytla3 Probleme donc Bash Yb9a Programe Khedam blama you9e3 Prob kakhdemdo b fonction Try catch
            try 
            {
          
                for (i = 0; i < ds.Tables["Auteurs"].Rows.Count; i++)
                {
                    if (txtID.Text == ds.Tables["Auteurs"].Rows[i][0].ToString())
                    {
                        pos = i;
                        break;
                    }
                }
                txtID.Text = ds.Tables["Auteurs"].Rows[pos][0].ToString();
                txtNom.Text = ds.Tables["Auteurs"].Rows[pos][1].ToString();
                txtAuteur.Text = ds.Tables["Auteurs"].Rows[pos][2].ToString();
                txtnbrpage.Text = ds.Tables["Auteurs"].Rows[pos][3].ToString();
                txtprix.Text = ds.Tables["Auteurs"].Rows[pos][4].ToString();
            }catch
            {
                MessageBox.Show("ID n'exciste Pas! ");
            }



        }

        private void dgv_Click(object sender, EventArgs e)
        {
            //Hada Code Dyal fash Ila clik 3la wahed Men li 3andi f dgv Ytla3 lia F mektoubin f textBox Bash Momkin Modifié
            if (dgv.SelectedRows.Count > 0)
            {
                txtID.Text = dgv.SelectedRows[0].Cells[0].Value.ToString();
                txtNom.Text = dgv.SelectedRows[0].Cells[1].Value.ToString();
                txtAuteur.Text = dgv.SelectedRows[0].Cells[2].Value.ToString();
                txtnbrpage.Text = dgv.SelectedRows[0].Cells[3].Value.ToString();
                txtprix.Text = dgv.SelectedRows[0].Cells[4].Value.ToString();
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}

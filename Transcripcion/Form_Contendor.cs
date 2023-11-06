using Hash;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Hash
{
    public partial class Form_Contenedor : Form
    {
        public Form_Contenedor()
        {
            InitializeComponent();


        }


        Form_Hash form_Hash;
        Form_Custodia form_custodia;
        private void Form_Hash_Click(object sender, EventArgs e)
        {
            form_Hash = new Form_Hash();
            //form_Hash.ShowDialog(); 
            form_Hash.MdiParent =  this;
            form_Hash.Show();
        }

        private void Form_Custodia_Click(object sender, EventArgs e)
        {
            form_custodia = new Form_Custodia();
            //form_custodia.ShowDialog();
            form_custodia.MdiParent = this;
            form_custodia.Show();
        }





    }
}

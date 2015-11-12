using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieOrganiser.Model;

namespace MovieOrganiser.Helpers
{
    public partial class SelectMovie : Form
    {
        public SelectMovie(List<ListItemObject> items)
        {
            InitializeComponent();

            foreach (ListItemObject lio in items)
            {
                listBox1.Items.Add(lio);
            }
        }

        public ListItemObject GetSelectedItem()
        {
            return (ListItemObject)listBox1.SelectedItem;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een item", "Fout", MessageBoxButtons.OK);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

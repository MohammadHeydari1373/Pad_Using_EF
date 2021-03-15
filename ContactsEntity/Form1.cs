using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            using (Contact_DBEntities contacts = new Contact_DBEntities())
            {
                dgContacts.AutoGenerateColumns = false;
                dgContacts.DataSource = contacts.MyContacts.ToList();
                dgContacts.Columns[0].Visible = false;
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmEdit frm = new FrmEdit();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                BindGrid();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                string Name = dgContacts.CurrentRow.Cells[1].Value.ToString();
                string Family = dgContacts.CurrentRow.Cells[2].Value.ToString();
                string FullName = Name + Family;
                string question = "آیا مطمین به حذف " + FullName + " هستید؟";

                if (MessageBox.Show(question, "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (Contact_DBEntities contacts = new Contact_DBEntities())
                    {
                        int contactId = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                        MyContacts rem = contacts.MyContacts.Single(p => p.ContactId == contactId);
                        contacts.MyContacts.Remove(rem);
                        contacts.SaveChanges();
                        BindGrid();

                    }

                }
            }

            else
            {
                MessageBox.Show("لطفا سطری را انتخاب کنید", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                int contactId = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                FrmEdit frmEdit = new FrmEdit();
                frmEdit.contactId = contactId;
                frmEdit.ShowDialog();
                if (frmEdit.DialogResult == DialogResult.OK)
                {
                    BindGrid();
                }
            }
            else
            {
                MessageBox.Show("لطفا سطری را انتخاب کنید", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            using (Contact_DBEntities contacts = new Contact_DBEntities())
            {
                string param = txtSearch.Text;
                MyContacts m = new MyContacts();
                dgContacts.DataSource = contacts.MyContacts
                    .Where(p => p.Name.Contains(param) ||
                        p.Family.Contains(param) ||
                        p.Age.ToString().Contains(param) ||
                        p.Address.Contains(param))
                        .ToList();


            }

        }


    }
}

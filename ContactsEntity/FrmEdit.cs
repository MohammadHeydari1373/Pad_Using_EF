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
    public partial class FrmEdit : Form
    {
        Contact_DBEntities contact = new Contact_DBEntities();

        public int contactId = 0;
        public FrmEdit()
        {
            InitializeComponent();
        }

        private void btnAddSubmit_Click(object sender, EventArgs e)
        {
            if (ValidationAdd())
            {
                if (contactId == 0)
                {
                    MyContacts c1 = new MyContacts()
                    {
                        Name = txtName.Text,
                        Family = txtFamily.Text,
                        Mobile = txtNumber.Text,
                        Age = (int)txtAge.Value,
                        Address = txtAddress.Text,
                    };
                    contact.MyContacts.Add(c1);
                    contact.SaveChanges();

                    MessageBox.Show("عملیات با موفقیت انجام شد");
                    DialogResult = DialogResult.OK;


                }
                else
                {
                    MyContacts c1 = contact.MyContacts.Find(contactId);

                    c1.Name = txtName.Text;
                    c1.Family = txtFamily.Text;
                    c1.Mobile = txtNumber.Text;
                    c1.Age = (int)txtAge.Value;
                    c1.Address = txtAddress.Text;

                    c1.ContactId = contactId;
                   // contact.Entry(c1).State = System.Data.Entity.EntityState.Modified;
                    contact.SaveChanges();

                    MessageBox.Show("عملیات با موفقیت انجام شد");
                    DialogResult = DialogResult.OK;

                }
            }

        }
        bool ValidationAdd()
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("نام را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            if (txtFamily.Text == "")
            {
                MessageBox.Show("نام خانوادگی را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            if (txtAge.Value == 0)
            {
                MessageBox.Show("سن را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }


            if (txtNumber.Text == "")
            {
                MessageBox.Show("شماره تماس را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            return true;
        }

        private void FrmEdit_Load(object sender, EventArgs e)
        {
            if (contactId == 0)
            {
                this.Text = "افزودن شخص جدید";

            }
            else
            {
                this.Text = "ویرایش";
                MyContacts dt = contact.MyContacts.Find(contactId);
                txtName.Text = dt.Name;
                txtFamily.Text = dt.Family;
                txtNumber.Text = dt.Mobile;
                txtAge.Text = dt.Age.ToString();
                txtAddress.Text = dt.Address;
                btnAddSubmit.Text = "ویرایش";


            }


        }
    }
}

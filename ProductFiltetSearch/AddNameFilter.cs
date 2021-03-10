using BlogForm.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProductFiltetSearch
{
    public partial class AddNameFilter : Form
    {
        EFContext context = new EFContext();

        public AddNameFilter()
        {
            InitializeComponent();
        }

        private void AddNameFilter_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
        }

        private void cbAddTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbAddTo.SelectedIndex == 0)
            {
                FilterName filterName = new FilterName();
                filterName.Name = tbAdd.Text;

                context.FilterNames.Add(filterName);
                context.SaveChanges();
            }
            else if (cbAddTo.SelectedIndex == 1)
            {
                FilterValue filterValue = new FilterValue();
                filterValue.Name = tbAdd.Text;

                context.FilterValues.Add(filterValue);
                context.SaveChanges();
            }
        }

        private void tbAdd_TextChanged(object sender, EventArgs e)
        {
            if(tbAdd.Text != "")
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
        }

    }
}
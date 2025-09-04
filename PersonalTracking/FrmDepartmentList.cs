using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using BLL;
using DAL;

namespace PersonalTracking
{
    public partial class FrmDepartmentList : Form
    {
        public FrmDepartmentList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmDepartment frmDepartment = new FrmDepartment();
            this.Hide();
            frmDepartment.ShowDialog();
            this.Visible = true;
            departmentList = DepartmentBLL.GetDepartments();
            dgvDepartmentList.DataSource = departmentList;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmDepartment frmDepartment = new FrmDepartment();
            this.Hide();
            frmDepartment.ShowDialog();
            this.Visible = true;
        }

        List<DEPARTMENT> departmentList = new List<DEPARTMENT>();

        private void FrmDepartmentList_Load(object sender, EventArgs e)
        {
            departmentList = DepartmentBLL.GetDepartments();
            dgvDepartmentList.DataSource = departmentList;
            dgvDepartmentList.Columns[0].Visible = false;
            dgvDepartmentList.Columns[1].HeaderText = "Department Name";
        }
    }
}

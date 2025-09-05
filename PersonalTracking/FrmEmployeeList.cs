using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DAL.DTO;

namespace PersonalTracking
{
    public partial class FrmEmployeeList : Form
    {
        public FrmEmployeeList()
        {
            InitializeComponent();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmEmployee frmEmployee = new FrmEmployee();
            this.Hide();
            frmEmployee.ShowDialog();
            this.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmEmployee frmEmployee = new FrmEmployee();
            this.Hide();
            frmEmployee.ShowDialog();
            this.Visible = true;
        }

        EmployeeDTO employeeDTO = new EmployeeDTO();
        bool comboFull = false;

        private void FrmEmployeeList_Load(object sender, EventArgs e)
        {
            employeeDTO = EmployeeBLL.GetAll();
            dgvEmployeeList.DataSource = employeeDTO.Employees;
            dgvEmployeeList.Columns[0].Visible = false;
            dgvEmployeeList.Columns[1].HeaderText = "User No";
            dgvEmployeeList.Columns[2].HeaderText = "Name";
            dgvEmployeeList.Columns[3].HeaderText = "Surname";
            dgvEmployeeList.Columns[4].HeaderText = "Department";
            dgvEmployeeList.Columns[5].HeaderText = "Position";
            dgvEmployeeList.Columns[6].Visible = false;
            dgvEmployeeList.Columns[7].Visible = false;
            dgvEmployeeList.Columns[8].HeaderText = "Salary";
            dgvEmployeeList.Columns[9].Visible = false;
            dgvEmployeeList.Columns[10].Visible = false;
            dgvEmployeeList.Columns[11].Visible = false;
            dgvEmployeeList.Columns[12].Visible = false;
            dgvEmployeeList.Columns[13].Visible = false;
            comboFull = false;
            cmbDepartment.DataSource = employeeDTO.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = employeeDTO.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                cmbPosition.DataSource = employeeDTO.Positions.Where(x => x.DepartmentID ==
                Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<EmployeeDetailDTO> list = employeeDTO.Employees;
            if (txtUserNo.Text.Trim() != "")
            {
                list = list.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            }
            if (txtName.Text.Trim() != "")
            {
                list = list.Where(x => x.Name.Contains(txtName.Text)).ToList();
            }
            if (txtSurname.Text.Trim() != "")
            {
                list = list.Where(x => x.Surname.Contains(txtSurname.Text)).ToList();
            }
            if (cmbDepartment.SelectedIndex != -1)
            {
                list = list.Where(x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
            if (cmbPosition.SelectedIndex != -1)
            {
                list = list.Where(x => x.PositionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            }

            dgvEmployeeList.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            comboFull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = employeeDTO.Positions;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
            dgvEmployeeList.DataSource = employeeDTO.Employees;
        }
    }
}

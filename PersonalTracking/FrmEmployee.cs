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
using System.IO;

namespace PersonalTracking
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        EmployeeDTO employeeDTO = new EmployeeDTO();

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            employeeDTO = EmployeeBLL.GetAll();
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

        bool comboFull = false;

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = employeeDTO.Positions.Where(x => x.DepartmentID == departmentID).ToList();
            }
          
        }

        string fileName = "";

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                pbProfilePicture.Load(openFileDialog1.FileName);
                txtImagePath.Text = openFileDialog1.FileName;
                string unique = Guid.NewGuid().ToString();
                fileName += unique + openFileDialog1.SafeFileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtUserNo.Text.Trim() == "")
            {
                MessageBox.Show("User Number is empty.");
            } 
            else if (!EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
            {
                MessageBox.Show("This user no is already used by another employee. Try again.");
            }
            else if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Password is empty.");
            }
            else if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Name is empty.");
            }
            else if (txtSurname.Text.Trim() == "")
            {
                MessageBox.Show("Surname is empty.");
            }
            else if (txtSalary.Text.Trim() == "")
            {
                MessageBox.Show("Salary is empty.");
            }
            else if (cmbDepartment.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Department.");
            }
            else if (cmbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Position.");
            } else
            {
                EMPLOYEE employee = new EMPLOYEE();
                employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                employee.Password = txtPassword.Text;
                employee.isAdmin = chAdmin.Checked;
                employee.Name = txtName.Text;
                employee.Surname = txtSurname.Text;
                employee.Salary = Convert.ToInt32(txtSalary.Text);
                employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                employee.Address = txtAddress.Text;
                employee.BirthDay = dtpBirthday.Value;
                employee.ImagePath = fileName;

                EmployeeBLL.AddEmployee(employee);
                File.Copy(txtImagePath.Text, @"images\\" + fileName);
                MessageBox.Show($"Employee: {employee.Name} was added.");

                txtUserNo.Clear();
                txtPassword.Clear();
                chAdmin.Checked = false;
                txtName.Clear();
                txtSurname.Clear();
                txtSalary.Clear();
                txtAddress.Clear();
                txtImagePath.Clear();
                pbProfilePicture.Image = null;
                comboFull = false;
                cmbDepartment.SelectedIndex = -1;
                cmbPosition.DataSource = employeeDTO.Positions;
                cmbPosition.SelectedIndex = -1;
                comboFull = true;
                dtpBirthday.Value = DateTime.Today;
            }
        }

        bool isUnique = false;

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
            {
                MessageBox.Show("User Number is empty.");
            }
            else
            {
                isUnique = EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text));
                if(!isUnique)
                {
                    MessageBox.Show("This user no is already used by another employee. Try again.");
                } else
                {
                    MessageBox.Show("This user no is usable.");
                }
            }
        }
    }
}

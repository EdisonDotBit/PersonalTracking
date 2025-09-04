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
using DAL.DTO;

namespace PersonalTracking
{
    public partial class FrmPositionList : Form
    {
        public FrmPositionList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmPosition frmPosition = new FrmPosition();
            this.Hide();
            frmPosition.ShowDialog();
            this.Visible = true;
            FillGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmPosition frmPosition = new FrmPosition();
            this.Hide();
            frmPosition.ShowDialog();
            this.Visible = true;
        }

        void FillGrid()
        {
            positionList = PositionBLL.GetPositions();
            dgvPositionList.DataSource = positionList;
        }

        List<PositionDTO> positionList = new List<PositionDTO>();

        private void FrmPositionList_Load(object sender, EventArgs e)
        {
            FillGrid();
            dgvPositionList.Columns[1].Visible = false;
            dgvPositionList.Columns[3].Visible = false;
            dgvPositionList.Columns[0].HeaderText = "Department Name";
            dgvPositionList.Columns[2].HeaderText = "Position Name";
        }
    }
}

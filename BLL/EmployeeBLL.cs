using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;
using DAL;

namespace BLL
{
    public class EmployeeBLL
    {
        public static void AddEmployee(EMPLOYEE employee)
        {
            EmployeeDAO.AddEmployee(employee); 
        }

        public static EmployeeDTO GetAll()
        {
            EmployeeDTO employeeDTO = new EmployeeDTO();
            employeeDTO.Departments = DepartmentDAO.GetDepartments();
            employeeDTO.Positions = PositionDAO.GetPositions();
            employeeDTO.Employees = EmployeeDAO.GetEmployees();
            return employeeDTO;
        }

        public static bool isUnique(int v)
        {
            List<EMPLOYEE> employeeList = EmployeeDAO.GetUsers(v);
            if(employeeList.Count > 0)
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}

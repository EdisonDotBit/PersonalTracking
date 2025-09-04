using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;

namespace DAL.DAO
{
    public class PositionDAO : EmployeeContext
    {
        public static void AddPosition(POSITION position)
        {
            try
            {
                db.POSITIONs.InsertOnSubmit(position);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PositionDTO> GetPositions()
        {
            try
            {
                var positionList = (from p in db.POSITIONs
                            join d in db.DEPARTMENTs
                            on p.DepartmentID equals d.ID
                            select new PositionDTO
                            {
                                ID = p.ID,
                                PositionName = p.PositionName,
                                DepartmentName = d.DepartmentName,
                                DepartmentID = p.DepartmentID

                            }).OrderBy(x => x.ID).ToList();
                return positionList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

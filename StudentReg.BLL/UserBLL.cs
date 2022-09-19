using StudentReg.Dal.UnitOfWork;
using StudentReg.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using StudentReg

namespace StudentReg.BLL
{
    public class UserBLL
    {

        private UnitOfWork<Dal.EntityDataModel.StudentRegEntities> unitOfWork;

        public UserBLL()
        {
            unitOfWork = new UnitOfWork<Dal.EntityDataModel.StudentRegEntities>();
        }

        public SRegResponse Login(string userName, string password)
        {
            SRegResponse res = new SRegResponse();

           var sss =  StudentReg.Cmn.Crypto.ConvertToMD5(password).ToUpper();

            try
            {
                var user = unitOfWork.tbl_sysuser.Get(filter: x => x.UserName == userName && x.Status == 1).Select(y => new SysUser
                {
                    Id = y.Id,
                    Name = y.Name,
                    UserName = y.UserName,
                    Status = y.Status ?? 0,
                    Password = y.Password
                }).FirstOrDefault();

                if (user != null)
                {
                    if (user.Password.ToUpper() == StudentReg.Cmn.Crypto.ConvertToMD5(password).ToUpper())
                    {
                        UserSession session = new UserSession();

                        session.Id = user.Id;
                        session.Name = user.Name;
                        session.Status = user.Status;
                        session.UserName = user.UserName;

                        res.ResData1 = session;

                        res.ResMsg = "Login successful";
                        res.ResStatus = true;

                    }
                    else
                    {
                        res.ResData1 = null;
                        res.ResMsg = "Invalid password";
                        res.ResStatus = false;
                    }
                }
                else
                {
                    res.ResMsg = "User not found";
                    res.ResStatus = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
    }
}

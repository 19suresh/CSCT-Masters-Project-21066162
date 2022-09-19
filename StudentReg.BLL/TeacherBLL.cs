using StudentReg.Cmn.Enums;
using StudentReg.Dal.EntityDataModel;
using StudentReg.Dal.UnitOfWork;
using StudentReg.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentReg.BLL
{
    public class TeacherBLL
    {
        private UnitOfWork<Dal.EntityDataModel.StudentRegEntities> unitOfWork;

        public TeacherBLL()
        {
            unitOfWork = new UnitOfWork<StudentRegEntities>();
        }

        public void SaveTeacher(Teacher teacher)
        {
            var _teacher = new TBL_TEACHER()
            {
                RegNo = teacher.RegNo,
                FName = teacher.FName,
                LName = teacher.LName,
                DoB = teacher.DoB,
                Title = teacher.Title,
                NIC = teacher.NIC
            };

            unitOfWork.tbl_teacher.Insert(_teacher);
            unitOfWork.Save();
        }

        public void UpdateTeacher(Teacher teacher)
        {
            var inboundObj = unitOfWork.tbl_teacher.Get(filter: x => x.Id == teacher.Id).FirstOrDefault();

            inboundObj.RegNo = teacher.RegNo;
            inboundObj.FName = teacher.FName;
            inboundObj.LName = teacher.LName;
            inboundObj.NIC = teacher.NIC;
            inboundObj.DoB = teacher.DoB;
            inboundObj.Title = teacher.Title;

            unitOfWork.tbl_teacher.Update(inboundObj);
            unitOfWork.Save();
        }

        public List<Teacher> GetTeachers()
        {
            List<Teacher> tList = unitOfWork.tbl_teacher.GetAll().Select(y => new Teacher
            {
                Id = y.Id,
                RegNo = y.RegNo,
                DoB = y.DoB ?? DateTime.MinValue,
                FName = y.FName,
                LName = y.LName,
                NIC = y.NIC,
                Title = y.Title ?? 0,
                StrTitle = y.Title == 1 ? "Mr." : "Ms."
            }).ToList();

            return tList;
        }

        public List<Teacher> SearchTeachers(string searchKey, enumSearchTeacherBy searchBy)
        {
            List<TBL_TEACHER> enList = null;

            switch (searchBy)
            {
                case enumSearchTeacherBy.RegNo:
                    enList = unitOfWork.tbl_teacher.Get(filter: x => x.RegNo.Contains(searchKey)).ToList();
                    break;
                case enumSearchTeacherBy.FName:
                    enList = unitOfWork.tbl_teacher.Get(filter: x => x.FName.Contains(searchKey)).ToList();
                    break;
                case enumSearchTeacherBy.LName:
                    enList = unitOfWork.tbl_teacher.Get(filter: x => x.LName.Contains(searchKey)).ToList();
                    break;
                case enumSearchTeacherBy.NIC:
                    enList = unitOfWork.tbl_teacher.Get(filter: x => x.NIC.Contains(searchKey)).ToList();
                    break;
                default:
                    enList = unitOfWork.tbl_teacher.GetAll().ToList();
                    break;
            }

            List<Teacher> teacherList = new List<Teacher>();
            Teacher s;
            foreach (var item in enList)
            {
                s = new Teacher();

                s.Id = item.Id;
                s.RegNo = item.RegNo;
                s.DoB = item.DoB ?? DateTime.MinValue;
                s.FName = item.FName;
                s.LName = item.LName;
                s.NIC = item.NIC;
                s.Title = item.Title ?? 0;
                s.StrTitle = item.Title == 1 ? "Mr." : "Ms.";
                teacherList.Add(s);
            }
            return teacherList;
        }

        public Teacher GetTeacherById(int id)
        {
            Teacher teacher = unitOfWork.tbl_teacher.Get(filter: x => x.Id == id).Select(y => new Teacher
            {
                Id = y.Id,
                DoB = y.DoB ?? DateTime.MinValue,
                FName = y.FName,
                LName = y.LName,
                NIC = y.NIC,
                RegNo = y.RegNo,
                StrTitle = y.Title == 1 ? "Mr." : "Ms.",
                Title = y.Title ?? 0,
                strDoB = (y.DoB ?? DateTime.MinValue).ToString("MM/dd/yyyy")
            }).FirstOrDefault();

            return teacher;
        }
    }
}

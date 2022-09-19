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
    public class StudentBLL
    {
        private UnitOfWork<Dal.EntityDataModel.StudentRegEntities> unitOfWork;

        public StudentBLL()
        {
            unitOfWork = new UnitOfWork<StudentRegEntities>();
        }

        public void SaveStudent(Student student)
        {
            var _student = new TBL_STUDENT()
            {
                RegNo = student.RegNo,
                FName = student.FName,
                LName = student.LName,
                DoB = student.DoB,
                Title = student.Title,
                NIC = student.NIC
            };

            unitOfWork.tbl_student.Insert(_student);
            unitOfWork.Save();
        }

        public void UpdateStudent(Student student)
        {
            var inboundObj = unitOfWork.tbl_student.Get(filter: x => x.Id == student.Id).FirstOrDefault();

            inboundObj.RegNo = student.RegNo;
            inboundObj.FName = student.FName;
            inboundObj.LName = student.LName;
            inboundObj.NIC = student.NIC;
            inboundObj.DoB = student.DoB;
            inboundObj.Title = student.Title;

            unitOfWork.tbl_student.Update(inboundObj);
            unitOfWork.Save();
        }

        public List<Student> GetStudents()
        {
            List<Student> studentList = unitOfWork.tbl_student.GetAll().Select(y => new Student
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

            return studentList;
        }

        public List<Student> SearchStudents(string searchKey, enumSearchTeacherBy searchBy)
        {
            List<TBL_STUDENT> enList = null;

            switch (searchBy)
            {
                case enumSearchTeacherBy.RegNo:
                    enList = unitOfWork.tbl_student.Get(filter: x => x.RegNo.Contains(searchKey)).ToList();
                    break;
                case enumSearchTeacherBy.FName:
                    enList = unitOfWork.tbl_student.Get(filter: x => x.FName.Contains(searchKey)).ToList();
                    break;
                case enumSearchTeacherBy.LName:
                    enList = unitOfWork.tbl_student.Get(filter: x => x.LName.Contains(searchKey)).ToList();
                    break;
                case enumSearchTeacherBy.NIC:
                    enList = unitOfWork.tbl_student.Get(filter: x => x.NIC.Contains(searchKey)).ToList();
                    break;
                default:
                    enList = unitOfWork.tbl_student.GetAll().ToList();
                    break;
            }

            List<Student> studentList = new List<Student>();
            Student s;
            foreach (var item in enList)
            {
                s = new Student();

                s.Id = item.Id;
                s.RegNo = item.RegNo;
                s.DoB = item.DoB ?? DateTime.MinValue;
                s.FName = item.FName;
                s.LName = item.LName;
                s.NIC = item.NIC;
                s.Title = item.Title ?? 0;
                s.StrTitle = item.Title == 1 ? "Mr." : "Ms.";
                studentList.Add(s);
            }
            return studentList;
        }

        public Student GetStudentById(int id)
        {
            Student student = unitOfWork.tbl_student.Get(filter: x => x.Id == id).Select(y => new Student
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

            return student;
        }

        public List<Student> GetStudentByGrade(int grade)
        {
            List<Student> student = unitOfWork.tbl_student.Get(filter: x => x.Grade == grade).Select(y => new Student
            {
                Id = y.Id,
                DoB = y.DoB ?? DateTime.MinValue,
                FName = y.FName,
                LName = y.LName,
                NIC = y.NIC,
                RegNo = y.RegNo,
                StrTitle = y.Title == 1 ? "Mr." : "Ms.",
                Title = y.Title ?? 0,
                strDoB = (y.DoB ?? DateTime.MinValue).ToString("MM/dd/yyyy"),
                Grade = y.Grade ?? 0
            }).ToList();

            return student;
        }

        public void DeleteResult(int id)
        {
            var inboundObj = unitOfWork.tbl_student.Get(filter: x => x.Id == id).FirstOrDefault();
            unitOfWork.tbl_exam_result.Delete(inboundObj);
            unitOfWork.Save();

        }
    }
}

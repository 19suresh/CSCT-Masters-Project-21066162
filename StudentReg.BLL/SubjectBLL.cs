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
    public class SubjectBLL
    {
        private UnitOfWork<Dal.EntityDataModel.StudentRegEntities> unitOfWork;

        public SubjectBLL()
        {
            unitOfWork = new UnitOfWork<StudentRegEntities>();
        }

        public void SaveSubject(Subject subject)
        {
            var _subject = new TBL_SUBJECT()
            {
                Name = subject.Name,
                Description = subject.Description,
                Year = subject.Year,
                Grade = subject.Grade,
                Term = subject.Term
            };

            unitOfWork.tbl_subject.Insert(_subject);
            unitOfWork.Save();
        }

        public void UpdateSubject(Subject subject)
        {
            var inboundObj = unitOfWork.tbl_subject.Get(filter: x => x.Id == subject.Id).FirstOrDefault();

            inboundObj.Name = subject.Name;
            inboundObj.Description = subject.Description;
            inboundObj.Year = subject.Year;
            inboundObj.Grade = subject.Grade;
            inboundObj.Term = subject.Term;

            unitOfWork.tbl_subject.Update(inboundObj);
            unitOfWork.Save();
        }

        public List<Subject> GetSubjects()
        {
            List<Subject> subList = unitOfWork.tbl_subject.GetAll().Select(y => new Subject
            {
                Id = y.Id,
                Name = y.Name,
                Description = y.Description,
                Year = y.Year ?? 0,
                Grade = y.Grade ?? 0,
                Term = y.Term ?? 0
            }).ToList();

            return subList;
        }

        public List<Subject> GetSubjects(int year, int grade, int term)
        {
            List<Subject> subList = unitOfWork.tbl_subject.Get(filter:x=>x.Year == year && x.Grade == grade && x.Term == term).Select(y => new Subject
            {
                Id = y.Id,
                Name = y.Name,
                Description = y.Description,
                Year = y.Year ?? 0,
                Grade = y.Grade ?? 0,
                Term = y.Term ?? 0
            }).ToList();

            return subList;
        }


        public List<Subject> SearchSubject(string searchKey, enumSearchSubjectBy searchBy)
        {
            List<TBL_SUBJECT> enList = null;

            switch (searchBy)
            {
                case enumSearchSubjectBy.Name:
                    enList = unitOfWork.tbl_subject.Get(filter: x => x.Name.Contains(searchKey)).ToList();
                    break;
                case enumSearchSubjectBy.Description:
                    enList = unitOfWork.tbl_subject.Get(filter: x => x.Description.Contains(searchKey)).ToList();
                    break;
                case enumSearchSubjectBy.Year:
                    enList = unitOfWork.tbl_subject.Get(filter: x => x.Year.Equals(searchKey)).ToList();
                    break;
                case enumSearchSubjectBy.Grade:
                    enList = unitOfWork.tbl_subject.Get(filter: x => x.Grade.Equals(searchKey)).ToList();
                    break;
                case enumSearchSubjectBy.Term:
                    enList = unitOfWork.tbl_subject.Get(filter: x => x.Term.Equals(searchKey)).ToList();
                    break;
                default:
                    enList = unitOfWork.tbl_subject.GetAll().ToList();
                    break;
            }

            List<Subject> studentList = new List<Subject>();
            Subject s;
            foreach (var item in enList)
            {
                s = new Subject();

                s.Id = item.Id;
                s.Name = item.Name;
                s.Description = item.Description;
                s.Year = item.Year ?? 0;
                s.Grade = item.Grade ?? 0;
                s.Term = item.Term ?? 0;
                studentList.Add(s);
            }
            return studentList;
        }

        public Subject GetSubjectById(int id)
        {
            Subject student = unitOfWork.tbl_subject.Get(filter: x => x.Id == id).Select(y => new Subject
            {
                Id = y.Id,
                Name = y.Name,
                Description = y.Description,
                Year = y.Year ?? 0,
                Grade = y.Grade ?? 0,
                Term = y.Term ?? 0,
            }).FirstOrDefault();

            return student;
        }

        public bool DeleteSubjectById(int id)
        {
            var delObj = unitOfWork.tbl_subject.Get(filter: x => x.Id == id).FirstOrDefault();
            unitOfWork.tbl_subject.Delete(delObj);
            unitOfWork.Save();
            return true;
        }
    }
}

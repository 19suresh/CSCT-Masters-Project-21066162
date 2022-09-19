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
    public class AssignmentBLL
    {
        private UnitOfWork<Dal.EntityDataModel.StudentRegEntities> unitOfWork;

        public AssignmentBLL()
        {
            unitOfWork = new UnitOfWork<StudentRegEntities>();
        }

        public void SaveAssignment(Assignment assignment)
        {
            var _assignment = new TBL_ASSIGNMENT()
            {
                Name = assignment.Name,
                Description = assignment.Description,
                Year = assignment.Year,
                Grade = assignment.Grade,
                Term = assignment.Term,
                SubjectId = assignment.SubjectId

            };

            unitOfWork.tbl_assignment.Insert(_assignment);
            unitOfWork.Save();
        }

        public void UpdateAssignment(Assignment assignment)
        {
            var inboundObj = unitOfWork.tbl_assignment.Get(filter: x => x.Id == assignment.Id).FirstOrDefault();

            inboundObj.Name = assignment.Name;
            inboundObj.Description = assignment.Description;
            inboundObj.Year = assignment.Year;
            inboundObj.Grade = assignment.Grade;
            inboundObj.Term = assignment.Term;
            inboundObj.SubjectId = assignment.SubjectId;

            unitOfWork.tbl_assignment.Update(inboundObj);
            unitOfWork.Save();
        }

        public List<Assignment> GetAssignment()
        {
            List<Assignment> exList = unitOfWork.tbl_assignment.GetAll().Select(y => new Assignment
            {
                Id = y.Id,
                Name = y.Name,
                Description = y.Description,
                Subject = (unitOfWork.tbl_subject.Get(filter: a => a.Id == y.TBL_SUBJECT.Id).Select(b => new Subject
                {
                    Id = b.Id,
                    Description = b.Description,
                    Grade = b.Grade ?? 0,
                    Name = b.Name,
                    Term = b.Term ?? 0,
                    Year = b.Year ?? 0

                }).FirstOrDefault()),
                Year = y.Year ?? 0,
                Grade = y.Grade ?? 0,
                Term = y.Term ?? 0
            }).ToList();

            return exList;
        }

        public List<Assignment> SearchAssignment(string searchKey, enumSearchAssignmentBy searchBy)
        {
            List<TBL_ASSIGNMENT> enList = null;

            switch (searchBy)
            {
                case enumSearchAssignmentBy.Name:
                    enList = unitOfWork.tbl_assignment.Get(filter: x => x.Name.Contains(searchKey)).ToList();
                    break;
                case enumSearchAssignmentBy.Description:
                    enList = unitOfWork.tbl_assignment.Get(filter: x => x.Description.Contains(searchKey)).ToList();
                    break;
                case enumSearchAssignmentBy.Year:
                    enList = unitOfWork.tbl_assignment.Get(filter: x => x.Year.Equals(searchKey)).ToList();
                    break;
                case enumSearchAssignmentBy.Grade:
                    enList = unitOfWork.tbl_assignment.Get(filter: x => x.Grade.Equals(searchKey)).ToList();
                    break;
                case enumSearchAssignmentBy.Term:
                    enList = unitOfWork.tbl_assignment.Get(filter: x => x.Term.Equals(searchKey)).ToList();
                    break;
                case enumSearchAssignmentBy.Subject:
                    enList = unitOfWork.tbl_assignment.Get(filter: x => x.SubjectId.Equals(searchKey)).ToList();
                    break;
                default:
                    enList = unitOfWork.tbl_assignment.GetAll().ToList();
                    break;
            }


            List<Assignment> exList = new List<Assignment>();
            Assignment a;
            foreach (var item in enList)
            {
                a = new Assignment();

                a.Id = item.Id;
                a.Name = item.Name;
                a.Description = item.Description;
                a.Year = item.Year ?? 0;
                a.Grade = item.Grade ?? 0;
                a.Term = item.Term ?? 0;
                a.Subject = (unitOfWork.tbl_subject.Get(filter: ab => ab.Id == item.TBL_SUBJECT.Id).Select(b => new Subject
                {
                    Id = b.Id,
                    Description = b.Description,
                    Grade = b.Grade ?? 0,
                    Name = b.Name,
                    Term = b.Term ?? 0,
                    Year = b.Year ?? 0

                }).FirstOrDefault());
                exList.Add(a);
            }
            return exList;
        }

        public List<Assignment> SearchAssignment(int year, int grade, int term)
        {
            List<Assignment> asList = unitOfWork.tbl_assignment.Get(filter: x => x.Year == year && x.Grade == grade && x.Term == term).Select(y => new Assignment
            {
                Id = y.Id,
                Term = y.Term ?? 0,
                Grade = y.Grade ?? 0,
                Year = y.Year ?? 0,
                Description = y.Description,
                Name = y.Name
            }).ToList();

            return asList;

        }

        public Assignment GetAssignmentById(int id)
        {
            Assignment ex = unitOfWork.tbl_assignment.Get(filter: x => x.Id == id).Select(y => new Assignment
            {
                Id = y.Id,
                Name = y.Name,
                Description = y.Description,
                Year = y.Year ?? 0,
                Grade = y.Grade ?? 0,
                Term = y.Term ?? 0,
                Subject = (unitOfWork.tbl_subject.Get(filter: ab => ab.Id == y.SubjectId).Select(b => new Subject
                {
                    Id = b.Id,
                    Description = b.Description,
                    Grade = b.Grade ?? 0,
                    Name = b.Name,
                    Term = b.Term ?? 0,
                    Year = b.Year ?? 0

                }).FirstOrDefault())
            }).FirstOrDefault();

            return ex;
        }

        public AssingmentResult InsertResult(ExamResult result)
        {
            var obj = new TBL_ASSINGMENT_RESULT()
            {
                Id = result.Id,
                EvaluatedBy = result.EvaluatedBy,
                AssingmentId = result.ExamId,
                SubjectId = result.SubjectId,
                Mark = result.Mark,
                Grade = result.Grade,
                Remarks = result.Remarks,
                Year = result.Year,
                Term = result.Term,
                RegNo = result.RegNo
            };

            var identity = unitOfWork.tbl_assingment_result.Insert(obj);
            unitOfWork.Save();
            var ret = GetResultById(identity.Id);
            //unitOfWork.Save();

            return ret;
        }


        public List<AssingmentResult> loadBulkResult(int subjectId, int examId, int grade, int year, int term)
        {
            var obj = unitOfWork.tbl_assingment_result.Get(filter: x => x.SubjectId == subjectId && x.AssingmentId == examId && x.Grade == grade && x.Year == year && x.Term == term).Select(y => new AssingmentResult
            {
                Id = y.Id,
                Term = y.Term ?? 0,
                Year = y.Year ?? 0,
                Grade = y.Grade ?? 0,
                EvaluatedBy = y.EvaluatedBy ?? 0,
                AssingmentId = y.AssingmentId ?? 0,
                Mark = y.Mark ?? 0,
                RegNo = y.RegNo,
                Remarks = y.Remarks,
                SubjectId = y.SubjectId ?? 0,
                FName = unitOfWork.tbl_student.Get(filter: a => a.RegNo == y.RegNo).FirstOrDefault().FName,
                LName = unitOfWork.tbl_student.Get(filter: a => a.RegNo == y.RegNo).FirstOrDefault().LName

            }).ToList();

            return obj;

        }

        public List<AssingmentResult> loadAllResults(int year, string regno)
        {
            var res = unitOfWork.tbl_assingment_result.Get(filter: x => x.Year == year && x.RegNo == regno).Select(y => new AssingmentResult
            {
                Id = y.Id,
                Term = y.Term ?? 0,
                Year = y.Year ?? 0,
                Grade = y.Grade ?? 0,
                EvaluatedBy = y.EvaluatedBy ?? 0,
                AssingmentId = y.AssingmentId ?? 0,
                Mark = y.Mark ?? 0,
                RegNo = y.RegNo,
                Remarks = y.Remarks,
                SubjectId = y.SubjectId ?? 0,
                FName = unitOfWork.tbl_student.Get(filter: a => a.RegNo == y.RegNo).FirstOrDefault().FName,
                LName = unitOfWork.tbl_student.Get(filter: a => a.RegNo == y.RegNo).FirstOrDefault().LName,
                AssingmnetName = unitOfWork.tbl_assignment.Get(filter: x => x.Id == y.AssingmentId).FirstOrDefault().Name
            }).ToList();

            return res;
        }

        public AssingmentResult GetResultById(int id)
        {
            var obj = unitOfWork.tbl_assingment_result.Get(filter: x => x.Id == id).Select(y => new AssingmentResult
            {
                Id = y.Id,
                EvaluatedBy = y.EvaluatedBy ?? 0,
                AssingmentId = y.AssingmentId ?? 0,
                Grade = y.Grade ?? 0,
                Mark = y.Mark ?? 0,
                RegNo = y.RegNo,
                Remarks = y.Remarks,
                SubjectId = y.SubjectId ?? 0,
                Term = y.Term ?? 0,
                Year = y.Year ?? 0

            }).FirstOrDefault();

            var objStudent = unitOfWork.tbl_student.Get(filter: x => x.RegNo == obj.RegNo).FirstOrDefault();

            obj.FName = objStudent.FName;
            obj.LName = objStudent.LName;

            return obj;
        }

        public bool DeleteAssignmentById(int id)
        {
            var delObj = unitOfWork.tbl_assignment.Get(filter: x => x.Id == id).FirstOrDefault();
            unitOfWork.tbl_assignment.Delete(delObj);
            unitOfWork.Save();
            return true;
        }
    }
}

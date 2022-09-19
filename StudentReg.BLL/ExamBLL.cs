using StudentReg.Cmn.Enums;
using StudentReg.Dal.EntityDataModel;
using StudentReg.Dal.UnitOfWork;
using StudentReg.Ent;
using System.Collections.Generic;
using System.Linq;

namespace StudentReg.BLL
{
    public class ExamBLL
    {
        private UnitOfWork<Dal.EntityDataModel.StudentRegEntities> unitOfWork;

        public ExamBLL()
        {
            unitOfWork = new UnitOfWork<StudentRegEntities>();
        }

        public void SaveExam(Exam exam)
        {
            var _exam = new TBL_EXAM()
            {
                Name = exam.Name,
                Description = exam.Description,
                Year = exam.Year,
                Grade = exam.Grade,
                Term = exam.Term
            };

            unitOfWork.tbl_exam.Insert(_exam);
            unitOfWork.Save();
        }

        public void UpdateExam(Exam subject)
        {
            var inboundObj = unitOfWork.tbl_exam.Get(filter: x => x.Id == subject.Id).FirstOrDefault();

            inboundObj.Name = subject.Name;
            inboundObj.Description = subject.Description;
            inboundObj.Year = subject.Year;
            inboundObj.Grade = subject.Grade;
            inboundObj.Term = subject.Term;

            unitOfWork.tbl_exam.Update(inboundObj);
            unitOfWork.Save();
        }

        public List<Exam> GetExams()
        {
            List<Exam> exList = unitOfWork.tbl_exam.GetAll().Select(y => new Exam
            {
                Id = y.Id,
                Name = y.Name,
                Description = y.Description,
                Year = y.Year ?? 0,
                Grade = y.Grade ?? 0,
                Term = y.Term ?? 0
            }).ToList();

            return exList;
        }

        public List<Exam> SearchExam(string searchKey, enumSearchExamBy searchBy)
        {
            List<TBL_EXAM> enList = null;

            switch (searchBy)
            {
                case enumSearchExamBy.Name:
                    enList = unitOfWork.tbl_exam.Get(filter: x => x.Name.Contains(searchKey)).ToList();
                    break;
                case enumSearchExamBy.Description:
                    enList = unitOfWork.tbl_exam.Get(filter: x => x.Description.Contains(searchKey)).ToList();
                    break;
                case enumSearchExamBy.Year:
                    enList = unitOfWork.tbl_exam.Get(filter: x => x.Year.Equals(searchKey)).ToList();
                    break;
                case enumSearchExamBy.Grade:
                    enList = unitOfWork.tbl_exam.Get(filter: x => x.Grade.Equals(searchKey)).ToList();
                    break;
                case enumSearchExamBy.Term:
                    enList = unitOfWork.tbl_exam.Get(filter: x => x.Term.Equals(searchKey)).ToList();
                    break;
                default:
                    enList = unitOfWork.tbl_exam.GetAll().ToList();
                    break;
            }

            List<Exam> exList = new List<Exam>();
            Exam e;
            foreach (var item in enList)
            {
                e = new Exam();

                e.Id = item.Id;
                e.Name = item.Name;
                e.Description = item.Description;
                e.Year = item.Year ?? 0;
                e.Grade = item.Grade ?? 0;
                e.Term = item.Term ?? 0;
                exList.Add(e);
            }
            return exList;
        }

        public Exam GetExamById(int id)
        {
            Exam ex = unitOfWork.tbl_exam.Get(filter: x => x.Id == id).Select(y => new Exam
            {
                Id = y.Id,
                Name = y.Name,
                Description = y.Description,
                Year = y.Year ?? 0,
                Grade = y.Grade ?? 0,
                Term = y.Term ?? 0,
            }).FirstOrDefault();

            return ex;
        }

        public bool DeleteExamById(int id)
        {
            var delObj = unitOfWork.tbl_exam.Get(filter: x => x.Id == id).FirstOrDefault();
            unitOfWork.tbl_exam.Delete(delObj);
            unitOfWork.Save();
            return true;
        }

        public List<Exam> SearchExam(int year, int grade, int term)
        {
            List<Exam> exList = unitOfWork.tbl_exam.Get(filter: x => x.Year == year && x.Grade == grade && x.Term == term).Select(y => new Exam
            {
                Id = y.Id,
                Term = y.Term ?? 0,
                Grade = y.Grade ?? 0,
                Year = y.Year ?? 0,
                Description = y.Description,
                Name = y.Name
            }).ToList();

            return exList;

        }

        public ExamResult InsertResult(ExamResult result)
        {
            var obj = new TBL_EXAM_RESULT()
            {
                Id = result.Id,
                EvaluatedBy = result.EvaluatedBy,
                ExamId = result.ExamId,
                SubjectId = result.SubjectId,
                Mark = result.Mark,
                Grade = result.Grade,
                Remarks = result.Remarks,
                Year = result.Year,
                Term = result.Term,
                RegNo = result.RegNo
            };

            var identity = unitOfWork.tbl_exam_result.Insert(obj);
            unitOfWork.Save();
            var ret = GetResultById(identity.Id);
            //unitOfWork.Save();

            return ret;
        }

        public ExamResult GetResultById(int id)
        {
            var obj = unitOfWork.tbl_exam_result.Get(filter: x => x.Id == id).Select(y => new ExamResult
            {
                Id = y.Id,
                EvaluatedBy = y.EvaluatedBy ?? 0,
                ExamId = y.ExamId ?? 0,
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

        public List<ExamResult> loadBulkResult(int subjectId, int examId, int grade, int year, int term)
        {
            var obj = unitOfWork.tbl_exam_result.Get(filter: x => x.SubjectId == subjectId && x.ExamId == examId && x.Grade == grade && x.Year == year && x.Term == term).Select(y => new ExamResult
            {
                Id = y.Id,
                Term = y.Term ?? 0,
                Year = y.Year ?? 0,
                Grade = y.Grade ?? 0,
                EvaluatedBy = y.EvaluatedBy ?? 0,
                ExamId = y.ExamId ?? 0,
                Mark = y.Mark ?? 0,
                RegNo = y.RegNo,
                Remarks = y.Remarks,
                SubjectId = y.SubjectId ?? 0,
                FName = unitOfWork.tbl_student.Get(filter: a => a.RegNo == y.RegNo).FirstOrDefault().FName,
                LName = unitOfWork.tbl_student.Get(filter: a => a.RegNo == y.RegNo).FirstOrDefault().LName

            }).ToList();

            return obj;

        }

        public List<ExamResult> loadAllExamResult(int year, string regno)
        {
            var obj = unitOfWork.tbl_exam_result.Get(filter: x => x.Year == year && x.RegNo == regno).Select(y => new ExamResult
            {
                Id = y.Id,
                Term = y.Term ?? 0,
                Year = y.Year ?? 0,
                Grade = y.Grade ?? 0,
                EvaluatedBy = y.EvaluatedBy ?? 0,
                ExamId = y.ExamId ?? 0,
                Mark = y.Mark ?? 0,
                RegNo = y.RegNo,
                Remarks = y.Remarks,
                SubjectId = y.SubjectId ?? 0,
                FName = unitOfWork.tbl_student.Get(filter: a => a.RegNo == y.RegNo).FirstOrDefault().FName,
                LName = unitOfWork.tbl_student.Get(filter: a => a.RegNo == y.RegNo).FirstOrDefault().LName,
                ExamName = unitOfWork.tbl_exam.Get(filter: a => a.Id == y.ExamId).FirstOrDefault().Name

            }).ToList();

            return obj;

        }


        public bool DeleteExamResultById(int id)
        {
            var obj = unitOfWork.tbl_exam_result.Get(filter: x => x.Id == id).FirstOrDefault();
            unitOfWork.tbl_exam_result.Delete(obj);
            unitOfWork.Save();
            return true;
        }
    }
}

using StudentReg.Dal.EntityDataModel;
using StudentReg.Dal.IRepository;
using StudentReg.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentReg.Dal.UnitOfWork
{
    public class UnitOfWork<TContext> : IDisposable where TContext : DbContext, new()
    {
        private DbContext context;

        private IGenericRepository<TBL_SYSUSER, DbContext> sys_user;
        public IGenericRepository<TBL_SYSUSER, DbContext> tbl_sysuser
        {
            get
            {
                if (this.sys_user == null)
                {
                    this.sys_user = new GenericRepository<TBL_SYSUSER, DbContext>(context);
                }
                return sys_user;
            }
        }

        private IGenericRepository<TBL_STUDENT, DbContext> student;
        public IGenericRepository<TBL_STUDENT, DbContext> tbl_student
        {
            get
            {
                if (this.student == null)
                {
                    this.student = new GenericRepository<TBL_STUDENT, DbContext>(context);
                }
                return student;
            }
        }

        private IGenericRepository<TBL_TEACHER, DbContext> teacher;
        public IGenericRepository<TBL_TEACHER, DbContext> tbl_teacher
        {
            get
            {
                if (this.teacher == null)
                {
                    this.teacher = new GenericRepository<TBL_TEACHER, DbContext>(context);
                }
                return teacher;
            }
        }

        private IGenericRepository<TBL_SUBJECT, DbContext> subject;
        public IGenericRepository<TBL_SUBJECT, DbContext> tbl_subject
        {
            get
            {
                if (this.subject == null)
                {
                    this.subject = new GenericRepository<TBL_SUBJECT, DbContext>(context);
                }
                return subject;
            }
        }

        private IGenericRepository<TBL_EXAM, DbContext> exam;
        public IGenericRepository<TBL_EXAM, DbContext> tbl_exam
        {
            get
            {
                if (this.exam == null)
                {
                    this.exam = new GenericRepository<TBL_EXAM, DbContext>(context);
                }
                return exam;
            }
        }

        private IGenericRepository<TBL_ASSIGNMENT, DbContext> assignment;
        public IGenericRepository<TBL_ASSIGNMENT, DbContext> tbl_assignment
        {
            get
            {
                if (this.assignment == null)
                {
                    this.assignment = new GenericRepository<TBL_ASSIGNMENT, DbContext>(context);
                }
                return assignment;
            }
        }

        private IGenericRepository<TBL_EXAM_RESULT, DbContext> exam_result;
        public IGenericRepository<TBL_EXAM_RESULT, DbContext> tbl_exam_result
        {
            get
            {
                if (this.exam_result == null)
                {
                    this.exam_result = new GenericRepository<TBL_EXAM_RESULT, DbContext>(context);
                }
                return exam_result;
            }
        }

        private IGenericRepository<TBL_ASSINGMENT_RESULT, DbContext> assingment_result;
        public IGenericRepository<TBL_ASSINGMENT_RESULT, DbContext> tbl_assingment_result
        {
            get
            {
                if (this.assingment_result == null)
                {
                    this.assingment_result = new GenericRepository<TBL_ASSINGMENT_RESULT, DbContext>(context);
                }
                return assingment_result;
            }
        }


        #region commons
        public UnitOfWork()
        {
            context = new TContext();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

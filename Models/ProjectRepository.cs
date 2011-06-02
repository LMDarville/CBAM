using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBAM.Models
{
    public class ProjectRepository : CBAM.Models.IProjectRepository
    {

        private CBAMDataContext db = new CBAMDataContext();
        
        //Query Methods
        public IQueryable<Project> GetAll()
        {
            return db.Projects.Where(p => p.IsActive); 
        }

        public Project GetByID(int ID)
        {
            return db.Projects.SingleOrDefault(s => s.ID == ID);
        }


        //Insert/Delete
        public void Add(Project project)
        {
            db.Projects.InsertOnSubmit(project);
        }
  
        public void Delete(Project project)
        {
            db.Projects.DeleteOnSubmit(project);
        }

        //Persistence
        public void Save()
        {
            db.SubmitChanges();
        }

    }
}


using System;
using System.Linq;
using System.Collections.Generic;

namespace CBAM.Models
{

    public interface IProjectRepository
    {
        IQueryable<Project> GetAll();
        Project GetByID(int id);

        void Add(Project Project);
        void Delete(Project Project);
        void Save();
  }

    
}


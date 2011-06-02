
using System;
using System.Linq;
using System.Collections.Generic;

namespace CBAM.Models
{

    public interface IUtilityRepository
    {

        Utility GetByID(int id);
        IQueryable<QualityAttributeResponseType> GetQualityAttributeResponseTypes();

      
        void Save();
    }

    
}


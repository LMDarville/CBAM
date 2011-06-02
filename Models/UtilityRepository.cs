using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBAM.Models
{
    public class UtilityRepository: CBAM.Models.IUtilityRepository {

        private CBAMDataContext db = new CBAMDataContext();
        
        //Query Methods
        public Utility GetByID(int id){
            return db.Utilities.SingleOrDefault(s => s.ID == id);
        }

        public IQueryable<QualityAttributeResponseType> GetQualityAttributeResponseTypes()
        {
            return db.QualityAttributeResponseTypes;
        }

        


        //Insert/Delete
        //insert in scenario controller
        //no delete

        //Persistence
        public void Save()
        {
            db.SubmitChanges();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

namespace CBAM.Models
{
    public class ArchitecturalStrategyRepository : CBAM.Models.IArchitecturalStrategyRepository 
    {
        private CBAMDataContext db = new CBAMDataContext();

        public IQueryable<ArchitecturalStrategy> GetAll()
        {
            return db.ArchitecturalStrategies;
        }

        public IQueryable<ArchitecturalStrategy> GetAllbyProjID(long projID)
        {
            return db.ArchitecturalStrategies.Where(x => x.IsActive == true && x.ProjectID == projID);
        }

        public IQueryable<Scenario> GetAllScenarios()
        {
            return db.Scenarios;
        }

        public Scenario GetScenarioByID(int id)
        {
            return db.Scenarios.SingleOrDefault(x => x.ID == id);
        }

        public ArchitecturalStrategy GetByID(int id)
        {
            return db.ArchitecturalStrategies.SingleOrDefault(x => x.ID == id);
        }

        public ExpectedUtility GetExpectedUtilityByID(long utilID)
        {
            return db.ExpectedUtilities.SingleOrDefault(x => x.ID == utilID);
        }

        public IQueryable<Scenario> GetAffiliatedScenariosByStratID(long stratID)
        {
            var item =
                (from p in db.Scenarios
                 where p.ExpectedUtilities.Select(x=>x.ArchitecturalStrategyID).Contains(stratID)
                 select p);
            return item;
        }

        //inserts
        //Insert/Delete
     

        public void Delete(ArchitecturalStrategy strategy)
        {
            strategy.IsActive = false;
        }


        public void Add(ArchitecturalStrategy strategy)
        {
            strategy.IsActive = true;
            strategy.IsComplete = false;
            db.ArchitecturalStrategies.InsertOnSubmit(strategy);
            //need to add scenarios to x walk after saving
        }

        public void ClearAllScenariosFromStrategy(long strategyID)
        {
            var strat = GetByID((int)strategyID);
            foreach (var item in strat.ExpectedUtilities)
            {
                db.ExpectedUtilities.DeleteOnSubmit(item);
            }
            //db.SubmitChanges(ConflictMode.ContinueOnConflict);
        }

        //Defines whether individual strategy is completely defined
        public void UpdateIsComplete(ArchitecturalStrategy strategy)
        {   //COMPLETE DEFINITION S/B SAME AS STEPS COMPLETE DEF FOR ARCHITECTURAL STRATEGY
            //complete defined as has: 1) name, 2) description, 
            //3) at least one affected scenario
            //4) expected utility and exepected utility descrtiption for each affected scenario
            if (strategy.Description != null &&             //descritpion not null
                strategy.Name != null &&                   //name not null
                strategy.Cost != null &&                    //cost is not null
                strategy.ExpectedUtilities.Count() > 0 &&    //att least one affected scenario
                !strategy.ExpectedUtilities.Any(x => x.ExpectedUtility1 == null) &&  //all expected utilities defined
                !strategy.ExpectedUtilities.Any(x => x.ExpectedUtilityDescription == null) //all expected utility descrtiption defiend
                )
            {
                strategy.IsComplete = true;
            }
            else strategy.IsComplete = false;


            //      var top = GetTopSixthUtilities();
            //return !top.Any(x => x.Utility1 == null); //if any are null, step is not complete
        }

        /// <summary>
        /// In Table ExpectedUtility
        /// Removes affected Scenarios not in newScenarioIDs
        /// Adds new scenarios listed in newScenarioIDs
        /// </summary>
        /// <param name="strategyID"></param>
        /// <param name="newScenarioIDs"></param>
        public void UpdateAffectedScenarios(long strategyID, int[] newScenarioIDs)
        {   
            var strat = GetByID((int)strategyID);

            var sListnew = new List<int>(newScenarioIDs.ToList());
            
            //list of scenarios no longer affiliated: strat scenarios not in newScenarioID list
            //Type: ExpectedUtility
            var relationstoDelete = strat.ExpectedUtilities
                            .Where(x => !sListnew.Contains((int)x.ScenarioID)).ToList();
            
            //List of Int IDs to add: newScenarioIDs not in strat
            var relationstoAdd = sListnew
                                .Where(x => !strat.ExpectedUtilities
                                .Select(b => b.ScenarioID)
                                .Contains(x))
                            .ToList();

            //estisting affiliations left in tact to retain expected utility information
            

            //remove records no longer affiliated
            foreach (var item in relationstoDelete)
            {
                db.ExpectedUtilities.DeleteOnSubmit(item);
            }

            //add updated recoreds
            foreach (var sid in relationstoAdd)
            {
                var xwalkItem = new ExpectedUtility();
                xwalkItem.ArchitecturalStrategyID = strategyID;
                xwalkItem.ScenarioID = sid;
                //xwalkItem.ExpectedUtility = 
                db.ExpectedUtilities.InsertOnSubmit(xwalkItem);
            }//insert each Scenario Affacted by Strategy

            //update complete status of strategy
            UpdateIsComplete(strat);
        }

      
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}

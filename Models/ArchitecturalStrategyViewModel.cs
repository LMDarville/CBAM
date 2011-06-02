using System.Web.Mvc;
using CBAM.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace CBAM.Models
{
    public class ArchitecturalStrategyViewModel
    {
   
        public CBAMDataContext db = new CBAMDataContext();

        //for Create
        public ArchitecturalStrategy Strategy { get; private set; }
        public MultiSelectList ScenarioSelectList { get; private set; }
        public MultiSelectList ScenariosSelectedList { get; private set; }
        public SelectList ResultUtilityList { get; private set; }
        public int[] ScenariosIDs { get; set; }
        public StrategyForExpectedResponse strategyForExpectedResponse;
        IQueryable<Scenario> AffiliatedScenarios { get; set; }

        public ArchitecturalStrategyViewModel()
        {
        }

        public ArchitecturalStrategyViewModel(ArchitecturalStrategy strategy)
        {
            var scenarioRepository = new ScenarioRepository();
            var strategyRepository = new ArchitecturalStrategyRepository();
            Strategy = strategy;
            Strategy.Description = strategy.Description == null ? null : strategy.Description.Trim();

            AffiliatedScenarios = strategyRepository.GetAffiliatedScenariosByStratID(strategy.ID).OrderBy(x => x.Priority);
            
            //scenarios (in top 6th) not affiliated with strategy 
            var slistNotUsed = scenarioRepository.GetTopSixth(strategy.ProjectID).OrderBy(x => x.Priority).ToList(); //new scenario
            if (strategy != null && strategy.ID !=0) //set used for existing scenario
            {
                slistNotUsed = scenarioRepository.GetTopSixth(strategy.ProjectID)
                    .Where(a => !Strategy.ExpectedUtilities.Select(x => x.Scenario.ID).Contains(a.ID)).OrderBy(x => x.Priority).ToList();
            }
            //scenarios affiliated with strategy
          //  var slistused = scenarioRepository.GetTopSixth(strategy.ProjectID)
          //                      .Where(a => strategy.ExpectedUtilities
          //                          .Select(x => x.ScenarioID). //Select Scenario IDs
            //                          Contains(a.ID)).ToList();   //a.ID = 
           //needs to be list of availble scenarios, exclude already selected

                          //(theObjList, value, text to show, pre-SelectedItems)
            ScenarioSelectList = new MultiSelectList(slistNotUsed, "ID", "Name", strategy.ExpectedUtilities.Select(x => x.ScenarioID));
            ScenariosSelectedList = new MultiSelectList(AffiliatedScenarios, "ID", "Name", strategy.ExpectedUtilities.Select(x => x.ScenarioID));
            strategyForExpectedResponse = populateStrategyForExpectedResponse(strategy, AffiliatedScenarios);
        }

        //for EditEntities get //top 1/6 to add util descriptions
        private StrategyForExpectedResponse populateStrategyForExpectedResponse(ArchitecturalStrategy strategy, IQueryable<Scenario> affiliatedScenarios) //int[] scenarios
        {
            var scenarioRepository = new ScenarioRepository();
            var respStrat = new StrategyForExpectedResponse();
            respStrat.ScenariosForStratUtil = new List<ScenariosForStratUtil>();
            respStrat.ID = strategy.ID;
            respStrat.Name = strategy.Name;

            var sToAdd = new ScenariosForStratUtil();

            //add scenarios to strategy
            foreach (var s in affiliatedScenarios)
            {
                //get xWalkItem for Scenario  --for Expected Response
                ExpectedUtility ExpectedUtilityItem = scenarioRepository.Get_ExpectedUtility(respStrat.ID, s.ID);
                //Scenario s = scenarioRepository.GetByID(sID);
                respStrat.ScenariosForStratUtil.Add(new ScenariosForStratUtil
                    {//s = a scenario
                          scenarioID = s.ID,
                          Name = s.Name,
                          Description = s.Description.Trim(),
                          expectedUtilID = ExpectedUtilityItem.ID,
                          ExpectedUtilityDescription = ExpectedUtilityItem.ExpectedUtilityDescription == null ? null : ExpectedUtilityItem.ExpectedUtilityDescription.Trim(),
                          ExpectedUtility = ExpectedUtilityItem.ExpectedUtility1,
                          Utilities = new List<Utility>(),
                    });

                     //add utilties best/worse/current/desired

                     //order utilities according to QualityAttribute order
                    var utils = s.Utilities.OrderBy(x => x.QualityAttributeResponseType.Order);
                    foreach (var u in utils)
                    {//Utilities to Scenario
                        //add utilities to last added scenario in ScenariosForUtilUpdate list
                        respStrat.ScenariosForStratUtil[respStrat.ScenariosForStratUtil.Count - 1].Utilities
                            .Add(new Utility
                        { 
                            ID = u.ID, 
                            Description = u.Description, 
                            QualityAttributeResponseType =   u.QualityAttributeResponseType,
                            Utility1 = u.Utility1
                        });
                    }//end util for loop
                  

               
            }//end scenario for
            return respStrat;
        }


        
  
    }
}

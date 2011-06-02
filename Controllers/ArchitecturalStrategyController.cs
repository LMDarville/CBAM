using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBAM.Models;
using CBAM.Helpers;

namespace CBAM.Controllers
{
    public class ArchitecturalStrategyController : Controller
    {
         IArchitecturalStrategyRepository asRepository;
       
        // Dependency Injection enabled constructors
        public ArchitecturalStrategyController()
            : this(new ArchitecturalStrategyRepository())
        {
        }

        public ArchitecturalStrategyController(IArchitecturalStrategyRepository repository)
        {
            asRepository = repository;
        }

        // GET: /ArchitecturalStrategy/
        public ActionResult Index(long projID)
        {
            var strategies = asRepository.GetAll().Where(x => x.IsActive == true && x.ProjectID == projID);
            return View(strategies);
        }


        // GET: /ArchitecturalStrategy/Create
        public virtual ActionResult Create(long projID)
        {
            ModelStateHelpers.ModelMessage = null; //clear any prior messages
            ArchitecturalStrategy strategy = new ArchitecturalStrategy();
            strategy.ProjectID = projID;
           
            return View(new ArchitecturalStrategyViewModel(strategy));
        }

        // Post: /ArchitecturalStrategy/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create(ArchitecturalStrategy strategy, int[] ScenariosIDs)
        {
            strategy.DateAdded = DateTime.Now;
            strategy.LastModified = DateTime.Now;
            strategy.Name = strategy.Name.Trim();
            strategy.Description = strategy.Description.Trim();
            strategy.Cost = strategy.Cost.Value;

            if (ScenariosIDs != null)//populate for validation
            {
                strategy.ScenariosIDs = new int[ScenariosIDs.Count()];
                Array.Copy(ScenariosIDs, strategy.ScenariosIDs, ScenariosIDs.Count());
            }
            ModelState.AddModelErrors(strategy.GetRuleViolations());
            if (ModelState.IsValid)
            {
                try
                {
                    asRepository.Add(strategy);
                    asRepository.Save();
                    asRepository.UpdateAffectedScenarios(strategy.ID, strategy.ScenariosIDs);
                    asRepository.UpdateIsComplete(strategy);
                    asRepository.Save();
                    ModelStateHelpers.ModelMessage = " Record(s) Saved Successfully.  Next update Expected Utility in Part 2 below.";
                    return View(new ArchitecturalStrategyViewModel(strategy));
                    //return Redirect(Url.RouteUrl(new { controller = "Scenario", action = "Index" }) + "#" + "tab-5");
                }
                catch
                {
                    //For Error w/o defined message
                    ModelState.AddModelError("ID", "Record not Added Sucessfully");
                    //ModelState.AddModelErrors(scenario.GetRuleViolations());
                    return View(new ArchitecturalStrategyViewModel(strategy));
                }
            }
            //else not valid 
            else ModelState.AddModelError("ID", "Record not Added Sucessfully");
            return View(new ArchitecturalStrategyViewModel(strategy));
        }//end create post

       
        // GET: /ArchitecturalStrategy/Edit
        public virtual ActionResult Edit(int id)
        {
            ModelStateHelpers.ModelMessage = null; //clear any prior messages
            ArchitecturalStrategy strategy = asRepository.GetByID(id);
            return View(new ArchitecturalStrategyViewModel(strategy));
        }

        // Post: /ArchitecturalStrategy/Edit 
        //Changes to name, description or affected scnearios
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Edit(int id, ArchitecturalStrategy strategy)
        {
            var sToUpdate = asRepository.GetByID(id); 
            TransferValuesTo(sToUpdate, strategy);

            ModelState.AddModelErrors(sToUpdate.GetRuleViolations());
            if (ModelState.IsValid)
            {
                try
                {
                    //updates scenarios affected list
                    asRepository.UpdateAffectedScenarios(sToUpdate.ID, sToUpdate.ScenariosIDs);
                    asRepository.Save();

                    //update complete status of strategy
                    asRepository.UpdateIsComplete(strategy);
                    asRepository.Save();
                    //return View(new ArchitecturalStrategyViewModel(strategy));

                    //update complete status on steps
                    var stepsRepository = new StepsRepository();
                    stepsRepository.UpdateSteps(strategy.ProjectID);
                    
                    ModelStateHelpers.ModelMessage = " Record(s) Saved Successfully.  Next update Expected Utility in Part 2 below.";
                    sToUpdate = asRepository.GetByID(id); //sToUpdate was not showing new affiliations
                    return View(new ArchitecturalStrategyViewModel(sToUpdate));
                  
                   // return Redirect(Url.RouteUrl(new { controller = "Scenario", action = "Index" }) + "#" + "tab-5");
                }
                catch
                {
                    //For Error w/o defined message
                    ModelState.AddModelError("ID", "Record not Added Sucessfully");
                    //ModelState.AddModelErrors(scenario.GetRuleViolations());
                    return View(new ArchitecturalStrategyViewModel(sToUpdate));
                }
            }
            //else not valid 
            else ModelState.AddModelError("ID", "Record not Added Sucessfully");
            return View(new ArchitecturalStrategyViewModel(strategy));
        }
   
       // Post: /ArchitecturalStrategy/Edit --Update Utility Descriptions
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult EditUtilityDescriptions(int id, StrategyForExpectedResponse erStrategy)
        {
            ArchitecturalStrategy strategy = asRepository.GetByID(id);
            
            foreach (var s in erStrategy.ScenariosForStratUtil)
            {  
                var expectedUtilToUpdate = asRepository.GetExpectedUtilityByID(s.expectedUtilID); ;
                TransferValuesTo(expectedUtilToUpdate, s);
                ModelState.AddModelErrors(s.GetRuleViolations());
            }

          
            if (ModelState.IsValid)
            {
                try
                {
                    //update complete status of strategy
                    asRepository.UpdateIsComplete(strategy);
                    asRepository.Save();
                    //return View(new ArchitecturalStrategyViewModel(strategy));

                    //update complete status on steps
                    var stepsRepository = new StepsRepository();
                    stepsRepository.UpdateSteps(strategy.ProjectID);

                    return Redirect(Url.RouteUrl(new { controller = "Scenario", action = "Index" }) + "#" + "tab-5");
                }
                catch
                {
                    //For Error w/o defined message
                    ModelState.AddModelError("ID", "Record not Added Sucessfully");
                    //ModelState.AddModelErrors(scenario.GetRuleViolations());
                    return View(new ArchitecturalStrategyViewModel(strategy));
                }
            }
           //else not valid 
           // else ModelState.AddModelError("ID", "Record not Added Sucessfully");
            return View(new ArchitecturalStrategyViewModel(strategy));
            
        }

        //Transfer Expected Utility Values: For /ArchitecturalStrategy/Edit   
        private static void TransferValuesTo(ExpectedUtility itemToUpdate, ScenariosForStratUtil scenario)
        {
            itemToUpdate.ExpectedUtility1 = scenario.ExpectedUtility;
            itemToUpdate.ExpectedUtilityDescription = scenario.ExpectedUtilityDescription.Trim();
        }

        //Transfer Values: For /ArchitecturalStrategy/Edit 
        private static void TransferValuesTo(ArchitecturalStrategy sToUpdate, ArchitecturalStrategy strategy)
        {
            sToUpdate.Name = strategy.Name.Trim();
            sToUpdate.Description = strategy.Description.Trim();
            sToUpdate.DateAdded = DateTime.Now;
            sToUpdate.LastModified = DateTime.Now;
            sToUpdate.Cost = strategy.Cost;
            

            if (strategy.ScenariosIDs != null)//populate for validation
            {
                sToUpdate.ScenariosIDs = new int[strategy.ScenariosIDs.Count()];
                Array.Copy(strategy.ScenariosIDs, sToUpdate.ScenariosIDs, strategy.ScenariosIDs.Count());
            }

        }
      


        ////}
        //// GET: /ArchitecturalStrategy/Create
        //[ChildActionOnly]
        //public virtual ActionResult CreateUtilityLists(int scenarioID)
        //{
        //    var vmodel = ArchitecturalStrategyViewModel.getUtilityDropDowns(scenarioID, asRepository);
        //    return View(vmodel);
        //}



    }
}

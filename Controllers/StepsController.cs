using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBAM.Helpers;
using CBAM.Models;

namespace CBAM.Controllers
{
    public class StepsController : Controller
    {
        IStepsRepository stepsRepository;
        // Dependency Injection enabled constructors
        public StepsController()
            : this(new StepsRepository())
        {
        }
        public StepsController(IStepsRepository repository)
        {
            stepsRepository = repository;
        }


        // GET: /Shared/
        public virtual ActionResult StepsList()
        {
            var steps = stepsRepository.GetAll();
            return View(steps);
        }

      
       
        [Serializable]
        public class jr  //jquery result
        {
            public string success { get; set; }
        }  

         [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult updateNextStep()
        {
            //Not implemented
            //stepsRepository.up.upups.updateSteps();
            //stepsRepository.Save();
            ////get current status
     
            var result = new jr { success = "true" };
            return Json(result);  
        }

    }
}

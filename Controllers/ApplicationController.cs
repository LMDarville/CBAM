using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBAM.Models;

namespace CBAM.Controllers
{
    public class ApplicationController : Controller
    {
        IScenarioRepository scenarioRepository;
        // Dependency Injection enabled constructors
        public ApplicationController()
            : this(new ScenarioRepository())
        {
        }

        public ApplicationController(IScenarioRepository repository)
        {
            scenarioRepository = repository;
        }

    }
}

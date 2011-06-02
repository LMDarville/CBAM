using System;
using System.Web;
using System.Web.Mvc;
using CBAM.Helpers;
using CBAM.Models;
using System.Collections.Generic;
using System.Threading;

namespace CBAM.Controllers
{
    public class ProjectController : Controller
    {
        IProjectRepository projectRepository;
        // Dependency Injection enabled constructors
        public ProjectController()
            : this(new ProjectRepository())
        {
        }

        public ProjectController(IProjectRepository repository)
        {
            projectRepository = repository;
        }


        // GET: /Project/
        public virtual ActionResult Index()
        {
            var vmodel = projectRepository.GetAll();
            return View(vmodel);
        }


        // GET: /Project/Detail/5
        public virtual ActionResult Detail(int? id)
        {
            if (id == null){
                return View("NotFound"); 
            }

            Project project = projectRepository.GetByID(id.Value);
            if (project == null)
            {
                return View("NotFound"); 
            }

            return View(project);
        }

        // GET: /Project/Edit/5
        public virtual ActionResult Edit(int id)
        {
            Project project = projectRepository.GetByID(id);
            if (project == null){
                return View("NotFound");
            }
            return View(project);
        }
             
        // POST: /Project/Edit/5
        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public virtual ActionResult Edit(int id, FormCollection collection)
        {
            try{
                Project project = projectRepository.GetByID(id);
                TryUpdateModel(project);
                //UpdateModel(project);
                project.LastModified = DateTime.Now;
                project.Name = project.Name.Trim();
                project.Description = project.Description.Trim();
                ModelState.AddModelErrors(project.GetRuleViolations());

                if (ModelState.IsValid)
                {
                    projectRepository.Save();
                    return RedirectToAction("Detail", new { id = project.ID });
                }
                else return View(project);

            }
            catch
            {
                //For Error w/o defined message
                ModelState.AddModelError("ID", "Record not updated sucessfully");
                return View(projectRepository.GetByID(id));
            }

          
        }

        // GET: /Project/Create
        public virtual ActionResult Create()
        {
            Project project = new Project();
            return View(project);
        }

        // POST: /Project/Create/
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create(Project project)
        {
            project.DateAdded = DateTime.Now;
            project.LastModified = DateTime.Now;
            project.Name = project.Name.Trim();
            project.Description = project.Description.Trim();
            project.IsActive = true;
    
            ModelState.AddModelErrors(project.GetRuleViolations());
            if (ModelState.IsValid)
            {
                try
                {
                    projectRepository.Add(project);
                    projectRepository.Save();
                    return RedirectToAction("Detail", new { id = project.ID });
                }
                catch
                {
                    //For Error w/o defined message
                    ModelState.AddModelError("ID", "Record not Added Sucessfully");
                    return View(project);
                }
            }
            //else not valid 
            else ModelState.AddModelError("ID", "Record not Added Sucessfully");
            return View(project);
        }//end create post


        protected override void HandleUnknownAction(string actionName)
        {
            throw new HttpException(404, "Action not found");
        }

        public virtual ActionResult Confused()
        {
            return View();
        }

    
        /// <summary>
        /// ///////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Trouble()
        {
            return View("Error");
        }
    }
}

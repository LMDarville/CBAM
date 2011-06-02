using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using CBAM.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CBAM.Models
{  //These classes created for use in "create" and "edit" in Project controller
   //Creates on object for updatable grid to pass relevant data pieces on form as an obj
    //update rules
    [MetadataType(typeof(ProjectMetaData))]
    public partial class Project
    {
        [Bind(Exclude = "ProjectID")]
        public class ProjectMetaData
        {  // Validation rules for the votes class
            [Required(ErrorMessage = "Description is Required")]
            [StringLength(1000, ErrorMessage = "Description must be Under 1000 Characters")]
            public String Description { get; set; }
            [Required(ErrorMessage = "Name is Required")]
            [StringLength(50, ErrorMessage = "Name must be Under 50 Characters")]
            public String Name { get; set; }
        }
    }//end partial class project

    [Bind(Include = "ID, Name, Description, DateAdded, LastModified")]
    public partial class Project
    {
           //VALIDATION-------------------------------------
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }
        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Description) || Description.Length < 1)
                yield return new RuleViolation("Description is required", "Project.Description");
            if (!String.IsNullOrEmpty(Description) && Description.Length > 1000)
                yield return new RuleViolation("Description may not be longer than 1000 characters", "Project.Description");
            if (String.IsNullOrEmpty(Name) || Name.Length < 1)
                yield return new RuleViolation("Name is required", "Project.Name");
            if (!String.IsNullOrEmpty(Name) && Name.Length > 50)
                yield return new RuleViolation("Name may not be longer than 50 characters", "Project.Name");
            yield break;
        }//end GetRuleViolations
        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }
 
    }//end partial class project
 
}//end namespace

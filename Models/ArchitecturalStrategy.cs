using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using CBAM.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CBAM.Models
{   //These classes created for use in "create" and "edit" in ArchitecturalStrategy controller
    //Creates on object for updatable grid to pass relevant data pieces on form as an obj

    [Bind(Include = "ID, Name, Description, ScenariosIDs, DateAdded, LastModified, ProjectID, Cost")]
    public partial class ArchitecturalStrategy
    {//items needed to create/edit ArchitecturalStrategy
        //ID, Name, etc. already defined.
        public int[] ScenariosIDs { get; set; }
        //VALIDATION-------------------------------------
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }
        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Description) || Description.Length < 1)
                yield return new RuleViolation("Description is required", "Description");
            if (!String.IsNullOrEmpty(Description) && Description.Length > 250)
                yield return new RuleViolation("Description may not be longer than 250 characters", "Description");
            if (String.IsNullOrEmpty(Name) || Name.Length < 1)
                yield return new RuleViolation("Name is required", "Name");
            if (!String.IsNullOrEmpty(Name) && Name.Length > 250)
                yield return new RuleViolation("Name may not be longer than 250 characters", "Name");
            if (ScenariosIDs == null)
                yield return new RuleViolation("Select Affected Scenarios", "ScenariosIDs");
            if (Cost == null)
                yield return new RuleViolation("Please Enter Cost", "Cost");
            if (Cost < 0)
                yield return new RuleViolation("Cost should be > 0", "Cost");
            yield break;
        }
    }

    //update rules
    [MetadataType(typeof(ArchitecturalStrategyMetaData))]
    public partial class ArchitecturalStrategy
    {
        [Bind(Exclude = "ID")]
        public partial class ArchitecturalStrategyMetaData
        {  // Validation rules for the votes class
            [Required(ErrorMessage = "Description is Required")]
            [StringLength(250, ErrorMessage = "Description must be Under 250 Characters")]
            public String Description { get; set; }
            [Required(ErrorMessage = "Name is Required")]
            [StringLength(250, ErrorMessage = "Name must be Under 250 Characters")]
            public String Name { get; set; }
            //[Required(ErrorMessage = "Scenario is Required")]
            public int[] ScenariosIDs { get; set; }
            [Required(ErrorMessage = "Cost is Required")]
            [Range(0, 9000000000)]
            //[RegularExpression(@"\b\d+\b",ErrorMessage = "Cost must be a postive integer, no commas")]
            public long Cost { get; set; } 
        }
    }


    /////// <summary>
    /////// For updateing the Utility and Response on Affected Scenarios
    /////// </summary>
    /////// 

    ////[Bind(Include = "scenarioID, ExpectedUtility, ExpectedUtilityRating")]
    ////public partial class ScenarioForStrategy
    ////{//must also update scenario controller class to populate
    ////    public long scenarioID { get; set; }
    ////    public int ExpectedUtility { get; set; }
    ////    public string ExpectedUtilityRating { get; set; }
    ////}

    ////[MetadataType(typeof(ScenarioForStrategyMetaData))]
    ////public partial class ScenarioForStrategy
    ////{
    ////   [Bind(Exclude = "ID")]
    ////   public partial class ScenarioForStrategyMetaData
    ////   {
    ////       [Required(ErrorMessage = "Scenario Required")]
    ////       public long scenarioID { get; set; }
    ////       [Required(ErrorMessage = "Utility is Required")]
    ////       public int ExpectedUtility { get; set; }
    ////       [Required(ErrorMessage = "Expected Utility is Required")]
    ////       [StringLength(250, ErrorMessage = "Expected Utility must be Under 250 Characters")]
    ////       public string ExpectedUtilityRating { get; set; }
    ////   }
    ////}


}
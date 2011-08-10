using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using CBAM.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CBAM.Models
{
    //These classes created for use in "editUtilities" in scneario controller
    //Creates on object for updatable grid to pass relevant scenario and utility data pieces on form as an obj

    [Bind(Include = "ScenariosForUtilUpdate")]
    public class ScenarioList
    {
        //List containing only items needed for updating utiltities
        public List<ScenarioForUtil> ScenariosForUtilUpdate { get; set; }
        public long projectID { get; set; }
    }

    [Bind(Include = "scenarioID, Description, utilities")]
    public class ScenarioForUtil
    {//must also update scenario controller class to populate
        public long scenarioID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<util> utilities { get; set; }
    }

    [Bind(Include = "ID, Description, projectID")]
    public partial class util
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public string QualityAttributeResponseTypeType { get; set; }
        //VALIDATION-------------------------------------
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }
        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Description) || Description.Length < 1)
                yield return new RuleViolation("Utility Description is required", "Description");
            if (!String.IsNullOrEmpty(Description) && Description.Length > 250)
                yield return new RuleViolation("Description may not be longer than 250 characters", "Description");

            yield break;
        }
        public void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }
    }
   
    [MetadataType(typeof(utilMetaData))]
    public partial class util
    {
        [Bind(Exclude = "utilID")]
        public partial class utilMetaData
        {  // Validation rules for the util class
            [Required(ErrorMessage = "Description is Required")]
            [StringLength(250, ErrorMessage = "Description must be Under 250 Characters")]
            public String Description { get; set; }
        }
    }
}
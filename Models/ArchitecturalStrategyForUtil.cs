using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using CBAM.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CBAM.Models
{
    //These classes created for use in "editArchitecturalUtilityDescriptions" in architecturalStrategy controller
    //Creates on object for updatable grid to pass relevant scenario and utility data pieces on form as an obj


    [Bind(Include = "ID, Name, Description, ScenariosForStratUtil")]
    public class StrategyForExpectedResponse
    {//must also update scenario controller class to populate

        //for Create/ExpectedUtilityDescription
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ScenariosForStratUtil> ScenariosForStratUtil { get; set; }
    }

     
    [Bind(Include = "scenarioID, Name, Description, ExpectedUtil")]
    public partial class ScenariosForStratUtil
    {//must also update scenario controller class to populate
        public long scenarioID { get; set; }
        public long expectedUtilID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExpectedUtilityDescription { get; set; } //description
        public int? ExpectedUtility { get; set; }  //int value
        public List<Utility> Utilities { get; set; } //b,w,c,d utilties for reference

        //VALIDATION-------------------------------------
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }
        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (!ExpectedUtility.HasValue)
                yield return new RuleViolation("Utility Level is required", "ExpectedUtility");
            if (String.IsNullOrEmpty(ExpectedUtilityDescription) || ExpectedUtilityDescription.Length < 1)
                yield return new RuleViolation("Utility Description is required", "ExpectedUtilityDescription");
            yield break;
        }

        public void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }

    }

    //for clientside validation
    [MetadataType(typeof(ScenariosForStratUtilMetaData))]
    public partial class ScenariosForStratUtil
    {
        [Bind(Exclude = "scenarioID")]
        public partial class ScenariosForStratUtilMetaData
        {  // Validation rules for the util class
            [Required(ErrorMessage = "Expected Utility is Required")]
            [StringLength(250, ErrorMessage = "Expected Utility Response must be Under 250 Characters")]
            public String ExpectedUtilityDescription { get; set; }

            [Required(ErrorMessage = "Expected Utility Level is Required")]
            public int? ExpectedUtility { get; set; }

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using CBAM.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CBAM.Models
{
    //These classes created for use in "editUtilitiesRatings" in scneario controller
    //Creates on object for updatable grid to pass relevant scenario and utility data pieces on form as an obj

    [Bind(Include = "ScenariosForUtilRatingUpdate")]
    public class ScenarioListUtilRatings
    {
        //List containing only items needed for updating utiltities
        public List<ScenarioForUtilRating> ScenariosForUtilRatingUpdate { get; set; }
        public long projectID { get; set; }
    }

    [Bind(Include = "scenarioID, Description, utilities")]
    public class ScenarioForUtilRating
    {//must also update scenario controller class to populate
        public long scenarioID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<utilRating> utilities { get; set; }
        public List<QualityAttributeResponseType> QualityAttributeResponseTypes { get; set; }
    }

    [Bind(Include = "ID, Description, Utility")]
    public partial class utilRating
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public string QualityAttributeResponseTypeType { get; set; }
        public int Order { get; set; }
        [Range(0,100)]
        public int? Utility { get; set; }

        //VALIDATION-------------------------------------
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }
        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (!Utility.HasValue)
                yield return new RuleViolation("Utility Must have value", "Utility");
            yield break;
        }

        public void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }

    }


    [MetadataType(typeof(utilRatingMetaData))]
    public partial class utilRating
    {
        [Bind(Exclude = "utilRatingID")]
        public partial class utilRatingMetaData
        {   //Validation rules for the utilRating class
            //rating for Worst=<Desired=<Best
            //[Required(ErrorMessage = "*Required")]
            //[StringLength(250, ErrorMessage = "Description must be Under 250 Characters")]
            public int? Utility { get; set; }

        }

    }
}
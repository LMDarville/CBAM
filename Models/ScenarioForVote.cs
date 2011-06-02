using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using CBAM.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CBAM.Models
{
    //These classes created for use in "Edit" in votes controller
    //Creates on object for updatable grid to pass relevant scenario and vote data pieces on form as an obj

    [Bind(Include = "ScenariosForVotes")]
    public class ScenarioListForVotes
    {
        //List containing only items needed for updating votes
        public List<ScenarioForVotes> ScenariosForVotes { get; set; }
        public long projectID { get; set; }
    }

    [Bind(Include = "scenarioID, Name, Description, Votes")]
    public partial class ScenarioForVotes
    {//must also update scenario controller class to populate
        public long scenarioID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 100)]
        [Required(ErrorMessage = "Vote is Required. Enter zero if no votes apply.")]
        public int? Votes { get; set; }

   
        //VALIDATION-------------------------------------
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }
        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (!Votes.HasValue)
            yield return new RuleViolation("Vote is required", "Votes");
            if (Votes < 0 || Votes > 100)
            yield return new RuleViolation("1 Vote must be between 0 and 100.", "Votes");
            yield break;
        }

        public void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }

    }

    //[MetadataType(typeof(ScenarioForVotesMetaData))]
    //public partial class ScenarioForVotes
    //{
    //    [Bind(Exclude = "ID")]
    //    public partial class ScenarioForVotesMetaData
    //    {  // Validation rules for the votes class
    //        [Range(0, 100)]
    //        [Required(ErrorMessage = "Vote is Required. Enter zero if no votes apply.")]
    //        public int? Votes { get; set; }

    //    }

    //}
}
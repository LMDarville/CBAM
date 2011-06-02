using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using CBAM.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CBAM.Models
{
    [Bind(Include = "Utilities")]
    public class UtilityList
    {
        public List<Utility> Utilities { get; set; }
    }
    [Bind(Include = "ID, scenarioID, QualityAttributeResponseTypeID, Description")]
        public partial class Utility
        {
            //VALIDATION-------------------------------------
            public bool IsValid
            {
                get { return (GetRuleViolations().Count() == 0); }
            }

            public IEnumerable<RuleViolation> GetRuleViolations()
            {
                //if (String.IsNullOrEmpty(Description) || Description.Length < 1)
                //    yield return new RuleViolation("Description is required", "Scenario.Description");
                if ((!(String.IsNullOrEmpty(Description)) && Description.Length > 250))
                    yield return new RuleViolation("Description may not be longer than 250 characters", "Scenario.Description");

                yield break;
            }

            partial void OnValidate(ChangeAction action)
            {
                if (!IsValid)
                    throw new ApplicationException("Rule violations prevent saving");
            }
        }

      

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using CBAM.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CBAM.Models
{
    [MetadataType(typeof(ScenarioMetaData))]
    public partial class Scenario
    {
        [Bind(Exclude = "ScenarioID")]
        public class ScenarioMetaData
        {
            [Required(ErrorMessage = "Please enter description")]
            public string Name { get; set; }
            [Required(ErrorMessage="Please enter description")]
            public string Description { get; set; }
        }
    }///metadata for client side validation
  
    [Bind(Include = "scenarioID, ID, projectID, Name, Description, Source, Stimulas, Source, Artifact, Environment, Response, ResponseMeasure, ImportanceRatingID, Importance, Votes")]
        public partial class Scenario
        {
            public virtual string ImportanceString
            {//returns "N/A" for Nulls
                get
                {
                    if (ImportanceRatingID.HasValue)
                    {
                        return Importance.Description;
                    }
                    else return "N/A";
                }
        }
            //VALIDATION-------------------------------------
            public bool IsValid
            {
                get { return (GetRuleViolations().Count() == 0); }
            }
            public IEnumerable<RuleViolation> GetRuleViolations()
            {
                if (String.IsNullOrEmpty(Name))
                    yield return new RuleViolation("Name is required", "Scenario.Name");
                if (!String.IsNullOrEmpty(Name) && Name.Length > 50)
                    yield return new RuleViolation("Name may not be longer than 50 characters", "Scenario.Name");
                
                if (String.IsNullOrEmpty(Description))
                    yield return new RuleViolation("Description is required", "Scenario.Description");
                if (!String.IsNullOrEmpty(Description) && Description.Length > 250)
                    yield return new RuleViolation("Description may not be longer than 250 characters", "Scenario.Description");
                
                if (String.IsNullOrEmpty(Source))
                    yield return new RuleViolation("Source is required", "Scenario.Source");
                if (!String.IsNullOrEmpty(Source) && Source.Length > 50)
                    yield return new RuleViolation("*Source may not be longer than 50 characters", "Source");

                if (String.IsNullOrEmpty(Stimulas))
                    yield return new RuleViolation("Stimulas is required", "Scenario.Stimulas");
                if (!String.IsNullOrEmpty(Stimulas) && Stimulas.Length > 50)
                    yield return new RuleViolation("*Stimulas may not be longer than 50 characters", "Stimulas");

                if (String.IsNullOrEmpty(Artifact))
                    yield return new RuleViolation("Artifact is required", "Scenario.Artifact");
                if (!String.IsNullOrEmpty(Artifact) && Artifact.Length > 50)
                    yield return new RuleViolation("*Artifact may not be longer than 50 characters", "Artifact");

                if (String.IsNullOrEmpty(Environment))
                    yield return new RuleViolation("Environment is required", "Scenario.Environment");
                if (!String.IsNullOrEmpty(Environment) && Environment.Length > 50)
                    yield return new RuleViolation("*Environment may not be longer than 50 characters", "Environment");


                if (String.IsNullOrEmpty(Response))
                    yield return new RuleViolation("Response is required", "Scenario.Response");
                if (!String.IsNullOrEmpty(Response) && Response.Length > 50)
                    yield return new RuleViolation("*Response may not be longer than 50 characters", "Response");


                if (String.IsNullOrEmpty(ResponseMeasure))
                    yield return new RuleViolation("ResponseMeasure is required", "Scenario.ResponseMeasure");
                if (!String.IsNullOrEmpty(ResponseMeasure) && ResponseMeasure.Length > 50)
                    yield return new RuleViolation("*ResponseMeasure may not be longer than 50 characters", "ResponseMeasure");
           
                yield break;
            }
            partial void OnValidate(ChangeAction action)
            {
                if (!IsValid)
                    throw new ApplicationException("Rule violations prevent saving");
            }
        }

    
 

}//end namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

namespace CBAM.Models
{
    public class ReportRepository : CBAM.Models.IReportRepository 
    {
        private CBAMDataContext db = new CBAMDataContext();

        public List<Benefit> GetBenefitbyProjID(long projID)
        {
            return db.spGetBenefit(projID).ToList();
        }

        public IEnumerable<Benefit> SummarizedBenefitData(List<Benefit> benefits)
        {
            IEnumerable<Benefit> totalData = from b in benefits
                    join strat in db.ArchitecturalStrategies on b.StrategyID equals strat.ID
                    //into bs
                    group b by new { b.StrategyID, strat.Cost, strat.Name, projName = strat.Project.Name } into s
                    select new Benefit
                    {
                        // ID = s.Key,
                        StrategyName = s.Key.Name,
                        StrategyID = s.Key.StrategyID,
                        ScenarioID = s.Max(b => b.StrategyID),
                        ProjectName = s.Key.projName,
                        StrategyCost = s.Key.Cost,
                        Benefit1 = s.Sum(b => b.Benefit1),
                        ROI = 5
                        //ROI = s.Sum(b => b.Benefit1) == null ? 0 : (decimal)(s.Key.Cost / s.Sum(b => b.Benefit1))
                    };
            return totalData;
        }

    }
}

<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
    <p>
        CBAM Assistant semi-automates the CBAM (Cost Benefit Analysis Method) 
        process developed by SEI (Software Engineering Institute) at CMU (Carnegie Mellon University).
         CBAM is a process used to estimate the ROI (Return on Investment) of various software architectural design strategies.
    </p>

    <p>CBAM is an incremental nine step process.</p>
        <p style="text-indent:50px";>1. First, stakeholders define, refine and further prioritize scenarios. The top 1/3 scenarios are selected based on priorities established.</p>
        <p style="text-indent:50px";>2. In Step 2 stakeholders define the response goals for the best, worst, current and desired cases for the selected scenarios.</p> 
        <p style="text-indent:50px";>3. In Step 3 the stakeholders vote on each of the selected scenarios considering the expected response goals.  100 votes in total are allocated by the stakeholders. The votes are used to rank the priority of each scenario. The scenario with the most votes has the highest priority. </p>
        <p style="text-indent:50px";>4. Step 4 uses the top 50% of the scenarios from Step 3 based on votes (i.e. top 1/6 of the total). In Step 4, stakeholders assign a utility rating to each response goal (best, worst, current and desired) for the selected scenarios.  </p>
        <p style="text-indent:50px";>5 & 6. CBAM Assistant consolidates step 5 and 6 of CBAM. In CBAM Step 5 stakeholders develop or review architectural strategies for the top 1/6 scenarios.  A cost is assigned to each architectural strategy.  The scenarios impacted by the strategy must also be defined. In Step 6, the stakeholders determine the expected response goals and utility rates for each scenario and architectural strategy. </p>
        <p style="text-indent:50px";>7. CBAM Assistant automates step 7. The output is available in the excel report once steps 1 through 5 of CBAM assistant (1-6 of CBAM) are completed. The benefit is calculated in Step 7. The benefit for each architectural strategy is based on the current utility rating and expected utility rating for each scenario impacted by the strategy.  The benefit (b) for a given scenario (i) based on strategy (j) is calculated  b_ij=U_(expected )- U_current.  U_(expected )is the utility expected if the scenario is implemented and U_current is the current utility. </p>
                <p style="text-indent:50px";>The total benefit for the strategy is the sum of the weighted benefit for each scenario impacted by that strategy. The weight (w_i) of a scenario is based on votes, calculated as votes_i⁄(total votes)*100 where total votes = 100. The total benefit (B_j) for an architectural strategy is the sum of the weighted benefit for each scenario impacted by the strategy or ∑_j (b_ij  × w_i ) where wi is the weighting for scenario i.  The ROI is the ratio of total benefit for an architectural strategy divided by the estimated cost of implementing the strategy. </p>
                <p style="text-indent:50px";>The output of CBAM is the ROI for each strategy being compared. The output also includes a list of architectural strategies and their expected utility, total benefit of each strategy. </p>
        <p style="text-indent:50px";>8. In Step 8, the developer uses CBAM output to determine the architectural strategies that best suits the business needs. </p>
        <p style="text-indent:50px";>9. In Step 9, the developer must confirm results based on experience and intuition.  For example if a costly strategy that the developer expected to have lower benefit, is returned with the best ROI, the developer may need to review prior steps for accuracy and revisions may be required.  The user can run an updated report and review again. </p>

</asp:Content>

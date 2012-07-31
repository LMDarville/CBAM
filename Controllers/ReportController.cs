using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBAM.Helpers;
using CBAM.Models;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Drawing;
using NPOI.HSSF.Util;

namespace CBAM.Controllers
{
    public class ReportController : Controller
    {
        IScenarioRepository scenarioRepository;
        IArchitecturalStrategyRepository strategyRepository;
        IReportRepository reportRepository;
        // Dependency Injection enabled constructors
        public ReportController()
            : this(new ScenarioRepository())
        {
        }
        public ReportController(IScenarioRepository repository)
        {
            scenarioRepository = repository;
            strategyRepository = new ArchitecturalStrategyRepository();
            reportRepository = new ReportRepository();
        }

        //Get: /1/Report/Index    
        public virtual ActionResult Index(long projID)
        {
            var proj = scenarioRepository.GetProjectByID(projID);
            return View(proj);
        }

        //Get: /1/Report/RunReport  
        public virtual ActionResult ReportButton(long projID)
        {
            var proj = scenarioRepository.GetProjectByID(projID);
            return PartialView(proj);
        }

        //Post: button click
        public void RunReports(int projID)
        {
            FileStream fs = new FileStream(Server.MapPath(@"\template\Template_DataTable.xls"), FileMode.Open, FileAccess.Read);
            HSSFWorkbook templateWorkbook = new HSSFWorkbook(fs, true);

            //add Styles to workbook
            HSSFCellStyle styleMiddle = templateWorkbook.CreateCellStyle();
            styleMiddle.Alignment = CellHorizontalAlignment.CENTER;

            HSSFCellStyle styleLeftWrap = templateWorkbook.CreateCellStyle();
            styleLeftWrap.Alignment = CellHorizontalAlignment.LEFT;
            //styleMiddle.VerticalAlignment = CellVerticalAlignment.CENTER;
            styleLeftWrap.WrapText = true;    //wrap the text in the cell
            //------------------------------------------------------

            //get data and populate each sheet
            ScenarioData(projID, ref fs, ref templateWorkbook);
            ResponseGoalData(projID, ref fs, ref templateWorkbook);
            ArchitecturalStrategyDetailData(projID, ref fs, ref templateWorkbook);
            ArchitecturalStrategySummaryData(projID, ref fs, ref templateWorkbook);

            //send to export
            MemoryStream ms = new MemoryStream();
            templateWorkbook.Write(ms);                   //write workbook infor to ms
            //ms.Flush();
            ExportDataTableToExcel(ms, "CBAMReport.xls"); //send ms and filename for export 
        }

        public void ScenarioData(int projID, ref FileStream fs, ref HSSFWorkbook templateWorkbook)
        {

            HSSFSheet sheet = templateWorkbook.GetSheet("Scenarios");

            var data = scenarioRepository.GetByProjectID(projID).OrderBy(x => x.Priority).AsEnumerable();

            //IEnumerable<TestData> data = db.TestDatas;

            populateScenarioData(templateWorkbook, sheet, data);

            sheet.ForceFormulaRecalculation = true;

        }

        public void ResponseGoalData(int projID, ref FileStream fs, ref HSSFWorkbook templateWorkbook)
        {
            HSSFSheet sheet = templateWorkbook.GetSheet("ResponseGoals");
            //var data = scenarioRepository.GetByProjectID(projID).OrderBy(x => x.Priority).AsEnumerable();

            var data = scenarioRepository.GetTopThird(projID).OrderBy(x => x.Priority).AsEnumerable();

            populateResponseGoalData(templateWorkbook, sheet, data);
            sheet.ForceFormulaRecalculation = true;
        }

        public void ArchitecturalStrategyDetailData(int projID, ref FileStream fs, ref HSSFWorkbook templateWorkbook)
        {
            HSSFSheet sheet = templateWorkbook.GetSheet("Architectural Strategies");
            var data = strategyRepository.GetAllbyProjID(projID).OrderBy(x => x.ID).AsEnumerable();
            populateStrategyDetailData(templateWorkbook, sheet, data);
            sheet.ForceFormulaRecalculation = true;


        }
  
        public void ArchitecturalStrategySummaryData(int projID, ref FileStream fs, ref HSSFWorkbook templateWorkbook)
        {  //Guid myguid = System.Guid.NewGuid();  //use GUID to generate temp table, delete after report runs
           //db.spGenerateBenefitTable(projID, myguid); //create table

            List<Benefit> benefits = reportRepository.GetBenefitbyProjID(projID); //db.spGetBenefit(projID).ToList();

            //summarized data, use same structure as "benefits"
            IEnumerable<Benefit> totalData = reportRepository.SummarizedBenefitData(benefits);

            HSSFSheet sheet = templateWorkbook.GetSheet("StrategyBenefitDetail");
            populateStrategyBenefitDetails(templateWorkbook, sheet, benefits); //s/b used on detail page?
            sheet.ForceFormulaRecalculation = true;

            HSSFSheet sheet1 = templateWorkbook.GetSheet("ROI");
            populateStrategyBenefitSummaryData(templateWorkbook, sheet1, totalData);
            sheet1.ForceFormulaRecalculation = true;
        }
        public static void populateStrategyBenefitDetails(HSSFWorkbook wb, HSSFSheet sheet, IEnumerable<Benefit> data)
        {
            //set headerRow and 1st column
            const int maxRows = 65535; //npoi uses excel 2003
            int hRowNum = 4; //row starts at 0.  
            int startCol = 0; //col starts at 0
            int errorRow = 2; //note errors
            int errorCol = 6; //note errors
            HSSFRow headerRow = sheet.GetRow(hRowNum);
            int colIndex = startCol;

            #region headers
            //date
            sheet.GetRow(0).GetCell(1).SetCellValue(DateTime.Now);

            //Title
            sheet.GetRow(1).GetCell(colIndex).SetCellValue(data.FirstOrDefault().ProjectName);

            // handling headers.
            headerRow.GetCell(colIndex).SetCellValue("ID");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Strategy");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Cost");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("ScenarioID");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("CurrentUtility");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("ExpectedUtility");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Raw Benefit");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("wt(Votes)");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("(Normalized) Benefit");
            colIndex++;
            //headerRow.GetCell(colIndex).SetCellValue("Benefit");
            //colIndex++;
            
            #endregion
            #region populateData
            // foreach (DataColumn column in propertyInfos)
            //     headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value.
            int rowIndex = hRowNum + 1;
            try
            {
                foreach (var item in data)
                {
                    HSSFRow dataRow = sheet.CreateRow(rowIndex);

                    if (rowIndex < maxRows - 1)
                    {
                        //write each field
                        colIndex = startCol;

                        dataRow.CreateCell(colIndex).SetCellValue(item.StrategyID.ToString()); //rank = rec#
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.StrategyName.ToString());
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.StrategyCost.Value);
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.ScenarioID.ToString());
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.CurrentUtility.Value);
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.ExpectedUtility.Value);
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.RawBenefit.Value);
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.wt.Value);
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.Benefit1.Value);
                        colIndex++;

                        rowIndex++;
                    }//end if check max rows
            #endregion
                    #region errors
                    else
                    {
                        colIndex = startCol;
                        dataRow.CreateCell(colIndex).SetCellValue("Dataset exceeds maximum number of rows");
                        sheet.GetRow(errorRow).CreateCell(errorCol).SetCellValue("Data Exceeds max records");
                    }

                }//end data loop
            }// end try
            catch
            {
                colIndex = startCol;
                if (sheet.GetRow(errorRow).LastCellNum >= errorCol) //error cell exists
                {
                    sheet.GetRow(errorRow).GetCell(errorCol).SetCellValue("Error Occured");
                }
                else
                {//create cell for error message
                    sheet.GetRow(errorRow).CreateCell(errorCol).SetCellValue("Error Occured");
                }


            }
                    #endregion

        }
  
        public static void populateStrategyBenefitSummaryData(HSSFWorkbook wb, HSSFSheet sheet, IEnumerable<Benefit> data)
        {
            //set headerRow and 1st column
            const int maxRows = 65535; //npoi uses excel 2003
            int hRowNum = 4; //row starts at 0.  
            int startCol = 1; //col starts at 0
            int errorRow = 2; //note errors
            int errorCol = 6; //note errors
            HSSFRow headerRow = sheet.GetRow(hRowNum);
            int colIndex = startCol;

            #region headers
            //date
            sheet.GetRow(0).GetCell(1).SetCellValue(DateTime.Now);

            //Title
            sheet.GetRow(1).GetCell(colIndex).SetCellValue(data.FirstOrDefault().ProjectName);

            // handling headers.
            headerRow.GetCell(colIndex).SetCellValue("Rank");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Strategy");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("ID");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Cost");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Benefit");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("ROI");
            colIndex++;
            #endregion
            #region populateData
            // foreach (DataColumn column in propertyInfos)
            //     headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value.
            int rowIndex = hRowNum + 1;
            try
            {
                foreach (var item in data)
                {
                    HSSFRow dataRow = sheet.GetRow(rowIndex);
      
                    if (rowIndex < maxRows - 1)
                    {
                        //write each field
                        colIndex = startCol;

                        dataRow.GetCell(colIndex).SetCellValue(rowIndex - hRowNum); //rank = rec#
                        colIndex++;
                        dataRow.GetCell(colIndex).SetCellValue(item.StrategyName.ToString());
                        colIndex++;
                        dataRow.GetCell(colIndex).SetCellValue(item.StrategyID.ToString());
                        colIndex++;
                        dataRow.GetCell(colIndex).SetCellValue(item.StrategyCost.Value);
                        colIndex++;
                        dataRow.GetCell(colIndex).SetCellValue(item.Benefit1.Value);
                        colIndex++;
                
                  
                        rowIndex++;
                    }//end if check max rows
            #endregion
            #region errors
                    else
                    {
                        colIndex = startCol;
                        dataRow.CreateCell(colIndex).SetCellValue("Dataset exceeds maximum number of rows");
                        sheet.GetRow(errorRow).CreateCell(errorCol).SetCellValue("Data Exceeds max records");
                    }

                }//end data loop
            }// end try
            catch
            {
                colIndex = startCol;
                if (sheet.GetRow(errorRow).LastCellNum >= errorCol) //error cell exists
                {
                    sheet.GetRow(errorRow).GetCell(errorCol).SetCellValue("Error Occured");
                }else
                {//create cell for error message
                    sheet.GetRow(errorRow).CreateCell(errorCol).SetCellValue("Error Occured");
                }


            }
               #endregion

        }
  
        public static void populateStrategyDetailData(HSSFWorkbook wb, HSSFSheet sheet, IEnumerable<ArchitecturalStrategy> data)
        {
            #region workbookStyles
            //add Styles to workbook
            HSSFCellStyle styleMiddle = wb.CreateCellStyle();
            styleMiddle.Alignment = CellHorizontalAlignment.CENTER;
            HSSFCellStyle styleLeftWrap = wb.CreateCellStyle();
            styleLeftWrap.Alignment = CellHorizontalAlignment.LEFT;
            styleMiddle.VerticalAlignment = CellVerticalAlignment.CENTER;
            styleLeftWrap.WrapText = true;    //wrap the text in the cell
            //----------------------------------------------------------

            //font style1:  italic, blue color, fontsize=20
            HSSFFont font1 = wb.CreateFont();
            font1.Color = HSSFColor.BLUE.index;
            font1.IsItalic = true;
            font1.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
            font1.Underline = (byte)HSSFBorderFormatting.BORDER_THIN;
           // font1.Underline = (byte)FontUnderlineType.DOUBLE;
           // font1.FontHeightInPoints = 20;

            //bind font with styleItalicBold
            HSSFCellStyle italicBold = wb.CreateCellStyle();
            italicBold.SetFont(font1);
            //----------------------------------------------------------
  
            //bind font with styleItalicBold
            HSSFCellStyle underline = wb.CreateCellStyle();
            underline.BorderBottom = CellBorderType.THIN;
            underline.BottomBorderColor = HSSFColor.BLUE_GREY.index;

            HSSFCellStyle topline = wb.CreateCellStyle();
            topline.BorderTop = CellBorderType.THIN;
            topline.TopBorderColor = HSSFColor.BLUE_GREY.index;

            #endregion
            //set headerRow and 1st column
            const int maxRows = 65536; //npoi uses excel 2003
            int hRowNum = 4; //row starts at 0.  
            int startCol = 0;
            int errorRow = 2; //note errors
            int errorCol = 6; //note errors
          
            HSSFRow headerRow = sheet.GetRow(hRowNum);
            int colIndex = startCol;
            
            #region Headers
            //date
            sheet.GetRow(0).GetCell(1).SetCellValue(DateTime.Now);

            //Title
            sheet.GetRow(1).GetCell(1).SetCellValue(data.FirstOrDefault().Project.Name);

            // handling headers.
            headerRow.GetCell(colIndex).SetCellValue("Strategy");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Name");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Description");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Scenarios Affected");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Current Response");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Expected Response");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Current Utility");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Expected Utility");
            colIndex++;
            #endregion //headers

            #region populateData
            // foreach (DataColumn column in propertyInfos)
            //     headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value.
            int rowIndex = hRowNum + 1;
            HSSFRow dataRow = sheet.CreateRow(rowIndex);
            Boolean newStrategyRow = true;
            var i = 0; //index for loops
            try
            {
                foreach (var item in data)
                {
                    dataRow = sheet.CreateRow(rowIndex);
                    if (rowIndex < maxRows - 1)
                    {
                        //write each field
                        newStrategyRow = true;
                        colIndex = startCol;
                        dataRow.CreateCell(colIndex).SetCellValue(item.ID);
                        dataRow.GetCell(colIndex).CellStyle = topline;
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.Name.ToString());
                        dataRow.GetCell(colIndex).CellStyle = topline;
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.Description.ToString());
                        dataRow.GetCell(colIndex).CellStyle = topline;
                        colIndex++;

                        var subcol = colIndex;
                        foreach (var s in item.ExpectedUtilities)
                        {
                            dataRow = sheet.CreateRow(rowIndex);
                            colIndex = subcol;
                            var currentUtil = s.Scenario.Utilities.Where(y => y.QualityAttributeResponseTypeID == "C").FirstOrDefault();
                            
                            //ID of scenario affected
                            dataRow.CreateCell(colIndex).SetCellValue(s.Scenario.ID);
                            colIndex++;
                            //current description
                            dataRow.CreateCell(colIndex).SetCellValue(currentUtil.Description.ToString());
                            dataRow.GetCell(colIndex).CellStyle = styleLeftWrap;

                            colIndex++;
                            //expected description
                            dataRow.CreateCell(colIndex).SetCellValue(s.ExpectedUtilityDescription.ToString());
                            colIndex++;
                            //current utility
                            dataRow.CreateCell(colIndex).SetCellValue(currentUtil.Utility1.Value);
                            colIndex++;
                            //expected utility
                            dataRow.CreateCell(colIndex).SetCellValue(s.ExpectedUtility1.Value);

                            #region Add formats to top of a strategy
                            if (newStrategyRow) //set formats for new strat row
                            {   i = subcol;
                                while (i <= colIndex)
                                {
                                    dataRow.GetCell(i).CellStyle = topline;
                                    i++;
                                }
                            }
                            #endregion

                            rowIndex++;
                            newStrategyRow = false;
                            if (rowIndex >= maxRows - 1)
                            {throw new InvalidDataException();}
                        }//end expected utilities 


                        #region Add formats to bottom of strategy
                        dataRow = sheet.CreateRow(rowIndex);
                        i = startCol;
                        while (i <= colIndex)//add formats to subsection w/utility info
                        {
                            dataRow.CreateCell(i).CellStyle = underline;
                            i++;
                        }
                        #endregion

                        //end of strategy
                        rowIndex++;

                    }//end if check max rows
                    
            #endregion
            #region errors

                    else
                    {
                        colIndex = startCol;
                        dataRow.CreateCell(colIndex).SetCellValue("Dataset exceeds maximum number of rows");
                        sheet.GetRow(errorRow).CreateCell(errorCol).SetCellValue("Data Exceeds max records");
                    }

                }//end data loop
            }// end try
            catch
            {
                colIndex = startCol;
                if (sheet.GetRow(errorRow).LastCellNum >= errorCol) //error cell exists
                {
                    sheet.GetRow(errorRow).GetCell(errorCol).SetCellValue("Error Occured");
                }
                else
                {//create cell for error message
                    sheet.GetRow(errorRow).CreateCell(errorCol).SetCellValue("Error Occured");
                }
            }
                    #endregion
        }
        
        public static void populateScenarioData(HSSFWorkbook wb, HSSFSheet sheet, IEnumerable<Scenario> data)
        {
           //set headerRow and 1st column
            const int maxRows = 65536; //npoi uses excel 2003
            int hRowNum = 4; //row starts at 0.  
            int startCol = 0;
            int errorRow = 2; //note errors
            int errorCol = 6; //note errors
            HSSFRow headerRow = sheet.GetRow(hRowNum);
            int colIndex = startCol;

            #region headers
            //date
            sheet.GetRow(0).GetCell(1).SetCellValue(DateTime.Now);

            //Title
            sheet.GetRow(1).GetCell(1).SetCellValue(data.FirstOrDefault().Project.Name);

            // handling headers.
            headerRow.GetCell(colIndex).SetCellValue("Priority");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Name");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Description");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Source");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Artifact");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Stimulas");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Environment");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Response");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("ResponseMeasure");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Votes");
    #endregion
            #region populateData
            // foreach (DataColumn column in propertyInfos)
            //     headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value.
            int rowIndex = hRowNum + 1;
            try
            {
                foreach (var item in data)
                {
                    HSSFRow dataRow = sheet.CreateRow(rowIndex);

                    if (rowIndex < maxRows - 1)
                    {
                        //write each field
                        colIndex = startCol;
                        dataRow.CreateCell(colIndex).SetCellValue(item.Priority.ToString());
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.Name.ToString());
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.Description.ToString());
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.Source.ToString());
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.Artifact.ToString());
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.Stimulas.ToString());
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.Environment.ToString());
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.Response.ToString());
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.ResponseMeasure.ToString());
                        colIndex++;
                        dataRow.CreateCell(colIndex).SetCellValue(item.Votes.Value);
                        rowIndex++;
                    }//end if check max rows
            #endregion populateData
            #region errors

                    else
                    {
                        colIndex = startCol;
                        dataRow.CreateCell(colIndex).SetCellValue("Dataset exceeds maximum number of rows");
                        sheet.GetRow(errorRow).CreateCell(errorCol).SetCellValue("Data Exceeds max records");
                    }

                }//end data loop
            }// end try
            catch
            {
                colIndex = startCol;
                if (sheet.GetRow(errorRow).LastCellNum >= errorCol) //error cell exists
                {
                    sheet.GetRow(errorRow).GetCell(errorCol).SetCellValue("Error Occured");
                }
                else
                {//create cell for error message
                    sheet.GetRow(errorRow).CreateCell(errorCol).SetCellValue("Error Occured");
                }
            }
            #endregion

            //Assumes template file has table named "ScenarioTable"
            // retrieve the named range
            //string sRangeName = "scenarioData";                //range name
            //int snamedRangeIdx = wb.GetNameIndex(sRangeName);   //range index
            //HSSFName sRange = wb.GetNameAt(snamedRangeIdx);     //get range
            //String sheetName = wb.GetSheetName(wb.ActiveSheetIndex);    //sheet name
            //String reference = sheetName +"!A6:C25";             //new area reference
            //sRange.SetRefersToFormula(reference);           //set range to new area reference

        }
        // IEnumerable<TestData> data = db.TestDatas;
        public static void populateResponseGoalData(HSSFWorkbook wb, HSSFSheet sheet, IEnumerable<Scenario> data)
        {
            //set headerRow and 1st column
            const int maxRows = 65536; //npoi uses excel 2003
            int hRowNum = 4; //row starts at 0.  
            int startCol = 0;
            HSSFRow headerRow = sheet.GetRow(hRowNum);
            int colIndex = startCol;
            int errorRow = 2; //note errors
            int errorCol = 6; //note errors

            #region headers
            //date
            sheet.GetRow(0).GetCell(1).SetCellValue(DateTime.Now);
            //Title
            sheet.GetRow(1).GetCell(1).SetCellValue(data.FirstOrDefault().Project.Name);

            // handling headers.
            headerRow.GetCell(colIndex).SetCellValue("Priority");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Name");
            colIndex++;
            headerRow.GetCell(colIndex).SetCellValue("Votes");
            colIndex++;
            foreach (var utilityitem in data.FirstOrDefault().Utilities)
            {
                //write each field

                headerRow.GetCell(colIndex).SetCellValue(utilityitem.QualityAttributeResponseType.Type.ToString() + " Response Goal");
                colIndex++;

                headerRow.GetCell(colIndex).SetCellValue(utilityitem.QualityAttributeResponseType.Type.ToString()+ " Utility");
                colIndex++;

            //    if (utilityitem.Utility1.HasValue)//only top 1/6 have utility value
            //    {
            //        headerRow.GetCell(colIndex).SetCellValue(utilityitem.Description.ToString());
            //    }
            //    colIndex++;
            }// end utility item loop
            #endregion

            #region populateData
            // foreach (DataColumn column in propertyInfos)
            //     headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value.
            int rowIndex = hRowNum + 1;
            try{

                foreach (var item in data)
                {
                    HSSFRow dataRow = sheet.CreateRow(rowIndex);
                    if (rowIndex < maxRows - 1)
                        {
                            colIndex = startCol;
    
                            dataRow.CreateCell(colIndex).SetCellValue(item.Priority.ToString());
                            colIndex++;

                            dataRow.CreateCell(colIndex).SetCellValue(item.Name.ToString());
                            colIndex++;


                            dataRow.CreateCell(colIndex).SetCellValue(item.Votes.Value);
                            colIndex++;

                            foreach (var utilityitem in item.Utilities)
                            {
                                    //write each field
                        
                                    dataRow.CreateCell(colIndex).SetCellValue(utilityitem.Description.ToString()); 
                                    colIndex++;

                                    if (utilityitem.Utility1.HasValue)//only top 1/6 have utility value
                                    {
                                        dataRow.CreateCell(colIndex).SetCellValue(utilityitem.Utility1.Value);
                                    }
                                    colIndex++;
                            }// end utility item loop
                                rowIndex++;
                            }//end if check max rows
            #endregion
            #region populateErrors
                     else
                    {
                        colIndex = startCol;
                        dataRow.CreateCell(colIndex).SetCellValue("Dataset exceeds maximum number of rows");
                        sheet.GetRow(errorRow).CreateCell(errorCol).SetCellValue("Data Exceeds max records");
                    }

                }//end data loop
            }// end try
            catch
            {
                colIndex = startCol;
                if (sheet.GetRow(errorRow).LastCellNum >= errorCol) //error cell exists
                {
                    sheet.GetRow(errorRow).GetCell(errorCol).SetCellValue("Error Occured");
                }
                else
                {//create cell for error message
                    sheet.GetRow(errorRow).CreateCell(errorCol).SetCellValue("Error Occured");
                }
            }
            #endregion
            //Assumes template file has table named "ScenarioTable"
            // retrieve the named range
            //string sRangeName = "scenarioData";                //range name
            //int snamedRangeIdx = wb.GetNameIndex(sRangeName);   //range index
            //HSSFName sRange = wb.GetNameAt(snamedRangeIdx);     //get range
            //String sheetName = wb.GetSheetName(wb.ActiveSheetIndex);    //sheet name
            //String reference = sheetName +"!A6:C25";             //new area reference
            //sRange.SetRefersToFormula(reference);           //set range to new area reference

        }
      
        public static void ExportDataTableToExcel(MemoryStream memoryStream, string fileName)
        {
            HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName));
            response.Clear();

            response.BinaryWrite(memoryStream.GetBuffer());
            response.End();
        }
      


    }
}

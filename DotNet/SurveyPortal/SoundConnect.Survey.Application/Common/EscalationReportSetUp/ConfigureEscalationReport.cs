using ClosedXML.Excel;
using SoundConnect.Survey.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Application.Common.EmailDetails
{
    public class ConfigureEscalationReport
    {
        public void ConfigureCell(IXLWorksheet worksheet, SurveyType surveyType)
        {
            #region SetUp
            int numberOfCells = 0;
            int detailColumns = 1; // Inicialized in one because the last column
            switch (surveyType)
            {
                case SurveyType.CareTeam:
                    detailColumns += 5;
                    numberOfCells = QuestionKey.CareTeamSurvey.NumberOfKeys() + detailColumns;
                    break;
                case SurveyType.PCP:
                    detailColumns += 7;
                    numberOfCells = QuestionKey.PCPSurvey.NumberOfKeys() + detailColumns;
                    break;
                case SurveyType.Specialist:
                    detailColumns += 7;
                    numberOfCells = QuestionKey.SpecialistSurvey.NumberOfKeys() + detailColumns;
                    break;
            }
            #endregion

            #region Headers
            for (int x = 1; x <= numberOfCells; x++)
            {
                worksheet.Cell(1, x).Style.Font.Bold = true;
                worksheet.Cell(1, x).Style.Alignment.WrapText = true;
                worksheet.Cell(1, x).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Cell(1, x).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(1, x).Style.Fill.BackgroundColor = XLColor.LightCyan;
                worksheet.Cell(1, x).Style.Font.FontSize = 11;
                worksheet.Cell(1, x).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(1, x).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(1, x).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(1, x).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                if (x > detailColumns)
                {
                    if (x == numberOfCells)
                        worksheet.Column(x).Width = 55;
                    else
                        worksheet.Column(x).Width = 30;
                }
                else
                    worksheet.Column(x).Width = 20;
            }
            #endregion

            #region Rows
            for (int x = 1; x <= numberOfCells; x++)
            {
                worksheet.Cell(2, x).Style.Alignment.WrapText = true;
                worksheet.Cell(2, x).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Cell(2, x).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                worksheet.Cell(2, x).Style.Font.FontSize = 11;
                worksheet.Cell(2, x).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(2, x).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(2, x).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(2, x).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            }
            #endregion

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using QuickRMS.Core.Service;

namespace QuickRMS.Site.WebUI.Common
{
    public class ExcelHelper
    {
        IWorkbook hssfworkbook;
        public DataTable ImportExcelFileToDatatable(string filePath)
        {
            #region//初始化信息
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    return ImportExcelFileToDatatable(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion
           
        }

        public DataTable ImportExcelFileToDatatable(Stream fileStream)
        {
            #region//初始化信息
            try
            {
                    hssfworkbook = new XSSFWorkbook(fileStream);
                    //XSSFFormulaEvaluator.EvaluateAllFormulaCells(hssfworkbook);
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion

            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            DataTable dt = new DataTable();

            //一行最后一个方格的编号 即总的列数
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
            }
            dt.Columns.Add("Result"); //总列数+1，最后一列用来记录导入结果。
            while (rows.MoveNext())
            {
                IRow row = (XSSFRow)rows.Current;
                DataRow dr = dt.NewRow();

                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);


                    if (cell == null)
                    {
                        dr[i] = string.Empty;
                    }
                    else
                    {
                        if (cell.CellType == CellType.Numeric && HSSFDateUtil.IsCellDateFormatted(cell))
                        {
                            var date = cell.TryGetDate();
                            dr[i] = date == null ? (cell.ToString().Trim()) : (Utilities.ConvertToDataStringValue(date));
                        }
                        else
                        {
                            if (cell.CellType == CellType.Formula)
                            {
                                dr[i] = cell.NumericCellValue;
                            }
                            else
                            {
                                dr[i] = cell.ToString().Trim();
                            }
                        }
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public Stream RenderDataTableToExcel(DataTable sourceTable)
        {
            XSSFWorkbook workbook = null;
            MemoryStream ms = null;
            ISheet sheet = null;
            XSSFRow headerRow = null;
            try
            {
                workbook = new XSSFWorkbook();
                ms = new MemoryStream();
                sheet = workbook.CreateSheet();
                headerRow = (XSSFRow)sheet.CreateRow(0);
                foreach (DataColumn column in sourceTable.Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                int rowIndex = 1;
                foreach (DataRow row in sourceTable.Rows)
                {
                    XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in sourceTable.Columns)
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    ++rowIndex;
                }
                
                for (int i = 0; i <= sourceTable.Columns.Count; ++i)
                    sheet.AutoSizeColumn(i);
                workbook.Write(ms);
                ms.Flush();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                ms.Close();
                sheet = null;
                headerRow = null;
                workbook = null;
            }
            return ms;
        }

        public void RenderDataTableToExcelFile(DataTable sourceTable,string filePath)
        {
            XSSFWorkbook workbook = null;
            FileStream ms = null;
            ISheet sheet = null;
            XSSFRow headerRow = null;
            try
            {
                workbook = new XSSFWorkbook();
                ms = File.Create(filePath);

                sheet = workbook.CreateSheet();
                int rowIndex = 0;
                
                var cellStyle = workbook.CreateCellStyle();
                cellStyle.FillPattern = FillPattern.SolidForeground;
                cellStyle.FillForegroundColor = HSSFColor.Yellow.Index;
                foreach (DataRow row in sourceTable.Rows)
                {
                    XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in sourceTable.Columns)
                    {
                        var cell = dataRow.CreateCell(column.Ordinal);
                        if (column.ColumnName == "Result")
                        {
                           
                            cell.CellStyle = cellStyle;
                        }
                        cell.SetCellValue(row[column].ToString());
                    }
                    ++rowIndex;
                }
                
                for (int i = 0; i <= sourceTable.Columns.Count; ++i)
                    sheet.AutoSizeColumn(i);
                workbook.Write(ms);
                ms.Flush();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                ms.Close();
                sheet = null;
                headerRow = null;
                workbook = null;
            }
           
        }
    }  
}
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ReadExcels.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReadExcels.Utils
{
    public class ExcelHelper
    {
        Dictionary<int, string> sheetData = new Dictionary<int, string>();
        List<PaymentSlipInfo> dataList = new List<PaymentSlipInfo>();
        
        List<List<ProductTable>> allProductTableList = new List<List<ProductTable>>();
        List<List<PromotionTable>> allPromotionTabletList = new List<List<PromotionTable>>();

        static FileStream fs = new FileStream(@"C:\Users\ADMIN\Downloads\ĐƠN HÀNG TH MILK VT 2.3.xls", FileMode.Open);

        // Khởi tại wordbook
        static HSSFWorkbook wordbook = new HSSFWorkbook(fs);
        static ISheet sheet = wordbook.GetSheetAt(0);
        public void ReadSheet()
        {
            
            int firstRow = sheet.FirstRowNum;
            int lastRow = sheet.LastRowNum;
            for (int i = firstRow + 1; i < lastRow; i++)
            {
                try
                {
                    IRow iRow = sheet.GetRow(i);
                    ICell key = iRow.GetCell((int)ExcelColumnEnum.B);
                    int indexRow = key.RowIndex;
                    object value = key.StringCellValue;

                    #region Try
                    /*if (key.StringCellValue.Equals("Số đơn hàng:"))
                    {
                        for(int j=0; j<4; j++)
                        {
                            IRow iRow = sheet.GetRow(i);
                                               
                            CellType cell = iRow.GetCell(i).GetCachedFormulaResultTypeEnum();
                            Console.WriteLine("Call"+cell.ToString());
                            switch (cell)
                            {
                                *//*case CellType.Numeric:
                                    ICell cellOne = iRow.GetCell(8);
                                    Console.WriteLine(cellOne.ToString());
                                    break;
                                case CellType.String:
                                    ICell cellTwo = iRow.GetCell(8);
                                    Console.WriteLine(cellTwo.StringCellValue);
                                    break;*//*
                            }
                            *//*ICell cellTwo = iRow.GetCell(5);
                            ICell cellThree = iRow.GetCell(8);*//*
                            //slipInfo = new PaymentSlipInfo(SDHIcell,NTGHIcell,NVBHIcell)
                            //Console.WriteLine("Cell 1: "+ cellOne);
                            *//*Console.WriteLine("Cell 2: " + formatter.FormatCellValue(cellTwo));
                            Console.WriteLine("Cell 3: " + formatter.FormatCellValue(cellThree));*//*
                            i++;
                        }
                        
                    }*/
                    #endregion
                    if (value.ToString().ToUpper().Equals("Số đơn hàng:".ToUpper()))
                    {
                        sheetData.Add(indexRow, key.StringCellValue);
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine(sheetData.Count);
        }

        public void WriteSheetToJson()
        {

        }

        public void ReadDataToExcel()
        {
            
            foreach (var item in sheetData)
            {
                int row = item.Key;
                List<ProductTable> productTableList = new List<ProductTable>();
                List<PromotionTable> promotionTablesList = new List<PromotionTable>();
                PaymentSlipInfo payment = new PaymentSlipInfo();
                ProductTable productTable = new ProductTable();
                //PromotionTable promotionTable = new PromotionTable();
                //int increase = 0;
                //Lấy ghi chú hoặc + đến = bao nhiều xác định 
                while (row != item.Key + 4)
                {
                    //Lấy đầu tiên là hắn row
                    IRow iRow = sheet.GetRow(row);
                    
                    if (row == item.Key)
                    {
                        ICell cellE = iRow.GetCell((int)ExcelColumnEnum.E);
                        ICell cellQ = iRow.GetCell((int)ExcelColumnEnum.Q);
                        ICell cellAB = iRow.GetCell((int)ExcelColumnEnum.AB);
                        //
                        
                        payment.orderNumber = (cellE.StringCellValue);
                        payment.dateCreate = (cellQ.ToString());
                        payment.NVBH = (cellAB.StringCellValue);
                    }
                    else if (row == item.Key + 1)
                    {
                        ICell cellE = iRow.GetCell((int)ExcelColumnEnum.E);
                        ICell cellQ = iRow.GetCell((int)ExcelColumnEnum.Q);
                        ICell cellAB = iRow.GetCell((int)ExcelColumnEnum.AD);
                        //
                        payment.MKH = (cellE.StringCellValue);
                        payment.datePrint = (cellQ.ToString());
                        payment.phoneNVBH = (cellAB.ToString());
                    }
                    else if(row == item.Key + 2)
                    {
                        ICell cellE = iRow.GetCell((int)ExcelColumnEnum.E);
                        ICell cellAB = iRow.GetCell((int)ExcelColumnEnum.AB);
                        //
                        payment.customerName = (cellE.StringCellValue);
                        payment.NVGH = (cellAB.StringCellValue);
                    }
                    else if(row == item.Key + 3)
                    {
                        ICell cellE = iRow.GetCell((int)ExcelColumnEnum.E);
                        ICell cellAB = iRow.GetCell((int)ExcelColumnEnum.AB);
                        //
                        payment.address =(cellE.StringCellValue);
                        payment.phoneNVGH =(cellAB.StringCellValue);
                    }
                    //Console.WriteLine("Số đơn hàng: "+ payment.orderNumber + "| Ngày tạo: "+ payment.dateCreate + "|"+"Nhân viên bảo hành:"+ payment.NVBH);
                    //increase++;
                    row++;
                }
                // Tính Hàng                    
                
                while (true)
                {
                    IRow r = sheet.GetRow(row);
                    string check = r?.GetCell((int)ExcelColumnEnum.B).ToString().ToLower();
                    if (string.IsNullOrEmpty(check))
                    {
                        //row++;
                    }
                    else
                    {
                        if (r.GetCell((int)ExcelColumnEnum.B).ToString().ToLower().Equals("STT".ToLower()))
                        {
                            break;
                        }
                    }
                    row++;
                }
                // Lây dữ liệu từ bảng sản phẩm    
                row += 1;
                //bool isPositionEnd = false;
                while(true)
                {
                    IRow iRow = sheet.GetRow(row);
                    
                    if(iRow.GetCell((int)ExcelColumnEnum.B).ToString().ToUpper().Equals("Khuyến mãi:".ToUpper()))
                    {
                        //isPositionEnd = true;
                        break;
                    }
                    else
                    {
                        productTable = GetDataToProductTable(iRow);
                    }
                    row++;
                    productTableList.Add(productTable);
                }

                // Tính ô khuyến mãi
                while (true)
                {
                    IRow r = sheet.GetRow(row);
                    string check = r?.GetCell((int)ExcelColumnEnum.B).ToString().ToLower();
                    if (string.IsNullOrEmpty(check))
                    {
                        //row++;
                    }
                    else
                    {
                        if (r.GetCell((int)ExcelColumnEnum.B).ToString().ToLower().Equals("STT".ToLower()))
                        {
                            break;
                        }
                    }
                    row++;
                }

                // Lấy dự liệu từ bảng khuyến mãi
                row += 1;
                bool isPositionEnd = false;
                while (!isPositionEnd)
                {
                    
                    PromotionTable promotionTable = new PromotionTable();
                    for(int i=0; i< 2; i++)
                    {
                        IRow iRow = sheet.GetRow(row + i);
                        if (iRow.GetCell((int)ExcelColumnEnum.B).ToString().ToUpper().Equals("Tổng giá trị đơn hàng:".ToUpper()))
                        {
                            isPositionEnd = true;
                            break;
                        }
                        if (i == 0)
                        {
                            ICell numericalOrder = iRow.GetCell((int)ExcelColumnEnum.B);
                            ICell productCode = iRow.GetCell((int)ExcelColumnEnum.C);
                            ICell name = iRow.GetCell((int)ExcelColumnEnum.F);
                            ICell HSD = iRow.GetCell((int)ExcelColumnEnum.P);
                            ICell QC = iRow.GetCell((int)ExcelColumnEnum.W);
                            ICell bin = iRow.GetCell((int)ExcelColumnEnum.Z);
                            ICell box = iRow.GetCell((int)ExcelColumnEnum.AC);
                            ICell infoMoney = iRow.GetCell((int)ExcelColumnEnum.AF);
                            promotionTable.bin = bin.ToString();
                            promotionTable.box = box.ToString();
                            promotionTable.numericalOrder = numericalOrder.ToString();
                            promotionTable.HSD = HSD.ToString();
                            promotionTable.productCode = productCode.ToString();
                            promotionTable.name = name.ToString();
                            promotionTable.promotionMoney = infoMoney.ToString(); //== "" ? "0" : infoMoney.ToString();
                            promotionTable.QC = QC.ToString();
                        }
                        else if (i == 1)
                        {
                            ICell promotionType = iRow.GetCell((int)ExcelColumnEnum.F);
                            ICell CTKM = iRow.GetCell((int)ExcelColumnEnum.C);
                            promotionTable.CTKM = CTKM.ToString();
                            promotionTable.promotionType = promotionType.ToString();
                        }
                    }
                    row += 2;
                    if (!isPositionEnd)
                    {
                        promotionTablesList.Add(promotionTable);
                    }
                }

                // Thêm dự liệu vào bảng
                payment.productTable = productTableList;
                payment.promotionTable = promotionTablesList;
               /* allProductTableList.Add(productTableList);
                allPromotionTabletList.Add(promotionTablesList);*/
                dataList.Add(payment);
            }
        }

        // Function Lấy dữ liều của bảng sản phẩm từ excels
        private ProductTable GetDataToProductTable(IRow iRow)
        {
            ProductTable tableProduct = new ProductTable();
            ICell numericalOrder = iRow.GetCell((int)ExcelColumnEnum.B);
            ICell productCode = iRow.GetCell((int)ExcelColumnEnum.C);
            ICell name = iRow.GetCell((int)ExcelColumnEnum.F);
            ICell HSD = iRow.GetCell((int)ExcelColumnEnum.O);
            ICell QC = iRow.GetCell((int)ExcelColumnEnum.S);
            ICell price = iRow.GetCell((int)ExcelColumnEnum.U);
            ICell bin = iRow.GetCell((int)ExcelColumnEnum.Z);
            ICell box = iRow.GetCell((int)ExcelColumnEnum.AC);
            ICell infoMoney = iRow.GetCell((int)ExcelColumnEnum.AF);
            tableProduct.bin = bin.ToString();
            tableProduct.box = box.ToString();
            tableProduct.numericalOrder = numericalOrder.ToString();
            tableProduct.price = price.ToString();
            tableProduct.HSD = HSD.ToString();
            tableProduct.productCode = productCode.ToString();
            tableProduct.name = name.ToString();
            tableProduct.infoMoney = infoMoney.ToString();
            tableProduct.QC = QC.ToString();
            return tableProduct;
        }

    }
}
// Lấy index của số đơn hàng có index với value
// Lấy được tất cả giá trị trong row
// Rồi thêm vào model
// Lưu models vào map<index,models>


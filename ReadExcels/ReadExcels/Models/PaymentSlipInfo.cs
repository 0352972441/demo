using System;
using System.Collections.Generic;
using System.Text;

namespace ReadExcels.Models
{
    class PaymentSlipInfo
    {
        /*private string customerName;
        private string orderNumber;
        private string dateCreate;
        private string NVBH;
        private string MKH;
        private string NVGH;
        private string address;
        private string phoneNVBH;
        private string datePrint;
        private string phoneNVGH;*/

        public string customerName { get; set; }
        public string orderNumber { get; set; }
        public string dateCreate { get; set; }
        public string NVBH { get; set; }
        public string MKH { get; set; }
        public string NVGH { get; set; }
        public string address { get; set; }
        public string phoneNVBH { get; set; }
        public string datePrint { get; set; }
        public string phoneNVGH { get; set; }
        public List<ProductTable> productTable { get; set; }
        public List<PromotionTable> promotionTable { get; set; }


        /*public PaymentSlipInfo() { }
        public PaymentSlipInfo(string orderNumber, string dateCreate, string NVBH, string MKH, string NVGH, string address, string phoneNVBH, string customerName, string datePrint, string phoneNVGH)
        {
            this.orderNumber = orderNumber;
            this.dateCreate = dateCreate;
            this.NVBH = NVBH;
            this.NVGH = NVGH;
            this.MKH = MKH;
            this.address = address;
            this.phoneNVBH = phoneNVBH;
            this.phoneNVGH = phoneNVGH;
            this.datePrint = datePrint;
            this.customerName = customerName;
        }

        public void setOrderNumber(string orderNumber)
        {
            this.orderNumber = orderNumber;
        }

        public void setDateCreate(string dateCreate)
        {
            this.dateCreate = dateCreate;
        }

        public void setNVBH(string NVBH)
        {
            this.NVBH = NVBH;
        }

        public void setNVGH(string NVGH)
        {
            this.NVGH = NVGH;
        }

        public void setMKH(string MKH)
        {
            this.MKH = MKH;
        }

        public void setPhoneNVBH(string phoneNVBH)
        {
            this.phoneNVBH = phoneNVBH;
        }

        public void setAddress(string address)
        {
            this.address = address;
        }

        public void setPhoneNVGH(string phoneNVGH)
        {
            this.phoneNVGH = phoneNVGH;
        }

        public void setDatePrint(string datePrint)
        {
            this.datePrint = datePrint;
        }

        public void setCustomerName(string customerName)
        {
            this.customerName = customerName;
        }*/

    }
}

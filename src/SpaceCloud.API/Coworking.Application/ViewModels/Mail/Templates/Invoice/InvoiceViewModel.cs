using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace Coworking.Application
{
    public class InvoiceViewModel
    {
        public string TCoworkingName { get; set; }

        public string TInvoiceNr { get; set; }
        public string TCoworkingAddress { get; set; }
        public string TCoworkingPlz { get; set; }
        public string TCoworkingCity { get; set; }
        public string TCoworkingState { get; set; }
        public string TCoworkingCountry { get; set; }
        public string TUserName { get; set; }
        public string TUserAddress { get; set; }
        public string TUserCity { get; set; }
        public string TUserCountry { get; set; }
        public string TUserPlz { get; set; }


        private DateTime _invoiceDate = DateTime.Now;
        public string TInvoiceDate => _invoiceDate.ToShortDateString();


        public DateTime _invoiceDueDate = DateTime.Now.AddDays(30);

        public string TInvoiceDueDate => _invoiceDueDate.ToShortDateString();


        private TimeSpan _paymentTime => _invoiceDueDate - _invoiceDate;
        public string TPaymentTime => (int) _paymentTime.TotalDays + " Days";


        public List<InvoicePurchaseViewModel> TPurchase { get; set; } = new List<InvoicePurchaseViewModel>();


        public decimal OrderPrice => TPurchase.Sum(x => x.ProductTotal);

        public decimal TotalNetPrice => OrderPrice / (decimal) 1.2;

        public decimal TotalTaxPrice => OrderPrice / 6;

        public string TOrderPrice => OrderPrice.ToString("C", new NumberFormatInfo {CurrencySymbol = "€ "});
        public string TTotalNetPrice => TotalNetPrice.ToString("C", new NumberFormatInfo {CurrencySymbol = "€ "});
        public string TTotalTaxPrice => TotalTaxPrice.ToString("C", new NumberFormatInfo {CurrencySymbol = "€ "});


        public string TUid { get; set; }
        public string TCoworkingBankName { get; set; }
        public string TCoworkingIban { get; set; }
        public string TCoworkingBic { get; set; }

        public string TContactPhone { get; set; }
        public string TContactEmail { get; set; }
        public string TContactName { get; set; }
    }


    public class InvoicePurchaseViewModel
    {
        public string TProductName { get; set; }

        /// <summary>
        /// z.B: pro Stunde
        /// </summary>
        public string TUnit { get; set; }

        public decimal UnitPrice { get; set; }
        public string TUnitPrice => UnitPrice.ToString("C", new NumberFormatInfo {CurrencySymbol = "€ "});

        public int ServiceDuration { get; set; }
        public string TServiceDuration => ServiceDuration.ToString();


        public decimal ProductTotal => UnitPrice * ServiceDuration;

        public string TProductTotal => ProductTotal.ToString("C", new NumberFormatInfo {CurrencySymbol = "€ "});
    }
}
using System;

namespace Models
{
    public class BankBase
    {
        public int BankID { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
    }

    public class Bank : BankBase
    {
        public string Addr { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public string ExtraText1 { get; set; }
        public string ExtraText2 { get; set; }
        public string ExtraText3 { get; set; }
        public string ExtraText4 { get; set; }
        public string ExtraText5 { get; set; }
        public string ExtraText6 { get; set; }
        public string ExtraText7 { get; set; }
        public string ExtraText8 { get; set; }
        public string ExtraText9 { get; set; }
        public string ExtraText10 { get; set; }
        public double? ExtraNumber1 { get; set; }
        public double? ExtraNumber2 { get; set; }
        public double? ExtraNumber3 { get; set; }
        public double? ExtraNumber4 { get; set; }
        public double? ExtraNumber5 { get; set; }
        public DateTime? ExtraDate1 { get; set; }
        public DateTime? ExtraDate2 { get; set; }
        public DateTime? ExtraDate3 { get; set; }
        public DateTime? ExtraDate4 { get; set; }
        public DateTime? ExtraDate5 { get; set; }
        public bool? ExtraLogic1 { get; set; }
        public bool? ExtraLogic2 { get; set; }
        public bool? ExtraLogic3 { get; set; }
        public bool? ExtraLogic4 { get; set; }
        public bool? ExtraLogic5 { get; set; }
        public string SysExtraText1 { get; set; }
        public string SysExtraText2 { get; set; }
        public string SysExtraText3 { get; set; }
        public string SysExtraText4 { get; set; }
        public string SysExtraText5 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string BankName1 { get; set; }
    }

    public class BankModel: Bank
    {

    }

    public class BankCreateModel : Bank
    {
        public BankCreateModel()
        {
            CreatedBy = 0;
            CreationDate = DateTime.Now;
        }
    }

    public class BankUpdateModel : Bank
    {
        public BankUpdateModel()
        {
            LastUpdatedBy = 0;
            LastUpdatedDate = DateTime.Now;
        }
    }
}

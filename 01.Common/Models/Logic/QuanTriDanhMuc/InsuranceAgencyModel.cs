using System;
namespace Models
{
    public class InsuranceAgency
    {
    }

    public class InsuranceAgencyBaseModel
    {
        public int InsuranceAgencyID { get; set; }
        public string InsuranceAgencyCode { get; set; }
        public string InsuranceAgencyName { get; set; }
    }

    public class InsuranceAgencyModel : InsuranceAgencyBaseModel
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }

    public class InsuranceAgencyCreateModel : InsuranceAgencyModel
    {

    }
}

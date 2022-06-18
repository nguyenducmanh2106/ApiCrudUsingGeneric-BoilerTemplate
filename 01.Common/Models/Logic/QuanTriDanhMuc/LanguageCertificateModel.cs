using System;
namespace Models
{
    public class LanguageCertificate
    {
    }

    public class LanguageCertificateBaseModel
    {
        public int LanguageCertificateID { get; set; }
        public string LanguageCertificateCode { get; set; }
        public string LanguageCertificateName { get; set; }
    }

    public class LanguageCertificateModel : LanguageCertificateBaseModel
    {
        public int LanguageID { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        #region Thông tin bổ sung
        public string LanguageName { get; set; }
        #endregion
    }

    public class LanguageCertificateCreateModel : LanguageCertificateModel
    {
    }
}

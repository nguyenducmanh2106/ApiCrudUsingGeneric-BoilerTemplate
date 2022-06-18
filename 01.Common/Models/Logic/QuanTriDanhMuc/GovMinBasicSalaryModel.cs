using System;
namespace Models
{
    public class GovMinBasicSalary
    {
    }

    public class GovMinBasicSalaryBaseModel
    {
        public int GovMinBasicSalaryID { get; set; }
    }

    public class GovMinBasicSalaryModel : GovMinBasicSalaryBaseModel
    {
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public int? ToMonth { get; set; }
        public int? ToYear { get; set; }
        public string Description { get; set; }
        public float? MinBasicSalary { get; set; }
        public bool? UseMaxSI { get; set; }
        public float? MaxSI { get; set; }
        public bool? UseMaxHI { get; set; }
        public float? MaxHI { get; set; }
        public float? MaternityLeaveAllowance { get; set; }
        public float? MaternityLeaveRate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool? UseMaxTU { get; set; }
        public float? MaxTU { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class GovMinBasicSalaryCreateModel : GovMinBasicSalaryModel
    {

    }
}

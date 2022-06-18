using System;
namespace Models
{
    public class RewardForm
    {

    }

    public class RewardFormBase
    {
        public int RewardFormID { get; set; }
        public string RewardFormCode { get; set; }
        public string RewardFormName { get; set; }
    }

    public class RewardFormModel : RewardFormBase
    {
        public bool? Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}

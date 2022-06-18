using System;
namespace Models
{
    [Serializable]
    public class GroupRoleBase
    {
        public int GroupRoleId { get; set; }
        public string GroupRoleName { get; set; }
        public string GroupRoleCode { get; set; }
    }
}

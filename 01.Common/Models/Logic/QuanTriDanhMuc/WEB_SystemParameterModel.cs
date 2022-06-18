using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class WEB_SystemParameterBase
    {
        public Guid ParameterID { get; set; }
    }
    public class WEB_SystemParameter : WEB_SystemParameterBase
    {
        public Guid ApplicationID { get; set; }
        public string ParameterCode { get; set; }
        public string Description { get; set; }
        public string ParameterValue { get; set; }
        public Guid CreatedByUserID { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public Guid LastModifiedByUserID { get; set; }
        public DateTime LastModifiedOnDate { get; set; }
    }
    public class WEB_SystemParameterModel : WEB_SystemParameter
    {
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCF.Model.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderProcessMessage
    {
        public int OrderMessageId { get; set; }
        public Nullable<byte> OrderMessageType { get; set; }
        public Nullable<byte> OrderMessageStatus { get; set; }
        public Nullable<System.DateTime> OrderMessageSubmitTime { get; set; }
        public Nullable<System.DateTime> OrderMessageProcessTime { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> MerId { get; set; }
        public Nullable<int> AdminId { get; set; }
    }
}

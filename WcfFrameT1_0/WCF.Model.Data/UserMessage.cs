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
    
    public partial class UserMessage
    {
        public int Id { get; set; }
        public int SendUserId { get; set; }
        public int ReceiveUserId { get; set; }
        public System.DateTime SendTime { get; set; }
        public string Countent { get; set; }
        public string Title { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentUrl { get; set; }
        public bool IsRead { get; set; }
        public bool Status { get; set; }
        public bool SendMessageIsDel { get; set; }
        public bool ReceiveMessageIsDel { get; set; }
        public bool UserMessageType { get; set; }
        public int InquiryId { get; set; }
        public string contents { get; set; }
        public Nullable<int> senduid { get; set; }
        public Nullable<int> receiveuid { get; set; }
        public Nullable<bool> send_isdel { get; set; }
        public Nullable<bool> Rece_isdel { get; set; }
        public Nullable<System.DateTime> ctime { get; set; }
        public byte ProjectType { get; set; }
    }
}
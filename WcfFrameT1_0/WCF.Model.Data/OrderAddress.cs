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
    
    public partial class OrderAddress
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> Privnce { get; set; }
        public Nullable<int> City { get; set; }
        public Nullable<int> County { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Receive { get; set; }
        public Nullable<System.DateTime> Up_time { get; set; }
        public string ZipCode { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public string AddressName { get; set; }
        public Nullable<byte> AddressType { get; set; }
        public string Coordinate { get; set; }
        public bool IsPayAfterReceive { get; set; }
        public bool IsSendBeforeTelConfirm { get; set; }
        public byte ReceiveTimeType { get; set; }
    }
}
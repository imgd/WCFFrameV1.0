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
    
    public partial class Consult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Businessid { get; set; }
        public string Quescontent { get; set; }
        public string Anscontent { get; set; }
        public int status { get; set; }
        public Nullable<System.DateTime> AppLnvTime { get; set; }
        public System.DateTime Addtime { get; set; }
        public byte Is_New { get; set; }
        public Nullable<byte> ConType { get; set; }
        public Nullable<byte> SakType { get; set; }
        public Nullable<int> ProductId { get; set; }
    }
}
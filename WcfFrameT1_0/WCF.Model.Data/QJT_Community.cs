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
    
    public partial class QJT_Community
    {
        public int Id { get; set; }
        public string CommunityName { get; set; }
        public short ProvinceId { get; set; }
        public short CityId { get; set; }
        public Nullable<short> CountyId { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public string CountyName { get; set; }
        public string Address { get; set; }
        public double CommunityLng { get; set; }
        public double CommunityLat { get; set; }
        public byte Status { get; set; }
    }
}
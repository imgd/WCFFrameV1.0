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
    
    public partial class HelpCenter
    {
        public int id { get; set; }
        public string title { get; set; }
        public int parents { get; set; }
        public byte depth { get; set; }
        public int sort { get; set; }
        public byte status { get; set; }
        public string url { get; set; }
        public string contents { get; set; }
    }
}
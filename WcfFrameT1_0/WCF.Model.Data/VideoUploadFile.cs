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
    
    public partial class VideoUploadFile
    {
        public int Fid { get; set; }
        public Nullable<int> Uid { get; set; }
        public string FileNameLocal { get; set; }
        public string FileNameRemote { get; set; }
        public string FilePathLocal { get; set; }
        public string FilePathRemote { get; set; }
        public string FilePathRelative { get; set; }
        public string FileMD5 { get; set; }
        public string FileLength { get; set; }
        public string FileSize { get; set; }
        public string FilePos { get; set; }
        public string PostedLength { get; set; }
        public string PostedPercent { get; set; }
        public Nullable<bool> PostComplete { get; set; }
        public Nullable<System.DateTime> PostedTime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string FileSubmitId { get; set; }
        public Nullable<int> Chunks { get; set; }
        public Nullable<int> Chunk { get; set; }
    }
}
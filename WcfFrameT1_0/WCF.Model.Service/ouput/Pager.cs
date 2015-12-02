using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCF.Model.Service.ouput
{
    /// <summary>
    /// 分页格式化
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class Pager<T>
    {
        public Pager() { }
        public Pager(int dataCount, List<T> data, int pageSize = 10, int pageIndex = 1)
        {
            Set(dataCount, data, pageSize, pageIndex);
        }
        
        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="dataCount"></param>
        /// <param name="data"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        private void Set(int dataCount, List<T> data, int pageSize = 10, int pageIndex = 1)
        {
            this.pageSize = pageSize;
            this.pageIndex = pageIndex;
            this.ListData = data;
            this.DataCount = dataCount;
            this.PageCount = (int)Math.Ceiling((double)dataCount / (double)pageSize);
        }
        public void SetData(int dataCount, List<T> data, int pageSize = 10, int pageIndex = 1)
        {
            Set(dataCount, data, pageSize, pageIndex);
        }

        /// <summary>
        /// 当前页
        /// </summary>
        private int pageIndex;
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value <= 0 ? 1 : value; }
        }
        /// <summary>
        /// 每页条数
        /// </summary>
        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value <= 0 ? 10 : value; }
        }
        /// <summary>
        /// 总条数
        /// </summary>
        public int DataCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 泛型数据
        /// </summary>
        public List<T> ListData { get; set; }

    }
}

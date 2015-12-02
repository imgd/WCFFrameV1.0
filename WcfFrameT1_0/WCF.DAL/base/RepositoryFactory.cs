using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF.IDAL;

namespace WCF.DAL
{
    public class RepositoryFactory
    {
        public static IAreaDataRespository AreaDataRepository
        {
            get { return new AreaDataRepository(); }
        }
        public static IOrderInfoRespository OrderInfoRepository
        {
            get { return new OrderInfoRepository(); }
        }
    }
}

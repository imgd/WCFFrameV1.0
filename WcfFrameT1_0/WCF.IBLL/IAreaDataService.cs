using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF.Model.Data;

namespace WCF.IBLL
{
    public interface IAreaDataService : IBaseService<AreaData>
    {
        List<AreaData> GetTopAreaData(int parentId);
        List<AreaData> GetPagerAreaData(int pageSize, int pageIndex, out int dataCount);
    }
}

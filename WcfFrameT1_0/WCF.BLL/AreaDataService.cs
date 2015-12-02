using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF.DAL;
using WCF.IBLL;
using WCF.IDAL;
using WCF.Model.Data;

namespace WCF.BLL
{
    public class AreaDataService : BaseService<AreaData>, IAreaDataService
    {
        private IAreaDataRespository _areaDataDal;
        public override void SetCurrentRepository()
        {
            _areaDataDal = RepositoryFactory.AreaDataRepository;
        }

        /// <summary>
        /// 获取父级下的子菜单
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<AreaData> GetTopAreaData(int parentId)
        {   
            
            var item = _areaDataDal.curDbContext.Set<AreaData>();
            var data = (from c in item
                        where c.Parent_Id == parentId
                        select c).ToList();
            return data;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<AreaData> GetPagerAreaData(int pageSize, int pageIndex, out int dataCount)
        {            
            var data = _areaDataDal.LoadPageEntities<int>(
                pageIndex, pageSize,
            out dataCount,
            o => o.Area_Level == 2, false, a => a.Area_Id
            ).ToList();
            return data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Extensions.ConvertExtension;

namespace WCF.Common.Tools
{
    /// <summary>
    /// 概率抽奖 类
    /// </summary>
    public class LotteryDrawHelper
    {
        /// <summary>
        /// 抽奖的奖品概率信息集合
        /// byte 指的是中奖百分比数字 如30
        /// </summary>
        public Dictionary<string, byte> Prizes { private get; set; }
        /// <summary>
        /// 随机数生成器
        /// </summary>
        private Random ran { get; set; }
        /// <summary>
        /// 随机数最小值
        /// </summary>
        private int RandomMin { get; set; }
        /// <summary>
        /// 随机数最大值
        /// </summary>
        private int RandomMax { get; set; }

        /// <summary>
        /// 抽奖容器
        /// </summary>
        private Dictionary<string, ArrayList> Container { get; set; }

        /// <summary>
        /// 构造 随机范围1-1000
        /// </summary>
        public LotteryDrawHelper()
        {
            Prizes = new Dictionary<string, byte>();
            ran = new Random();
            RandomMin = 1;
            RandomMax = 1000;
            InitLotteryDrawContainer();
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="prizes">奖品及中奖几率集合 byte 指的是中奖百分比数字 如30</param>
        public LotteryDrawHelper(Dictionary<string, byte> prizes)
        {
            Prizes = prizes;
            ran = new Random();
            RandomMin = 1;
            RandomMax = 1000;
            InitLotteryDrawContainer();
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="prizes">奖品及中奖几率集合 byte 指的是中奖百分比数字 如30</param>
        /// <param name="randomMin">随机数范围最小值 必须大于0</param>
        /// <param name="randomMax">随机数范围最大值 </param>
        public LotteryDrawHelper(Dictionary<string, byte> prizes, int randomMin, int randomMax)
        {
            randomMin = randomMin <= 0 ? 1 : randomMin;
            Prizes = prizes;
            ran = new Random();
            RandomMin = randomMin;
            RandomMax = randomMax;
            InitLotteryDrawContainer();
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private int GetRadomNumber()
        {
            return ran.Next(RandomMin, RandomMax + 1);
        }
        /// <summary>
        /// 是否在范围区间
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsContains(int min, int max, int value)
        {
            return (value >= min && value <= max);
        }

        /// <summary>
        /// 获取中奖范围
        /// </summary>
        /// <returns></returns>
        private ArrayList CreateTypeRange(int odds, int lastMax)
        {
            if (odds == 0)
            {
                return new ArrayList { 0, 0 };
            }

            int maxTop = (int)(RandomMax * ((float)odds / (float)100));
            return new ArrayList { lastMax + 1, maxTop + lastMax };
        }

        /// <summary>
        /// 初始化抽奖容器
        /// </summary>
        /// <returns></returns>
        private void InitLotteryDrawContainer()
        {
            if (!Prizes.IsNull() && Prizes.Count > 0)
            {
                int lastMax = 0;
                Container = new Dictionary<string, ArrayList>();
                foreach (KeyValuePair<string, byte> ld in Prizes)
                {
                    ArrayList typeRange = CreateTypeRange(ld.Value, lastMax);
                    Container.Add(ld.Key, typeRange);
                    lastMax = (int)typeRange[1];
                }
            }
        }

        /// <summary>
        /// 抽奖
        /// </summary>
        /// <returns></returns>
        public string LotteryDrawRun()
        {
            if (!Container.IsNull() && Container.Count > 0)
            {
                int ranNumber = GetRadomNumber();
                foreach (KeyValuePair<string, ArrayList> lp in Container)
                {
                    //中奖
                    if (IsContains((int)lp.Value[0], (int)lp.Value[1], ranNumber))
                    {
                        return lp.Key;
                    }
                }
            }

            return null;
        }
    }
}

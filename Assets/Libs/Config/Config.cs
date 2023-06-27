using System.Collections.Generic;

namespace Assets.Libs.Config
{
    using Assets.Scripts.Module.Snake;

    using MessagePack;

    /// <summary>
    /// 配置
    /// </summary>
    [MessagePackObject()]
    public class Config
    {
        /// <summary>
        /// 全局设置
        /// </summary>
        [Key(0)]
        public GlobalSetting globalSetting = GlobalSetting.instance;

        /// <summary>
        /// 游戏记录
        /// </summary>
        [Key(1)]
        public List<Record> records = new List<Record>();

        /// <summary>
        /// 应用设置
        /// </summary>
        public void ApplySetting()
        {
            GlobalSetting.instance.Apply(this.globalSetting);
            Snake.instance.config.records = this.records;
        }

        /// <summary>
        /// 获取最高分
        /// </summary>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public float GetHightestScore() 
        {
            float maxValue = 0;
            foreach (var record in this.records)
            {
                if (record.score > maxValue)
                {
                    maxValue = record.score;
                }
            }

            return maxValue;
        }
    }
}

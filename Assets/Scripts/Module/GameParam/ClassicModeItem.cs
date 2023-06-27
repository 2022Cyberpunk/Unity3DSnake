
namespace Assets.Scripts.Module.GameParam
{
    using Assets.Scripts.Base;

    /// <summary>
    /// 经典模式
    /// </summary>
    public class ClassicModeItem:Singleton<ClassicModeItem>
    {
        /// <summary>
        /// 历史最高分
        /// </summary>
        public float highestScore;

        /// <summary>
        /// 当前得分
        /// </summary>
        public float curScore;  

        /// <summary>
        /// 当前速度
        /// </summary>
        public float curSpeed;

        /// <summary>
        /// 得分加成
        /// </summary>
        public float scoreBonus;

        /// <summary>
        /// 当前长度
        /// </summary>
        public int curLength;

        public void InitValue()
        {
            curScore = 0;
            curSpeed = 0;
            scoreBonus = 0;
            curLength = 0;
        }

        /// <summary>
        /// 清零重新开始
        /// </summary>
        public void Clear()
        {
            curScore = 0;
            curSpeed = 0;
            scoreBonus = 0;
            curLength = 0;
        }
    }
}

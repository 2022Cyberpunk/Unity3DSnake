namespace Assets.Libs.Config
{
    using MessagePack;

    /// <summary>
    /// 游戏记录
    /// </summary>
    [MessagePackObject()]
    public class Record
    {
        /// <summary>
        /// 模式
        /// </summary>
        [Key(0)]
        public string playMode;

        /// <summary>
        /// 分数
        /// </summary>
        [Key(1)]
        public float score;

        /// <summary>
        /// 长度
        /// </summary>
        [Key(2)]
        public float length;

        /// <summary>
        /// 单局时长
        /// </summary>
        [Key(3)]
        public string duration;

        /// <summary>
        /// 时间
        /// </summary>
        [Key(4)]
        public string timeOver; 
    }
}

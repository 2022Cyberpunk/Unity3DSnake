namespace Assets.Scripts.EventManager
{
    /// <summary>
    /// 配置事件
    /// </summary>
    public class ConfigEvent
    {
        /// <summary>
        /// 游戏暂停
        /// </summary>
        public const string kGamePauseChange = "kGamePauseChange";

        /// <summary>
        /// 继续游戏
        /// </summary>  
        public const string kGameContinueChange = "kGameContinueChange";

        /// <summary>
        /// 当前速度
        /// </summary>  
        public const string kCurSpeedChange = "kCurSpeedChange";

        /// <summary>
        /// 蛇身长度
        /// </summary>  
        public const string kLengthChange = "kLengthChange";

        /// <summary>
        /// 语言改变
        /// </summary>  
        public const string kSystemLanguageChange = "kSystemLanguageChange";

        /// <summary>
        /// 主题改变
        /// </summary>  
        public const string kSystemThemeChange = "SystemThemeChange";

        /// <summary>
        /// 小蛇皮肤改变
        /// </summary>  
        public const string kSnakeSkinChange = "SnakeSkinChange";

        /// <summary>
        /// 重新开始
        /// </summary>  
        public const string kRestartChange = "RestartChange";

        /// <summary>
        /// 游戏模式改变
        /// </summary>  
        public const string kPlayModeChange = "PlayModeChange";
    }
}

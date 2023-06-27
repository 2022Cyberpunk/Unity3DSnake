namespace Assets.Libs.Config
{
    using Assets.Libs.Enum;
    using Assets.Scripts.Base;
    using Assets.Scripts.EventManager;

    using MessagePack;

    /// <summary>
    /// 全局设置
    /// </summary>
    [MessagePackObject()]
    public class GlobalSetting : Singleton<GlobalSetting>
    {
        /// <summary>
        /// 当前语言
        /// </summary>
        [Key(0)]
        public Language curLanguage;

        /// <summary>
        /// 当前模式
        /// </summary>
        [Key(1)]
        public PlayMode curPlayMode;

        /// <summary>
        /// 当前主题
        /// </summary>
        [Key(2)]
        public Theme curTheme;

        /// <summary>
        /// 当前食物图集
        /// </summary>
        [Key(3)]
        public FoodSprite curFoodSprite;

        /// <summary>
        /// 当前小蛇皮肤图集
        /// </summary>
        [Key(4)]
        public SnakeSprite curSnakeSprite;

        /// <summary>
        /// 当前字体
        /// </summary>  
        [Key(5)]
        public FontStyle curFontStyle;

        /// <summary>
        /// 初始化配置
        /// </summary>
        public void Init()
        {
            curLanguage = Language.Ch;
            curPlayMode = PlayMode.Classic;
            curTheme = Theme.Light;
            curFoodSprite = FoodSprite.IceCream;
            curSnakeSprite = SnakeSprite.BlueSkin;
            curFontStyle = FontStyle.YueYuan;
        }

        /// <summary>
        /// 应用设置
        /// </summary>
        /// <param name="globalSetting">
        /// The global setting.
        /// </param>
        public void Apply(GlobalSetting globalSetting)
        {
            curLanguage = globalSetting.curLanguage;
            curPlayMode = globalSetting.curPlayMode;
            curTheme = globalSetting.curTheme;
            curFoodSprite = globalSetting.curFoodSprite;
            curSnakeSprite = globalSetting.curSnakeSprite;
            curFontStyle = globalSetting.curFontStyle;
            EventDispatcher.TriggerEvent(ConfigEvent.kPlayModeChange);
            EventDispatcher.TriggerEvent(ConfigEvent.kSnakeSkinChange);
            EventDispatcher.TriggerEvent(ConfigEvent.kSystemLanguageChange);
            EventDispatcher.TriggerEvent(ConfigEvent.kSystemThemeChange);
        }
    }
}

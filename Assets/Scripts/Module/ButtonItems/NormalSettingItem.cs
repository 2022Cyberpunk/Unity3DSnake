namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.ToggleItems;
    using Assets.Scripts.ThemeModel;
    using Assets.Scripts.View.GameSetting;

    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 常规设置
    /// </summary>
    public class NormalSettingItem : ButtonItem
    {
        /// <summary>
        /// 语言设置
        /// </summary>
        private LanguageSetting m_LanguageSetting;

        /// <summary>
        /// 主题设置
        /// </summary>
        private ThemeSetting m_ThemeSetting;

        /// <summary>
        /// 食物设置
        /// </summary>
        private FoodSetting m_FoodSetting;

        /// <summary>
        /// 小蛇皮肤设置
        /// </summary>
        private SnakeSkinSetting m_SnakeSkinSetting;

        /// <summary>
        /// 游戏模式设置
        /// </summary>
        private PlayModeSetting m_PlayModeSetting;

        /// <summary>
        /// The m_ image.
        /// </summary>
        private Image m_LeftImage;  

        /// <summary>
        /// 按钮节点名称
        /// </summary>
        internal override string name => "NormalSettingBtn";

        /// <summary>
        /// 字典key值
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kNormalSetting;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parenTransform">
        /// The paren transform.
        /// </param>
        public override void Init(Transform parenTransform)
        {
            base.Init(parenTransform.transform.Find("Left"));

            this.m_LeftImage = parenTransform.Find("Left").GetComponent<Image>();
            this.m_LeftImage.color = ThemeModel.instance.GetColorValue(ColorKey.kLeftBackGround);

            m_LanguageSetting = new LanguageSetting();
            this.m_LanguageSetting.Init(parenTransform.Find("Right"));

            m_ThemeSetting = new ThemeSetting();
            this.m_ThemeSetting.Init(parenTransform.Find("Right"));

            m_FoodSetting = new FoodSetting();
            m_FoodSetting.Init(parenTransform.Find("Right"));

            m_SnakeSkinSetting = new SnakeSkinSetting();
            m_SnakeSkinSetting.Init(parenTransform.Find("Right"));

            m_PlayModeSetting = new PlayModeSetting();
            m_PlayModeSetting.Init(parenTransform.Find("Right"));

            this.AddEvent();
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void ShowItems()     
        {
            this.m_LanguageSetting.Show();
            this.m_ThemeSetting.Show();
            m_FoodSetting.Show();
            m_SnakeSkinSetting.Show();
            m_PlayModeSetting.Show();
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void HideItems() 
        {
            m_LanguageSetting.Hide();
            m_ThemeSetting.Hide();
            m_FoodSetting.Hide();
            m_SnakeSkinSetting.Hide();
            m_PlayModeSetting.Hide();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        public override void AddEvent() 
        {
            EventDispatcher.AddEventListener(ConfigEvent.kSystemThemeChange, this.OnSystemThemeChange);
        }

        /// <summary>
        /// 按钮点击
        /// </summary>
        internal override void BtnClick()
        {
            GameSettingView.instance.advancedSettingItem.HideItems();
            GameSettingView.instance.historyRecordItem.HideItems();
            this.ShowItems();
        }

        /// <summary>
        /// 主题改变调用
        /// </summary>
        private void OnSystemThemeChange()
        {
            this.m_LeftImage.color = ThemeModel.instance.GetColorValue(ColorKey.kLeftBackGround);
        }
    }
}

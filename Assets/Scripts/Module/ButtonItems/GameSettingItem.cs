namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;
    using Assets.Scripts.ThemeModel;
    using Assets.Scripts.View.GameSetting;
    using Assets.Scripts.View.HomeView;

    using UnityEngine;

    /// <summary>
    /// 游戏设置按钮
    /// </summary>
    public class GameSettingItem : ButtonItem   
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "GameSettingBtn";

        /// <summary>
        /// 字典key值
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kSetting;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parenTransform">
        /// 父节点
        /// </param>
        public override void Init(Transform parenTransform)
        {
            base.Init(parenTransform);
            m_Text.color = ThemeModel.instance.GetColorValue(ColorKey.kHomeText);
            m_Image.color = ThemeModel.instance.GetColorValue(ColorKey.kHomeButton);
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        public override void AddEvent()
        {
            base.AddEvent();
            EventDispatcher.AddEventListener(ConfigEvent.kSystemThemeChange, this.OnSystemThemeChange);
        }

        /// <summary>
        /// 按钮点击
        /// </summary>
        internal override void BtnClick()
        {
            HomeView.instance.Close();
            GameSettingView.instance.Open();
        }

        /// <summary>
        /// 主题改变
        /// </summary>
        private void OnSystemThemeChange()
        {
            m_Text.color = ThemeModel.instance.GetColorValue(ColorKey.kHomeText);
            m_Image.color = ThemeModel.instance.GetColorValue(ColorKey.kHomeButton);
        }
    }
}

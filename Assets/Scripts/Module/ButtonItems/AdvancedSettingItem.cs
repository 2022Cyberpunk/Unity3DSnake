namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.TextItems;
    using Assets.Scripts.ThemeModel;
    using Assets.Scripts.View.GameSetting;

    using UnityEngine;

    /// <summary>
    /// 高级设置
    /// </summary>
    public class AdvancedSettingItem : ButtonItem
    {
        /// <summary>
        /// 敬请期待文本
        /// </summary>
        private ExpectItem m_ExpectItem;

        /// <summary>
        /// 节点name.
        /// </summary>
        internal override string name => "AdvancedSettingBtn";

        /// <summary>
        /// 标题文本key值
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kAdvancedSetting;

        /// <summary>
        /// 初始化 
        /// </summary>
        /// <param name="parenTransform">
        /// 父节点
        /// </param>
        public override void Init(Transform parenTransform)
        {
            base.Init(parenTransform.transform.Find("Left"));
            m_ExpectItem = new ExpectItem();
            this.m_ExpectItem.Init(parenTransform.Find("Right"));
            m_Image.color = ThemeModel.instance.GetColorValue(ColorKey.kLeftButton);
            this.AddEvent();
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void ShowItems()
        {
            m_ExpectItem.Show();
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void HideItems()
        {
            m_ExpectItem.Hide();
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
            GameSettingView.instance.normalSettingItem.HideItems();
            GameSettingView.instance.historyRecordItem.HideItems();
            m_ExpectItem.Show();
        }

        /// <summary>
        /// The on system theme change.
        /// </summary>
        private void OnSystemThemeChange()
        {
            m_Image.color = ThemeModel.instance.GetColorValue(ColorKey.kLeftButton);
        }
    }
}

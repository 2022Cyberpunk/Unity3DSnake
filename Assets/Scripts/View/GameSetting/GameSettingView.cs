namespace Assets.Scripts.View.GameSetting
{
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Module.ButtonItems;
    using Assets.Scripts.ThemeModel;
    using Assets.Scripts.View.BaseView;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 游戏设置视图
    /// </summary>
    public class GameSettingView : BaseView<GameSettingView>
    {
        /// <summary>
        /// 常规设置
        /// </summary>
        public NormalSettingItem normalSettingItem;

        /// <summary>
        /// 高级设置
        /// </summary>
        public AdvancedSettingItem advancedSettingItem;

        /// <summary>
        /// 历史记录
        /// </summary>
        public HistoryRecordItem historyRecordItem;

        /// <summary>
        /// 返回主界面按钮
        /// </summary>
        private SettingReturnHome m_SettingReturnHome;

        /// <summary>
        /// The m_ image.
        /// </summary>
        private Image m_Image;

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="parenTransform">
        /// The paren transform.
        /// </param>
        public void Init(Transform parenTransform)
        {
            m_Parent = parenTransform;
            this.normalSettingItem = new NormalSettingItem();
            this.normalSettingItem.Init(m_Parent.Find("NormalSetting"));

            this.advancedSettingItem = new AdvancedSettingItem();
            this.advancedSettingItem.Init(m_Parent.Find("AdvancedSetting"));

            this.historyRecordItem = new HistoryRecordItem();
            this.historyRecordItem.Init(m_Parent.Find("HistoryRecord"));

            m_Image = m_Parent.transform.Find("BG").GetComponent<Image>();
            m_Image.color = ThemeModel.instance.GetColorValue(ColorKey.kBackGround);

            m_SettingReturnHome = new SettingReturnHome();
            m_SettingReturnHome.Init(m_Parent);

            this.AddEvent();
        }

        /// <summary>
        /// The open.
        /// </summary>
        public override void Open()
        {
            base.Open();
            this.normalSettingItem.ShowItems();
        }

        /// <summary>
        /// The close.
        /// </summary>
        public override void Close()
        {
            base.Close();
            this.normalSettingItem.HideItems();
            this.advancedSettingItem.HideItems();
            this.historyRecordItem.HideItems();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        internal void AddEvent()
        {
            EventDispatcher.AddEventListener(ConfigEvent.kSystemThemeChange, OnSystemThemeChange);
        }

        /// <summary>
        /// The on system theme change.
        /// </summary>
        private void OnSystemThemeChange()
        {
            m_Image.color = ThemeModel.instance.GetColorValue(ColorKey.kBackGround);
        }
    }
}

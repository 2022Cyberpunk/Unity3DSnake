namespace Assets.Scripts.View.GameCaption
{
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Module.ButtonItems;
    using Assets.Scripts.ThemeModel;
    using Assets.Scripts.View.BaseView;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 游戏说明视图
    /// </summary>
    public class GameCaptionView : BaseView<GameCaptionView>
    {
        /// <summary>
        /// The m_ announcement item.
        /// </summary>
        private AnnouncementItem m_AnnouncementItem;

        /// <summary>
        /// The m_ introduction item.
        /// </summary>
        private IntroductionItem m_IntroductionItem;

        /// <summary>
        /// 返回主界面按钮
        /// </summary>
        private AttentionReturnHome m_AttentionReturnHome;

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
            this.m_Parent = parenTransform;
            m_AnnouncementItem = new AnnouncementItem();
            this.m_AnnouncementItem.Init(m_Parent.Find("Announcement"));

            this.m_IntroductionItem = new IntroductionItem();
            m_IntroductionItem.Init(m_Parent.Find("Introduction"));

            m_Image = m_Parent.transform.Find("BG").GetComponent<Image>();
            m_Image.color = ThemeModel.instance.GetColorValue(ColorKey.kBackGround);

            m_AttentionReturnHome = new AttentionReturnHome();
            m_AttentionReturnHome.Init(m_Parent);
            this.AddEvent();
        }

        /// <summary>
        /// The hide all.
        /// </summary>
        public void HideAll()
        {
            m_IntroductionItem.HideItems();
            m_AnnouncementItem.HideItems();
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

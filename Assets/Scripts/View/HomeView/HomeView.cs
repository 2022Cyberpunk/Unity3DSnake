namespace Assets.Scripts.View.HomeView
{
    using Assets.Scripts.Module.ButtonItems;
    using Assets.Scripts.View.BaseView;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 主页视图
    /// </summary>
    public class HomeView : BaseView<HomeView>
    {
        /// <summary>
        /// 游戏开始按钮
        /// </summary>
        private GameStartItem m_GameStartItem;

        /// <summary>
        /// 游戏设置按钮
        /// </summary>
        private GameSettingItem m_GameSettingBtn;

        /// <summary>
        /// 游戏说明按钮
        /// </summary>
        private GameCaptionItem m_GameCaptionBtn;

        /// <summary>
        /// 游戏主界面音频
        /// </summary>
        private AudioSource m_AudioSource;

        /// <summary>
        /// 背景图片
        /// </summary>
        private Image m_Image;

        /// <summary>
        /// 视图初始化
        /// </summary>
        /// <param name="parenTransform">
        /// The paren transform.
        /// </param>
        public void Init(Transform parenTransform)
        {
            m_Parent = parenTransform;
            m_AudioSource = m_Parent.transform.Find("Audio").GetComponent<AudioSource>();
            m_AudioSource.clip = Resources.Load<AudioClip>("Audios/Main");
            m_AudioSource.Play();
            m_AudioSource.loop = true;
            m_GameStartItem = new GameStartItem();
            m_GameStartItem.Init(parenTransform.Find("Center"));
            m_GameSettingBtn = new GameSettingItem();
            m_GameSettingBtn.Init(m_Parent.Find("Center"));
            m_GameCaptionBtn = new GameCaptionItem();
            m_GameCaptionBtn.Init(this.m_Parent.Find("Center"));

            this.AddEvent();
            Open();
        }

        /// <summary>
        /// The close.
        /// </summary>
        public override void Close()
        {
            base.Close();
            this.m_AudioSource.Stop();
        }

        /// <summary>
        /// The add event.
        /// </summary>
        public void AddEvent()
        {
            //EventDispatcher.AddEventListener(ConfigEvent.kSystemThemeChange, OnSystemThemeChange);
        }
    }
}

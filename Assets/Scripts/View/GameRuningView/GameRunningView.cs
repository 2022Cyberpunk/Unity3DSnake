namespace Assets.Scripts.View.GameRuningView
{
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Module.ButtonItems;
    using Assets.Scripts.Module.Snake;
    using Assets.Scripts.Module.TextItems;
    using Assets.Scripts.ThemeModel;
    using Assets.Scripts.View.BaseView;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 游戏运行视图
    /// </summary>
    public class GameRunningView : BaseView<GameRunningView>
    {
        /// <summary>
        /// 当前模式
        /// </summary>
        private CurrentModeItem m_CurrentModeItem;

        /// <summary>
        /// 历史最高分
        /// </summary>
        private HisHighestScoreItem m_HighestScoreItem;

        /// <summary>
        /// 当前得分
        /// </summary>
        private CurrentScoreItem m_CurrentScoreItem;

        /// <summary>
        /// 当前速度
        /// </summary>
        private CurrentSpeedItem m_CurrentSpeedItem;

        /// <summary>
        /// 当前长度    
        /// </summary>
        private CurrentLengthItem m_CurrentLengthItem;

        /// <summary>
        /// 得分加成
        /// </summary>
        private ScoreBonusItem m_ScoreBonusItem;

        /// <summary>
        /// 单局时长
        /// </summary>
        private DurationItem m_DurationItem;

        /// <summary>
        /// 暂停游戏
        /// </summary>
        private GamePauseItem m_GamePauseItem;

        /// <summary>
        /// 继续游戏
        /// </summary>
        private GameContinueItem m_GameContinueItem;

        /// <summary>
        /// 返回主界面
        /// </summary>
        private ReturnHomeItem m_ReturnHomeItem;

        /// <summary>
        /// The m_ image.
        /// </summary>
        private Image m_BackGroundImage;

        /// <summary>
        /// The m_ image.
        /// </summary>
        private Image m_LeftImage;

        /// <summary>
        /// 碰撞死亡音频
        /// </summary>
        public AudioSource bgAudioSource;       

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="parenTransform">
        /// The paren transform.
        /// </param>
        public void Init(Transform parenTransform)
        {
            m_Parent = parenTransform;
            var name = "Left";
            m_CurrentModeItem = new CurrentModeItem();
            m_CurrentModeItem.Init(m_Parent.Find(name));

            m_HighestScoreItem = new HisHighestScoreItem();
            m_HighestScoreItem.Init(m_Parent.Find(name));

            m_CurrentScoreItem = new CurrentScoreItem();
            m_CurrentScoreItem.Init(m_Parent.Find(name));

            m_CurrentSpeedItem = new CurrentSpeedItem();
            m_CurrentSpeedItem.Init(m_Parent.Find(name));

            m_CurrentLengthItem = new CurrentLengthItem();
            m_CurrentLengthItem.Init(m_Parent.Find(name));

            this.m_ScoreBonusItem = new ScoreBonusItem();
            this.m_ScoreBonusItem.Init(m_Parent.Find(name));

            this.m_DurationItem = new DurationItem();
            this.m_DurationItem.Init(m_Parent.Find(name));

            m_GamePauseItem = new GamePauseItem();
            m_GamePauseItem.Init(m_Parent.Find(name));

            m_GameContinueItem = new GameContinueItem();
            m_GameContinueItem.Init(m_Parent.Find(name));

            m_ReturnHomeItem = new ReturnHomeItem();
            m_ReturnHomeItem.Init(m_Parent.Find(name));

            this.bgAudioSource = m_Parent.transform.Find("BG").GetComponent<AudioSource>();
            this.bgAudioSource.clip = Resources.Load<AudioClip>("Audios/miracle");
            this.bgAudioSource.Play();
            this.bgAudioSource.loop = true;

            Snake.instance.Init(this.m_Parent.Find("Right/Snake"));
            FoodMaker.instance.Init(this.m_Parent.Find("Right/MakeFood"));

            this.m_BackGroundImage = m_Parent.transform.Find("BG").GetComponent<Image>();
            m_LeftImage = m_Parent.transform.Find(name).GetComponent<Image>();
            this.m_BackGroundImage.color = ThemeModel.instance.GetColorValue(ColorKey.kBackGround);
            m_LeftImage.color = ThemeModel.instance.GetColorValue(ColorKey.kLeftBackGround);
            this.InitValue();
            this.AddEvent();
        }

        /// <summary>
        /// 初始值
        /// </summary>
        public void InitValue()
        {
            FoodMaker.instance.InitValue();
        }

        /// <summary>
        /// The open.
        /// </summary>
        public override void Open()
        {
            base.Open();
            this.bgAudioSource.Play();
        }

        /// <summary>
        /// The close.
        /// </summary>
        public override void Close()
        {
            base.Close();
            this.bgAudioSource.Stop();
        }

        /// <summary>
        /// The update.
        /// </summary>
        public void Update()
        {
            Snake.instance.Update();
            UpdateValue();
        }

        /// <summary>
        /// The get play mode.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetPlayMode()
        {
            return m_CurrentModeItem.strValue;
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        internal void AddEvent()
        {
            EventDispatcher.AddEventListener(ConfigEvent.kSystemThemeChange, OnSystemThemeChange);
            EventDispatcher.AddEventListener(ConfigEvent.kLengthChange, this.UpdateValue);
        }

        /// <summary>
        /// The update value.
        /// </summary>
        private void UpdateValue()
        {
            m_CurrentScoreItem.UpdateValue();
            m_CurrentLengthItem.UpdateValue(); 
            m_CurrentSpeedItem.UpdateValue();
            m_ScoreBonusItem.UpdateValue();
            this.m_CurrentModeItem.UpdateValue();
            this.m_DurationItem.UpdateValue();
            this.m_HighestScoreItem.UpdateValue();
        }

        /// <summary>
        /// The on system theme change.
        /// </summary>
        private void OnSystemThemeChange()
        {
            this.m_BackGroundImage.color = ThemeModel.instance.GetColorValue(ColorKey.kBackGround);
            m_LeftImage.color = ThemeModel.instance.GetColorValue(ColorKey.kLeftBackGround);
        }
    }
}

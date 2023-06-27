namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Libs.Enum;
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;
    using Assets.Scripts.LogManager;
    using Assets.Scripts.Module.Snake;
    using Assets.Scripts.ThemeModel;
    using Assets.Scripts.View.GameRuningView;
    using Assets.Scripts.View.HomeView;
    using UnityEngine;

    /// <summary>
    /// 游戏开始按钮
    /// </summary>
    public class GameStartItem : ButtonItem
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "GameStartBtn";

        /// <summary>
        /// 字典key值
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kStart;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parenTransform">
        /// 父节点
        /// </param>
        public override void Init(Transform parenTransform)
        {
            base.Init(parenTransform);
            m_Text.fontSize = 45;
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
            GameRunningView.instance.Open();
            Snake.instance.Clear();
            Snake.instance.TimeStart();
            if (FoodMaker.instance.reward == null && FoodMaker.instance.food == null)
            {
                FoodMaker.instance.CreateFood(false);
            }

            EventDispatcher.TriggerEvent(ConfigEvent.kRestartChange);
            LogManager.instance.LogToFile(
                LanguageModel.instance.GetCurLanguageValue(LanguageKey.kStart),
                LogLevel.Information);
        }

        /// <summary>
        /// 主题改变调用
        /// </summary>
        private void OnSystemThemeChange()
        {
            m_Text.color = ThemeModel.instance.GetColorValue(ColorKey.kHomeText);
            m_Image.color = ThemeModel.instance.GetColorValue(ColorKey.kHomeButton);
        }
    }
}

namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.Snake;
    using Assets.Scripts.View.GameRuningView;
    using UnityEngine;

    /// <summary>
    /// 游戏开始按钮
    /// </summary>
    public class GameContinueItem : ButtonItem
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "ContinueBtn";

        /// <summary>
        /// 字典key值
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kContinueGame;

        /// <summary>
        /// 按钮点击
        /// </summary>
        internal override void BtnClick()
        {
            Snake.instance.Speed = 100;
            Snake.instance.IsStop = false;
            GameRunningView.instance.bgAudioSource.Play();
            EventDispatcher.TriggerEvent(ConfigEvent.kGameContinueChange);
        }
    }
}

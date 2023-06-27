namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.Snake;
    using Assets.Scripts.View.GameRuningView;

    /// <summary>
    /// 游戏开始按钮
    /// </summary>
    public class GamePauseItem : ButtonItem
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "PauseBtn";

        /// <summary>
        /// 字典key值
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kPause;

        /// <summary>
        /// 按钮点击
        /// </summary>
        internal override void BtnClick()
        {
            Snake.instance.Speed = 0;
            Snake.instance.IsStop = true;
            GameRunningView.instance.bgAudioSource.Stop();
            EventDispatcher.TriggerEvent(ConfigEvent.kGamePauseChange);
        }
    }
}

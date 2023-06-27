namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.GameParam;
    using Assets.Scripts.Module.Snake;
    using Assets.Scripts.View.GameRuningView;
    using Assets.Scripts.View.HomeView;

    /// <summary>
    /// 返回主界面
    /// </summary>
    public class ReturnHomeItem : ButtonItem
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "ReturnBtn";

        /// <summary>
        /// The title language key.
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kReturnHome;

        /// <summary>
        /// 按钮点击
        /// </summary>
        internal override void BtnClick()
        {
            GameRunningView.instance.Close();
            Snake.instance.Clear();
            Snake.instance.Record();
            FoodMaker.instance.Destroy();
            ClassicModeItem.instance.Clear();
            HomeView.instance.Open();
        }
    }
}

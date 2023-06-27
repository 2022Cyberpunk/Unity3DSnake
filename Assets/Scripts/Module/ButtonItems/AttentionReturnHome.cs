namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;
    using Assets.Scripts.View.GameCaption;
    using Assets.Scripts.View.HomeView;
    using UnityEngine;

    /// <summary>
    /// 公告返回主界面
    /// </summary>
    internal class AttentionReturnHome : ButtonItem 
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "ReturnBtn";

        /// <summary>
        /// 字典key值
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kReturn;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parenTransform">
        /// 父节点
        /// </param>
        public override void Init(Transform parenTransform)
        {
            base.Init(parenTransform);
            m_Text.fontSize = 30;
        }

        /// <summary>
        /// 按钮点击
        /// </summary>
        internal override void BtnClick()
        {
            GameCaptionView.instance.Close();
            HomeView.instance.Open();
        }
    }
}

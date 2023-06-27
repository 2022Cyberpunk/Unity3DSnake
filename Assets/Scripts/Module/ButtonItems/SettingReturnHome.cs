namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;
    using Assets.Scripts.View.GameSetting;
    using Assets.Scripts.View.HomeView;
    using UnityEngine;

    /// <summary>
    /// 设置返回主界面按钮
    /// </summary>
    internal class SettingReturnHome : ButtonItem
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
        /// The paren transform.
        /// </param>
        public override void Init(Transform parenTransform)
        {
            base.Init(parenTransform);
            m_Text.fontSize = 35;
        }

        /// <summary>
        /// 按钮点击
        /// </summary>
        internal override void BtnClick()
        {
            GameSettingView.instance.Close();
            HomeView.instance.Open();
        }
    }
}

namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.TextItems;
    using Assets.Scripts.Module.ToggleItems;
    using Assets.Scripts.View.GameCaption;

    using UnityEngine;

    /// <summary>
    /// 公告按钮
    /// </summary>
    public class IntroductionItem : ButtonItem      
    {
        /// <summary>
        /// 游戏介绍
        /// </summary>
        private Introduction m_Introduction;

        /// <summary>
        /// 按钮节点名称
        /// </summary>
        internal override string name => "IntroductionBtn";

        /// <summary>
        /// 字典key值
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kIntroduction;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parenTransform">
        /// The paren transform.
        /// </param>
        public override void Init(Transform parenTransform)
        {
            base.Init(parenTransform.transform.Find("Left"));
            this.m_Text.fontSize = 40;

            m_Introduction = new Introduction();
            this.m_Introduction.Init(parenTransform.Find("Right/ScrollView/Viewport"));
            this.Show();
            this.HideItems();
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void ShowItems()     
        {
            this.m_Introduction.Show();
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void HideItems() 
        {
            this.m_Introduction.Hide();
        }

        /// <summary>
        /// The btn click.
        /// </summary>
        internal override void BtnClick()
        {
            GameCaptionView.instance.HideAll();
            this.m_Introduction.Show();
        }
    }
}

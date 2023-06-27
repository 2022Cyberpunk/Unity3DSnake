using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Module.TextItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.Snake;

    /// <summary>
    /// 历史最高分
    /// </summary>
    public class HisHighestScoreItem : DoubleTextItem
    {
        /// <summary>
        /// The name.
        /// </summary>
        internal override string name => "HisHighestScore";

        /// <summary>
        /// Gets or sets the str value.
        /// </summary>
        internal override string strValue { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        internal override float value { get; set; }

        /// <summary>
        /// The title language key.
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kHighestScore;

        /// <summary>
        /// The update value.
        /// </summary>
        public override void UpdateValue()
        {
            base.UpdateValue();
            value = Snake.instance.config.GetHightestScore();
            this.m_ValueText.text = value.ToString();
        }
    }
}

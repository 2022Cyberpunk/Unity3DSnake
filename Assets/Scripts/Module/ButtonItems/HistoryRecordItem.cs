namespace Assets.Scripts.Module.ButtonItems
{
    using Assets.Scripts.Base.Param;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.Snake;
    using Assets.Scripts.Module.TextItems;
    using Assets.Scripts.View.GameSetting;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 历史记录
    /// </summary>
    public class HistoryRecordItem : ButtonItem 
    {
        /// <summary>
        /// 编号
        /// </summary>
        private TitleNumberItem m_TitleNumberItem;

        /// <summary>
        /// 模式
        /// </summary>
        private TitleModeItem m_TitleModeItem;

        /// <summary>
        /// 时间
        /// </summary>
        private TitleTimeItem m_TitleTimeItem;

        /// <summary>
        /// 分数
        /// </summary>
        private TitleScoreItem m_TitleScoreItem;

        /// <summary>
        /// 长度
        /// </summary>
        private TitleLengthItem m_TitleLengthItem;

        /// <summary>
        /// 时长
        /// </summary>
        private TitleDurationItem m_TitleDurationItem;

        /// <summary>
        /// The m_ record list parent.
        /// </summary>
        private Transform m_RecordListParent;

        /// <summary>
        /// 按钮节点名称
        /// </summary>
        internal override string name => "HistoryRecordBtn";

        /// <summary>
        /// 字典key值
        /// </summary>
        internal override int titleLanguageKey => LanguageKey.kHistoryRecord;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parenTransform">
        /// The paren transform.
        /// </param>
        public override void Init(Transform parenTransform)
        {
            base.Init(parenTransform.transform.Find("Left"));

            m_TitleNumberItem = new TitleNumberItem();
            m_TitleNumberItem.Init(parenTransform.Find("Right/Title"));

            m_TitleModeItem = new TitleModeItem();
            m_TitleModeItem.Init(parenTransform.Find("Right/Title"));

            m_TitleTimeItem = new TitleTimeItem();
            m_TitleTimeItem.Init(parenTransform.Find("Right/Title"));

            m_TitleScoreItem = new TitleScoreItem();
            m_TitleScoreItem.Init(parenTransform.Find("Right/Title"));

            m_TitleLengthItem = new TitleLengthItem();
            m_TitleLengthItem.Init(parenTransform.Find("Right/Title"));

            m_TitleDurationItem = new TitleDurationItem();
            m_TitleDurationItem.Init(parenTransform.Find("Right/Title"));

            m_RecordListParent = parenTransform.Find("Right/ScrollView");
            m_RecordListParent.gameObject.SetActive(false);

            AddEvent();
            HideItems();
            CreateList();
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void ShowItems()     
        {
            m_TitleNumberItem.Show();
            m_TitleModeItem.Show();
            m_TitleTimeItem.Show();
            m_TitleScoreItem.Show();
            m_TitleLengthItem.Show();
            m_TitleDurationItem.Show();
            m_RecordListParent.gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void HideItems() 
        {
            m_TitleNumberItem.Hide();
            m_TitleModeItem.Hide();
            m_TitleTimeItem.Hide();
            m_TitleScoreItem.Hide();
            m_TitleLengthItem.Hide();
            m_TitleDurationItem.Hide();
            m_RecordListParent.gameObject.SetActive(false);
        }

        /// <summary>
        /// 历史记录清单
        /// </summary>
        public void CreateList()
        {
            var count = Snake.instance.config.records.Count;
            var recordList = Snake.instance.config.records;
            var space = 110;
            for (int i = 0; i < count; i++)
            {
                var obj = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/RecordItem"));
                obj.transform.SetParent(m_RecordListParent.Find("Viewport/Content"));
                obj.transform.Find("Number").transform.Find("Text").GetComponent<Text>().text = (i + 1).ToString("F0");
                obj.transform.Find("Mode").transform.Find("Text").GetComponent<Text>().text = recordList[i].playMode;
                obj.transform.Find("Time").transform.Find("Text").GetComponent<Text>().text = recordList[i].timeOver;
                obj.transform.Find("Score").transform.Find("Text").GetComponent<Text>().text = recordList[i].score.ToString("F0");
                obj.transform.Find("Length").transform.Find("Text").GetComponent<Text>().text = recordList[i].length.ToString("F0");
                obj.transform.Find("Duration").transform.Find("Text").GetComponent<Text>().text =
                    recordList[i].duration;
                var pos = obj.transform.localPosition;
                obj.transform.localPosition = new Vector3(pos.x, pos.y - space, pos.z);
                obj.transform.localScale = Vector3.one;
                space += 110;
            }
        }

        /// <summary>
        /// 按钮点击
        /// </summary>
        internal override void BtnClick()
        {
            GameSettingView.instance.advancedSettingItem.HideItems();
            GameSettingView.instance.normalSettingItem.HideItems();
            this.ShowItems();
        }
    }
}

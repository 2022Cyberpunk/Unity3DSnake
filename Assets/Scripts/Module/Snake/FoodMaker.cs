namespace Assets.Scripts.Module.Snake
{
    using Assets.Scripts.Base;
    using Assets.Scripts.SpriteManager;

    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 食物生成类
    /// </summary>
    public class FoodMaker : Singleton<FoodMaker>
    {
        /// <summary>
        /// 食物
        /// </summary>
        public GameObject food;

        /// <summary>
        /// 食物图集
        /// </summary>
        public Sprite[] foodSprites = new Sprite[10];

        /// <summary>
        /// 奖励图集
        /// </summary>
        public Sprite rewardSprite;

        /// <summary>
        /// 奖励
        /// </summary>
        public GameObject reward;

        /// <summary>
        /// 食物容器
        /// </summary>
        private Transform m_FoodContainer;

        /// <summary>
        /// 食物X坐标
        /// </summary>
        private int m_X;

        /// <summary>
        /// 食物Y坐标
        /// </summary>
        private int m_Y;

        /// <summary>
        /// Y值范围
        /// </summary>
        private int m_YLimited; 

        /// <summary>
        /// X值范围
        /// </summary>
        private int m_XLimited; 

        /// <summary>
        /// 偏移
        /// </summary>
        private int m_XOffset;  

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parenTransform">
        /// The paren transform.
        /// </param>
        public void Init(Transform parenTransform)     
        {
            m_FoodContainer = parenTransform;

            //加载奖励图集
            rewardSprite = Resources.Load<Sprite>("Sprites/Food/IceCream/reward");
        }

        /// <summary>
        /// The init value.
        /// </summary>
        public void InitValue()
        {
            this.m_XLimited = 760;
            this.m_YLimited = 540;
            this.m_XOffset = 30;
            CreateFood(false);
        }

        /// <summary>
        /// 制作食物
        /// </summary>
        /// <param name="isReward">
        /// The isnoreward.
        /// </param>
        public void CreateFood(bool isReward)   
        {
            if (isReward)
            {
                this.m_X = Random.Range(-(this.m_XLimited - this.m_XOffset), this.m_XLimited - this.m_XOffset);
                this.m_Y = Random.Range(-(this.m_YLimited - this.m_XOffset), this.m_YLimited - this.m_XOffset);
                var rewardGameObject = Resources.Load<GameObject>("Prefab/Reward");
                reward = UnityEngine.Object.Instantiate(rewardGameObject);
                reward.transform.SetParent(m_FoodContainer);
                var rewardImage = this.reward.transform.GetComponent<Image>();
                rewardImage.enabled = true;
                rewardImage.sprite = this.rewardSprite;
                reward.transform.localScale = new Vector3(1, 1, 1);
                reward.transform.localPosition = new Vector3(this.m_X, this.m_Y, 0);
                reward.transform.GetComponent<BoxCollider2D>().enabled = true;
                reward.transform.GetComponent<BoxCollider2D>().isTrigger = true;

                //创建了奖励直接返回,不在创建食物
                return;
            }

            int index = Random.Range(0, 9);
            this.m_X = Random.Range(-(this.m_XLimited - this.m_XOffset), this.m_XLimited - this.m_XOffset);
            this.m_Y = Random.Range(-(this.m_YLimited - this.m_XOffset), this.m_YLimited - this.m_XOffset);
            var foodGameObject = Resources.Load<GameObject>("Prefab/EatFood");
            food = UnityEngine.Object.Instantiate(foodGameObject);
            food.transform.SetParent(m_FoodContainer);
            var image = food.transform.GetComponent<Image>();
            image.enabled = true;
            image.sprite = SpriteManager.instance.GetFoodSprite(index);
            food.transform.localScale = new Vector3(1, 1, 1);
            food.transform.localPosition = new Vector3(this.m_X, this.m_Y, 0);
            food.transform.GetComponent<BoxCollider2D>().enabled = true;
            food.transform.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        /// <summary>
        /// 立即销毁食物或奖励，重新开始后重新生成
        /// </summary>
        public void Destroy()
        {
            if (this.food != null)
            {
                UnityEngine.Object.Destroy(this.food.gameObject);
            }

            if (this.reward != null)
            {
                UnityEngine.Object.Destroy(this.reward.gameObject);
            }
        }
    }
}

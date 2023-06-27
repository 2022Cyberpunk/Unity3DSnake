using System.Collections.Generic;

namespace Assets.Scripts.Module.Snake
{
    using System;
    using System.Diagnostics;

    using Assets.Libs.Config;
    using Assets.MessageBox.Scripts;
    using Assets.Scripts.Base;
    using Assets.Scripts.EventManager;
    using Assets.Scripts.Language;
    using Assets.Scripts.Module.GameParam;
    using Assets.Scripts.SpriteManager;
    using Assets.Scripts.View.GameRuningView;

    using UnityEngine;
    using UnityEngine.UI;

    using PlayMode = Assets.Libs.Enum.PlayMode;
    using Random = UnityEngine.Random;

    /// <summary>
    /// 蛇类
    /// </summary>
    public class Snake : Singleton<Snake>
    {
        /// <summary>
        /// Y值范围
        /// </summary>
        public const int kYLimited = 760;

        /// <summary>
        /// X值范围
        /// </summary>
        public const int kXLimited = 540;

        /// <summary>
        /// 偏移
        /// </summary>
        public const int kXOffset = 50;

        /// <summary>
        /// The m_ parent.
        /// </summary>
        public Transform parent;

        /// <summary>
        /// The collider 2 d.
        /// </summary>
        public BoxCollider2D boxCollider2D;

        /// <summary>
        /// The head transform.
        /// </summary>
        public Transform headTransform;

        /// <summary>
        /// The image.
        /// </summary>
        public Image image;
            
        /// <summary>
        /// 蛇身体
        /// </summary>
        public List<Transform> bodyList = new List<Transform>();

        /// <summary>
        /// The body.   
        /// </summary>
        public Transform bodyContainer; 

        /// <summary>
        /// The body.
        /// </summary>
        public GameObject body;

        /// <summary>
        /// The config.
        /// </summary>
        public Config config;

        /// <summary>
        /// 历史记录
        /// </summary>
        public List<Record> records;

        /// <summary>
        /// The m_ move time.
        /// </summary>
        private float m_MoveTime;

        /// <summary>
        /// 是否吃到奖励
        /// </summary>
        private bool m_IsReward;

        /// <summary>   
        /// 蛇身图集
        /// </summary>
        private Sprite[] m_Sprites = new Sprite[2];

        /// <summary>
        /// 吃到食物音频
        /// </summary>
        private AudioSource m_EatAudioSource;

        /// <summary>
        /// 碰撞死亡音频
        /// </summary>
        private AudioSource m_DieAudioSource;

        /// <summary>
        /// 蛇头X坐标
        /// </summary>
        private float m_X;

        /// <summary>
        /// 蛇头Y坐标
        /// </summary>
        private float m_Y;

        /// <summary>   
        /// The m_ step.
        /// </summary>
        private float m_Step;

        /// <summary>
        /// 蛇头坐标.
        /// </summary>
        private Vector3 m_Head;

        /// <summary>
        /// 计时器
        /// </summary>
        private Stopwatch m_Stopwatch = new Stopwatch();

        /// <summary>
        /// 当前得分    
        /// </summary>
        public float score { get; set; }

        /// <summary>
        /// 得分加成    
        /// </summary>
        public float bonus { get; set; }

        /// <summary>
        /// Gets蛇的长度
        /// </summary>
        public int length { get; set; }

        /// <summary>
        /// Gets当前速度
        /// </summary>
        public float Speed { get; set; }


        /// <summary>
        /// Gets 是否停止   
        /// </summary>
        public bool IsStop { get; set; }

        /// <summary>
        /// The m_ time str.
        /// </summary>
        public  string TimeStr { get; set; }

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="parenTransform">
        /// The paren transform.
        /// </param>
        public void Init(Transform parenTransform)
        {
            parent = parenTransform;
            headTransform = parent.Find("Head").transform;
            headTransform.GetComponent<BoxCollider2D>().isTrigger = true;
            image = headTransform.GetComponent<Image>();
            image.sprite = SpriteManager.instance.GetSnakeSkinSprite(0);
            m_Sprites[0] = SpriteManager.instance.GetSnakeSkinSprite(1);
            m_Sprites[1] = SpriteManager.instance.GetSnakeSkinSprite(2);

            m_EatAudioSource = parent.Find("EatAudio").GetComponent<AudioSource>();
            m_EatAudioSource.clip = Resources.Load<AudioClip>("Audios/Success");
            m_EatAudioSource.playOnAwake = false;
            m_DieAudioSource = parent.Find("DieAudi").GetComponent<AudioSource>();
            m_DieAudioSource.clip = Resources.Load<AudioClip>("Audios/notification");
            m_DieAudioSource.playOnAwake = false;

            bodyContainer = parent.Find("Body").transform;
            boxCollider2D = headTransform.GetComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;

            this.config = new Config();
            this.records = new List<Record>(1000);

            InitValue();
            AddEvent();
        }

        /// <summary>
        /// 初始化值
        /// </summary>
        public void InitValue()
        {
            Speed = 100;
            m_Step = 1;
            score = 0;
            bonus = 10;
            m_X = 0;
            m_Y = m_Step;
            IsStop = false;
            m_MoveTime = 0;
            headTransform.localPosition = new Vector3(0, 0, 0);
            ClassicModeItem.instance.InitValue();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        public void AddEvent()
        {
            EventDispatcher.AddEventListener(ConfigEvent.kGamePauseChange, OnGamePauseChang);
            EventDispatcher.AddEventListener(ConfigEvent.kGameContinueChange, OnGameContinueChang);
            EventDispatcher.AddEventListener(ConfigEvent.kLengthChange, OnSnakeLengthChange);
            EventDispatcher.AddEventListener(ConfigEvent.kSnakeSkinChange, OnSnakeSkinChange);
        }

        /// <summary>
        /// 是否向上移动
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool MovingUp()
        {
            return this.m_Y > 0;
        }

        /// <summary>
        /// 是否向下移动
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool MovingDown()
        {
            return this.m_Y < 0;
        }

        /// <summary>
        /// 是否向左移动
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool MovingLeft()
        {
            return this.m_X < 0;
        }

        /// <summary>
        /// 是否向右移动
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool MovingRight()
        {
            return this.m_X > 0;
        }

        /// <summary>
        /// 每帧更新
        /// </summary>
        public void Update()
        {
            //没有打开界面则不更新
            if (!GameRunningView.instance.isOpen || this.IsStop)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (this.MovingDown())
                {
                    return;
                }

                headTransform.localRotation = Quaternion.Euler(0, 0, 0);
                m_Y = m_Step;
                m_X = 0;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (this.MovingRight())
                {
                    return;
                }

                headTransform.localRotation = Quaternion.Euler(0, 0, -90);
                m_Y = 0;
                m_X = -m_Step;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (this.MovingUp())
                {
                    return;
                }

                headTransform.localRotation = Quaternion.Euler(0, 0, 180);
                m_Y = -m_Step;
                m_X = 0;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (this.MovingLeft())
                {
                    return;
                }

                headTransform.localRotation = Quaternion.Euler(0, 0, 90);
                m_Y = 0;
                m_X = m_Step; 
            }

            if (Input.GetKey(KeyCode.Space))
            {
                this.Speed += 10f;
                if (Speed > 500)
                {
                    Speed = 500f;
                }
            }
            
            if (Input.GetKeyUp(KeyCode.Space))
            {
                this.Speed = 100;
            }

            //更新移动间隔时间
            this.m_MoveTime += Time.deltaTime;
            var time = 23 / this.Speed;
            if (this.m_MoveTime > time)
            {
                this.Move();
                this.m_MoveTime = 0;
            }

            this.UpdateValue();

            if (this.m_Stopwatch != null && m_Stopwatch.IsRunning)
            {
                TimeSpan elapsedTime = m_Stopwatch.Elapsed;
                string timeString = string.Format(
                    "{0:00}:{1:00}:{2:00}",
                    elapsedTime.Hours,
                    elapsedTime.Minutes,
                    elapsedTime.Seconds);
                TimeStr = timeString;
            }
        }

        /// <summary>
        /// 蛇的移动
        /// </summary>
        public void Move()
        {
            //记录蛇头位置
            m_Head = headTransform.localPosition;

            //蛇头要移动到的位置
            headTransform.localPosition = new Vector3(
                m_Head.x + (m_X * Speed * m_MoveTime),
                m_Head.y + (m_Y * Speed * m_MoveTime),
                0);

            if (bodyList.Count > 0)
            {
                for (int i = bodyList.Count - 2; i >= 0; i--)
                {
                    bodyList[i + 1].localPosition = bodyList[i].localPosition;
                }

                bodyList[0].localPosition = m_Head;
            }
        }

        /// <summary>
        /// 更新值
        /// </summary>
        public void UpdateValue()
        {
            ClassicModeItem.instance.curSpeed = this.Speed;
            ClassicModeItem.instance.curScore = this.score;
            EventDispatcher.TriggerEvent(ConfigEvent.kCurSpeedChange);
        }

        /// <summary>
        /// 碰撞触发函数
        /// </summary>
        /// <param name="collision">
        /// The collision.
        /// </param>
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("food"))
            {
                //吃到食物
                m_IsReward = false;

                //销毁碰撞到的食物
                UnityEngine.Object.Destroy(collision.gameObject);
                m_EatAudioSource.Play();

                //变长
                Grow();
                EventDispatcher.TriggerEvent(ConfigEvent.kLengthChange);

                //生成食物
                FoodMaker.instance.CreateFood(Random.Range(0, 100) < 20);
            }
            else if (collision.tag == "reward")
            {
                //吃到奖励
                m_IsReward = true;

                //销毁碰撞到的奖励
                UnityEngine.Object.Destroy(collision.gameObject);
                m_EatAudioSource.Play();

                //变长
                Grow();
                EventDispatcher.TriggerEvent(ConfigEvent.kLengthChange);

                //生成食物
                FoodMaker.instance.CreateFood(Random.Range(0, 100) < 20);
            }
            else if (collision.tag == "body")
            {
                //撞到自己身体
                m_IsReward = false;
                m_DieAudioSource.Play();
                TimeStop();
                OnGamePauseChang();
                Record();

                //由消息盒子实现弹窗
                var msg = LanguageModel.instance.GetCurLanguageValue(LanguageKey.kRestart);
                var btnArray = new[]
                              {
                                  LanguageModel.instance.GetCurLanguageValue(LanguageKey.kConfirm), 
                                  LanguageModel.instance.GetCurLanguageValue(LanguageKey.kReturn)
                              };
                MessageBox.Show(
                    msg,
                    btnArray,
                    RestartItem.Restart,
                    RestartItem.ReturnHome);

                //TODO 待实现死亡窗口，重新开始......
            }
            else
            {
                this.m_IsReward = false;
                var mode = GlobalSetting.instance.curPlayMode;
                var isBorder = collision.tag == "Left" ||
                              collision.tag == "Right" ||
                              collision.tag == "Top" || collision.tag == "Bottom";
                if (mode == PlayMode.Classic)
                {
                    if (isBorder)
                    {
                        m_DieAudioSource.Play();
                        this.TimeStop();
                        OnGamePauseChang();
                        this.Record();
                        var msg = LanguageModel.instance.GetCurLanguageValue(LanguageKey.kRestart);
                        var btnArray = new[]
                                           {
                                               LanguageModel.instance.GetCurLanguageValue(LanguageKey.kConfirm),
                                               LanguageModel.instance.GetCurLanguageValue(LanguageKey.kReturn)
                                           };
                        MessageBox.Show(
                            msg,
                            btnArray,
                            RestartItem.Restart,
                            RestartItem.ReturnHome);
                    }
                }
                else if (mode == PlayMode.Freedom)
                {
                    switch (collision.gameObject.name)
                    {
                        case "Top":
                            this.headTransform.localPosition = new Vector3(headTransform.localPosition.x, -headTransform.localPosition.y + 30, 0);
                            break;
                        case "Bottom":
                            headTransform.localPosition = new Vector3(headTransform.localPosition.x, -headTransform.localPosition.y - 30, 0);
                            break;
                        case "Left":
                            headTransform.localPosition = new Vector3(-headTransform.localPosition.x - 30, headTransform.localPosition.y, 0);
                            break;
                        case "Right":
                            headTransform.localPosition = new Vector3(-headTransform.localPosition.x + 30, headTransform.localPosition.y, 0);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 身体增加
        /// </summary>
        public void Grow()
        {
            body = Resources.Load<GameObject>("Prefab/SnakeBody");

            //实例化长的身体
            body = UnityEngine.Object.Instantiate(body, new Vector3(2000, 2000, 0), Quaternion.identity);
            body.transform.localScale = new Vector3(1, 1, 1);
            body.transform.GetComponent<BoxCollider2D>().isTrigger = true;
            body.transform.SetParent(this.parent, false);

            //身体的sprite
            int index = bodyList.Count % 2 == 0 ? 0 : 1;
            body.GetComponent<Image>().sprite = this.m_Sprites[index];

            //加入身体链表
            bodyList.Add(body.transform);
        }

        /// <summary>
        /// 退出、清理
        /// </summary>
        public void Clear()
        {
            Speed = 0;
            score = 0;
            length = 0;
            InitValue();

            //销毁小蛇身体
            foreach (var bodyTransform in this.bodyList)
            {
                UnityEngine.Object.Destroy(bodyTransform.gameObject);
            }

            bodyList.Clear();
        }

        /// <summary>
        /// 开始计时
        /// </summary>
        public void TimeStart()
        {
            this.m_Stopwatch.Start();
        }

        /// <summary>
        /// 计时停止
        /// </summary>
        public void TimeStop()
        {
            this.m_Stopwatch.Stop();
        }

        /// <summary>
        /// 重新开始计时
        /// </summary>
        public void TimeRestart()
        {
            this.m_Stopwatch.Restart();
        }

        /// <summary>
        /// The record.
        /// </summary>
        public void Record()
        {
            if (score < 10)
            {
                return;
            }

            var record = new Record
                             {
                                 playMode = GameRunningView.instance.GetPlayMode(),
                                 score = this.score,
                                 length = this.length,
                                 duration = this.TimeStr,
                                 timeOver = DateTime.Now.ToString("yyyyMMddHHmmss")
                             };
            config.records.Add(record);
        }

        /// <summary>
        /// 游戏暂停
        /// </summary>
        private void OnGamePauseChang()
        {
            Speed = 0;
            IsStop = true;
        }

        /// <summary>
        /// 游戏继续
        /// </summary>
        private void OnGameContinueChang()
        {
            Speed = 100;
            IsStop = false;
        }

        /// <summary>
        /// 小蛇长度改变
        /// </summary>
        private void OnSnakeLengthChange()
        {
            this.length = this.bodyList.Count;

            //吃到奖励随机加分0-100
            if (this.m_IsReward)
            {
                var random = Random.Range(0, 100);
                score += random;
                bonus = random;
                return;
            }

            score += 10;
            bonus = 10;
        }

        /// <summary>
        /// 小蛇皮肤改变
        /// </summary>
        private void OnSnakeSkinChange()
        {
            image.sprite = SpriteManager.instance.GetSnakeSkinSprite(0);
            m_Sprites[0] = SpriteManager.instance.GetSnakeSkinSprite(1);
            m_Sprites[1] = SpriteManager.instance.GetSnakeSkinSprite(2);
        }
    }
}

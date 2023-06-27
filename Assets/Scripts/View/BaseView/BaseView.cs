namespace Assets.Scripts.View.BaseView
{
    using UnityEngine;

    /// <summary>
    /// The base view.
    /// </summary>
    /// <typeparam name="T">
    /// 视图类
    /// </typeparam>
    public class BaseView<T>
        where T : new()
    {
        /// <summary>
        /// 视图是否打开
        /// </summary>
        public bool isOpen;

        /// <summary>
        /// 视图父节点
        /// </summary>
        protected Transform m_Parent;

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object s_Lock = new object();   

        /// <summary>
        /// 视图单列
        /// </summary>
        private static T s_Instance;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected BaseView()
        {
            System.Diagnostics.Debug.Assert(s_Instance == null, "视图为空");
        }

        /// <summary>
        /// Gets视图对象属性（视图单列全局访问点）
        /// </summary>
        public static T instance
        {
            get
            {
                if (s_Instance == null)
                {
                    lock (s_Lock)
                    {
                        if (s_Instance == null)
                        {
                            s_Instance = new T();
                        }
                    }
                }

                return s_Instance;
            }
        }

        /// <summary>
        /// 视图对象是否存在
        /// </summary>
        /// <value><c>true</c> if exists; otherwise, <c>false</c>.</value>
        public static bool exists => s_Instance != null;

        /// <summary>
        /// 打开视图
        /// </summary>
        public virtual void Open()
        {
            isOpen = true;
            m_Parent.gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏视图
        /// </summary>
        public virtual void Close()
        {
            isOpen = false;
            m_Parent.gameObject.SetActive(false);
        }
    }
}

namespace Assets.Scripts.Base
{
    /// <summary>
    /// 泛型单例模式
    /// </summary>
    /// <typeparam name="T">
    /// 单例类“T”
    /// </typeparam>
    public class Singleton<T>
        where T : new()
    {
        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object s_Lock= new object();

        /// <summary>
        /// The s_ instance.
        /// </summary>
        private static T s_Instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Singleton{T}" /> class.
        /// </summary>
        protected Singleton()
        {
            System.Diagnostics.Debug.Assert(s_Instance == null, "instance is null");
        }


        /// <summary>
        /// 单例模式全局访问点
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
    }
}

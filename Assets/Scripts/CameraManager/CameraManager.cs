namespace Assets.Scripts.CameraManager
{
    using Assets.Scripts.Base;
    using UnityEngine;

    /// <summary>
    /// 相机管理
    /// </summary>
    public class CameraManager : Singleton<CameraManager>
    {
        /// <summary>
        /// Gets主相机
        /// </summary>
        /// <value>The main camera.</value>
        public Camera mainCamera { get; private set; }

        /// <summary>
        /// GetsUI相机
        /// </summary>
        /// <value>The UI camera.</value>
        public Camera uiCamera { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            mainCamera = Camera.main;
            uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        }
    }
}

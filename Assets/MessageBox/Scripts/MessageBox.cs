namespace Assets.MessageBox.Scripts
{
    using System;

    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Class MessageBox.
    /// Implements the <see cref="UnityEngine.MonoBehaviour" />
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class MessageBox : MonoBehaviour
    {
        /// <summary>
        /// The instance
        /// </summary>
        public static MessageBox instance;

        /// <summary>
        /// The BTN1
        /// </summary>
        public Button btn1;

        /// <summary>
        /// The BTN2
        /// </summary>
        public Button btn2;

        /// <summary>
        /// The BTN3
        /// </summary>
        public Button btn3;

        /// <summary>
        /// The mask
        /// </summary>
        public RectTransform mask;

        /// <summary>
        /// The text information
        /// </summary>
        public Text textInfo;

        /// <summary>
        /// The m action1
        /// </summary>
        private Action m_Action1;

        /// <summary>
        /// The m action2
        /// </summary>
        private Action m_Action2;

        /// <summary>
        /// The m action3
        /// </summary>
        private Action m_Action3;

        /// <summary>
        /// Occurs when [opened callback].
        /// </summary>
        public static event Action OpenedCallback;

        /// <summary>
        /// Occurs when [closed callback].
        /// </summary>
        public static event Action ClosedCallback;

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public static void Close()
        {
            if (instance == null)
            {
                return;
            }

            ClosedCallback?.Invoke();
            Destroy(instance.gameObject);
            instance = null;
        }

        /// <summary>
        /// Hides this instance.
        /// </summary>
        public static void Hide()
        {
            if (instance == null)
            {
                return;
            }

            ClosedCallback?.Invoke();
            instance.gameObject.SetActive(false);
        }

        /// <summary>
        /// Opens this instance.
        /// </summary>
        /// <returns>MessageBox.</returns>
        public static MessageBox Open()
        {
            if (instance != null)
            {
                return instance;
            }

            instance = Instantiate(Resources.Load<GameObject>("MessageBox")).GetComponent<MessageBox>();
            var objs = GameObject.FindGameObjectsWithTag("Root");
            var currentCanvas = objs[objs.Length - 1];
            instance.transform.SetParent(currentCanvas.transform);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localScale = Vector3.one;
            instance.transform.localRotation = Quaternion.identity;
            return instance;
        }

        /// <summary>
        /// Shows the specified information.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="btnName">Name of the BTN.</param>
        /// <param name="btn1">The BTN1.</param>
        /// <param name="btn2">The BTN2.</param>
        /// <param name="btn3">The BTN3.</param>
        public static void Show(
            string info,
            string[] btnName = null,
            Action btn1 = null,
            Action btn2 = null,
            Action btn3 = null)
        {
            Open();
            if (btnName != null)
            {
                // Debug.Log(btnName[0]);
            }

            instance.gameObject.SetActive(true);
            instance.mask.localScale = Vector3.one;
            instance.textInfo.text = info;
            if (btnName == null || btnName.Length == 1)
            {
                instance.btn1.transform.Find("Text").GetComponent<Text>().text = btnName == null ? "OK" : btnName[0];
                instance.btn1.gameObject.transform.localPosition = new Vector3(0, -70, 0);
                instance.btn1.gameObject.SetActive(true);
                instance.btn2.gameObject.SetActive(false);
                instance.btn3.gameObject.SetActive(false);
            }

            if (btnName != null && btnName.Length == 2)
            {
                instance.btn1.transform.localPosition = new Vector3(-120, -70, 0);
                instance.btn2.transform.localPosition = new Vector3(120, -70, 0);
                instance.btn1.transform.Find("Text").GetComponent<Text>().text = btnName[0];
                instance.btn2.transform.Find("Text").GetComponent<Text>().text = btnName[1];
                instance.btn1.gameObject.SetActive(true);
                instance.btn2.gameObject.SetActive(true);
                instance.btn3.gameObject.SetActive(false);
            }

            if (btnName != null && btnName.Length == 3)
            {
                instance.btn1.transform.localPosition = new Vector3(-120, -70, 0);
                instance.btn2.transform.localPosition = new Vector3(0, -70, 0);
                instance.btn3.transform.localPosition = new Vector3(120, -70, 0);
                instance.btn1.transform.Find("Text").GetComponent<Text>().text = btnName[0];
                instance.btn2.transform.Find("Text").GetComponent<Text>().text = btnName[1];
                instance.btn3.transform.Find("Text").GetComponent<Text>().text = btnName[2];
                instance.btn1.gameObject.SetActive(true);
                instance.btn2.gameObject.SetActive(true);
                instance.btn3.gameObject.SetActive(true);
            }

            instance.m_Action1 = btn1;
            instance.m_Action2 = btn2;
            instance.m_Action3 = btn3;

            OpenedCallback?.Invoke();
        }

        /// <summary>
        /// Called when [BTN1 click].
        /// </summary>
        private void OnBtn1Click()
        {
            Hide();
            this.m_Action1?.Invoke();
            this.m_Action1 = null;
        }

        /// <summary>
        /// Called when [BTN2 click].
        /// </summary>
        private void OnBtn2Click()
        {
            Hide();
            this.m_Action2?.Invoke();
            this.m_Action2 = null;
        }

        /// <summary>
        /// Called when [BTN m click].
        /// </summary>
        private void OnBtnMClick()
        {
            Hide();
            this.m_Action3?.Invoke();
            this.m_Action3 = null;
        }

        /// <summary>
        /// Called when [destroy].
        /// </summary>
        private void OnDestroy()
        {
            instance = null;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            this.btn1.onClick.AddListener(this.OnBtn1Click);
            this.btn2.onClick.AddListener(this.OnBtn2Click);
            this.btn3.onClick.AddListener(this.OnBtnMClick);
        }
    }
}
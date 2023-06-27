namespace Assets.Scripts.LogManager
{
    using System;
    using System.IO;
    using System.Text;
    using Assets.Libs.Enum;
    using Assets.Scripts.Base;
    using UnityEngine;

    /// <summary>
    /// 日志管理
    /// </summary>
    public class LogManager : Singleton<LogManager>
    {
        /// <summary>
        /// The writer.
        /// </summary>
        private StreamWriter m_Writer;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            // 获取exe文件所在目录的路径
            string path = Path.GetDirectoryName(Application.dataPath) ?? Application.dataPath;

            // 创建日志文件
            string filePath = Path.Combine(path, "log.txt");
            m_Writer = new StreamWriter(filePath, true, Encoding.UTF8);
            OnEnable();
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="log">
        /// 日志信息
        /// </param>
        /// <param name="level">
        /// 日志等级
        /// </param>
        public void LogToFile(string log, LogLevel level)
        {
            var currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var logStr = "[" + level.ToString() + "] " + "[" + currentTime + "] " + log;

            // 将日志写入文件
            m_Writer.WriteLine(logStr);
        }

        /// <summary>
        /// 程序退出
        /// </summary>
        public void Quit()
        {
            OnDisable();
            if (m_Writer != null)
            {
                m_Writer.Close();
                m_Writer.Dispose();
                m_Writer = null;
            }
        }

        /// <summary>
        /// 注册全局异常捕获事件
        /// </summary>
        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        /// <summary>
        /// 取消注册全局异常捕获事件
        /// </summary>
        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        /// <summary>
        /// The handle log.
        /// </summary>
        /// <param name="logString">
        /// The log string.
        /// </param>
        /// <param name="stackTrace">
        /// The stack trace.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception)
            {
                // 捕获到错误或异常时，记录日志
                LogToFile(logString, LogLevel.Error);
                LogToFile(stackTrace, LogLevel.Error);
            }
            else if (type == LogType.Warning)
            {
                // 捕获到警告时，记录日志
                LogToFile(stackTrace, LogLevel.Warning);
            }
        }
    }
}

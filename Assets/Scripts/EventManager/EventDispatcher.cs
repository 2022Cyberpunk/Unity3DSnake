using System;

namespace Assets.Scripts.EventManager
{
    /// <summary>
    /// 事件调度器
    /// </summary>
    public class EventDispatcher
    {
        /// <summary>
        /// 事件控制器
        /// </summary>
        private static readonly EventController s_EventController = new EventController();

        /// <summary>
        /// 注册不带参数的事件
        /// </summary>
        /// <param name="eventType">
        /// The event Type.
        /// </param>
        /// <param name="handle">
        /// The handle.
        /// </param>
        public static void AddEventListener(string eventType, Action handle)
        {
            s_EventController.AddEventListener(eventType, handle);
        }

        /// <summary>
        /// 注册带有一个参数的事件
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <typeparam name="T">
        /// 类
        /// </typeparam>
        public static void AddEventListener<T>(string eventType, Action<T> handle)
        {
            s_EventController.AddEventListener(eventType, handle);
        }

        /// <summary>
        /// 取消注册不带参数的事件
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <param name="handle">The handle</param>
        public static void RemoveEventListener(string eventType, Action handle)
        {
            s_EventController.RemoveEventListener(eventType, handle);
        }

        /// <summary>
        /// 取消注册带一个参数的事件
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <typeparam name="T">
        /// 类
        /// </typeparam>
        public static void RemoveEventListener<T>(string eventType, Action<T> handle)
        {
            s_EventController.RemoveEventListener(eventType, handle);
        }

        /// <summary>
        /// 触发不带参数的事件
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        public static void TriggerEvent(string eventType)
        {
            s_EventController.TriggerEvent(eventType);
        }

        /// <summary>
        /// 触发带一个参数的事件
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <param name="arg1">
        /// The arg 1.
        /// </param>
        /// <typeparam name="T">
        /// 类
        /// </typeparam>
        public static void TriggerEvent<T>(string eventType, Action<T> arg1)
        {
            s_EventController.TriggerEvent(eventType, arg1);
        }
    }
}

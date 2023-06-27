using System;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.EventManager
{
    /// <summary>
    /// 事件控制
    /// </summary>
    public class EventController
    {
        /// <summary>
        /// 永久性的消息，在Cleanup的时候，这些消息的响应是不会被清理的。  
        /// </summary>
        private readonly List<string> m_PermanentEvents = new List<string>();

        /// <summary>
        /// 事件路由    
        /// </summary>
        private readonly Dictionary<string, Delegate> m_Router = new Dictionary<string, Delegate>();

        /// <summary>
        /// 公开字段获取事件路由
        /// </summary>
        /// <value>The router.</value>
        public Dictionary<string, Delegate> theRouter => this.m_Router;

        /// <summary>
        /// 添加事件（不带参数）
        /// </summary>
        /// <param name="eventType">
        /// 事件类型
        /// </param>
        /// <param name="handle">
        /// 事件方法对象
        /// </param>
        public void AddEventListener(string eventType, Action handle)
        {
            this.OnListenerAdding(eventType, handle);
            this.m_Router[eventType] = (Action)Delegate.Combine((Action)this.m_Router[eventType], handle);
        }

        /// <summary>
        /// 添加事件（带一个参数）
        /// </summary>
        /// <typeparam name="T">类参数</typeparam>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="handler">The handler.</param>
        public void AddEventListener<T>(string eventType, Action<T> handler)
        {
            this.OnListenerAdding(eventType, handler);
            this.m_Router[eventType] = (Action<T>)Delegate.Combine((Action<T>)this.m_Router[eventType], handler);
        }

        /// <summary>
        /// 删除事件（不带参数）
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <param name="handle">
        /// The handle.
        /// </param>
        public void RemoveEventListener(string eventType, Action handle)
        {
            if (this.OnListenerRemoving(eventType, handle))
            {
                this.m_Router[eventType] = (Action)Delegate.Remove((Action)this.m_Router[eventType], handle);
            }

            this.OnListenerRemoved(eventType);
        }

        /// <summary>
        /// 删除事件（带一个参数）
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <typeparam name="T">
        /// 类参数
        /// </typeparam>
        public void RemoveEventListener<T>(string eventType, Action<T> handle)
        {
            if (this.OnListenerRemoving(eventType, handle))
            {
                this.m_Router[eventType] = (Action)Delegate.Remove((Action)this.m_Router[eventType], handle);
            }
        
            this.OnListenerRemoved(eventType);
        }

        /// <summary>
        /// 触发事件（不带参数）
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        public void TriggerEvent(string eventType)
        {
            if (this.m_Router.TryGetValue(eventType, out var delegateHandle))
            {
                var invocationList = delegateHandle.GetInvocationList();
                for (int invocationIndex = 0; invocationIndex < invocationList.Length; invocationIndex++)
                {
                    var action = invocationList[invocationIndex] as Action;
                    if (action == null)
                    {
                        throw new EventException($"TriggerEvent {eventType} error: types of parameters are not match.");
                    }

                    try
                    {
                        action();
                    }
                    catch (Exception exception)
                    {   
                        Debug.LogError($"msg:{exception.Message} \nstacktrace:{exception.StackTrace}");
                    }
                }
            }
        }

        /// <summary>
        /// 触发事件（带一个参数）
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <param name="args1">
        /// The args 1.
        /// </param>
        /// <typeparam name="T">
        /// 类
        /// </typeparam>
        public void TriggerEvent<T>(string eventType, T args1)
        {
            if (this.m_Router.TryGetValue(eventType, out var delegateHandle))
            {
                var invocationList = delegateHandle.GetInvocationList();
                for (int invocationIndex = 0; invocationIndex < invocationList.Length; invocationIndex++)
                {
                    var action = invocationList[invocationIndex] as Action<T>;
                    if (action == null)
                    {
                        throw new EventException($"TriggerEvent {eventType} error: types of parameters are not match.");
                    }

                    try
                    {
                        action(args1);
                    }
                    catch (Exception exception) 
                    {
                        Debug.LogError($"msg:{exception.Message} \nstacktrace:{exception.StackTrace}");
                    }
                }
            }
        }

        /// <summary>
        /// 清理事件
        /// </summary>
        public void CleanUp()
        {
            List<string> list = new List<string>();
            foreach (var pair in this.m_Router)
            {
                var flag = false;
                foreach (var str in this.m_PermanentEvents)
                {
                    if (pair.Key == str)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    list.Add(pair.Key);
                }
            }

            foreach (var str in list)
            {
                this.m_Router.Remove(str);
            }
        }

        /// <summary>
        /// Marks as permanent.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        public void MarkAsPermanent(string eventType)
        {
            this.m_PermanentEvents.Add(eventType);
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="eventType">
        /// 事件类型
        /// </param>
        /// <param name="listenerBeingAdded">
        /// The listener Being Added.
        /// </param>
        private void OnListenerAdding(string eventType, Delegate listenerBeingAdded)    
        {
            if (!m_Router.ContainsKey(eventType))
            {
                m_Router.Add(eventType, null);
            }

            var delegateHandle = this.m_Router[eventType];
            if (delegateHandle != null && delegateHandle.GetType() != listenerBeingAdded.GetType())
            {
                throw new EventException(
                    $"Try to add not correct event {eventType}. Current type is {delegateHandle.GetType().Name}, adding type is {listenerBeingAdded.GetType().Name}.");
            }
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <param name="listenerBeingRemoved">
        /// The listener being removed.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="EventException">
        /// 异常消息
        /// </exception>
        private bool OnListenerRemoving(string eventType, Delegate listenerBeingRemoved)
        {
            if (!this.m_Router.ContainsKey(eventType))
            {
                return false;
            }

            var delegateHandle = this.m_Router[eventType];
            if (delegateHandle != null && delegateHandle.GetType() != listenerBeingRemoved.GetType())
            {
                throw new EventException(
                    $"Remove listener {eventType}\" failed, Current type is {delegateHandle.GetType()}, adding type is {listenerBeingRemoved.GetType()}.");
            }

            return true;
        }

        /// <summary>
        /// 移除该类型的所有事件
        /// </summary>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        private void OnListenerRemoved(string eventType)
        {
            if (this.m_Router.ContainsKey(eventType) && this.m_Router[eventType] == null)
            {
                this.m_Router.Remove(eventType);
            }
        }
    }
}

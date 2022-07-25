using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SimpleCore.Extensions
{
    /// <summary>
    ///     Unity Component 的扩展类。
    /// </summary>
    public static class ComponentExtensions
    {
        #region public static functions

        /// <summary>
        ///     获取或添加组件。
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            if (!component.TryGetComponent(out T t)) t = component.gameObject.AddComponent<T>();
            return t;
        }

        /// <summary>
        ///     获取或添加组件。
        /// </summary>
        /// <param name="component"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Component GetOrAddComponent(this Component component, Type type)
        {
            if (!component.TryGetComponent(type, out var t)) t = component.gameObject.AddComponent(type);
            return t;
        }

        /// <summary>
        ///     设置组件绑定的GameObject的状态为激活或停用。
        /// </summary>
        /// <param name="component"></param>
        /// <param name="value"></param>
        public static void SetActive(this Component component, bool value)
        {
            component.gameObject.SetActive(value);
        }

        /// <summary>
        ///     添加 Unity 原生触发事件。(先移除已注册的当前类型的事件回调。)
        /// </summary>
        /// <param name="component"></param>
        /// <param name="triggerType"></param>
        /// <param name="callback"></param>
        public static void AddEventTriggerHandler(this Component component, EventTriggerType triggerType,
            UnityAction<BaseEventData> callback)
        {
            component.gameObject.AddEventTriggerHandler(triggerType, callback);
        }

        /// <summary>
        ///     添加带参数的 Unity 原生触发事件。(先移除已注册的当前类型的事件回调。)
        /// </summary>
        /// <param name="component"></param>
        /// <param name="triggerType"></param>
        /// <param name="callback"></param>
        /// <param name="t"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddEventTriggerHandler<T>(this Component component, EventTriggerType triggerType,
            UnityAction<BaseEventData, T> callback, T t)
        {
            component.gameObject.AddEventTriggerHandler(triggerType, callback, t);
        }

        /// <summary>
        ///     移除所有的 Unity 原生触发事件。
        /// </summary>
        /// <param name="component"></param>
        public static void RemoveAllEventTriggerHandlers(this Component component)
        {
            component.gameObject.RemoveAllEventTriggerHandlers();
        }

        #endregion
    }
}
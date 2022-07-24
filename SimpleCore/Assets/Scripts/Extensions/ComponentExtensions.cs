using System;
using UnityEngine;

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

        #endregion
    }
}
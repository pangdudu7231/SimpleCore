using System;
using UnityEngine;

namespace SimpleCore.HotKeys
{
    /// <summary>
    /// 提供Unity事件的执行类。
    /// </summary>
    public class UnityExecutor : MonoBehaviour
    {
        #region static Instance

        private static UnityExecutor _instance;

        /// <summary>
        /// 单例。
        /// </summary>
        internal static UnityExecutor Instance
        {
            get
            {
                if (_instance == null) _instance = GetComponentSafely();
                return _instance;
            }
        }

        /// <summary>
        ///     获得或生成组件
        /// </summary>
        /// <returns></returns>
        private static UnityExecutor GetComponentSafely()
        {
            var components = FindObjectsOfType<UnityExecutor>();
            if (components == null || components.Length == 0) return GenerateComponent();
            return components[0];
        }

        /// <summary>
        ///     生成空的物体，并添加单例组件
        /// </summary>
        /// <returns></returns>
        private static UnityExecutor GenerateComponent()
        {
            var tempGo = new GameObject("UnityHotKeyExecutor");
            DontDestroyOnLoad(tempGo);
            return tempGo.AddComponent<UnityExecutor>();
        }

        #endregion

        #region event
        
        internal event Action OnUpdateHandler; // 帧函数的事件

        #endregion

        #region unity life cycles

        private void Update()
        {
            OnUpdateHandler?.Invoke();
        }

        #endregion
    }
}
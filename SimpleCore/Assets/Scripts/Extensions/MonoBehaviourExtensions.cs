using System;
using System.Collections;
using UnityEngine;

namespace SimpleCore.Extensions
{
    /// <summary>
    ///     MonoBehaviour 的扩展类。
    /// </summary>
    public static class MonoBehaviourExtensions
    {
        #region public static functions

        /// <summary>
        ///     延迟 frames 帧后执行回调函数。
        /// </summary>
        /// <param name="behaviour"></param>
        /// <param name="frames"></param>
        /// <param name="callback"></param>
        public static void DelayEndFrames(this MonoBehaviour behaviour, uint frames, Action callback)
        {
            behaviour.StartCoroutine(DelayCoroutine());

            IEnumerator DelayCoroutine()
            {
                for (var i = 0; i < frames; ++i) yield return new WaitForEndOfFrame();
                callback.Invoke();
            }
        }

        /// <summary>
        ///     延迟 frames 固定帧后执行回调函数。
        /// </summary>
        /// <param name="behaviour"></param>
        /// <param name="frames"></param>
        /// <param name="callback"></param>
        public static void DelayFixFrames(this MonoBehaviour behaviour, uint frames, Action callback)
        {
            behaviour.StartCoroutine(DelayCoroutine());

            IEnumerator DelayCoroutine()
            {
                for (var i = 0; i < frames; ++i) yield return new WaitForFixedUpdate();
                callback.Invoke();
            }
        }

        /// <summary>
        ///     延迟 seconds 秒后执行回调函数。
        /// </summary>
        /// <param name="behaviour"></param>
        /// <param name="seconds"></param>
        /// <param name="callback"></param>
        public static void DelayTime(this MonoBehaviour behaviour, float seconds, Action callback)
        {
            behaviour.StartCoroutine(DelayCoroutine());

            IEnumerator DelayCoroutine()
            {
                yield return new WaitForSeconds(seconds);
                callback.Invoke();
            }
        }

        /// <summary>
        ///     延迟 seconds 秒后执行回调函。(不受时间缩放的影响)
        /// </summary>
        /// <param name="behaviour"></param>
        /// <param name="seconds"></param>
        /// <param name="callback"></param>
        public static void DelayUnscaledTime(this MonoBehaviour behaviour, float seconds, Action callback)
        {
            behaviour.StartCoroutine(DelayCoroutine());

            IEnumerator DelayCoroutine()
            {
                yield return new WaitForSecondsRealtime(seconds);
                callback.Invoke();
            }
        }

        #endregion
    }
}
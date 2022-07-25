using System;
using System.Collections;

namespace SimpleCore.Extensions
{
    /// <summary>
    ///     Unity Coroutine 的扩展类。
    /// </summary>
    public static class CoroutineExtensions
    {
        #region public static functions

        /// <summary>
        ///     在IEnumerator后添加延续的IEnumerator。（可以组合多个顺序执行的协程。）
        /// </summary>
        /// <param name="coroutine"></param>
        /// <param name="continuations"></param>
        /// <returns></returns>
        public static IEnumerator ContinueWith(this IEnumerable coroutine, params IEnumerator[] continuations)
        {
            yield return coroutine;
            foreach (var continuation in continuations) yield return continuation;
        }

        /// <summary>
        ///     在IEnumerator后添加延续的回调事件。
        /// </summary>
        /// <param name="coroutine"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public static IEnumerator ContinueWith(this IEnumerator coroutine, Action continuation)
        {
            yield return coroutine;
            continuation.Invoke();
        }

        #endregion
    }
}
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleCore.Extensions
{
    /// <summary>
    /// Unity GameObject 的扩展类。
    /// </summary>
    public static class GameObjetExtensions
    {
        #region public static functions

        /// <summary>
        /// 取消 GameObject 的 DontDestroyOnLoad 的状态。
        /// </summary>
        /// <param name="gameObject"></param>
        public static void CancelDontDestroyOnLoad(this GameObject gameObject)
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        }

        /// <summary>
        /// 设置 GameObject 的父节点。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="parent"></param>
        /// <param name="worldPositionStays"></param>
        public static void SetParent(this GameObject gameObject, Transform parent, bool worldPositionStays = false)
        {
            gameObject.transform.SetParent(parent, worldPositionStays);
        }

        /// <summary>
        /// 设置 GameObject 的父节点。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="parentGo"></param>
        /// <param name="worldPositionStays"></param>
        public static void SetParent(this GameObject gameObject, GameObject parentGo, bool worldPositionStays = false)
        {
            gameObject.transform.SetParent(parentGo.transform, worldPositionStays);
        }

        /// <summary>
        /// 设置 GameObject 的局部坐标的位置。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetLocalPos(this GameObject gameObject, float? x = null, float? y = null, float? z = null)
        {
            if(x == null && y == null && z == null ) throw new ArgumentNullException();

            SetLocalPosInternal(gameObject, x, y, z);
        }

        /// <summary>
        /// 设置 GameObject 的全局坐标的位置。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetPosition(this GameObject gameObject, float? x = null, float? y = null, float? z = null)
        {
            if(x == null && y == null && z == null ) throw new ArgumentNullException();

            SetPositionInternal(gameObject, x, y, z);
        }

        /// <summary>
        /// 设置 GameObject 的局部旋转角度、
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetLocalAngle(this GameObject gameObject, float? x = null, float? y = null, float? z = null)
        {
            if(x == null && y == null && z == null ) throw new ArgumentNullException();

            SetLocalAngleInternal(gameObject, x, y, z);
        }

        /// <summary>
        /// 设置 GameObject 的全局旋转角度。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetAngle(this GameObject gameObject, float? x = null, float? y = null, float? z = null)
        {
            if(x == null && y == null && z == null ) throw new ArgumentNullException();

            SetAngleInternal(gameObject, x, y, z);
        }

        /// <summary>
        /// 设置 GameObject 的局部缩放值。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetLocalScale(this GameObject gameObject, float? x = null, float? y = null, float? z = null)
        {
            if(x == null && y == null && z == null ) throw new ArgumentNullException();

            SetLocalScaleInternal(gameObject, x, y, z);
        }
        
        /// <summary>
        /// Transform 归一化处理、
        /// </summary>
        /// <param name="gameObject"></param>
        public static void localIdentity(this GameObject gameObject)
        {
            var transform = gameObject.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// Transform 归一化处理、
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Identity(this GameObject gameObject)
        {
            var transform = gameObject.transform;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// 获取或添加组件。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (!gameObject.transform.TryGetComponent(out T t)) t = gameObject.AddComponent<T>();
            return t;
        }

        /// <summary>
        /// 获取或添加组件。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Component GetOrAddComponent(this GameObject gameObject,Type type)
        {
            if (!gameObject.TryGetComponent(type, out var t)) t = gameObject.AddComponent(type);
            return t;
        }

        #endregion

        #region private static internal functions

        /// <summary>
        /// 设置 GameObject 的局部坐标的位置。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private static void SetLocalPosInternal(GameObject gameObject, float? x = null, float? y = null,
            float? z = null)
        {
            var localPos = gameObject.transform.localPosition;
            if (x.HasValue) localPos.x = x.Value;
            if (y.HasValue) localPos.y = y.Value;
            if (z.HasValue) localPos.z = z.Value;
            gameObject.transform.localPosition = localPos;
        }

        /// <summary>
        /// 设置 GameObject 的全局坐标的位置。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private static void SetPositionInternal(GameObject gameObject, float? x = null, float? y = null,
            float? z = null)
        {
            var pos = gameObject.transform.position;
            if (x.HasValue) pos.x = x.Value;
            if (y.HasValue) pos.y = y.Value;
            if (z.HasValue) pos.z = z.Value;
            gameObject.transform.position = pos;
        }

        /// <summary>
        /// 设置 GameObject 的局部旋转角度、
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private static void SetLocalAngleInternal(GameObject gameObject, float? x = null, float? y = null,
            float? z = null)
        {
            var localAngle = gameObject.transform.localEulerAngles;
            if (x.HasValue) localAngle.x = x.Value;
            if (y.HasValue) localAngle.y = y.Value;
            if (z.HasValue) localAngle.z = z.Value;
            gameObject.transform.localEulerAngles = localAngle;
        }

        /// <summary>
        /// 设置 GameObject 的全局旋转角度。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private static void SetAngleInternal(GameObject gameObject, float? x = null, float? y = null, float? z = null)
        {
            var angle = gameObject.transform.eulerAngles;
            if (x.HasValue) angle.x = x.Value;
            if (y.HasValue) angle.y = y.Value;
            if (z.HasValue) angle.z = z.Value;
            gameObject.transform.eulerAngles = angle;
        }

        /// <summary>
        /// 设置 GameObject 的局部缩放值。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private static void SetLocalScaleInternal(GameObject gameObject, float? x = null, float? y = null,
            float? z = null)
        {
            var localScale = gameObject.transform.localScale;
            if (x.HasValue) localScale.x = x.Value;
            if (y.HasValue) localScale.y = y.Value;
            if (z.HasValue) localScale.z = z.Value;
            gameObject.transform.localScale = localScale;
        }

        #endregion
    }
}
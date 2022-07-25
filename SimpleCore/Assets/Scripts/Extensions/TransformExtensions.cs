using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace SimpleCore.Extensions
{
    /// <summary>
    ///     Unity Transform 的扩展类。
    /// </summary>
    public static class TransformExtensions
    {
        #region public static functions

        /// <summary>
        ///     设置 Transform 的局部坐标的位置。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetLocalPos(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            if (x == null && y == null && z == null) throw new ArgumentNullException();

            SetLocalPosInternal(transform, x, y, z);
        }

        /// <summary>
        ///     设置 Transform 的全局坐标的位置。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetPosition(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            if (x == null && y == null && z == null) throw new ArgumentNullException();

            SetPositionInternal(transform, x, y, z);
        }

        /// <summary>
        ///     设置 Transform 的局部旋转角度。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void SetLocalAngle(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            if (x == null && y == null && z == null) throw new ArgumentNullException();

            SetLocalAngleInternal(transform, x, y, z);
        }

        /// <summary>
        ///     设置 Transform 的全局旋转角度。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetAngle(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            if (x == null && y == null && z == null) throw new ArgumentNullException();

            SetAngleInternal(transform, x, y, z);
        }

        /// <summary>
        ///     设置 Transform 的局部缩放。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetLocalScale(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            if (x == null && y == null && z == null) throw new ArgumentNullException();

            SetLocalScaleInternal(transform, x, y, z);
        }

        /// <summary>
        ///     Transform 归一化处理、
        /// </summary>
        /// <param name="transform"></param>
        public static void localIdentity(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        ///     Transform 归一化处理、
        /// </summary>
        /// <param name="transform"></param>
        public static void Identity(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        ///     尝试根据子节点的名称获取子节点。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="name"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool TryFind(this Transform transform, string name, out Transform target)
        {
            target = transform.Find(name);
            return target != null;
        }

        /// <summary>
        ///     尝试获取子节点。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="index"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool TryGetChild(this Transform transform, int index, out Transform target)
        {
            if (index >= 0 && index < transform.childCount)
            {
                target = transform.GetChild(index);
                return target != null;
            }

            target = null;
            return false;
        }

        /// <summary>
        ///     获得 Transform 包含场景名称的全路径名称。
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static string GetFullNameWithSceneName(this Transform transform)
        {
            const char SEPARATE = '/'; //名称之间的分隔符号

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(transform.name); // 添加当前节点的名称

            while (transform.parent) //一层层添加父节点的名称
            {
                transform = transform.parent;
                stringBuilder.Insert(0, SEPARATE);
                stringBuilder.Insert(0, transform.name);
            }

            stringBuilder.Insert(0, SEPARATE);
            stringBuilder.Insert(0, transform.gameObject.scene.name); //添加场景名称
            return stringBuilder.ToString();
        }

        /// <summary>
        ///     获得 Transform 的全路径名称。
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static string GetFullName(this Transform transform)
        {
            const char SEPARATE = '/'; //名称之间的分隔符号

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(transform.name); // 添加当前节点的名称

            while (transform.parent) //一层层添加父节点的名称
            {
                transform = transform.parent;
                stringBuilder.Insert(0, SEPARATE);
                stringBuilder.Insert(0, transform.name);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     获得 Transform 下的所有子节点。
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Transform[] GetAllChildren(this Transform transform)
        {
            return transform.GetComponentsInChildren<Transform>().Where(item => item != transform).ToArray();
        }

        /// <summary>
        ///     获得 Transform 下所有标签为tag的子节点。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static Transform[] GetAllChildrenWithTag(this Transform transform, string tag)
        {
            return transform.GetComponentsInChildren<Transform>()
                .Where(item => item != transform && item.CompareTag(tag)).ToArray();
        }

        /// <summary>
        ///     利用反射的方式，给脚本里的Unity组件赋值。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="tObj">脚本实例类。</param>
        /// <typeparam name="T"></typeparam>
        public static void RecursionField<T>(this Transform transform, T tObj)
        {
            //反射赋值私有的实例字段
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

            //过滤出有效的字段
            var fieldInfos = typeof(T).GetFields(bindingFlags).Where(IsComponentField).ToList();
            SetFieldValue(transform, fieldInfos, tObj);

            //判断字段是否是组件字段。
            bool IsComponentField(FieldInfo fieldInfo)
            {
                return fieldInfo.FieldType.IsSubclassOf(typeof(Component));
            }
        }

        #endregion

        #region private static internal functions

        /// <summary>
        ///     设置 Transform 的局部坐标的位置。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private static void SetLocalPosInternal(Transform transform, float? x = null, float? y = null, float? z = null)
        {
            var localPos = transform.localPosition;
            if (x.HasValue) localPos.x = x.Value;
            if (y.HasValue) localPos.y = y.Value;
            if (z.HasValue) localPos.z = z.Value;
            transform.localPosition = localPos;
        }

        /// <summary>
        ///     设置 Transform 的全局坐标的位置。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private static void SetPositionInternal(Transform transform, float? x = null, float? y = null, float? z = null)
        {
            var pos = transform.position;
            if (x.HasValue) pos.x = x.Value;
            if (y.HasValue) pos.y = y.Value;
            if (z.HasValue) pos.z = z.Value;
            transform.position = pos;
        }

        /// <summary>
        ///     设置 Transform 的局部旋转角度。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private static void SetLocalAngleInternal(Transform transform, float? x = null, float? y = null,
            float? z = null)
        {
            var localAngle = transform.localEulerAngles;
            if (x.HasValue) localAngle.x = x.Value;
            if (y.HasValue) localAngle.y = y.Value;
            if (z.HasValue) localAngle.z = z.Value;
            transform.localEulerAngles = localAngle;
        }

        /// <summary>
        ///     设置 Transform 的全局旋转角度。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private static void SetAngleInternal(Transform transform, float? x = null, float? y = null, float? z = null)
        {
            var angle = transform.eulerAngles;
            if (x.HasValue) angle.x = x.Value;
            if (y.HasValue) angle.y = y.Value;
            if (z.HasValue) angle.z = z.Value;
            transform.eulerAngles = angle;
        }

        /// <summary>
        ///     设置 Transform 的局部缩放。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private static void SetLocalScaleInternal(Transform transform, float? x = null, float? y = null,
            float? z = null)
        {
            var localScale = transform.localScale;
            if (x.HasValue) localScale.x = x.Value;
            if (y.HasValue) localScale.y = y.Value;
            if (z.HasValue) localScale.z = z.Value;
            transform.localScale = localScale;
        }

        #endregion

        #region private static functions

        /// <summary>
        ///     给字段实例赋值。
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="fieldInfos"></param>
        /// <param name="tObj"></param>
        /// <typeparam name="T"></typeparam>
        private static void SetFieldValue<T>(this Transform transform, IList<FieldInfo> fieldInfos, T tObj)
        {
            if (!fieldInfos.Any()) return; // 字段全部赋值完成

            //遍历子节点，根据字段的名称查找子节点上的对应组件，并赋值。
            for (var index = 0; index < transform.childCount; index++)
            {
                var childTransform = transform.GetChild(index);
                SetFieldValue(childTransform, fieldInfos, tObj);
                for (var i = fieldInfos.Count - 1; i >= 0; i--)
                {
                    var fieldInfo = fieldInfos[i];
                    if (!IsMatch(fieldInfo.Name, childTransform.name)) continue;
                    var component = childTransform.GetComponent(fieldInfo.FieldType);
                    fieldInfo.SetValue(tObj, component);
                    fieldInfos.RemoveAt(i);
                }
            }

            //判断字段的名称和节点的名称是否匹配
            //忽略字段开头的'_'字符。
            //忽略大小写。
            bool IsMatch(string fieldName, string nodeName)
            {
                var compareStr = fieldName.StartsWith("_") ? fieldName.Substring(1, fieldName.Length - 1) : fieldName;
                return compareStr.ToLower().Equals(nodeName.ToLower());
            }
        }

        #endregion
    }
}
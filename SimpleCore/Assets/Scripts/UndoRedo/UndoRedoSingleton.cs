using System;
using System.Collections.Generic;

namespace SimpleCore.UndoRedoes
{
    /// <summary>
    ///     撤销还原的单例类。
    /// </summary>
    public sealed class UndoRedoSingleton
    {
        private const int RECORD_LIMIT = 1000; //记录容器中记录数量的上限

        #region static instance

        /// <summary>
        ///     单例实例
        /// </summary>
        public static UndoRedoSingleton Instance { get; } = new();

        #endregion

        #region private members

        private readonly Dictionary<int, RecordContainer> _recordContainerDic; //记录容器的字典。
        private int _defContainerKey = -1; //默认容器的key值

        #endregion

        #region ctor

        /// <summary>
        ///     构造函数(私有)
        /// </summary>
        private UndoRedoSingleton()
        {
            _recordContainerDic = new Dictionary<int, RecordContainer>(2);
        }

        #endregion

        #region public functions

        #region 添加或移除回调事件

        /// <summary>
        ///     向 记录容器中 添加 记录新的撤销还原之前的回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="containerKey"></param>
        /// <param name="isClear">是否先清空事件</param>
        public void AddBeforeRecordHandler(Action<IRecord> callback, int? containerKey = null, bool isClear = true)
        {
            var container = GetContainer(containerKey);
            if (isClear) container.OnBeforeRecordHandler = null;
            container.OnBeforeRecordHandler += callback;
        }

        /// <summary>
        ///     从 记录容器中 移除 记录新的撤销还原之前的回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="containerKey"></param>
        public void RemoveBeforeRecordHandler(Action<IRecord> callback, int? containerKey = null)
        {
            GetContainer(containerKey).OnBeforeRecordHandler -= callback;
        }

        /// <summary>
        ///     向 默认的记录容器中 添加 记录新的撤销还原之后的回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="containerKey"></param>
        /// <param name="isClear"></param>
        public void AddEndRecordHandler(Action<IRecord> callback, int? containerKey = null, bool isClear = true)
        {
            var container = GetContainer(containerKey);
            if (isClear) container.OnEndRecordHandler = null;
            container.OnEndRecordHandler += callback;
        }

        /// <summary>
        ///     从 默认的记录容器中 移除 记录新的撤销还原之后的回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="containerKey"></param>
        public void RemoveEndRecordHandler(Action<IRecord> callback, int? containerKey = null)
        {
            GetContainer(containerKey).OnEndRecordHandler -= callback;
        }

        /// <summary>
        ///     向 默认的记录容器中 添加 执行撤销之前的回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="containerKey"></param>
        /// <param name="isClear"></param>
        public void AddBeforeUndoHandler(Action<IRecord> callback, int? containerKey = null, bool isClear = true)
        {
            var container = GetContainer(containerKey);
            if (isClear) container.OnBeforeUndoHandler = null;
            container.OnBeforeUndoHandler += callback;
        }

        /// <summary>
        ///     从 默认的记录容器中 移除 执行撤销之前的回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="containerKey"></param>
        public void RemoveBeforeUndoHandler(Action<IRecord> callback, int? containerKey = null)
        {
            GetContainer(containerKey).OnBeforeUndoHandler -= callback;
        }

        /// <summary>
        ///     向 默认的记录容器中 添加 执行撤销之后的回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="containerKey"></param>
        /// <param name="isClear"></param>
        public void AddEndUndoHandler(Action<IRecord> callback, int? containerKey = null, bool isClear = true)
        {
            var container = GetContainer(containerKey);
            if (isClear) container.OnEndUndoHandler = null;
            container.OnEndUndoHandler += callback;
        }

        /// <summary>
        ///     从 默认的记录容器中 移除 执行撤销之后的回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="containerKey"></param>
        public void RemoveEndUndoHandler(Action<IRecord> callback, int? containerKey = null)
        {
            GetContainer(containerKey).OnEndUndoHandler -= callback;
        }

        /// <summary>
        ///     向 默认的记录容器中 添加 执行还原之前的回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="containerKey"></param>
        /// <param name="isClear"></param>
        public void AddBeforeRedoHandler(Action<IRecord> callback, int? containerKey = null, bool isClear = true)
        {
            var container = GetContainer(containerKey);
            if (isClear) container.OnBeforeRedoHandler = null;
            container.OnBeforeRedoHandler += callback;
        }

        /// <summary>
        ///     从 默认的记录容器中 移除 执行还原之前的回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="containerKey"></param>
        public void RemoveBeforeRedoHandler(Action<IRecord> callback, int? containerKey = null)
        {
            GetContainer(containerKey).OnBeforeRedoHandler -= callback;
        }

        /// <summary>
        ///     向 默认的记录容器中 添加 执行还原之后的回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="containerKey"></param>
        /// <param name="isClear"></param>
        public void AddEndRedoHandler(Action<IRecord> callback, int? containerKey = null, bool isClear = true)
        {
            var container = GetContainer(containerKey);
            if (isClear) container.OnEndRedoHandler = null;
            container.OnEndRedoHandler += callback;
        }

        /// <summary>
        ///     从 默认的记录容器中 移除 执行还原之后的回调
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="containerKey"></param>
        public void RemoveEndRedoHandler(Action<IRecord> callback, int? containerKey = null)
        {
            GetContainer(containerKey).OnEndRedoHandler -= callback;
        }

        #endregion

        /// <summary>
        ///     添加一个新的记录容器。
        /// </summary>
        /// <param name="limit">容器的容量。</param>
        /// <returns></returns>
        public int AddContainer(int limit = RECORD_LIMIT)
        {
            var container = new RecordContainer(limit);
            var containerKey = container.GetHashCode();
            _recordContainerDic[containerKey] = container;
            return containerKey;
        }

        /// <summary>
        ///     是否可以执行撤销。
        /// </summary>
        /// <param name="containerKey"></param>
        /// <returns></returns>
        public bool CanUndo(int? containerKey = null)
        {
            return GetContainer(containerKey).CanUndo;
        }

        /// <summary>
        ///     是否可以执行还原。
        /// </summary>
        /// <param name="containerKey"></param>
        /// <returns></returns>
        public bool CanRedo(int? containerKey = null)
        {
            return GetContainer(containerKey).CanRedo;
        }

        /// <summary>
        ///     是否启用。
        /// </summary>
        /// <param name="containerKey"></param>
        /// <returns></returns>
        public bool IsEnable(int? containerKey = null)
        {
            return GetContainer(containerKey).Enable;
        }

        /// <summary>
        ///     设置是否启用。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="containerKey"></param>
        /// <returns></returns>
        public bool SetEnable(bool value, int? containerKey = null)
        {
            return GetContainer(containerKey).Enable = value;
        }

        /// <summary>
        ///     记录撤销还原。
        /// </summary>
        /// <param name="record"></param>
        /// <param name="containerKey"></param>
        public void Record(IRecord record, int? containerKey = null)
        {
            GetContainer(containerKey).Record(record);
        }

        /// <summary>
        ///     撤销。
        /// </summary>
        /// <param name="containerKey"></param>
        public void Undo(int? containerKey = null)
        {
            GetContainer(containerKey).Undo();
        }

        /// <summary>
        ///     还原。
        /// </summary>
        /// <param name="containerKey"></param>
        public void Redo(int? containerKey = null)
        {
            GetContainer(containerKey).Redo();
        }

        /// <summary>
        ///     清理记录容器
        /// </summary>
        /// <param name="containerKey"></param>
        public void ClearContainer(int? containerKey = null)
        {
            GetContainer(containerKey).Clear();
        }

        /// <summary>
        ///     清理撤销还原。
        /// </summary>
        public void Clear()
        {
            _defContainerKey = -1;
            foreach (var recordContainer in _recordContainerDic.Values) recordContainer.Clear();
            _recordContainerDic.Clear();
        }

        #endregion

        #region private functions

        /// <summary>
        ///     获得记录容器。
        /// </summary>
        /// <param name="containerKey">为空时获得默认的容器</param>
        /// <returns></returns>
        private RecordContainer GetContainer(int? containerKey = null)
        {
            return containerKey.HasValue ? _recordContainerDic[containerKey.Value] : GetOrAddDefContainer();
        }

        /// <summary>
        ///     生成或添加默认的记录容器。
        /// </summary>
        /// <returns></returns>
        private RecordContainer GetOrAddDefContainer()
        {
            if (_defContainerKey == -1 || !_recordContainerDic.ContainsKey(_defContainerKey))
            {
                var container = new RecordContainer(RECORD_LIMIT);
                _defContainerKey = container.GetHashCode();
                _recordContainerDic[_defContainerKey] = container;
                return container;
            }

            return _recordContainerDic[_defContainerKey];
        }

        #endregion
    }
}
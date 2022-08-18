using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleCore.UndoRedoes.Example
{
    public class UndoRedoDemo : MonoBehaviour
    {
        #region private members

        [SerializeField] private Button _undoBtn, _redoBtn; //撤销还原按钮
        [SerializeField] private Text _undoCountTxt, _redoCountTxt;
        [SerializeField] private Button _spawnBtn, _delBtn;
        [SerializeField] private Text _spawnCountTxt;

        private int _containerKey = -1; //撤销还原容器的key值
        private readonly List<GameObject> _spawnGos = new(); //保存生成的物体

        #endregion

        #region unity life cycles

        private void Start()
        {
            InitContainer();
            AddListeners();
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        #endregion

        #region public functions

        public void AddGameObject(GameObject go)
        {
            _spawnGos.Add(go);
            _spawnCountTxt.text = _spawnGos.Count.ToString();
        }

        public void RemoveGameObject(GameObject go)
        {
            _spawnGos.Remove(go);
            _spawnCountTxt.text = _spawnGos.Count.ToString();
        }

        #endregion

        #region private functions

        /// <summary>
        ///     初始化撤销还原容器
        /// </summary>
        private void InitContainer()
        {
            var containerKey = UndoRedoSingleton.Instance.AddContainer(2); //设置容器只保存20条记录
            UndoRedoSingleton.Instance.AddEndRecordHandler(_ => ResetView(), containerKey);
            UndoRedoSingleton.Instance.AddEndRedoHandler(_ => ResetView(), containerKey);
            UndoRedoSingleton.Instance.AddEndUndoHandler(_ => ResetView(), containerKey);
            _containerKey = containerKey;
        }

        /// <summary>
        ///     注册监听事件
        /// </summary>
        private void AddListeners()
        {
            _undoBtn.onClick.AddListener(() => UndoRedoSingleton.Instance.Undo(_containerKey));
            _redoBtn.onClick.AddListener(() => UndoRedoSingleton.Instance.Redo(_containerKey));
            _spawnBtn.onClick.AddListener(SpawnGO);
            _delBtn.onClick.AddListener(DeleteGO);
        }

        /// <summary>
        ///     注销监听事件
        /// </summary>
        private void RemoveListeners()
        {
            _undoBtn.onClick.RemoveAllListeners();
            _redoBtn.onClick.RemoveAllListeners();
            _spawnBtn.onClick.RemoveAllListeners();
            _delBtn.onClick.RemoveAllListeners();
        }

        /// <summary>
        ///     刷新页面显示
        /// </summary>
        private void ResetView()
        {
            var containerKey = _containerKey;
            _undoBtn.interactable = UndoRedoSingleton.Instance.CanUndo(containerKey);
            _redoBtn.interactable = UndoRedoSingleton.Instance.CanRedo(containerKey);
            _undoCountTxt.text = UndoRedoSingleton.Instance.GetUndoCount(containerKey).ToString();
            _redoCountTxt.text = UndoRedoSingleton.Instance.GetRedoCount(containerKey).ToString();
        }

        /// <summary>
        ///     随机生成物体
        /// </summary>
        private void SpawnGO()
        {
            var goType = (PrimitiveType) Random.Range(0, 6);
            var go = GameObject.CreatePrimitive(goType);
            var x = Random.Range(-15f, 15f);
            var y = Random.Range(-7f, 5f);
            go.transform.position = new Vector3(x, y, 15);
            AddGameObject(go);

            //记录撤销还原
            var spawnRecord = new SpawnRecord(this, go);
            UndoRedoSingleton.Instance.Record(spawnRecord, _containerKey);
        }

        /// <summary>
        ///     随机删除物体
        /// </summary>
        private void DeleteGO()
        {
            var total = _spawnGos.Count;
            if (total == 0) return;

            var index = Random.Range(0, total);
            var go = _spawnGos[index];
            go.SetActive(false);
            RemoveGameObject(go);

            //记录撤销还原
            var delRecord = new DelRecord(this, go);
            UndoRedoSingleton.Instance.Record(delRecord, _containerKey);
        }

        #endregion
    }
}
using System.Collections.Generic;
using System.Linq;

namespace SimpleCore.HotKeys
{
    /// <summary>
    /// 热键容器。
    /// </summary>
    public sealed class HotKeyContainer
    {
        #region private members

        private readonly List<IHotKeyInPress> _hotKeyInPresses;
        private readonly List<IHotKeyInFrame> _hotKeyInFrames;
        
        private bool _enabled = true;//热键是否可用

        #endregion

        #region public properties

        /// <summary>
        /// 热键是否可用。
        /// </summary>
        public bool Enabled
        {
            get => _enabled;
            set => _enabled = value;
        }

        #endregion

        #region ctor

        /// <summary>
        /// 构造函数。
        /// </summary>
        public HotKeyContainer()
        {
            _hotKeyInPresses = new List<IHotKeyInPress>();
            _hotKeyInFrames = new List<IHotKeyInFrame>();
        }

        #endregion

        #region public functions
        
        /// <summary>
        /// 添加热键。
        /// </summary>
        /// <param name="hotKeyInPress"></param>
        public bool Add(IHotKeyInPress hotKeyInPress)
        {
            //判断是否存在相同的热键
            if (_hotKeyInPresses.Any(hotKeyInPress.IsSame)) return false;
            if (_hotKeyInFrames.Any(hotKeyInPress.IsSame)) return false;
            
            _hotKeyInPresses.Add(hotKeyInPress);
            return true;
        }

        /// <summary>
        /// 添加热键。
        /// </summary>
        /// <param name="hotKeyInFrame"></param>
        public void Add(IHotKeyInFrame hotKeyInFrame)
        {
            
        }

        /// <summary>
        /// 移除热键。
        /// </summary>
        /// <param name="hotKeyInPress"></param>
        public void Remove(IHotKeyInPress hotKeyInPress)
        {
            
        }

        /// <summary>
        /// 移除热键。
        /// </summary>
        /// <param name="hotKeyInFrame"></param>
        public void Remove(IHotKeyInFrame hotKeyInFrame)
        {
            
        }

        #endregion

        #region private functions

        

        #endregion
    }
}
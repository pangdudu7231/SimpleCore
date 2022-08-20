namespace SimpleCore.HotKeys
{
    /// <summary>
    /// 热键容器的接口。
    /// </summary>
    public interface IHotKeyContainer
    {
        /// <summary>
        /// 添加持续触发的热键。
        /// </summary>
        /// <param name="hotKeyInPress"></param>
        /// <returns></returns>
        bool Add(IHotKeyInPress hotKeyInPress);

        /// <summary>
        /// 添加当前帧触发的热键
        /// </summary>
        /// <param name="hotKeyInFrame"></param>
        /// <returns></returns>
        bool Add(IHotKeyInFrame hotKeyInFrame);

        /// <summary>
        /// 移除持续触发的热键。
        /// </summary>
        /// <param name="hotKeyInPress"></param>
        void Remove(IHotKeyInPress hotKeyInPress);

        /// <summary>
        /// 移除当前帧触发的热键。
        /// </summary>
        /// <param name="hotKeyInFrame"></param>
        void Remove(IHotKeyInFrame hotKeyInFrame);
    }
}
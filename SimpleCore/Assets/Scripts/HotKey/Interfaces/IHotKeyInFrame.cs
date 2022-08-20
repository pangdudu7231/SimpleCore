namespace SimpleCore.HotKeys
{
    /// <summary>
    ///     当前帧触发的热键接口。
    /// </summary>
    public interface IHotKeyInFrame : IHotKey
    {
        /// <summary>
        ///     当前帧触发。
        /// </summary>
        /// <returns></returns>
        bool IsActiveInFrame();
    }
}
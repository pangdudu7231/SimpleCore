namespace SimpleCore.HotKeys
{
    /// <summary>
    ///     持续触发的热键接口。
    /// </summary>
    public interface IHotKeyInPress : IHotKey
    {
        /// <summary>
        ///     持续触发。
        /// </summary>
        /// <returns></returns>
        bool IsActiveInPress();
    }
}
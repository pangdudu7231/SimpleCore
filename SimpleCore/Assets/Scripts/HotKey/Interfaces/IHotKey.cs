namespace SimpleCore.HotKeys
{
    /// <summary>
    /// 热键接口。
    /// </summary>
    public interface IHotKey
    {
        /// <summary>
        ///     热键是否可用。
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// 热键的注册键位是否相同。
        /// </summary>
        /// <param name="hotKey"></param>
        /// <returns></returns>
        bool IsSame(IHotKey hotKey);
    }
}
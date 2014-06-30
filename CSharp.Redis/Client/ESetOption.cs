namespace CSharp.Redis.Client
{
    public enum ESetOption
    {
        /// <summary>
        /// 设置键的过期时间为 second 秒
        /// </summary>
        EX,

        /// <summary>
        /// 设置键的过期时间为 millisecond 毫秒
        /// </summary>
        PX,

        /// <summary>
        /// 只在键不存在时，才对键进行设置操作
        /// </summary>
        NX,

        /// <summary>
        /// 只在键已经存在时，才对键进行设置操作
        /// </summary>
        XX
    }
}
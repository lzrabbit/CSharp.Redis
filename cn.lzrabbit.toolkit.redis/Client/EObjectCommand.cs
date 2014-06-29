namespace CSharp.Redis.Client
{
    /// <summary>
    /// RedisKeys.Object命令
    /// </summary>
    public enum EObjectCommand
    {
        /// <summary>
        /// 返回给定 key 引用所储存的值的次数
        /// </summary>
        REFCOUNT,

        /// <summary>
        /// 返回给定 key 锁储存的值使用的编码
        /// </summary>
        ENCODING,

        /// <summary>
        /// 返回给定 key 自储存以来的空转时间(idle， 没有被读取也没有被写入)，以秒为单位
        /// </summary>
        IDLETIME
    }
}
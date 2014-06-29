using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharp.Redis
{
    public class Note
    {
        public static readonly string V1 = string.Format(@"肥兔Redis ver:{0} (建议在Redis2.8以上版本使用)


v0.1 2014.06.10
新建项目
KEY类型,集合数量查询
服务器信息查询
    
v0.2 2014.06.28
全部代码重构
RedisClient增加RedisPool链接池支持
修复v0.1集合查询BUG
新增服务器保存功能，改善易用性
可执行自定义Redis命令
改善Redis集合类2.8版本以下兼容性，对小数据集使用HGETALL,SMEMBERS,ZRANGEBYSCORE代替HSCAN,SSCAN,ZSCAN等命令", Application.ProductVersion);


    }
}

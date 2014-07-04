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
代码重构
RedisClient增加RedisPool链接池支持
修复v0.1集合查询BUG
新增服务器保存功能，改善易用性
可执行自定义Redis命令
改善Redis集合类2.8版本以下兼容性，对小数据集使用HGETALL,SMEMBERS,ZRANGEBYSCORE代替HSCAN,SSCAN,ZSCAN等命令
无Redis命令输入时禁止执行
文本框精简,文本输出框不在自动换行

v0.3 2014.07.04
改善程序稳定性
增加执行命令监控功能
添加常用服务器指令DBSIZE,CLIENT LIST,INFO,TIME,CONFIG
", Application.ProductVersion);


    }
}

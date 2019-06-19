using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.iChunqiuQQBot.MahuaEvents
{
    /// <summary>
    /// 常量工具类
    /// </summary>
    class Constants
    {
        // 数据库连接字符串
        public static String connectionStr = ConfigTools.getValue("connectionStr");

        // 间隔艾特时间
        public static String sleepTime = ConfigTools.getValue("sleepTime");

        // redis配置字符串
        public static String redisStr = ConfigTools.getValue("redisStr");
    }
}

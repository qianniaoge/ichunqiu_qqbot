using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.iChunqiuQQBoot.Beta.Tools
{ 
    /// <summary>
    ///  配置文件处理工具类
    /// </summary>
    class ConfigTools
    {
        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>配置结果</returns>
        public static string getValue(string key) {
            return ConfigurationManager.AppSettings[key].ToString();
        }
    }
}

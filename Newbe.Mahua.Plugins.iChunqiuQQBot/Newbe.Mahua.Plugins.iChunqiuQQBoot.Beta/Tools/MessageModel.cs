using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.iChunqiuQQBoot.Beta.Tools
{
    class MessageModel
    {
        // 发送消息
        private string sendMessage;
        // 是否为管理员
        private bool isAdmin;
        // 执行的指令
        private string code;

        public string SendMessage { get => sendMessage; set => sendMessage = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public string Code { get => code; set => code = value; }

        /// <summary>
        /// 有参构造方法
        /// </summary>
        /// <param name="sendMessage">发送内容信息</param>
        /// <param name="isAdmin">是否为管理员</param>
        /// <param name="code">执行命令</param>
        public MessageModel(string sendMessage,bool isAdmin,string code) {
            this.Code = code;
            this.SendMessage = sendMessage;
            this.IsAdmin = isAdmin;
        }
    }
}

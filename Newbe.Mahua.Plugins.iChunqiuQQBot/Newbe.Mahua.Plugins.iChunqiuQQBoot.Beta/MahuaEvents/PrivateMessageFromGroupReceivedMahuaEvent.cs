using Newbe.Mahua.MahuaEvents;
using Newbe.Mahua.Plugins.iChunqiuQQBoot.Beta.Controller;
using Newbe.Mahua.Plugins.iChunqiuQQBoot.Beta.Tools;
using StackExchange.Redis;
using System;

namespace Newbe.Mahua.Plugins.iChunqiuQQBoot.Beta.MahuaEvents
{
    /// <summary>
    /// 来自群成员的私聊消息接收事件
    /// </summary>
    public class PrivateMessageFromGroupReceivedMahuaEvent
        : IPrivateMessageFromGroupReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public PrivateMessageFromGroupReceivedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }
        //private MessageController messageController = new MessageController();
        public void ProcessGroupMessage(PrivateMessageFromGroupReceivedContext context)
        {
            string message = context.Message;
            if (message == "" || message.Length == 0 || message == null)
            {
                return;
            }
            IDatabase redis = RedisHelper.getRedis();
            // 判断用户是否在缓冲中
            if (redis.StringGet(context.FromQq).IsNull)
            {
                redis.StringSet(context.FromQq, "flag");
                redis.KeyExpire(context.FromQq, new TimeSpan(10000000 * Convert.ToInt16(Constants.sleepTime)));
            }
            else
            {
                string tmpStr = "为防止造成刷屏，您每次使用机器人的时间间隔"+ Constants.sleepTime + "秒哦！";
                _mahuaApi.SendPrivateMessage(context.FromQq, tmpStr);
                return;
            };
            if (message == null || message == "" || message.Length == 0)
            {
                //
            }
            else
            {
                string tmpStr = MessageController.main(message, context.FromQq).SendMessage;
                if (tmpStr == null || tmpStr == "" || tmpStr.Length == 0)
                {
                    tmpStr = "\n无数据!";
                }
                if (tmpStr != "" && tmpStr.Length > 0)
                {
                    _mahuaApi.SendPrivateMessage(context.FromQq, tmpStr);
                }
            }
        }
    }
}

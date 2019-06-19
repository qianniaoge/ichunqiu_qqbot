using Newbe.Mahua.MahuaEvents;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Newbe.Mahua.Plugins.iChunqiuQQBot.MahuaEvents
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class GroupMessageReceivedMahuaEvent
        : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public GroupMessageReceivedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }
       // private MessageController messageController = new MessageController();
        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            String message = context.Message;
            message = message.Trim();
            if (message == "" || message.Length == 0 || message == null)
            {
                return;
            }
            String myQQ = _mahuaApi.GetLoginQq();
            String aiteQQ = "[CQ:at,qq=" + myQQ + "]";
            if (message.Contains(aiteQQ)) {
                String sendMessage = "[CQ:at,qq=" + context.FromQq + "]";
                message = message.Replace(aiteQQ, "").Replace("\"\"","").Replace("“","").Replace("”","").Trim();
                IDatabase redis = RedisHelper.getRedis();
                // 判断用户是否在缓冲中
                if (redis.StringGet(context.FromQq).IsNull)
                {
                    redis.StringSet(context.FromQq, "flag");
                    redis.KeyExpire(context.FromQq, new TimeSpan(10000000 * Convert.ToInt16(Constants.sleepTime)));
                }
                else {
                    string tmpStr = "为防止造成刷屏，您每次使用机器人的时间间隔"+ Constants.sleepTime + "秒哦！";
                    sendMessage += tmpStr;
                    _mahuaApi.SendGroupMessage(context.FromGroup, sendMessage);
                    return;
                };
                if (message == null || message =="" || message.Length == 0) {
                    //
                }
                else
                {
                    MessageModel messageModel = MessageController.main(message, context.FromQq);
                    // 发送消息
                    string tmpStr = messageModel.SendMessage;
                    if (messageModel.IsAdmin) {
                        // 群数量
                        if (MessageConstant.QUN_TOTAL.Equals(messageModel.Code))
                        {
                            ModelWithSourceString<IEnumerable<GroupInfo>> modelWithSourceString = _mahuaApi.GetGroupsWithModel();
                            IEnumerable<GroupInfo> groupInfo = modelWithSourceString.Model;
                            int count = 0;
                            foreach (var item in groupInfo)
                            {
                                count ++;
                            }
                            _mahuaApi.SendGroupMessage(context.FromQq, "群数量：" + count.ToString());
                        }
                        else
                        {
                            if (tmpStr != "" && tmpStr.Length > 0)
                            {
                                sendMessage += tmpStr;
                                _mahuaApi.SendGroupMessage(context.FromQq, sendMessage);
                            }
                        }
                    }
                    else
                    {
                        if (tmpStr != "" && tmpStr.Length > 0)
                        {
                            sendMessage += tmpStr;
                            _mahuaApi.SendGroupMessage(context.FromGroup, sendMessage);
                        }
                    }
                }
            }
        }
    }
}

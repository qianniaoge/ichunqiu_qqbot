using Newbe.Mahua.MahuaEvents;
using StackExchange.Redis;
using System.Collections.Generic;
using System;

namespace Newbe.Mahua.Plugins.iChunqiuQQBot.MahuaEvents
{
    /// <summary>
    /// 来自好友的私聊消息接收事件
    /// </summary>
    public class PrivateMessageFromFriendReceivedMahuaEvent
        : IPrivateMessageFromFriendReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public PrivateMessageFromFriendReceivedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }
      //  private MessageController messageController = new MessageController();
        public void ProcessFriendMessage(PrivateMessageFromFriendReceivedContext context)
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
                MessageModel messageModel = MessageController.main(message, context.FromQq);
                string tmpStr = messageModel.SendMessage;
                bool isAdmin = messageModel.IsAdmin;
                if (isAdmin)
                {
                    // 判断是否为 公告指令
                    if (MessageConstant.GONG_GAO.Equals(messageModel.Code))
                    {
                        // 获取所有群信息
                        ModelWithSourceString<IEnumerable<GroupInfo>> modelWithSourceString = _mahuaApi.GetGroupsWithModel();
                        IEnumerable<GroupInfo> groupInfo = modelWithSourceString.Model;
                        foreach (var item in groupInfo)
                        {
                            _mahuaApi.SendGroupMessage(item.Group, tmpStr);
                            _mahuaApi.SendPrivateMessage(context.FromQq, "【" + item.Name + "】 " + item.Group + " 推送成功！");
                        }
                    }
                    // 判断是否为全体成员处理
                    else if (MessageConstant.AITE_ALL.Equals(messageModel.Code))
                    {
                        // 获取所有群信息
                        ModelWithSourceString<IEnumerable<GroupInfo>> modelWithSourceString = _mahuaApi.GetGroupsWithModel();
                        IEnumerable<GroupInfo> groupInfo = modelWithSourceString.Model;
                        tmpStr = "[CQ:at,qq=all]\n" + tmpStr;
                        foreach (var item in groupInfo)
                        {
                            _mahuaApi.SendGroupMessage(item.Group, tmpStr);
                            _mahuaApi.SendPrivateMessage(context.FromQq, "【" + item.Name + "】 " + item.Group + " 推送成功！");
                        }
                    }
                    // 更新金额
                    else if(MessageConstant.UPDATE_MONEY.Equals(messageModel.Code))
                    {
                        _mahuaApi.SendPrivateMessage(context.FromQq, messageModel.SendMessage);
                    }
                    // 群数量
                    else if (MessageConstant.QUN_TOTAL.Equals(messageModel.Code))
                    {
                        ModelWithSourceString<IEnumerable<GroupInfo>> modelWithSourceString = _mahuaApi.GetGroupsWithModel();
                        IEnumerable<GroupInfo> groupInfo = modelWithSourceString.Model;
                        int count = 0;
                        foreach (var item in groupInfo)
                        {
                            count++;
                        }
                        _mahuaApi.SendPrivateMessage(context.FromQq, "群数量："+count.ToString());
                    }
                    else
                    {
                        if (tmpStr != "" && tmpStr.Length > 0)
                        {
                            _mahuaApi.SendPrivateMessage(context.FromQq, tmpStr);
                        }
                    }
                }
                else
                {
                    if (tmpStr != "" && tmpStr.Length > 0)
                    {
                        _mahuaApi.SendPrivateMessage(context.FromQq, tmpStr);
                    }
                }
            }
        }
    }
}

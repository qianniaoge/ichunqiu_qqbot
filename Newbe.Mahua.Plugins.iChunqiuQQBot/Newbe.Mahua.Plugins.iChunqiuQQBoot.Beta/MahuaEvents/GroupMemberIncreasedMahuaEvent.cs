using Newbe.Mahua.MahuaEvents;
using System;

namespace Newbe.Mahua.Plugins.iChunqiuQQBoot.Beta.MahuaEvents
{
    /// <summary>
    /// 群成员增多事件
    /// </summary>
    public class GroupMemberIncreasedMahuaEvent
        : IGroupMemberIncreasedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public GroupMemberIncreasedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessGroupMemberIncreased(GroupMemberIncreasedContext context)
        {
            // 
            string joinedQQ = context.JoinedQq;
            string sendMessage = string.Format("[CQ:at,qq={0}]\n欢迎您加入技术交流群，我是AlphaRebot智能机器人，艾特我回复“指令”两个字可以为您提供i春秋知识库哦。", joinedQQ);
            _mahuaApi.SendGroupMessage(context.FromGroup, sendMessage);
        }
    }
}

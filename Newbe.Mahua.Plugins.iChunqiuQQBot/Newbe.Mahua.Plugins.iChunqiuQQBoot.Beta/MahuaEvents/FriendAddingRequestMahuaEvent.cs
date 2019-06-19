using Newbe.Mahua.MahuaEvents;
using System;

namespace Newbe.Mahua.Plugins.iChunqiuQQBoot.Beta.MahuaEvents
{
    /// <summary>
    /// 好友申请接受事件
    /// </summary>
    public class FriendAddingRequestMahuaEvent
        : IFriendAddingRequestMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public FriendAddingRequestMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessAddingFriendRequest(FriendAddingRequestContext context)
        {
            // 同意好友请求,备注设置为QQ号
            _mahuaApi.AcceptFriendAddingRequest(context.AddingFriendRequestId,context.FromQq,context.FromQq);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.iChunqiuQQBoot.Beta.Tools
{
    class MessageConstant
    {
        // 标题搜索随机文章
        public readonly static string RANDOM_CONTENT_TITLE = "随机文章";
        // 标题搜索最新文章
        public readonly static string DESC_CONTENT_TITLE = "最新文章";
        // 作者搜索随机文章
        public readonly static string RANDOM_CONTENT_AUTHOR = "作者随机";
        // 作者搜索最新文章
        public readonly static string DESC_CONTENT_AUTHOR = "作者最新";
        // 随机教程
        public readonly static string RANDOM_VIDEO = "随机教程";
        // 最新教程
        public readonly static string DESC_VIDEO = "最新教程";
        // 今日最新
        public readonly static string DESC_CONTENT_TODAY = "今日最新";
        // 最新帖子
        public readonly static string DESC_CONTENT = "最新帖子";
        // help
        public readonly static string HELP = "help";
        // 指令
        public readonly static string HELP_TWO = "指令";
        // 公告
        public readonly static string GONG_GAO = "发布";
        // 艾特全体
        public readonly static string AITE_ALL = "艾特全体";
        // 测试
        public readonly static string TEST = "测试";
        // 删除文章
        public readonly static string DELTE_CONTENT = "删除文章";
        // 申请提现
        public readonly static string APPLY_MONEY = "申请提现";
        // 更新作家金额
        public readonly static string UPDATE_MONEY = "QQ：";
        // 查询余额
        public readonly static string SELECT_MONEY = "查询余额";
        // 提现记录
        public readonly static string HISTORY_MONEY = "提现记录";
        // 财富榜
        public readonly static string MONEY_DESC = "财富榜";
        // 作家信息
        public readonly static string MY_DATA = "我的信息";
        // 关于我
        public readonly static string ABOUT = "关于我";
        // 今日使用
        public readonly static string TODAY_COUNT = "今日使用";
        // 总人数
        public readonly static string USER_TOTAL = "总人数";
        // 七日使用
        public readonly static string THIS_WEEK = "7日使用";
        // 群数
        public readonly static string QUN_TOTAL = "群数";

        // 加钱
        public readonly static string ADD_MONEY = "加钱";

        // 提现
        public readonly static string PUT_FORWARD = "提现";

        // 指令内容
        public readonly static string HELP_CONTENT = "\n使用方法：艾特我后，输入以下四个字指令，然后引号内注明你想搜索的词就行（不用打引号），例如最新文章 XSS \n\n" +
                    "查看今天最新的文章请输入：“今日最新”或“最新文章””\n" +
                    "查看指定内容请参考以下指令：\n" +
                    "随机文章 “关键词”\n" +
                    "随机教程“关键词”\n" +
                    "最新文章“关键词”\n" +
                    "最新教程“关键词”\n\n"+
                    "查询指定作者的文章请参考以下指令：\n"+
                    "作者随机“作者名字”\n"+
                    "作者最新“作者名字”\n\n" +
                    "作家团使用指令：\n" +
                    "查询余额\n" +
                    "提现记录\n" +
                    "申请提现\n" +
                    "财富榜\n"+
                    "\nPs：免费提供群内即时搜索服务，邀请我进群即可使用哦。（私聊查询服务即将开通）";
    }
}

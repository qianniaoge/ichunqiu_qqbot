using Newbe.Mahua.Plugins.iChunqiuQQBoot.Beta.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.iChunqiuQQBoot.Beta.Controller
{
    /// <summary>
    /// 文本消息处理
    /// </summary>
    class MessageController
    {
        //private DBHelperMySQL dBHelper = new DBHelperMySQL();
        /// <summary>
        /// 负责文本信息处理
        /// </summary>
        /// <param name="message">文本信息</param>
        /// <returns></returns>
        public static MessageModel main(String message,string fromQQ) {
            string resultContent = "\n无数据";
            string content = message;
            MessageModel messageModel = null;
            if (message == null || message == "" || message.Length == 0)
            {
                messageModel = new MessageModel(sendMessage: MessageConstant.HELP_CONTENT, isAdmin: false, code: "");
                return messageModel;
            }
            resultContent = "\n无数据";
            message = executeCodeReplace(message);
            message = message.Trim();
            if (message.Length == 0 || message == "")
            {
                messageModel = new MessageModel(sendMessage: MessageConstant.HELP_CONTENT, isAdmin: false, code: "");
                return messageModel;
            }
            else if (message.Contains(MessageConstant.RANDOM_CONTENT_TITLE)) {
                // 标题查询随机文章
                message = message.Replace(MessageConstant.RANDOM_CONTENT_TITLE, "").Trim();
                resultContent = DBHelperMySQL.GetContentRandomByTitle(message);
                // 标题查询随机课程
                string classContent = DBHelperMySQL.getClassRandomByTitle(message);
                if (classContent.Length > 0 && !"".Equals(classContent))
                {
                    resultContent += classContent;
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.RANDOM_CONTENT_TITLE);
            }
            else if (message.Contains(MessageConstant.DESC_CONTENT_TITLE)) {
                // 标题查询最新文章
                message = message.Replace(MessageConstant.DESC_CONTENT_TITLE, "").Trim();
                string classContent = "";
                if (message.Length == 0 || message == "" || "".Equals(message) || message == null)
                {
                    // 调用每日最新
                    resultContent = DBHelperMySQL.getContentDateByToday();
                    // 标题查询最新课程
                    classContent = DBHelperMySQL.getClassDateByTitle("");
                }
                else
                {
                    resultContent = DBHelperMySQL.getContentDateByTitle(message);
                    classContent = DBHelperMySQL.getClassDateByTitle(message);
                }
                if (classContent.Length > 0 && !"".Equals(classContent))
                {
                    resultContent += classContent;
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.DESC_CONTENT_TITLE);
            }
            else if (message.Contains(MessageConstant.DESC_CONTENT_AUTHOR)) {
                // 作者查询最新文章
                message = message.Replace(MessageConstant.DESC_CONTENT_AUTHOR, "").Trim();
                resultContent = DBHelperMySQL.getContentDateByAuthor(message);
                // 作者查询最新课程
                string classContent = DBHelperMySQL.getClassDateByAuthor(message);
                if (classContent.Length > 0 && !"".Equals(classContent))
                {
                    resultContent += classContent;
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.DESC_CONTENT_AUTHOR);
            } else if (message.Contains(MessageConstant.RANDOM_CONTENT_AUTHOR)) {
                // 作者查询随机文章
                message = message.Replace(MessageConstant.RANDOM_CONTENT_AUTHOR, "").Trim();
                resultContent = DBHelperMySQL.getContentRandomByAuthor(message);
                // 作者查询随机课程
                string classContent = DBHelperMySQL.getClassRandomByAuthor(message);
                if (classContent.Length > 0 && !"".Equals(classContent))
                {
                    resultContent += classContent;
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.RANDOM_CONTENT_AUTHOR);
            }
            else if (message.Contains(MessageConstant.DESC_CONTENT_TODAY) || message.Contains(MessageConstant.DESC_CONTENT))
            {
                // 今日最新
                message = message.Replace(MessageConstant.DESC_CONTENT_TODAY, "").Trim();
                resultContent = DBHelperMySQL.getContentDateByToday();
                string classContent = DBHelperMySQL.getClassDateByTitle("");
                if (classContent.Length > 0 && !"".Equals(classContent))
                {
                    resultContent += classContent;
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.DESC_CONTENT_TODAY);
            } else if (message.Contains(MessageConstant.GONG_GAO)) {
                // 公告处理
                bool flag = DBHelperMySQL.isAdmin(fromQQ);
                // 判断是否管理员
                if (flag) {
                    message = message.Replace(MessageConstant.GONG_GAO, "来自i春秋机器人的智能推送\n");
                    resultContent = message;
                    messageModel = new MessageModel(sendMessage: resultContent, isAdmin: true, code: MessageConstant.GONG_GAO);
                }
                else {
                    resultContent = "您不是管理员！请联系坏蛋，QQ286894635！";
                    messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.GONG_GAO);
                }
            }
            else if (message.Contains(MessageConstant.AITE_ALL))
            {
                // 艾特全体处理
                bool flag = DBHelperMySQL.isAdmin(fromQQ);
                if (flag)
                {
                    message = message.Replace(MessageConstant.AITE_ALL, "来自i春秋机器人的智能推送\n");
                    resultContent = message;
                    messageModel = new MessageModel(sendMessage: resultContent, isAdmin: true, code: MessageConstant.AITE_ALL);
                }
                else
                {
                    resultContent = "您不是管理员！请联系坏蛋，QQ286894635！";
                    messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.AITE_ALL);
                }
            }
            else if (message.Contains(MessageConstant.HELP) || message.Contains(MessageConstant.HELP_TWO))
            {
                // Help 帮助等指令
                resultContent = MessageConstant.HELP_CONTENT;
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.HELP_TWO);
            } else if (message.Contains(MessageConstant.DESC_VIDEO)) {
                // 标题查询最新教程
                message = message.Replace(MessageConstant.DESC_VIDEO, "").Trim();
                resultContent = DBHelperMySQL.getCourseDateByTitle(message);
                // 标题查询最新课程
                string classContent = DBHelperMySQL.getClassDateByTitle(message);
                if (classContent.Length > 0 && !"".Equals(classContent))
                {
                    resultContent += classContent;
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.DESC_VIDEO);
            }
            else if (message.Contains(MessageConstant.RANDOM_VIDEO))
            {
                // 标题查询随机教程
                message = message.Replace(MessageConstant.RANDOM_VIDEO, "").Trim();
                resultContent = DBHelperMySQL.getCourseRandomByTitle(message);
                // 标题查询随机课程
                string classContent = DBHelperMySQL.getClassRandomByTitle(message);
                if (classContent.Length > 0 && !"".Equals(classContent))
                {
                    resultContent += classContent;
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.RANDOM_VIDEO);
            }
            else if (message.Contains(MessageConstant.TEST))
            {
                // 测试命令
                resultContent = " [CQ:image,file=photo.jpg]";
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.TEST);
            } else if (message.Contains(MessageConstant.SELECT_MONEY)) {
                string tmpStr = message.Replace(MessageConstant.SELECT_MONEY, "").Trim();
                // 判断是否为管理员
                bool isAdmin = DBHelperMySQL.isAdmin(fromQQ);
                string money = "";
                if (tmpStr.Length > 0 && isAdmin)
                {
                    // 查询余额
                    money = DBHelperMySQL.getBlankMoney(tmpStr);
                }
                else
                {
                    // 查询余额
                    money = DBHelperMySQL.getBlankMoney(fromQQ);
                }
                if (money == "" || money == null || money.Length == 0)
                {
                    resultContent = "没有数据，请联系QQ：758841765 给予昵称QQ等信息。";
                }
                else
                {
                    resultContent = "\n当前余额为 " + money;
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: isAdmin, code: MessageConstant.SELECT_MONEY);
            }
            else if (message.Contains(MessageConstant.HISTORY_MONEY))
            {
                // 提现记录
                string historyMoney = DBHelperMySQL.getBlankHistory(fromQQ);
                // 查询当前余额
                string money = DBHelperMySQL.getBlankMoney(fromQQ);
                if (money == "" || money == null || money.Length == 0) {
                    resultContent = "没有数据，请联系QQ：758841765 给予昵称QQ等信息。";
                }
                else {
                    resultContent = "\n当前余额为 " + money + "\n";
                    // 查询历史记录
                    resultContent += "提现记录\n";
                    resultContent += "金额\t\t时间\n" + historyMoney;
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.HISTORY_MONEY);
            }
            else if (message.Contains(MessageConstant.APPLY_MONEY))
            {
                // 申请提现
                resultContent = "请联系【坏蛋】，QQ号：286894635。\n[CQ:at,qq=286894635]";
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.APPLY_MONEY);
            } else if (message.Contains(MessageConstant.ABOUT)) {
                resultContent = "i春秋社区机器人，研发人员为：0nise，产品设计为：坏蛋！";
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.ABOUT);
            } else if (message.Contains(MessageConstant.UPDATE_MONEY)) {
                // 更新金额
                bool isAdmin = DBHelperMySQL.isAdmin(fromQQ);
                if (isAdmin) {
                    try
                    {
                        decimal tmpMoney = 0.0M;
                        // 提现
                        if (message.Contains(MessageConstant.PUT_FORWARD))
                        {
                            int moneyIndex = message.IndexOf(MessageConstant.PUT_FORWARD + "：");
                            int index = message.IndexOf(MessageConstant.UPDATE_MONEY);
                            if (moneyIndex >= 0 && index >= 0)
                            {
                                decimal money = Convert.ToDecimal(message.Substring(moneyIndex).Replace("提现：", "").Replace(" ", ""));
                                if (money > tmpMoney)
                                {
                                    string qq = message.Replace(message.Substring(moneyIndex), "").Replace(MessageConstant.UPDATE_MONEY, "").Replace(" ", "");
                                    resultContent = DBHelperMySQL.updateBlank(qq, money,"0");
                                }
                                else
                                {
                                    resultContent = "提现金额必须大于0！";
                                }
                            }
                        }
                        // 加钱
                        else if (message.Contains(MessageConstant.ADD_MONEY))
                        {
                            int moneyIndex = message.IndexOf(MessageConstant.ADD_MONEY + "：");
                            int index = message.IndexOf(MessageConstant.ADD_MONEY);
                            if (moneyIndex >= 0 && index >= 0)
                            {
                                decimal money = Convert.ToDecimal(message.Substring(moneyIndex).Replace("加钱：", "").Replace(" ", ""));
                                if (money > tmpMoney)
                                {
                                    string qq = message.Replace(message.Substring(moneyIndex), "").Replace(MessageConstant.UPDATE_MONEY, "").Replace(" ", "");
                                    resultContent = DBHelperMySQL.updateBlank(qq, money,"1");
                                }
                                else
                                {
                                    resultContent = "加钱金额必须大于0！";
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        resultContent = "更新失败。";
                    }
                }
                else
                {
                    resultContent = "您不是管理员！请联系坏蛋，QQ286894635！";
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: isAdmin, code: MessageConstant.UPDATE_MONEY);
            }
            else if (message.Contains(MessageConstant.TODAY_COUNT))
            {
                // 今日使用数量
                bool isAdmin = DBHelperMySQL.isAdmin(fromQQ);
                if (isAdmin)
                {
                    int count = DBHelperMySQL.getToday();
                    resultContent = "今日使用人数达"+count+"人";
                }
                else
                {
                    resultContent = "您不是管理员！请联系坏蛋，QQ286894635！";
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: isAdmin, code: MessageConstant.TODAY_COUNT);
            }
            else if (message.Contains(MessageConstant.USER_TOTAL))
            {
                // 总人数
                // 判定是否为管理员
                bool isAdmin = DBHelperMySQL.isAdmin(fromQQ);
                if (isAdmin)
                {
                    int count = DBHelperMySQL.getUserTotal();
                    DateTime dt = DateTime.Now;
                    // 获取日期
                    string date = dt.ToLongDateString().ToString();
                    resultContent = "截止" + date + "，机器人历史使用人数达"+count+"人。";
                }
                else
                {
                    resultContent = "您不是管理员！请联系坏蛋，QQ286894635！";
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: isAdmin, code: MessageConstant.USER_TOTAL);
            }
            else if (message.Contains(MessageConstant.THIS_WEEK))
            {
                // 七日使用
                // 判定是否为管理员
                bool isAdmin = DBHelperMySQL.isAdmin(fromQQ);
                if (isAdmin)
                {
                    Hashtable hashtable = DBHelperMySQL.getThisWeek();
                    resultContent = "";
                    foreach (Object item in hashtable.Keys)
                    {
                        string dateData =  item.ToString();
                        string count = hashtable[item].ToString();
                        resultContent += dateData + " 使用人数为：" + count + "人;\n";
                    }
                }
                else
                {
                    resultContent = "您不是管理员！请联系坏蛋，QQ286894635！";
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: isAdmin, code: MessageConstant.USER_TOTAL);
            }
            else if (message.Contains(MessageConstant.QUN_TOTAL))
            {
                // 群数量
                // 判定是否为管理员
                bool isAdmin = DBHelperMySQL.isAdmin(fromQQ);
                if (isAdmin)
                {
                    resultContent = "群数量";
                }
                else
                {
                    resultContent = "您不是管理员！请联系坏蛋，QQ286894635！";
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: isAdmin, code: MessageConstant.QUN_TOTAL);
            }
            else if (message.Contains(MessageConstant.MONEY_DESC))
            {
                // 财富榜
                bool isAdmin = DBHelperMySQL.isAdmin(fromQQ);
                // 检索是否为做作家团
                if (DBHelperMySQL.exitsUser(fromQQ) || isAdmin)
                {
                    resultContent = DBHelperMySQL.Top30Money();
                }
                else
                {
                    resultContent = "您不是作家团成员！";
                }
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: isAdmin, code: MessageConstant.MONEY_DESC);
            }
            else
            {
                // 其他内容
                resultContent = MessageConstant.HELP_CONTENT;
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: "");
            }

            if (resultContent == "" || resultContent == null || resultContent.Length == 0) {
                messageModel.SendMessage = "\n无数据";
                messageModel = new MessageModel(sendMessage: resultContent, isAdmin: false, code: MessageConstant.HELP);
            }
            // 记录用户操作数据
            DBHelperMySQL.addUserData(fromQQ, content, resultContent);
            return messageModel;
        }

        /// <summary>
        /// 模糊指令替换
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string executeCodeReplace(string message) {
            message = Regex.Replace(message,"随.{2}程","随机教程");
            message = Regex.Replace(message, "随.{2}章", "随机文章");
            message = Regex.Replace(message, "最.{2}章", "最新文章");
            message = Regex.Replace(message, "最.{2}程", "最新教程");
            message = message.Replace("余额查询",MessageConstant.SELECT_MONEY);
            return message;
        }
    }
}

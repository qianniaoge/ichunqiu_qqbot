#!/usr/bin/python
#-*- coding: UTF-8 -*-
#coding=utf-8
'''
i春秋社区机器人插件
link: https://bbs.ichunqiu.com/thread-33770-1-1.html
'''
import sys
import MySQLdb
from DBUtils.PooledDB import PooledDB
# 数据库连接池
pool = PooledDB(MySQLdb,20,host='127.0.0.1',user='root',passwd='root',db='ichunqiu',port=3306,charset='utf8')
def onQQMessage(bot, contact, member, content):
    if '@ME' in content:
        content = str(content).replace('[@ME]  ','')
        if '[author]' in content:
            content = content.replace('[author] ','')
            content = MySQLdb.escape_string(content)
            content = "%"+content+"%"
            connection = pool.connection()
            cursor = connection.cursor()
            cursor.execute("SELECT title,url,author,content_date FROM ichunqiu_content WHERE author LIKE %s ORDER BY content_date DESC LIMIT 0,3",[content])
            all_data = cursor.fetchall()
            if len(all_data) == 0:
                bot.SendTo(contact,'无数据')
            else:
                send_str = ''
                for data_tup in all_data:
                    title = data_tup[0]
                    url = data_tup[1]
                    author = data_tup[2]
                    content_date = data_tup[3]
                    tmp_str = ''
                    tmp_str = title + '\n' + url + '\n'
                    send_str = send_str +'\n'+tmp_str
                bot.SendTo(contact,send_str)
        elif '[content]' in content:
            content = content.replace('[content] ','')
            if len(content) > 2:
                content = "%"+content+"%"
                content = MySQLdb.escape_string(content)
                connection = pool.connection()
                cursor = connection.cursor()
                cursor.execute( "SELECT title,url,author,content_date FROM ichunqiu_content WHERE title LIKE %s ORDER BY content_date DESC  LIMIT 0,3",[content])
                all_data = cursor.fetchall()
                if len(all_data) == 0:
                    bot.SendTo(contact,'无数据')
                else:
                    send_str = ''
                    for data_tup in all_data:
                        title = data_tup[0]
                        url = data_tup[1]
                        author = data_tup[2]
                        content_date = data_tup[3]
                        tmp_str = ''
                        tmp_str = title + '\n' + url + '\n'
                        send_str = send_str +'\n'+tmp_str
                    bot.SendTo(contact,send_str)
            all_data = cursor.fetchall()
            if len(all_data) == 0:
                bot.SendTo(contact,'无数据')
            else:
                send_str = ''
                for data_tup in all_data:
                    title = data_tup[0]
                    url = data_tup[1]
                    author = data_tup[2]
                    content_date = data_tup[3]
                    tmp_str = ''
                    tmp_str = title + '\n' + url + '\n'
                    send_str = send_str +'\n'+tmp_str
                bot.SendTo(contact,send_str)
        elif '[content]' in content:
            content = content.replace('[content] ','')
            if len(content) > 2:
                sql = "SELECT title,url,author,content_date FROM ichunqiu_content WHERE title LIKE '%"+content+"%' ORDER BY content_date DESC  LIMIT 0,3"
                print sql
                connection = pool.connection()
                cursor = connection.cursor()
                cursor.execute(sql)
                all_data = cursor.fetchall()
                if len(all_data) == 0:
                    bot.SendTo(contact,'无数据')
                else:
                    send_str = ''
                    for data_tup in all_data:
                        title = data_tup[0]
                        url = data_tup[1]
                        author = data_tup[2]
                        content_date = data_tup[3]
                        tmp_str = ''
                        tmp_str = title + '\n' + url + '\n'
                        send_str = send_str +'\n'+tmp_str
                    bot.SendTo(contact,send_str)

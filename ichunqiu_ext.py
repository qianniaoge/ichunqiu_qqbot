#!/usr/bin/python
#-*- coding: UTF-8 -*-
#coding=utf-8
import random
import requests
import sys
import MySQLdb
import re
from bs4 import BeautifulSoup
from ThreadPool import *
reload(sys)
sys.setdefaultencoding("utf-8")
requests.packages.urllib3.disable_warnings()
from DBUtils.PooledDB import PooledDB
pool = PooledDB(MySQLdb,20,host='127.0.0.1',user='root',passwd='root',db='ichunqiu',port=3306,charset='utf8')
'''
USER_AGENTS 随机头信息
'''
USER_AGENTS = [
    "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; AcooBrowser; .NET CLR 1.1.4322; .NET CLR 2.0.50727)",
    "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; Acoo Browser; SLCC1; .NET CLR 2.0.50727; Media Center PC 5.0; .NET CLR 3.0.04506)",
    "Mozilla/4.0 (compatible; MSIE 7.0; AOL 9.5; AOLBuild 4337.35; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)",
    "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)",
    "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Win64; x64; Trident/5.0; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET CLR 2.0.50727; Media Center PC 6.0)",
    "Mozilla/5.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET CLR 1.0.3705; .NET CLR 1.1.4322)",
    "Mozilla/4.0 (compatible; MSIE 7.0b; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.2; .NET CLR 3.0.04506.30)",
    "Mozilla/5.0 (Windows; U; Windows NT 5.1; zh-CN) AppleWebKit/523.15 (KHTML, like Gecko, Safari/419.3) Arora/0.3 (Change: 287 c9dfb30)",
    "Mozilla/5.0 (X11; U; Linux; en-US) AppleWebKit/527+ (KHTML, like Gecko, Safari/419.3) Arora/0.6",
    "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.2pre) Gecko/20070215 K-Ninja/2.1.1",
    "Mozilla/5.0 (Windows; U; Windows NT 5.1; zh-CN; rv:1.9) Gecko/20080705 Firefox/3.0 Kapiko/3.0",
    "Mozilla/5.0 (X11; Linux i686; U;) Gecko/20070322 Kazehakase/0.4.5",
    "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.0.8) Gecko Fedora/1.9.0.8-1.fc10 Kazehakase/0.5.6",
    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.56 Safari/535.11",
    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_3) AppleWebKit/535.20 (KHTML, like Gecko) Chrome/19.0.1036.7 Safari/535.20",
    "Opera/9.80 (Macintosh; Intel Mac OS X 10.6.8; U; fr) Presto/2.9.168 Version/11.52",
    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.11 (KHTML, like Gecko) Chrome/20.0.1132.11 TaoBrowser/2.0 Safari/536.11",
    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.71 Safari/537.1 LBBROWSER",
    "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; LBBROWSER)",
    "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; QQDownload 732; .NET4.0C; .NET4.0E; LBBROWSER)",
    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.84 Safari/535.11 LBBROWSER",
    "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)",
    "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; QQBrowser/7.0.3698.400)",
    "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; QQDownload 732; .NET4.0C; .NET4.0E)",
    "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; SV1; QQDownload 732; .NET4.0C; .NET4.0E; 360SE)",
    "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; QQDownload 732; .NET4.0C; .NET4.0E)",
    "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)",
    "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1",
    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1",
    "Mozilla/5.0 (iPad; U; CPU OS 4_2_1 like Mac OS X; zh-cn) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8C148 Safari/6533.18.5",
    "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:2.0b13pre) Gecko/20110307 Firefox/4.0b13pre",
    "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:16.0) Gecko/20100101 Firefox/16.0",
    "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11",
    "Mozilla/5.0 (X11; U; Linux x86_64; zh-CN; rv:1.9.2.10) Gecko/20100922 Ubuntu/10.10 (maverick) Firefox/3.6.10"
]
# 请求超时时间
REQUEST_TIME_OUT = 20
MAX_THREAD = 20
"""
reuqst请求发送
:param url: 需要请求的url
"""
def request_url(url='',method='',data=''):
    HEADER = {
        'User-Agent': random.choice(USER_AGENTS),
        'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
        'Accept-Language': 'en-US,en;q=0.5',
        'Accept-Encoding': 'gzip, deflate',
        'Connection':'keep-alive',
    }
    #返回内容
    result_text = '请求错误'
    break_flag = True
    break_count = 0
    while break_flag:
        try:
            if 'get' in method:
                rs = requests.get(url, data=data, headers=HEADER, timeout=REQUEST_TIME_OUT,verify=False)
                print url
                print 'code ----------',rs.status_code
                if rs.status_code == 200:
                    result_text = rs.text
                else:
                    while True:
                        print 'url ',url
                        print 'start sleep ',60
                        time.sleep(60)
                        rs = requests.get(url, data=data, headers=HEADER, timeout=REQUEST_TIME_OUT,verify=False)
                        if rs.status_code == 200:
                            result_text = rs.text
                            break
            elif 'post' in method:
                rs = requests.get(url, data=data, headers=HEADER, timeout=REQUEST_TIME_OUT,verify=False)
                if rs.status_code == 200:
                    result_text = rs.text
            break_flag = False
        except Exception, e:
            print e
            break_count = break_count + 1
            break_flag = False
        if break_count > 10:
            break
    
    return result_text

"""
ichunqiu爬虫
@param url 爬虫url
"""
def ichunqiu_sipder(url,fid):
    content = request_url(url,'get','')
    soup = BeautifulSoup(content,'lxml')
    content_id = url.replace('https://bbs.ichunqiu.com/thread-','').replace('-1-1.html','')
    title_ext = soup.title.string
    if '提示信息' == title_ext:
        return
    title = soup.find('span',attrs={'id':'thread_subject'}).text
    title = title.replace('\r','').replace('\n','').replace('\t','')
    author = ''
    content_date = ''
    type_id = ''
    a_hre_str = soup.find('a',class_='forum_type').get('href')
    type_id = a_hre_str[a_hre_str.find('&typeid=')+len('&typeid='):]
    for string_tag in soup.find_all('strong'):
        if string_tag.find('a',class_='xi2'):
            author = string_tag.find('a',class_='xi2').string
            break
    for date_div in soup.find_all('div',class_='cl',attrs={'style':'font-size: 12px; color: #888888;'}):
        tmp_str = str(date_div)
        date_arr =  re.findall('\d{4}-\d{1,2}-\d{1,2} \d{1,2}:\d{1,2}:\d{1,2}',tmp_str)
        if len(date_arr) > 0:
            content_date = date_arr[0]
            break
    content_see = '' 
    content_see = soup.find('span',class_='ico_see y').string
    content_comment = ''
    content_comment = soup.find('span',class_='ico_reply y').string
    content_see = MySQLdb.escape_string(content_see)
    content_comment = MySQLdb.escape_string(content_comment)
    content_id = MySQLdb.escape_string(content_id)
    title = MySQLdb.escape_string(title)
    author = MySQLdb.escape_string(author)
    content_date = MySQLdb.escape_string(content_date)
    fid = str(fid)
    fid = MySQLdb.escape_string(fid)
    type_id = str(type_id)
    type_id = MySQLdb.escape_string(type_id)
    connection = pool.connection()
    cursor = connection.cursor()
    sql = "REPLACE INTO ichunqiu_content(id,content_id,title,url,author,content_date,content_see,content_comment,fid,type_id,create_date,update_date) VALUES (DEFAULT,'"+content_id+"','"+title+"','"+url+"','"+author+"','"+content_date+"','"+content_see+"','"+content_comment+"','"+fid+"','"+type_id+"',NOW(),NOW())"
    cursor.execute(sql)
    connection.commit()
    cursor.close()
    connection.close()
    print 'content_id -->',content_id,' ok'

"""
按照板块爬虫
@param fid 板块id
"""
def spider_type(fid):
    url = "https://bbs.ichunqiu.com/forum.php?mod=forumdisplay&fid="+str(fid)+"&orderby=dateline&orderby=dateline&filter=author&page=1"
    content = request_url(url,'get','')
    soup = BeautifulSoup(content,'lxml')
    page_div = soup.find('div',class_='pg')
    if not page_div:
        spider_page(url)
    else:
        page_count = int(page_div.find('label').find('span').string.replace(' ','').replace('/','').replace('共','').replace('页',''))
        for index in range(1,page_count+1):
            url = "https://bbs.ichunqiu.com/forum.php?mod=forumdisplay&fid="+str(fid)+"&orderby=dateline&orderby=dateline&filter=author&page="+str(index)
            spider_page(url,fid)

"""
爬虫每页文章信息
@url 网站url
""" 
def spider_page(url,fid):
    content = request_url(url,'get','')
    soup = BeautifulSoup(content,'lxml')
    tbody_list = soup.find('table',id='threadlisttableid').find_all('tbody')
    main_pool = ThreadPool(MAX_THREAD)
    for tbody in tbody_list:
        id = tbody.get('id')
        id = id.replace('separatorline','').replace('stickthread','').replace('normalthread','').replace('_','')
        if len(id) > 0 and id:
            content_url = 'https://bbs.ichunqiu.com/thread-'+str(id)+'-1-1.html'
            #ichunqiu_sipder(content_url,fid)
            main_pool.run(ichunqiu_sipder,(content_url,fid,),callback=None)
    main_pool.close()

"""
爬虫主函数
"""
def spider_main():
    main_arr = [59,49,60,61,81,77,65,42,76,75]
    for fid in main_arr:
        spider_type(fid)

"""
定时爬虫
"""
def sleep_main(flag):
    while flag:
        print 'start spider while'
        main_arr = [59,49,60,61,81,77,65,42,76,75]
        for fid in main_arr:
            url = "https://bbs.ichunqiu.com/forum.php?mod=forumdisplay&fid="+str(fid)+"&orderby=dateline&orderby=dateline&filter=author&page=1"
            spider_page(url,fid)
        print 'start sleep -->',10*60+'s'
        time.sleep(10*60)

#白帽子分享技术 59
# 热门话题 49
# 逆向破解 60
# 泛安全技术分享 61
# ＳＲＣ部落 81
# 招聘专版 77
# 工具源码分享 65
# 教程书籍分享 42
# 竞赛训练 76
# 课程学习中心 75
if __name__ == '__main__':
    #spider_main()
    sleep_main(True)
    '''
    main_pool = ThreadPool(5)
    main_pool.run(spider_main,(),callback=None)
    flag = True
    main_pool.run(sleep_main,(flag,),callback=None)
    main_pool.close()
    '''
# i春秋社区机器人

效果：

[![](https://github.com/0nise/ichunqiu_qqbot/blob/master/images/images.png)](https://github.com/0nise/ichunqiu_qqbot/blob/master/images/images.png "演示图片")

开发语言：python2.7

一款基于[QQBot](https://github.com/pandolia/qqbot "https://github.com/pandolia/qqbot")的机器人

文章链接：https://bbs.ichunqiu.com/thread-33770-1-1.html

## 文件简介

- spider_ichunqiu_class.py 爬虫i春秋课程信息

- spider_ichunqiu_ext.py 定时爬虫i春秋社区文章，只爬虫每个板块的第一页

- spider_ichunqiu.py 爬虫i春秋全部社区文章

- ichunqiu.sql 数据库结构

- sendQQ.py QQBot插件

- ThreadPool.py 线程池

- update_content.py 定时更新数据数据库与论坛内容保持同步

## 程序更新日志

2018.5.21 修改一些细微的BUG

2018.3.13 添加定时爬虫i春秋社区文章信息

2018.3.9 更新爬虫社区类别以及子类信息

2018.3.1 添加爬虫ｉ春秋课程的脚本

2018.2.4 修复机器人ＳＱＬ注入漏洞

## 相关文章

https://bbs.ichunqiu.com/thread-34956-1-1.html

https://bbs.ichunqiu.com/thread-33770-1-1.html
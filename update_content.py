#!/usr/bin/python
#-*- coding: UTF-8 -*-
#coding=utf-8
import os
import sys
from datetime import datetime
from ichunqiu_ext import *
reload(sys)
sys.setdefaultencoding("utf-8")

"""
定时更新论坛文章信息
"""
def update_content():
    print 'start update content '
    while True:
        now = datetime.now()
        if now.hour == 2:
            print 'run spider content'
            # 更新文章
            spider_main()
        else:
            print 'start sleep '+str(60*60)+'s'
            time.sleep(60*60)

if __name__ == '__main__':
    update_content()
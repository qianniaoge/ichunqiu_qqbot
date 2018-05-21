/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50555
Source Host           : localhost:3306
Source Database       : ichunqiu

Target Server Type    : MYSQL
Target Server Version : 50555
File Encoding         : 65001

Date: 2018-03-09 16:54:46
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for ichunqiu_admin
-- ----------------------------
DROP TABLE IF EXISTS `ichunqiu_admin`;
CREATE TABLE `ichunqiu_admin` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `qq` varchar(255) DEFAULT NULL COMMENT 'QQ号',
  `nick_name` varchar(255) DEFAULT NULL COMMENT '昵称 ',
  `str1` varchar(255) DEFAULT NULL,
  `str2` varchar(255) DEFAULT NULL,
  `str3` varchar(255) DEFAULT NULL,
  `str4` varchar(255) DEFAULT NULL,
  `create_date` datetime DEFAULT NULL,
  `update_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for ichunqiu_blank
-- ----------------------------
DROP TABLE IF EXISTS `ichunqiu_blank`;
CREATE TABLE `ichunqiu_blank` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(255) DEFAULT NULL,
  `user_qq` varchar(255) DEFAULT NULL,
  `user_money` float DEFAULT NULL,
  `str1` varchar(255) DEFAULT NULL,
  `str2` varchar(255) DEFAULT NULL,
  `str3` varchar(255) DEFAULT NULL,
  `str4` varchar(255) DEFAULT NULL,
  `create_date` datetime DEFAULT NULL,
  `update_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ichunqiu_blank
-- ----------------------------

-- ----------------------------
-- Table structure for ichunqiu_class
-- ----------------------------
DROP TABLE IF EXISTS `ichunqiu_class`;
CREATE TABLE `ichunqiu_class` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `class_id` varchar(255) DEFAULT NULL COMMENT '课程编号',
  `author_name` varchar(255) DEFAULT NULL COMMENT '作者名称',
  `class_name` varchar(255) DEFAULT NULL COMMENT '课程名称',
  `class_url` varchar(255) DEFAULT NULL COMMENT '课程链接',
  `create_time` datetime DEFAULT NULL COMMENT '创建时间',
  `part_count` varchar(255) DEFAULT NULL COMMENT '课程总数量',
  `buy_num` varchar(255) DEFAULT NULL COMMENT '购买总数量',
  `str1` varchar(255) DEFAULT NULL,
  `str2` varchar(255) DEFAULT NULL,
  `str3` varchar(255) DEFAULT NULL,
  `str4` varchar(255) DEFAULT NULL,
  `str5` varchar(255) DEFAULT NULL,
  `create_date` datetime DEFAULT NULL,
  `update_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `class_id` (`class_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2380 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for ichunqiu_content
-- ----------------------------
DROP TABLE IF EXISTS `ichunqiu_content`;
CREATE TABLE `ichunqiu_content` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `content_id` varchar(255) DEFAULT '',
  `title` varchar(255) DEFAULT '',
  `url` varchar(255) DEFAULT '',
  `author` varchar(255) DEFAULT '',
  `content_date` date DEFAULT NULL,
  `content_see` varchar(255) DEFAULT '',
  `content_comment` varchar(255) DEFAULT '',
  `fid` varchar(255) DEFAULT '' COMMENT '类别\r\n#白帽子分享技术 59\r\n# 热门话题 49\r\n# 逆向破解 60\r\n# 泛安全技术分享 61\r\n# ＳＲＣ部落 81\r\n# 招聘专版 77\r\n# 工具源码分享 65\r\n# 教程书籍分享 42\r\n# 竞赛训练 76\r\n# 课程学习中心 75',
  `type_id` varchar(255) DEFAULT '' COMMENT '类别编号',
  `create_date` datetime DEFAULT NULL,
  `update_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `content_id` (`content_id`)
) ENGINE=InnoDB AUTO_INCREMENT=28727 DEFAULT CHARSET=utf8;



#圈子板块
 DROP TABLE IF EXISTS `community_group_classify`;
CREATE TABLE IF NOT EXISTS `community_group_classify`(
`id` int UNSIGNED not null auto_increment,
`name` varchar(20) not null,
`description` varchar(5000) NOT null, #板块描述
`order` int UNSIGNED DEFAULT 0,
`is_offline`  TINYINT (1) UNSIGNED NOT NULL DEFAULT 0,
`gmt_create` datetime NOT NULL,
`gmt_modified` datetime DEFAULT NULL,
PRIMARY KEY (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

#圈子信息
 DROP TABLE IF EXISTS `community_group_info`;
 CREATE TABLE IF NOT EXISTS `community_group_info`(
 `id` BIGINT UNSIGNED NOT NULL auto_increment,
 `name` varchar(50) NOT NULL,
 `created_user` char(36) not NULL,#圈子创建人
 `classify_id` int not null,#圈子板块id
 `description` varchar(5000) NOT null, #圈子描述
 `cover_url` varchar(200) not NULL,
 `order` int not null DEFAULT 0,#圈子排序
 `is_hot` TINYINT (1) UNSIGNED NOT NULL DEFAULT 0,#是否热门
 `is_offline`  TINYINT (1) UNSIGNED NOT NULL DEFAULT 0,#是否删除
 `gmt_create` datetime NOT NULL,#创建时间
 `gmt_modified` datetime DEFAULT NULL,#修改时间
 PRIMARY KEY (`id`)
 )ENGINE=InnoDB DEFAULT CHARSET=utf8;

#圈子成员表
 DROP TABLE IF EXISTS `community_group_user`;
 CREATE TABLE IF NOT EXISTS `community_group_user`(
 `id` BIGINT UNSIGNED NOT NULL auto_increment,
 `group_id` BIGINT UNSIGNED  not null,#圈子id
 `user_id` char(36) not null,#成员id
 `gmt_create` datetime NOT NULL,#创建时间
 `gmt_modified` datetime DEFAULT NULL,#修改时间
 PRIMARY KEY (`id`) 
 )ENGINE=InnoDB DEFAULT CHARSET=utf8;


#圈子帖子
DROP TABLE IF EXISTS `community_group_post`;
CREATE TABLE IF NOT EXISTS `community_group_post`(
`id` BIGINT UNSIGNED NOT NULL auto_increment,
`group_id` BIGINT UNSIGNED  NOT NULL,
`title` varchar(500) NOT NULL,
`content_id` BIGINT NOT null,
`author_id` CHAR(36) NOT NULL,
`is_offline` TINYINT (1) UNSIGNED NOT NULL DEFAULT 0,
`like_count` int NOT NULL DEFAULT 0,
`comment_count` int NOT NULL DEFAULT 0,
`collect_count` int NOT NULL DEFAULT 0,
`gmt_create` datetime NOT NULL,#创建时间
`gmt_modified` datetime DEFAULT NULL,#修改时间
 PRIMARY KEY (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

#圈子正文
DROP TABLE IF EXISTS `community_group_post_content`;
CREATE TABLE IF NOT EXISTS `community_group_post_content`(
`id` BIGINT UNSIGNED NOT NULL auto_increment,
`post_id` BIGINT NOT NULL,
`content` text CHARACTER SET 'utf8mb4' not null,
`gmt_create` datetime NOT NULL,#创建时间
`gmt_modified` datetime DEFAULT NULL,#修改时间
PRIMARY KEY(`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;


#圈子评论
DROP TABLE IF EXISTS `community_group_comment`;
CREATE TABLE IF NOT EXISTS `community_group_comment` (
	`id` BIGINT UNSIGNED NOT NULL auto_increment,
	`post_id` BIGINT UNSIGNED NOT NULL,	
	`reply_comment_id` BIGINT UNSIGNED NOT NULL DEFAULT 0,
	`reply_user_id` CHAR(36) NOT NULL,
	`author_id` CHAR(36) NOT NULL,
	`content` VARCHAR (5000)  CHARACTER SET 'utf8mb4' NOT NULL DEFAULT '',
	`is_offline` TINYINT (1) UNSIGNED NOT NULL DEFAULT 0,
	`gmt_create` datetime NOT NULL,
	`gmt_modified` datetime DEFAULT NULL,
	PRIMARY KEY (`id`)
) ENGINE = INNODB DEFAULT CHARSET = utf8;
#圈子帖子喜欢
DROP TABLE
IF EXISTS `community_group_post_like`;
CREATE TABLE
IF NOT EXISTS `community_group_post_like` (
	`id` BIGINT UNSIGNED NOT NULL auto_increment,
	`post_id` BIGINT UNSIGNED NOT null,
	`user_id` CHAR (36) NOT NULL,
	`gmt_create` datetime NOT NULL,
	`mgt_modified` datetime DEFAULT NULL,
	PRIMARY KEY (`id`)
) ENGINE = INNODB charset = utf8;
#圈子评论喜欢数
DROP TABLE
IF EXISTS `community_group_comment_like`;
CREATE TABLE
IF NOT EXISTS `community_group_comment_like` (
	`id` BIGINT UNSIGNED NOT NULL auto_increment,
	`comment_id` BIGINT UNSIGNED NOT null,
	`user_id` CHAR (36) NOT NULL,
	`gmt_create` datetime NOT NULL,
	`mgt_modified` datetime DEFAULT NULL,
	PRIMARY KEY (`id`)
) ENGINE = INNODB charset = utf8;

#圈子帖子评论喜欢表 
DROP TABLE IF EXISTS `community_group_comment_like_count`;
CREATE TABLE `community_group_comment_like_count` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `comment_id` BIGINT UNSIGNED NOT NULL,
  `count` int NOT NULL,
  `gmt_create` date NOT NULL,
  `gmt_modified` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  CHARSET=utf8;

#帖子举报
DROP TABLE IF EXISTS `community_group_post_report`;
CREATE TABLE `community_group_post_report`(
`id` BIGINT(20) UNSIGNED not NULL auto_increment,
`post_id` BIGINT UNSIGNED NOT NULL,
`report_author_id` char(36) not null,
`report_reason` varchar(200) not null,
`audit_status` varchar(20) not null default 'pending',
`gmt_create` datetime NOT NULL,
`gmt_modified` datetime DEFAULT NULL,
PRIMARY KEY(`id`)
)ENGINE=INNODB CHARSET=utf8;
select 'commun ty_group_post_report';




#评论类型表
#community_target_type
DROP TABLE
IF EXISTS `community_target_type`;
CREATE TABLE
IF NOT EXISTS `community_target_type` (
	`id` INT UNSIGNED NOT NULL auto_increment,
	`name` VARCHAR (50) NOT NULL,
	`value` TINYINT UNSIGNED NOT NULL,
	PRIMARY KEY (`id`)
) ENGINE = INNODB DEFAULT CHARSET = utf8;

SELECT
	'community_target_type';

-- 
#评论表
#community_comment
DROP TABLE
IF EXISTS `community_comment`;
CREATE TABLE
IF NOT EXISTS `community_comment` (
	`id` CHAR (36) NOT NULL,
	`target_type` TINYINT UNSIGNED NOT NULL,
	`target_id` CHAR (36) NOT NULL,
	`reply_comment_id` CHAR (36) DEFAULT NULL,
	`reply_user_id` CHAR(36) DEFAULT NULL,
	`author_id` CHAR(36) NOT NULL,
	`content` VARCHAR (5000) CHARACTER SET 'utf8mb4' DEFAULT '',
	`is_offline` TINYINT (1) UNSIGNED NOT NULL DEFAULT 0,
	`gmt_create` datetime NOT NULL,
	`gmt_modified` datetime DEFAULT NULL,
	PRIMARY KEY (`id`)
) ENGINE = INNODB DEFAULT CHARSET = utf8;

SELECT
	'community_comment';

#评论历史表
#community_comment_history
DROP TABLE
IF EXISTS `community_comment_history`;
CREATE TABLE
IF NOT EXISTS `community_comment_history` (
	`id` BIGINT UNSIGNED NOT NULL auto_increment,
	`comment_id` CHAR (36) NOT NULL,
	`target_type` TINYINT UNSIGNED NOT NULL,
	`target_id` CHAR (36) NOT NULL,
	`parget_id` CHAR (36) DEFAULT NULL,
	`content` VARCHAR (5000) DEFAULT '',
	`is_offline` TINYINT (1) UNSIGNED NOT NULL DEFAULT 0,
	`operation_type` VARCHAR (10),
	#INSERT|DELETE|UPDATED
	`gmt_create` datetime NOT NULL,
	`gmt_modified` datetime DEFAULT NULL,
	PRIMARY KEY (`id`)
) ENGINE = INNODB charset = UTF8;

SELECT
	'community_comment_history';

#资源评论喜欢表
#community_like
DROP TABLE
IF EXISTS `community_like`;
CREATE TABLE
IF NOT EXISTS `community_like` (
	`id` BIGINT UNSIGNED NOT NULL auto_increment,
	`target_type` TINYINT UNSIGNED NOT NULL,
	`target_id` CHAR (36) NOT NULL,
	`comment_id` CHAR(36) NOT null,
	`user_id` CHAR (36) NOT NULL,
	`gmt_create` datetime NOT NULL,
	`mgt_modified` datetime DEFAULT NULL,
	PRIMARY KEY (`id`)
) ENGINE = INNODB charset = utf8;

SELECT
	'community_like';

#喜欢历史表
#community_like_history
DROP TABLE
IF EXISTS `community_like_history`;
CREATE TABLE
IF NOT EXISTS `community_like_history` (
	`id` BIGINT UNSIGNED NOT NULL auto_increment,
	`like_id` BIGINT UNSIGNED NOT NULL,
	`target_type` TINYINT UNSIGNED NOT NULL,
	`target_id` CHAR (36) NOT NULL,
	`operation_type` VARCHAR (10),
	#INSERT|DELETE|UPDATED
	`gmt_create` datetime NOT NULL,
	`mgt_modified` datetime DEFAULT NULL,
	PRIMARY KEY (`id`)
) ENGINE = INNODB charset = utf8;

SELECT
	'community_like_history';
#资源评论喜欢表
DROP TABLE IF EXISTS `community_comment_like_count`;
CREATE TABLE `community_comment_like_count` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `comment_id` char(36) NOT NULL,
  `count` int(11) NOT NULL,
  `gmt_create` date NOT NULL,
  `gmt_modified` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  CHARSET=utf8;
SELECT
	'community_comment_like_count';
#资源喜欢表
DROP TABLE IF EXISTS `community_resource_like_count`;
CREATE TABLE `community_resource_like_count` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `resource_id` char(36) NOT NULL,
	`resource_type` TINYINT NOT NULL,
  `count` int(11) NOT NULL,
  `gmt_create` date NOT NULL,
  `gmt_modified` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  CHARSET=utf8;
SELECT
	'community_resource_like_count';

DROP TABLE IF EXISTS `community_comment_delete_history`;
CREATE TABLE `community_comment_delete_history` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `comment_id` char(36) NOT NULL,
	`reason` varchar(100) DEFAULT '',  
  `gmt_create` date NOT NULL,
  `gmt_modified` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB CHARSET=utf8;
SELECT
	'community_comment_delete_history';


DROP TABLE IF EXISTS `community_comment_report`;
CREATE TABLE `community_comment_report`(
`id` BIGINT(20) UNSIGNED not NULL auto_increment,
`comment_id` char(36) NOT NULL,
`comment_content`VARCHAR (5000) not null ,
`report_author_name` varchar(50) not null,
`report_author_id` char(36) not null,
`report_reason` varchar(200) not null,
`audit_status` varchar(20) not null default 'pending',
`gmt_create` datetime NOT NULL,
`gmt_modified` datetime DEFAULT NULL,
PRIMARY KEY(`id`)
)ENGINE=INNODB CHARSET=utf8;
select 'community_comment_report';

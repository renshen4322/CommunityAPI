#community_base_user
DROP TABLE IF EXISTS `community_base_user`;
 CREATE TABLE if not exists `community_base_user` (
  	`id` char(36)  NOT NULL,
  	`user_base_id` int not null,
	`nick_name` varchar(20) DEFAULT NULL,
	`real_name` varchar(20) DEFAULT NULL,
	`intro` varchar(200) default null,
	`birthday` varchar(100) default null,
	`gender` varchar(10) default 'x',
	`email` varchar(100) DEFAULT NULL,
  	`phone` varchar(20) DEFAULT NULL,
	`area_code` varchar(10) DEFAULT NULL,
	`user_role` varchar(20) not NULL DEFAULT 'customer',
	`created_date` datetime not null,	
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


#community_designer_meta
DROP TABLE IF EXISTS `community_designer_meta`;
 CREATE TABLE if not exists `community_designer_meta` (
  `id` INT NOT NULL auto_increment,
	`base_user_id` char(36) not null,
	`design_age` TINYINT not null default 0,
	`created_date` datetime not null,	
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#community_supplier_meta
DROP TABLE IF EXISTS `community_supplier_meta`;
 CREATE TABLE if not exists `community_supplier_meta` (
  `id` INT NOT NULL auto_increment,
	`base_user_id` char(36) not null,
	`mobile` varchar(20) default null,
	`created_date` datetime not null,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

 #community_address
DROP TABLE IF EXISTS `community_address`;
 CREATE TABLE if not exists `community_address` (
 	 `id` INT NOT NULL auto_increment,
	`user_id` char(36) not null,
	`type` varchar(100) not null,
	`country_id` int DEFAULT null,
	`province_id` int DEFAULT null,
	`city_id` int DEFAULT null,
	`district_id` int DEFAULT null,
	`street` varchar(500) DEFAULT NULL,
	`created_date` datetime not null,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#community_user_images
DROP TABLE IF EXISTS `community_user_images`;
CREATE TABLE IF NOT EXISTS `community_user_images`(
`id` int not null auto_increment,
`user_id` char(36) not null,
`type` varchar(20) not null,
`img_url`	varchar(200) not null,
`is_used` TINYINT not null DEFAULT 0,
`created_date` datetime not null,
PRIMARY KEY(`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;



#community_category_type
DROP TABLE IF EXISTS `community_category_type`;
 CREATE TABLE if not exists `community_category_type` (
  `id` INT NOT NULL auto_increment,
	`type_name` varchar(50) not null,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#community_category
DROP TABLE IF EXISTS `community_category`;
 CREATE TABLE if not exists `community_category` (
  `id` INT NOT NULL auto_increment,
	`type_id` int not null,
	`parent_id` int not null DEFAULT 0,
	`name` varchar(50) not null,
	`en_name` varchar(50) not null,
	`sys_name` varchar(50) not null,
	`is_system_category` TINYINT not NULL DEFAULT 0,
	`off_line` TINYINT not null default 0,
	`is_multiple` TINYINT not null DEFAULT 0,
	`is_hot` TINYINT not null default 0,
	`display_index` int not null DEFAULT 0,
	`created_date` datetime not null,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


#community_category_relationships
DROP TABLE IF EXISTS `community_category_relationships`;
 CREATE TABLE if not exists `community_category_relationships` (
  `id` INT NOT NULL auto_increment,
	`category_id` int not null,
	`object_id` char(36) not null,
	`created_date` datetime not null,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#community_works
DROP TABLE IF EXISTS `community_works`;
CREATE TABLE if not exists `community_works` (
  `id` char(36) NOT NULL,
  `import_type` varchar(50) NOT NULL,
  `name` varchar(30) NOT NULL,
  `user_id` char(36) NOT NULL,
  `origin_id` int(11) DEFAULT NULL,
  `thumbnail` varchar(100) NOT NULL,
  `introduction` varchar(1000) NOT NULL,
  `description` text CHARACTER SET 'utf8mb4',
  `pano_url` varchar(1000) DEFAULT NULL,
  `design_date` datetime NOT NULL,
  `pano_thumbnail` varchar(1000) DEFAULT NULL,
  `images` varchar(1000) DEFAULT NULL,
  `image_thumbnail` varchar(1000) DEFAULT NULL,
  `created_date` datetime NOT NULL,
  `off_line` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `id` (`id`,`off_line`),
  KEY `id_2` (`id`,`off_line`),
  KEY `index_id_offLine` (`id`,`off_line`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8

#community_works_meta
DROP TABLE IF EXISTS `community_works_meta`;
CREATE TABLE IF NOT EXISTS `community_works_meta`(
`id` int not null auto_increment,
`works_id` char(36) not null,
`cost` DECIMAL(20,10) default null,
`actual_area` float default null,
`helper` varchar(200) default null,	
`is_hot`  TINYINT not null default 0,
 PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


 #community_works_items
DROP TABLE IF EXISTS `community_works_items`;
 CREATE TABLE if not exists `community_works_items` (
  `id` INT NOT NULL auto_increment,
	`works_id`  char(36) not null,
	`owner_origin_id` int not null,
	`product_origin_id` int not null,
	`img_url`  varchar(100) not null,
	`product_id` char(36) DEFAULT null,
	`name`  varchar(20) not null,
	`created_date` datetime not null,
	`updated_date` datetime DEFAULT null,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#community_product
DROP TABLE IF EXISTS `community_product`;
 CREATE TABLE if not exists `community_product` (
  `id` char(36) NOT NULL,
  `origin_id` int(11) DEFAULT NULL,
  `user_id` char(36) NOT NULL,
  `import_type` varchar(100) NOT NULL,
  `name` varchar(30) NOT NULL,
  `thumbnail` varchar(100) DEFAULT NULL,
  `images` varchar(1000) NOT NULL,
  `image_thumbnail` varchar(1000) NOT NULL,
  `cost` decimal(20,5) DEFAULT NULL,
  `introduction` varchar(1000) NOT NULL,
  `description` text CHARACTER SET 'utf8mb4' NOT NULL,
  `created_date` datetime NOT NULL,
  `off_line` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `index_origin_id` (`origin_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



#community_product_meta
DROP TABLE IF EXISTS `community_product_meta`;
 CREATE TABLE if not exists `community_product_meta` (
  `id` INT NOT NULL  auto_increment,
	`product_id` char(36) not null,	
	`is_hot`  TINYINT not null default 0,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;




#community_news
DROP TABLE IF EXISTS `community_news`;
 CREATE TABLE if not exists `community_news` (
  `id` char(36)  NOT NULL,
	`title` varchar(50) not null,
	`news_url` varchar(100) not null,
	`thumbnail` varchar(100) not null,
	`introduction` varchar(1000) not null,
	`off_line` TINYINT not null default 0,
	`is_hot` TINYINT not null default 0,
	`created_date` datetime not null,	
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


#community_works_qindex 作品索引创建队列表
DROP TABLE IF EXISTS `community_works_qindex`;
 CREATE TABLE if not exists `community_works_qindex` (
  `id` INT NOT NULL auto_increment,
	`works_id` char(36) not null,
	`state_name` varchar(20) default null,
	`type` VARCHAR(50) not null,
	`created_date` datetime not null,
	`updated_date` datetime default null,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#community_sys_image
 DROP table IF EXISTS `community_sys_image`;
CREATE TABLE if not EXISTS `community_sys_image`(
`id` int NOT NULL auto_increment,
`project_id` varchar(200) not null,
`image_name` varchar(200) DEFAULT null,
`image_uri` varchar(500) not null,
`description` varchar(500) default null,
`created_date` datetime not null,
primary KEY(`id`)
) ENGINE=INNODB DEFAULT CHARSET=utf8;
#community_sys_project_name	
DROP table IF EXISTS `community_sys_project_name`;
CREATE TABLE if not EXISTS `community_sys_project_name`(
`id` int NOT NULL auto_increment,
`name` varchar(200) not null,
`created_date` datetime not null,
primary KEY(`id`)
) ENGINE=INNODB DEFAULT CHARSET=utf8;

#community_user_tag	
DROP table IF EXISTS `community_user_tag`;
CREATE TABLE if not EXISTS `community_user_tag`(
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `user_id` char(36) NOT NULL,
  `user_role` varchar(20) NOT NULL,
  `created_date` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8;






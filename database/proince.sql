/*
SQLyog Ultimate v12.09 (64 bit)
MySQL - 5.7.11-log : Database - vida_community
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`vida_community` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `vida_community`;

/*Table structure for table `province` */

DROP TABLE IF EXISTS `province`;

CREATE TABLE `province` (
  `id` int(11) NOT NULL,
  `name` varchar(50) NOT NULL,
  `orderid` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `province` */

insert  into `province`(`id`,`name`,`orderid`) values (110000,'北京市',0),(120000,'天津市',0),(130000,'河北省',0),(140000,'山西省',0),(150000,'内蒙古自治区',0),(210000,'辽宁省',0),(220000,'吉林省',0),(230000,'黑龙江省',0),(310000,'上海市',0),(320000,'江苏省',0),(330000,'浙江省',0),(340000,'安徽省',0),(350000,'福建省',0),(360000,'江西省',0),(370000,'山东省',0),(410000,'河南省',0),(420000,'湖北省',0),(430000,'湖南省',0),(440000,'广东省',0),(450000,'广西壮族自治区',0),(460000,'海南省',0),(500000,'重庆市',0),(510000,'四川省',0),(520000,'贵州省',0),(530000,'云南省',0),(540000,'西藏自治区',0),(610000,'陕西省',0),(620000,'甘肃省',0),(630000,'青海省',0),(640000,'宁夏回族自治区',0),(650000,'新疆维吾尔自治区',0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

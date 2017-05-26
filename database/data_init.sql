#目录资源类别初始化
insert into community_category_type(`type_name`) values('works');
insert into community_category_type(`type_name`) values('product');
insert into community_category_type(`type_name`) values('news');
select * from community_category_type;

#系统分类
insert into community_category(`type_id`,`parent_id`,`name`,`off_line`,`is_hot`,`display_index`,`created_date`,`is_multiple`,`en_name`,`sys_name`,`is_system_category`)
 values(1,0,"风格"	,0	,0	,0	,NOW(),0,'Style','Style',1);
insert into community_category(`type_id`,`parent_id`,`name`,`off_line`,`is_hot`,`display_index`,`created_date`,`is_multiple`,`en_name`,`sys_name`,`is_system_category`)
 values(1,0,"空间"	,0	,0	,0	,NOW(),0,'Space','Space',1);
insert into community_category(`type_id`,`parent_id`,`name`,`off_line`,`is_hot`,`display_index`,`created_date`,`is_multiple`,`en_name`,`sys_name`,`is_system_category`)
 values(1,0,"面积"	,0	,0	,0	,NOW(),0,'Area','Area',1);
 #新闻
 insert into community_category(`type_id`,`parent_id`,`name`,`off_line`,`is_hot`,`display_index`,`created_date`,`is_multiple`,`en_name`,`sys_name`,`is_system_category`)
 values(3,0,"分类"	,0	,0	,0	,NOW(),0,'Style','Style',1);
select * from community_category;


#测试数据
#物品目录
insert into community_category values(1,2,	0	,"风格"	,0	,0	,0	,NOW());
insert into community_category values(2,"2"	,"0"	,"空间"	,"0"	,"0"	,"0"	,NOW());
insert into community_category values(3,"2"	,"0"	,"面积"	,"0"	,"0"	,"0"	,NOW());
insert into community_category values(4,"2"	,"1"	,"北欧"	,"0"	,"0"	,"0"	,NOW());
insert into community_category values(5,"2"	,"1"	,"唯美"	,"0"	,"0"	,"0"	,NOW());
insert into community_category values(6,"2"	,"2"	,"卧室"	,"0"	,"0"	,"0"	,NOW());
insert into community_category values(7,"2"	,"2"	,"厨房"	,"0"	,"0"	,"0"	,NOW());
#作品目录
insert into community_category values(8,1,	0	,"风格"	,0	,0	,0	,NOW());
insert into community_category values(9,1	,"0"	,"空间"	,"0"	,"0"	,"0"	,NOW());
insert into community_category values(10,1	,"0"	,"面积"	,"0"	,"0"	,"0"	,NOW());
insert into community_category values(11,1	,"8"	,"北欧"	,"0"	,"0"	,"0"	,NOW());
insert into community_category values(12,1	,"8"	,"唯美"	,"0"	,"0"	,"0"	,NOW());
insert into community_category values(13,1	,"9"	,"卧室"	,"0"	,"0"	,"0"	,NOW());
insert into community_category values(14,1,"0"	,"厨房"	,"0"	,"0"	,"0"	,NOW());
#新闻目录
insert into community_category(`type_id`,`parent_id`,`name`,`off_line`,`is_hot`,
`display_index`,`created_date`,`is_multiple`,`en_name`,`sys_name`,`is_system_category`) 
values(3,0,'空间',0,0,0,NOW(),0,'SPACE','SPACE',1);
insert into community_category(`type_id`,`parent_id`,`name`,`off_line`,`is_hot`,
`display_index`,`created_date`,`is_multiple`,`en_name`,`sys_name`,`is_system_category`) 
values(3,0,'家具',0,0,0,NOW(),0,'SPACE','SPACE',1);
insert into community_category(`type_id`,`parent_id`,`name`,`off_line`,`is_hot`,
`display_index`,`created_date`,`is_multiple`,`en_name`,`sys_name`,`is_system_category`) 
values(3,0,'工业',0,0,0,NOW(),0,'SPACE','SPACE',1);
insert into community_category(`type_id`,`parent_id`,`name`,`off_line`,`is_hot`,
`display_index`,`created_date`,`is_multiple`,`en_name`,`sys_name`,`is_system_category`) 
values(3,0,'三维',0,0,0,NOW(),0,'SPACE','SPACE',1);

select * from community_category;
#删除指定id的作品

set @id='7fcb6346-33cd-4854-9665-e579cbbf1e4f';
delete from community_works_meta 
where works_id=@id;
delete from community_works_items
where works_id=@id;
delete from community_category_relationships
where object_id=@id;
delete from community_works
where id=@id;
select * from community_works where id=@id;
select * from community_works_meta where works_id=@id;
select * from community_works_items where works_id=@id;
select * from community_category_relationships where object_id=@id;





#对应作品创建评论数据
insert into community_comment(`id`,`target_type`,`target_id`,`author_id`,`content`,`gmt_create`)
select UUID() as 'id',1 as target_type,cw.id as 'target_id',cw.user_id as 'author_id','nihao' 'content',now() as 'gmt_create' from  community_works as cw;
select * from community_comment;

#创建测试举报数据
insert into community_comment_report(`comment_id`,`comment_content`,`report_author_name`,`report_author_id`,`report_reason`,`audit_status`,`gmt_create`)
select cc.id as 'comment_id',cc.content as 'comment_content', cc.author_id as 'report_author_id','昵称' as 'report_author_name','huangdudu'as 'report_reason','pending' as 'audit_status',NOW() as 'gmt_create'   from community_comment as cc;

select * from community_comment_report;
#根据目录id查询作品信息
select cc.`name` as 'Style', t.* from(
select cw.`id` as 'works_id', cw.`name` as 'name',
cbu.`nick_name` as 'author',cui.img_url as 'avatar_url',
cbu.`id` as 'author_id',cw.introduction as 'introduction',
cw.thumbnail as 'thumbnail', cw.created_date as 'created_at' from community_works as cw
INNER JOIN community_base_user as cbu
on cw.user_id=cbu.id
LEFT JOIN community_user_images as cui
on cbu.id=cui.user_id and cui.type='Avatar' and cui.is_used=1
where cw.`id` in (select object_id from community_category_relationships
where (category_id=12) 
GROUP BY object_id
HAVING count(object_id)>0)
ORDER BY cw.created_date desc
LIMIT 0,10) as t
LEFT  join community_category_relationships as ccr
on t.works_id=ccr.object_id
left join community_category as cc
on ccr.category_id=cc.id
ORDER BY t.created_at desc
;

#根据目录id查询指定产品
select cc.`name` as 'Style',t.* from (
select cp.id as `product_id`,cp.cost as `cost`,
cp.`name` as `name`,cp.user_id as `author_id`,
cp.introduction as `introduction`,cp.thumbnail as`thumbnail`,
cp.created_date as `created_date`,cbu.nick_name as `author`,
cui.img_url as `avatar_url`
 from community_product as cp 
INNER JOIN community_base_user as cbu 
on cp.user_id=cbu.id 
LEFT JOIN community_user_images as cui 
on cbu.id=cui.user_id and cui.type='Avatar' and cui.is_used=1 
where cp.id in (select object_id from community_category_relationships 
where (category_id=11) 
GROUP BY object_id 
HAVING count(object_id)>0)
ORDER BY cp.created_date desc 
limit 0,10) as t
LEFT  join community_category_relationships as ccr
on t.product_id=ccr.object_id
left join community_category as cc
on ccr.category_id=cc.id
ORDER BY t.created_date desc 
;
#根据目录id查询指定新闻
select cn.title,cn.news_url,cn.introduction,cn.thumbnail,cn.created_date from community_news as cn
where cn.id in (select object_id from community_category_relationships 
where (category_id=5||category_id=22)
GROUP BY object_id 
HAVING count(object_id)>1)
ORDER BY cn.created_date desc 
limit 0,10
;

#获取指定id的对象的目录信息
select t2.id as 'parent_id',t2.name as 'parent_name',t1.id as 'id',t1.name as 'name' from
(select * from community_category
where id in(
select category_id from community_category_relationships
where object_id='fe7a6c15-bf7f-11e6-8dad-d8cb8ac1c5a2'
)) as t1
INNER JOIN community_category as t2
on t2.id=t1.parent_id
ORDER BY parent_id 
;


#根据设计师id获取作品列表
select t.*,cc.name as 'Stype' from
(select cw.id as 'WorksId',
cw.name as 'Name',cw.user_id as 'OwnerId',
cw.introduction as 'Introduction',cw.pano_url as 'PanoUrl'
,cw.pano_thumbnail as 'PanoThumbnail',cw.images as 'Images',
cw.image_thumbnail as 'ImageThumbnail', cw.design_date as 'DesignDate',
cw.created_date as 'CreatedDate'
 from community_works as cw
where cw.user_id='b66e4a43-e758-4902-a650-e25a77c90d94'
order by cw.created_date desc
LIMIT 20,20 ) as t
left join community_category_relationships as ccr
on t.WorksId=ccr.object_id
left join community_category as cc
on ccr.category_id=cc.id and cc.parent_id in (
select id from community_category
where sys_name='Style' and type_id in(
select id from community_category_type
where type_name='works'));
#根据供应商id获取产品列表
select t.*,cc.`name` as 'Stype' from
(select cp.id as 'ProductId',cp.`name` as 'Name',
cp.user_id as 'OwnerId',cp.introduction as 'Introduction',
cp.images as 'Images',cp.image_thumbnail as 'ImageThumbnail',
cp.created_date as 'CreatedDate'
 from community_product as cp
where cp.user_id='7906f1ff-bf6f-4efd-912c-6d12b9f28cd3' && cp.off_line=0
order by cp.created_date desc
LIMIT 20,10
) as t
LEFT  join community_category_relationships as ccr
on t.ProductId=ccr.object_id
LEFT join community_category as cc
on ccr.category_id=cc.id and cc.parent_id in (
select id from community_category
where sys_name='Style' and type_id in(
select id from community_category_type
where type_name='product'
))
;




#获取指定用户的地址信息
select p.id as 'ProvinceId',p.`name` as 'ProvinceName',
c.id as 'CityId',c.`name` as 'CityName',
d.id as 'DistrictId',d.`name` as 'DistrictName'
 from community_address as ca
left join province as p
on ca.province_id=p.id
LEFT JOIN city as c
on c.id=ca.city_id
LEFT JOIN district as d
on d.id=ca.district_id
where ca.type='Home' and ca.user_id='8bf9e8d8-46d7-4108-aa23-58f0b276d7da'
;

#根据产品名或用户昵称搜索产品列表
select cp.id as 'ProductId',cp.cost as 'Cost',
cp.`name` as 'Name',cbu.nick_name as 'Author',
cui.img_url as 'AvatarUrl',cp.user_id as 'AuthorId',
cp.introduction as 'Introduction',cp.thumbnail as 'Thumbnial',
cp.created_date as 'CreatedDate'
 from community_product as cp
inner join community_base_user as cbu
LEFT JOIN community_user_images as cui
on cp.user_id=cui.user_id and cui.is_used=1
where (cp.`name` like '%12%'|| cbu.nick_name like '%s2tr%')&& cp.off_line=0
order by cp.created_date desc
LIMIT 0,10
;

#根据作品名或用户昵称搜索作品列表
select cw.id as 'WorksId',cw.`name` as 'Name',
cbu.nick_name as 'Author',
cui.img_url as 'AvatarUrl',cw.user_id as 'AuthorId',
cw.introduction as 'Introduction',cw.thumbnail as 'Thumbnial',
cw.created_date as 'CreatedDate'
 from community_works as cw
inner join community_base_user as cbu
LEFT JOIN community_user_images as cui
on cw.user_id=cui.user_id and cui.is_used=1
where (cw.`name` like '%1str%'|| cbu.nick_name like '%str%')&& cw.off_line=0
order by cw.created_date desc
LIMIT 0,10
;


#查询所有资源
select * from (
select cbu.user_role as 'ResourceType',cbu.id as 'ResourceId',
 cbu.nick_name as 'Title', cbu.intro as 'Intro',cui.img_url as 'Thumbnail',
cbu.created_date as 'CreatedDate','' as 'ResourceUrl'
from community_base_user as cbu
LEFT JOIN community_user_images as cui
on cbu.id=cui.user_id and cui.type='Avator' and cui.is_used=1
WHERE cbu.user_role ='Designer'
union all
select cbu.user_role as 'ResourceType',cbu.id as 'ResourceId',
 cbu.nick_name as 'Title', cbu.intro as 'Intro',cui.img_url as 'Thumbnail',
cbu.created_date as 'CreatedDate','' as 'ResourceUrl'
from community_base_user as cbu
LEFT JOIN community_user_images as cui
on cbu.id=cui.user_id and cui.type='Avator' and cui.is_used=1
WHERE cbu.user_role ='Supplier'
union all
select cbu.user_role as 'ResourceType',cbu.id as 'ResourceId',
 cbu.nick_name as 'Title', cbu.intro as 'Intro',cui.img_url as 'Thumbnail',
cbu.created_date as 'CreatedDate','' as 'ResourceUrl'
from community_base_user as cbu
LEFT JOIN community_user_images as cui
on cbu.id=cui.user_id and cui.type='Avator' and cui.is_used=1
WHERE cbu.user_role ='Customer'
union all
select 'Works' as 'ResourceType', cw.id as 'ResourceId',
cw.`name` as 'Title',cw.introduction as 'Intro',
cw.thumbnail as 'Thumbnail',cw.created_date as 'CreatedDate','' as 'ResourceUrl'
from community_works as cw
where cw.off_line=0
union all
select 'Product' as 'ResourceType',cp.id as 'ResourceId',
cp.`name` as 'Title',cp.introduction as 'Intro',
cp.thumbnail as 'Thumbnail',cp.created_date as 'CreatedDate','' as 'ResourceUrl'
 from community_product as cp
where cp.off_line=0
union all
select 'News' as 'ResourceType',cn.id as 'ResourceId',
cn.title as 'Title',cn.introduction as 'Intro',
cn.thumbnail as 'Thumbnail',cn.created_date 'CreatedDate',cn.news_url as 'ResourceUrl'
from community_news as cn
where cn.off_line=0) as t
where Title like '%%'
order by CreatedDate DESC
limit 0,110
;
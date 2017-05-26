#根据目录id查询作品信息
select cw.`id` as 'works_id', cw.`name` as 'name',
cbu.`nick_name` as 'author',cui.img_url as 'avatar_url',
cbu.`id` as 'author_id',cw.introduction as 'introduction',
cw.thumbnail as 'thumbnail', cw.created_date as 'created_at' from community_works as cw
INNER JOIN community_base_user as cbu
on cw.user_id=cbu.id
LEFT JOIN community_user_images as cui
on cbu.id=cui.user_id and cui.type='Avatar' and cui.is_used=1
where cw.`id` in (select object_id from community_category_relationships
where (category_id=5||category_id=1) 
GROUP BY object_id
HAVING count(object_id)>1)
ORDER BY cw.created_date desc
LIMIT 1,10
;
#根据目录id查询指定产品
select cp.id as `product_id`,cp.cost as `cost`,
cp.`name` as `name`,cp.user_id as `author_id`,
cp.introduction as `introduction`,cp.thumbnail as`thumbnail`,
cp.created_date as `created_date`,cbu.nick_name as `author`,
cui.img_url as `avatar_url`
 from community_puoduct as cp 
INNER JOIN community_base_user as cbu 
on cp.user_id=cbu.id 
LEFT JOIN community_user_images as cui 
on cbu.id=cui.user_id and cui.type='Avatar' and cui.is_used=1 
where cp.id in (select object_id from community_category_relationships 
where (category_id=5||category_id=22) 
GROUP BY object_id 
HAVING count(object_id)>1)
ORDER BY cp.created_date desc 
limit 0,10
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

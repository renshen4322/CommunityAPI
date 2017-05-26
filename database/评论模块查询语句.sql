#根据id查询指定资源的评论信息 按时间倒序
select cbu.id as 'author_id',cbu.nick_name as 'author_name',cbu.user_role as 'author_role',cbu.intro as 'author_bio',
cui.img_url as 'author_avatar',cc.id as 'comment_id',if(cc.is_offline=1,"该评论已被删除!",cc.content) as 'content',
cc.reply_comment_id as 'reply_comment_id',if(repcc.is_offline=1,"该评论已被删除!",repcc.content) as 'reply_comment_content',cc.reply_user_id as 'reply_author_id',
replycbu.nick_name as 'reply_author_name',replycbu.intro as 'reply_author_bio',replycbu.user_role as 'reply_author_role',
replycui.img_url as 'reply_author_avatar',cc.gmt_create as 'created_at',
clc.count as 'likes_count'
 from community_comment as cc
INNER JOIN community_base_user as cbu
on cc.author_id=cbu.id and cc.is_offline=0
LEFT JOIN community_user_images as cui
on cc.author_id=cui.user_id and cui.is_used=1 and cui.type='Avatar'
LEFT JOIN community_base_user as replycbu
on cc.reply_user_id=replycbu.id
LEFT JOIN community_user_images as replycui
on cc.reply_user_id=replycui.user_id and replycui.is_used=1 and replycui.type='Avatar'
INNER JOIN community_comment_like_count clc
on cc.id=clc.comment_id
LEFT JOIN community_comment repcc
on repcc.id=cc.reply_comment_id
where cc.target_id='c13b9e9d-f689-407b-9e67-71102269a680' 
order BY cc.gmt_create asc
limit 0, 10
;
#根据id查询指定资源的评论信息 按点赞数倒序
select cbu.id as 'author_id',cbu.nick_name as 'author_name',cbu.user_role as 'author_role',cbu.intro as 'author_bio',
cui.img_url as 'author_avatar',cc.id as 'comment_id',if(cc.is_offline=1,"该评论已被删除!",cc.content) as 'content',
cc.reply_comment_id as 'reply_comment_id',if(repcc.is_offline=1,"该评论已被删除!",repcc.content) as 'reply_comment_content',cc.reply_user_id as 'reply_author_id',
replycbu.nick_name as 'reply_author_name',replycbu.intro as 'reply_author_bio',replycbu.user_role as 'reply_author_role',
replycui.img_url as 'reply_author_avatar',cc.gmt_create as 'created_at',
clc.count as 'likes_count'
 from community_comment as cc
INNER JOIN community_base_user as cbu
on cc.author_id=cbu.id and cc.is_offline=0
LEFT JOIN community_user_images as cui
on cc.author_id=cui.user_id and cui.is_used=1 and cui.type='Avatar'
LEFT JOIN community_base_user as replycbu
on cc.reply_user_id=replycbu.id
LEFT JOIN community_user_images as replycui
on cc.reply_user_id=replycui.user_id and replycui.is_used=1 and replycui.type='Avatar'
INNER JOIN community_comment_like_count clc
on cc.id=clc.comment_id
LEFT JOIN community_comment repcc
on repcc.id=cc.reply_comment_id
where cc.target_id='c13b9e9d-f689-407b-9e67-71102269a680' 
order BY clc.count desc
limit 0, 10
;

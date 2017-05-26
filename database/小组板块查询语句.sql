#获取特定小组的成员列表
select cbu.id as 'Id',cbu.user_role as 'Role',cbu.intro as  'Bio',cbu.nick_name as 'Name',cui.img_url as 'AvatarUrl',cgu.gmt_create as 'GMTCreate' from community_group_user as cgu
INNER JOIN community_base_user as cbu
on cgu.user_id=cbu.id and cgu.group_id=1
LEFT JOIN community_user_images as cui
on cgu.user_id=cui.user_id and cui.is_used=1 and cui.type='Avatar'
ORDER BY cgu.gmt_create asc; 


#根据帖子id获取帖子详情
select cgpc.content as 'PostContent',cgp.id as 'PostId',cgp.title as 'PostTitle',cgp.like_count as 'LikeCount',cgp.comment_count as 'CommentCount',
cgp.collect_count as 'CollectCount',cgp.gmt_create as 'GMTCreate',cgp.author_id as 'AuthorId',cbu.user_role as 'AuthorRole',
cbu.intro as 'AuthorBio',cbu.nick_name as 'AuthorName',cui.img_url as 'AuthorAvatarUrl',cgi.id as 'GroupId',cgi.`name` as 'GroupName',
cgi.description as 'GroupDesc',cgi.cover_url as 'GroupCoverUrl'
 from community_group_post as cgp
INNER JOIN community_group_post_content as cgpc
on cgp.id=cgpc.post_id and cgp.id=1
INNER JOIN community_base_user as cbu
on cgp.author_id=cbu.id
LEFT JOIN community_user_images cui
on cbu.id=cui.user_id and cui.is_used=1 and cui.type='Avatar'
INNER JOIN community_group_info as cgi
on cgi.id=cgp.group_id;


#获取小组帖子列表
select cgp.id as 'PostId',cgp.title as 'PostTitle',cgp.like_count as 'LikeCount',cgp.comment_count as 'CommentCount',
cgp.collect_count as 'CollectCount',cgp.gmt_create as 'GMTCreate',cgp.author_id as 'AuthorId',cbu.user_role as 'AuthorRole',
cbu.intro as 'AuthorBio',cbu.nick_name as 'AuthorName',cui.img_url as 'AuthorAvatarUrl',cgi.id as 'GroupId',cgi.`name` as 'GroupName'
 from community_group_post as cgp
INNER JOIN community_base_user as cbu
on cgp.author_id=cbu.id
LEFT JOIN community_user_images cui
on cbu.id=cui.user_id and cui.is_used=1 and cui.type='Avatar'
INNER JOIN community_group_info as cgi
on cgi.id=cgp.group_id and cgp.group_id=1
order by cgp.gmt_create desc
limit 0, 10;


#获取帖子评论列表
select cbu.id as 'AuthorId',cbu.nick_name as 'AuthorName',cbu.user_role as 'AuthorRole',cbu.intro as 'AuthorBio',
cui.img_url as 'AuthorAvatar', cgc.id as 'CommentId',cgc.content as 'Content',
cgc.reply_comment_id as 'ReplyCommtentId',if(repcgc.is_offline=1,"该评论已被删除!",repcgc.content) as 'ReplyCommtnetContent',
cgc.reply_user_id as 'ReplyAuthorId',replycbu.nick_name as 'ReplyAuthorName',
replycbu.intro as 'ReplyAuthorBio',replycbu.user_role as 'ReplyAuthorRole',replycui.img_url as 'ReplyAuthorAvatar',
cgc.gmt_create as 'GMTCreate'
from community_group_comment as cgc
INNER JOIN community_base_user as cbu
on cgc.author_id=cbu.id and cgc.is_offline=0
LEFT JOIN community_user_images as cui
on cgc.author_id=cui.user_id and cui.is_used=1 and cui.type='Avatar'
LEFT JOIN community_base_user as replycbu
on cgc.reply_user_id=replycbu.id
LEFT JOIN community_user_images as replycui
on cgc.reply_user_id=replycui.user_id and replycui.is_used=1 and replycui.type='Avatar'
LEFT JOIN community_group_comment repcgc
on repcgc.id=cgc.reply_comment_id
where cgc.post_id=1 and cgc.is_offline=0
order BY cgc.gmt_create asc
limit 0, 10
;   


#板块最新帖子列表
select cgp.id as 'PostId',cgp.group_id as 'GroupId',cgi.`name` as 'GroupName',cgp.title as 'PostTitle',cgp.comment_count as 'CommentCount',
cgp.gmt_create as 'GMTCreate',cgp.author_id as 'AuthorId',cbu.user_role as 'AuthorRole',
cbu.intro as 'AuthorBio',cbu.nick_name as 'AuthorName',cui.img_url as 'AuthorAvatarUrl'
 from community_group_post as cgp
INNER JOIN community_group_info as cgi
on cgp.group_id=cgi.id and cgi.classify_id=1 and cgp.is_offline=0
INNER JOIN community_base_user as cbu
on cgp.author_id=cbu.id
LEFT JOIN community_user_images cui
on cbu.id=cui.user_id and cui.is_used=1 and cui.type='Avatar'
ORDER BY cgp.gmt_create desc 
limit 0,10;


#获取用户加入的小组的最新帖子
select cgp.id as 'PostId',cgp.group_id as 'GroupId',cgi.`name` as 'GroupName',cgp.title as 'PostTitle',cgp.comment_count as 'CommentCount',
cgp.gmt_create as 'GMTCreate',cgp.author_id as 'AuthorId',cbu.user_role as 'AuthorRole',
cbu.intro as 'AuthorBio',cbu.nick_name as 'AuthorName',cui.img_url as 'AuthorAvatarUrl'
 from community_group_post as cgp
INNER JOIN community_group_info as cgi
on cgp.group_id=cgi.id and cgp.is_offline=0 and cgp.group_id in(
select group_id from community_group_user
where user_id='9269c349-0683-45e8-bc3c-c569647b760d')
INNER JOIN community_base_user as cbu
on cgp.author_id=cbu.id
LEFT JOIN community_user_images cui
on cbu.id=cui.user_id and cui.is_used=1 and cui.type='Avatar'
ORDER BY cgp.gmt_create desc 
limit 0,10;
#获取用户加入的小组的最新帖子总数
select count(1) from community_group_post as cgp 
INNER JOIN community_group_info as cgi 
on cgp.group_id=cgi.id and cgp.is_offline=0
and cgp.group_id in(
select group_id from community_group_user
where user_id='9269c349-0683-45e8-bc3c-c569647b760d')

#获取用户加入的小组信息
select cgi.id as 'GroupId',cgi.`name` as 'GroupName',cgi.classify_id as 'ClassifyId',cgc.`name` as 'ClassifyName',
cgi.description as 'GroupDescription',cgi.cover_url as 'GroupCoverUrl',cgi.is_hot as 'GroupIsHot',cgi.`order` as 'Order',
cgi.gmt_create as 'GMTCreate'
 from community_group_info as cgi
INNER JOIN community_group_user as cgu
on cgi.id=cgu.group_id and cgu.user_id=@UserId
INNER JOIN community_group_classify as cgc
on cgc.id=cgi.classify_id;
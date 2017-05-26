alter table community_user_images add index index_userid_isused (`user_id`,`is_used`);
alter table community_category_relationships add index index_obhectid (`object_id`);
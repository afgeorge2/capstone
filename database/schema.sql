--drop table breweries

create table breweries
(
id int identity(1,1),
name varchar(100) not null,
history varchar(MAX) null,
imagery varchar(MAX) null,
address varchar(300) null,
contact_name varchar(100) null,
contact_email varchar(100) null,
contact_phone varchar(11) null,

constraint pk_breweries primary key (id),
);

create table operation
(
id int identity(1,1),
brewery_id int not null,
day varchar(12) null,
opens time null,
closes time null,

constraint pk_operation primary key (id),
constraint fk_operation_breweries foreign key (brewery_id) references breweries (id)
);

create table users
(
id int identity(1,1),
email varchar(200) null,
username varchar(100) not null,
password varchar(200) null,
is_brewer bit null,
brewery_id int null,
is_admin bit null,

constraint pk_users primary key (id),
constraint fk_users_breweries foreign key (brewery_id) references breweries (id)
);

create table beers
(
id int identity(1,1),
name varchar(MAX) null,
description varchar(MAX) null,
image varchar(100) null,
abv real null,
beer_type varchar(100) null,
brewery_id int null,

constraint pk_beers primary key (id),
constraint fk_beers_brewery_info foreign key (brewery_id) references breweries (id)
);

create table reviews
(
id int identity(1,1),
user_id int null,
beer_id int null,
rating int null,
review varchar(MAX) null,

constraint pk_reviews primary key (id),
constraint fk_reviews_user_info foreign key (user_id) references users (id),
constraint fk_reviews_beers foreign key (beer_id) references beers (id),
);


create table [Role]
(
	id int primary key identity,
	[name] varchar(100),
	[description] varchar(255)
)

create table [User]
(
	id bigint primary key identity,
	[name] varchar(255),
	[surname] varchar(255),
	[phone] varchar(255),
	[mail] varchar(255),
	[login] varchar(255),
	[password] varchar(255),
	[id_role] int foreign key (id_role) references [Role](id) default 1
)

create table [Category]
(
	id int primary key identity,
	[name] varchar(255)
)

create table [Goods]
(
	id bigint primary key identity,
	[name] varchar(255),
	[price] money,
	[id_category] int foreign key (id_category) references [Category](id) default 1,
	[image] varchar(255)
)

create table [Basket]
(
	id bigint primary key identity,	
	[id_user] bigint foreign key (id_user) references [User](id) default 1,
	[id_goods] bigint foreign key (id_goods) references [Goods](id) default 1,
	[condition] bit default 1
)
CREATE TABLE users (
	id serial primary key,
	name varchar(255),
	address varchar(255),
	"createdAt" timestamp default current_timestamp,
	"createdBy" varchar(255) default 'Erikas',
	"modifiedAt" timestamp,
	"modifiedBy" varchar(255),
	"isDeleted" boolean default FALSE
);

CREATE TABLE accounts (
	id serial primary key,
	"userId" int references users(id),
	type varchar(255),
	balance decimal,
	"createdAt" timestamp default current_timestamp,
	"createdBy" varchar(255) default 'Erikas',
	"modifiedAt" timestamp,
	"modifiedBy" varchar(255),
	"isDeleted" boolean default FALSE
);

CREATE TABLE transactions (
	id serial primary key,
	"accountId" int references accounts(id),
	type varchar(255),
	amount decimal NOT NULL,
	fees decimal,
	"createdAt" timestamp default current_timestamp,
	"createdBy" varchar(255) default 'Erikas',
	"modifiedAt" timestamp,
	"modifiedBy" varchar(255),
	"isDeleted" boolean default FALSE
);



	
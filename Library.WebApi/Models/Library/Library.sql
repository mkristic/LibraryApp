create table "Book"
(
	"Id" integer,
	"Title" varchar(50),
	"Author" varchar(50),
	"Year" integer
);

select * from "Book";

drop table "Book";

create table "Book"
(
	"Id" integer primary key,
	"Title" varchar(50),
	"Author" varchar(50),
	"Year" integer
);

create table "Employee"
(
	"Id" integer primary key,
	"FirstName" varchar(50),
	"LastName" varchar(50),
	"Age" integer,
	"Qualification" varchar(100)
);

select * from "Employee";
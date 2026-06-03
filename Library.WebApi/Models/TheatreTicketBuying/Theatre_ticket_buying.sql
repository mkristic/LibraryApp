create table "Play"
(
	"Id" integer primary key,
	"Name" varchar(50),
	"Duration" integer,
	"genre" varchar(50)
);

alter table "Play" rename column "genre" to "Genre";

select * from "Play";

drop table if exists "Play";

create table "Seat"
(
	"Id" integer primary key,
	"RowNumber" integer,
	"SeatNumber" integer
);

create table "Ticket"
(
	"Id" integer primary key,
	"Price" decimal(10,2),
	"SeatId" integer,
	foreign key ("SeatId") references "Seat"("Id")
);

select * from "Seat";
select * from "Ticket";

drop table if exists "Ticket";
drop table if exists "Seat";

create table "Seat"
(
	"Id" integer primary key,
	"RowNumber" integer not null,
	"SeatNumber" integer not null
);

select * from "Seat";

create table "Ticket"
(
	"Id" integer primary key,
	"Price" decimal(10,2) not null,
	"SeatId" integer not null,
	foreign key ("SeatId") references "Seat"("Id") 
);

select * from "Ticket";

create table "Transaction" 
(
	"Id" integer primary key,
	"Date" date not null,
	"Price" decimal(10,2) not null,
	"TicketId" integer not null,
	foreign key ("TicketId") references "Ticket"("Id")
);

select * from "Transaction";

create table "Buyer" 
(
	"Id" integer primary key,
	"FirstName" varchar(50) not null,
	"LastName" varchar(50) not null,
	"Email" varchar(50) not null,
	"TransactionId" integer not null,
	foreign key ("TransactionId") references "Transaction"("Id")
);

create table "Performance"
(
	"Id" integer primary key,
	"Date" date not null,
	"Time" time not null,
	"TicketId" integer not null,
	foreign key ("TicketId") references "Ticket"("Id")
);

create table "Play"
(
	"Id" integer primary key,
	"Name" varchar(50) not null,
	"Duration" integer,
	"Genre" varchar(50),
	"PerformanceID" integer not null,
	foreign key("PerformanceID") references "Performance"("Id")
);

------------------------------------------------------------------
----------------------------- INSERT -----------------------------
------------------------------------------------------------------

insert into "Seat" ("Id", "RowNumber", "SeatNumber") values
	(1, 1, 1),
	(2, 1, 2),
	(3, 1, 3),
	(4, 1, 4),
	(5, 1, 5);

select * from "Seat";

insert into "Seat" ("Id", "RowNumber", "SeatNumber") values
	(6, 2, 1),
	(7, 2, 2),
	(8, 2, 3),
	(9, 2, 4),
	(10, 2, 5);

alter table "Play" rename column "PerformanceID" to "PerformanceId";

select * from "Play";

alter table "Play" alter column "PerformanceId" drop not null;

alter table "Buyer" alter column "TransactionId" drop not null;

insert into "Play" ("Id", "Name", "Duration", "Genre", "PerformanceId") values
	(1, 'Hamlet', 95, 'tragedy', null),
	(2, 'Othello', 90, 'tragedy', null),
	(3, 'King Lear', 75, 'tragedy', null),
	(4, 'Much Ado About Nothing', 67, 'comedy', null);

insert into "Ticket" ("Id", "Price", "SeatId") values
	(1, 15.00, 1),
	(2, 15.00, 2),
	(3, 15.00, 3),
	(4, 15.00, 4),
	(5, 15.00, 5),
	(6, 12.00, 6),
	(7, 12.00, 7),
	(8, 12.00, 8),
	(9, 12.00, 9),
	(10, 12.00, 10);

insert into "Performance" ("Id", "Date", "Time", "TicketId") values
	(1, '2025-03-01', '19:00', 1),
	(2, '2025-03-01', '19:00', 2),
	(3, '2025-03-08', '19:00', 3),
	(4, '2025-03-08', '19:00', 4);

insert into "Transaction" ("Id", "Date", "Price", "TicketId") values
	(1, '2025-02-15', 15.00, 1),
	(2, '2025-02-15', 15.00, 2),
	(3, '2025-02-20', 15.00, 3),
	(4, '2025-02-20', 15.00, 4),
	(5, '2025-02-25', 12.00, 5);

insert into "Buyer" ("Id", "FirstName", "LastName", "Email", "TransactionId") values
	(1, 'James', 'Smith', 'james.smith@email.com', 1),
	(2, 'Emma', 'Johnson', 'emma.johnson@email.com', 2),
	(3, 'Oliver', 'Williams', 'oliver.williams@email.com', 3),
	(4, 'Sophia', 'Brown', 'sophia.brown@email.com', 4);

------------------------------------------------------------------
----------------------------- SELECT -----------------------------
------------------------------------------------------------------

select "Name" from "Play" where ("Duration" < 90);

insert into "Performance" ("Id", "Date", "Time", "TicketId") values
	(5, '2026-04-01', '19:00', 1),
	(6, '2026-05-01', '19:00', 2),
	(7, '2026-06-08', '19:00', 3);

insert into "Transaction" ("Id", "Date", "Price", "TicketId") values
	(6, '2026-03-01', 14.12, 1),
	(7, '2026-02-15', 17.32, 2),
	(8, '2026-05-20', 18.01, 3);

select t."Id", t."Price", s."RowNumber", s."SeatNumber"
from "Ticket" t
inner join "Seat" s 
on t."SeatId" = s."Id";

select b."FirstName", b."LastName", tr."Date", tr."Price"
from "Buyer" b
inner join "Transaction" tr 
on b."TransactionId" = tr."Id";

update "Play" set "PerformanceId" = 1 where "Id" = 1;
update "Play" set "PerformanceId" = 2 where "Id" = 2;
update "Play" set "PerformanceId" = 3 where "Id" = 3;
update "Play" set "PerformanceId" = 4 where "Id" = 4;

select pl."Genre", p."Date"
from "Play" pl
inner join "Performance" p
on pl."PerformanceId" = p."Id" 
left join "Ticket" t
on t."Id" = p."TicketId" 
where t."Price" = 15;

select b."FirstName", b."LastName", t."Price"
from "Buyer" b
inner join "Transaction" t
on t."Id" = b."TransactionId"
where t."Price" = 15; 

select t."Id", t."Date", b."LastName"
from "Transaction" t
right join "Buyer" b
on b."TransactionId" = t."Id";

select pl."Name", p."Date", t."Price"
from "Play" pl
inner join "Performance" p
on p."Id" = pl."PerformanceId"
inner join "Ticket" t
on p."TicketId" = t."Id"
right join "Seat" s
on t."SeatId" = s."Id";









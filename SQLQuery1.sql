create database Nabu

use Nabu

drop database Nabu

drop table USERS

create table USERS
(
ID int primary key identity,
[LOGIN] CHAR(16) not null unique,
[PASSWORD] CHAR(9),
TYPE_OF_USER CHAR(8) check(TYPE_OF_USER='Creater' or TYPE_OF_USER='User' )
)

select *  from USERS

SELECT * FROM USERS WHERE ([LOGIN] = 'alena87')
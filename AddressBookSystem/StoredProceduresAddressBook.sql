--Update Contact details on DB
create procedure spUpdateContact
(
@Id int,
@FirstName varchar(100),
@City varchar(100),
@Zip  bigint
)
as
update AddressBook_Table set City=@City where Id=@Id and FirstName=@FirstName;
update AddressBook_Table set Zip=@Zip where Id=@Id and FirstName=@FirstName;

--introduce now column date-added--
alter table AddressBook_Table add Date_Added date not null default getdate();
update AddressBook_Table set Date_Added = '2019-02-10' where id in (1,3,5);

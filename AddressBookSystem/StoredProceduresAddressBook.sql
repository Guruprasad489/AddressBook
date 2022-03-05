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



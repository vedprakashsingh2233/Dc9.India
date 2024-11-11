

cReate table CategoryMaster(
Id int identity(1,1),
CategoryName varchar(200),
IsActive bit
)
Go
Insert into CategoryMaster values('VPS hosting',1),('Cloud hosting',1)
Go
Create Table ItemMaster(
ItemId int identity(1,1),
ItemName varchar(100),
ItemPrice decimal (18,3),
ItemType varchar(100),
Ram varchar(20),
vCPU varchar(20),
SSD varchar(20),
Bandwidth varchar(20),
Cat_Id_Fk int,
IsActive bit
)

go
insert into ItemMaster values('1 GB Ram',5,'Self-managed','1 GB Ram',' 1 vCPU','30 GB SSD','1 TB bandwidth',1,1),
('24 GB Ram',33,'Self-managed','24 GB Ram',' 6 vCPU','540 GB SSD','8 TB bandwidth',1,1)
GO
Create Table AdditionItem(
Id int identity(1,1),
ItemName varchar(100),
ItemPrice decimal (18,3),
IsActive bit
)
Go
insert into AdditionItem values('Antivirus A',5,1),('Antivirus B',7,1)
Go
Create Table OrderMaster(
OrderId int identity(1,1),
User_Id_Fk int,
Item_Id_Fk int ,
Addtional_Item_ids_Fk varchar(50),
TotalorderPrice decimal (18,3),
IsPayment bit,
OrderDate datetime
)

Go
CREATE FUNCTION STRING_SPLIT
(
    @String NVARCHAR(MAX),
    @Delimiter CHAR(1)
)
RETURNS @Result TABLE (Value NVARCHAR(MAX))
AS
BEGIN
    DECLARE @i INT = 1
    DECLARE @pos INT

    WHILE @i <= LEN(@String)
    BEGIN
        SET @pos = CHARINDEX(@Delimiter, @String, @i)
        IF @pos = 0 
            SET @pos = LEN(@String) + 1

        INSERT INTO @Result (Value)
        VALUES (SUBSTRING(@String, @i, @pos - @i))

        SET @i = @pos + 1
    END

    RETURN
END
go
SELECT t1.*, t2.*
FROM OrderMaster t1
CROSS APPLY STRING_SPLIT(t1.Addtional_Item_ids_Fk, ',') AS split_ids
JOIN AdditionItem t2 ON t2.ID = split_ids.value
go

insert into OrderMaster values(3,1,'1,2',43,1,getdate())

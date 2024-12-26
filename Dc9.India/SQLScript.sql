

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
go
CREATE proc [dbo].[USP_InsertUpdateDelCategoryMaster]
@Id Int=0,
@CategoryName nvarchar(100)=null ,
@IsActive bit=1,
@Action varchar(20)

As 
Declare @msg varchar(100)
Begin
	 If(@Action='Insert')
		 Begin
		 if Exists(select 1 from CategoryMaster where CategoryName=@CategoryName )
		 Begin
			set @msg='Record Already Exist ..'
			select @msg as Message
		 End
		 Else
			Begin

				Insert Into CategoryMaster(CategoryName,IsActive)values(@CategoryName,@IsActive)
				set @msg='Record Inserted Successfully'
				select @msg as Message
			End
		 End
	 Else  If(@Action='Update')
		 Begin
			 if Exists(select 1 from CategoryMaster where CategoryName=@CategoryName and  Id!=@Id)
			 Begin
				set @msg='Record Already Exist ..'
				select @msg as Message
			 End
			 Else
				 Begin
					Update CategoryMaster set CategoryName=@CategoryName,IsActive=@IsActive where Id=@Id
					set @msg='Record Updated Successfully'
					select @msg as Message
				End
		 End
	 Else  If(@Action='Select')
		 Begin
			Select Id,CategoryName,IsActive from CategoryMaster
		 End
	  Else  If(@Action='Edit')
		 Begin
			Select * from CategoryMaster where Id=@Id
		 End
	 Else  If(@Action='Delete')
		 Begin
			Delete From CategoryMaster where Id=@Id
			set @msg='Record Deleted Successfully'
			select @msg as Message
		 End
	
End

go
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LoginDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[UserType] [nvarchar](10) NOT NULL,
	[UserCode]  AS ('U'+right('00'+CONVERT([varchar](2),[id]),(4))) PERSISTED,
	[Password] [nvarchar](50) NULL,
	[createDate] [datetime] NULL,
	[updateDate] [datetime] NULL,
	[IsActive] [bit] NULL
) ON [PRIMARY]
go
insert into LoginDetails(UserId,UserType,Password,createDate,IsActive)values(0,'Admin','Admin',GETDATE(),1)
go
cREATE proc [dbo].[USP_GetLoginDetail] 
@UserCode nvarchar(10)=null,
@Passwrd nvarchar(50)=null,
@Id int =0,
@Isactive int=0,
@Action varchar(20)
As
Declare @msg nvarchar(100)
Begin
 If(@Action='Login')
	Begin
		If  Exists(Select 1 from LoginDetails where UserCode=@UserCode and IsActive=0)
			Begin
				Select '1' as Status
			End
		Else If Not Exists(Select 1 from LoginDetails where UserCode=@UserCode and Password=@Passwrd and IsActive=1)
			Begin
				Select '2' as Status
			End
		Else
			Begin
				Select UserType,UserId,'3' as Status from  LoginDetails where UserCode=@UserCode
			End
	ENd
 Else If(@Action='GetUsers')
	 Begin
		Select Isnull(SD.Name,'Admin') as Name,LD.UserType,LD.Password,Format(LD.createDate,'dd-MMM-yyyy') as createDate
		,LD.id,LD.IsActive,LD.UserCode from LoginDetails LD Left Join StudentDetail SD on LD.UserId=SD.StudentID
	 End
Else If(@Action='Avtive')
	 Begin
		Update LoginDetails set IsActive=@Isactive,updateDate=getdate() where id=@Id
		Select '1' as Status
	 End
End

go


CREATE TABLE [dbo].[SubCategoryMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubCategoryName] [varchar](300) NULL,
	[Heading1] varchar(500) null,
	[Heading2] varchar(500) null,
	[Heading3] varchar(500) null,
	[Heading4] varchar(500) null,
	[Category_Id_FK] int null,
	[IsActive] [bit] NULL
) ON [PRIMARY]



go
Create proc [dbo].[USP_InsertUpdateDelSubCategoryMaster]
@Id Int=0,
@SubCategoryName nvarchar(300)=null ,
@Heading1 varchar(500)= null,
@Heading2 varchar(500)= null,
@Heading3 varchar(500)= null,
@Heading4 varchar(500)= null,
@Category_Id_FK int =null,
@IsActive bit=1,
@Action varchar(20)

As 
Declare @msg varchar(100)
Begin
	 If(@Action='Insert')
		 Begin
		 if Exists(select 1 from SubCategoryMaster where SubCategoryName=@SubCategoryName )
		 Begin
			set @msg='Record Already Exist ..'
			select @msg as Message
		 End
		 Else
			Begin

				Insert Into SubCategoryMaster(SubCategoryName,Heading1,Heading2,Heading3,Heading4,Category_Id_FK,IsActive)values
				(@SubCategoryName,@Heading1,@Heading2,@Heading3,@Heading4,@Category_Id_FK,@IsActive)
				set @msg='Record Inserted Successfully'
				select @msg as Message
			End
		 End
	 Else  If(@Action='Update')
		 Begin
			 if Exists(select 1 from SubCategoryMaster where SubCategoryName=@SubCategoryName and  Id!=@Id)
			 Begin
				set @msg='Record Already Exist ..'
				select @msg as Message
			 End
			 Else
				 Begin
					Update SubCategoryMaster set SubCategoryName=@SubCategoryName,Heading1=@Heading1,Heading2=@Heading2
					,Heading3=@Heading3,Heading4=@Heading4,Category_Id_FK=@Category_Id_FK,IsActive=@IsActive where Id=@Id
					set @msg='Record Updated Successfully'
					select @msg as Message
				End
		 End
	 Else  If(@Action='Select')
		 Begin
			Select sm.Id,sm.SubCategoryName,cm.CategoryName,sm.Heading1 from SubCategoryMaster sm
			left join categorymaster cm on sm.Category_Id_FK=cm.Id
		 End
	  Else  If(@Action='Edit')
		 Begin
			Select * from SubCategoryMaster where Id=@Id
		 End
	 Else  If(@Action='Delete')
		 Begin
			Delete From SubCategoryMaster where Id=@Id
			set @msg='Record Deleted Successfully'
			select @msg as Message
		 End
	
End


go
USE [DC9India]
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertUpdateDelSubCategoryMaster]    Script Date: 26-12-2024 15:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[USP_InsertUpdateDelSubCategoryMaster]
@Id Int=0,
@SubCategoryName nvarchar(300)=null ,
@Heading1 varchar(500)= null,
@Heading2 varchar(500)= null,
@Heading3 varchar(500)= null,
@Heading4 varchar(500)= null,
@Category_Id_FK int =null,
@IsActive bit=1,
@Action varchar(20)

As 
Declare @msg varchar(100)
Begin
	 If(@Action='Insert')
		 Begin
		 if Exists(select 1 from SubCategoryMaster where SubCategoryName=@SubCategoryName )
		 Begin
			set @msg='Record Already Exist ..'
			select @msg as Message
		 End
		 Else
			Begin

				Insert Into SubCategoryMaster(SubCategoryName,Heading1,Heading2,Heading3,Heading4,Category_Id_FK,IsActive)values
				(@SubCategoryName,@Heading1,@Heading2,@Heading3,@Heading4,@Category_Id_FK,@IsActive)
				set @msg='Record Inserted Successfully'
				select @msg as Message
			End
		 End
	 Else  If(@Action='Update')
		 Begin
			 if Exists(select 1 from SubCategoryMaster where SubCategoryName=@SubCategoryName and  Id!=@Id)
			 Begin
				set @msg='Record Already Exist ..'
				select @msg as Message
			 End
			 Else
				 Begin
					Update SubCategoryMaster set SubCategoryName=@SubCategoryName,Heading1=@Heading1,Heading2=@Heading2
					,Heading3=@Heading3,Heading4=@Heading4,Category_Id_FK=@Category_Id_FK,IsActive=@IsActive where Id=@Id
					set @msg='Record Updated Successfully'
					select @msg as Message
				End
		 End
	 Else  If(@Action='Select')
		 Begin
			Select sm.Id,sm.SubCategoryName,cm.CategoryName,sm.Heading1 from SubCategoryMaster sm
			left join categorymaster cm on sm.Category_Id_FK=cm.Id
		 End
	  Else  If(@Action='Edit')
		 Begin
			Select * from SubCategoryMaster where Id=@Id
		 End
	 Else  If(@Action='Delete')
		 Begin
			Delete From SubCategoryMaster where Id=@Id
			set @msg='Record Deleted Successfully'
			select @msg as Message
		 End
	
End



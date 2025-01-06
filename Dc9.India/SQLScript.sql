alter table planmaster add PriceType varchar(100)

go

alter proc [dbo].[USP_InsertUpdateDelPlanMaster]
@Id Int=0,
@PlanName [varchar](100)= NULL,
@Price [decimal](18, 3)= NULL,
@PriceType varchar(100)=Null,
@PlanType [varchar](100)= NULL,
@Ram [varchar](20)= NULL,
@vCPU [varchar](20)= NULL,
@SSD [varchar](20)= NULL,
@HDD [varchar](20)= NULL,
@Memory [varchar](100) =NULL,
@Bandwidth [varchar](20) =NULL,
@Sub_Cat_Id_Fk [int] =NULL,
@IsActive [bit]= NULL,
@DedicatedIP varchar(20)=Null,
@OSChoice varchar(100)=Null,
@Remark varchar(300)=Null,
@ServerLocation varchar(100)=Null,
@NVMe varchar(100)=Null,
@Bonus varchar(100)=Null,
@Migration varchar(100)=Null,
@SSL varchar(100)=Null,
@Security varchar(100)=Null,
@Monitoring varchar(200)=Null,
@Prevention varchar(200)=Null,
@Service_Support varchar(200)=Null,
@Support varchar(100)=Null,
@Guarantee varchar(200)=Null,
@Action varchar(20)

As 
Declare @msg varchar(100)
Begin
	 If(@Action='Insert')
		 Begin
		 if Exists(select 1 from PlanMaster where PlanName=@PlanName )
		 Begin
			set @msg='Record Already Exist ..'
			select @msg as Message
		 End
		 Else
			Begin

				Insert Into PlanMaster(PlanName,Price,PriceType,PlanType,Ram,vCPU,SSD,HDD,Memory,Bandwidth,Sub_Cat_Id_Fk,DedicatedIP,OSChoice,
				Remark,ServerLocation,NVMe,Bonus,Migration,SSL,Security,Monitoring,Prevention,Service_Support,Support,Guarantee,IsActive)values
			   (@PlanName,@Price,@PriceType,@PlanType,@Ram,@vCPU,@SSD,@HDD,@Memory,@Bandwidth,@Sub_Cat_Id_Fk,@DedicatedIP,@OSChoice,@Remark,
				@ServerLocation,@NVMe,@Bonus,@Migration,@SSL,@Security,@Monitoring,@Prevention,@Service_Support,@Support,@Guarantee,@IsActive)
				set @msg='Record Inserted Successfully'
				select @msg as Message
			End
		 End
	 Else  If(@Action='Update')
		 Begin
			 if Exists(select 1 from PlanMaster where PlanName=@PlanName and  Id!=@Id)
			 Begin
				set @msg='Record Already Exist ..'
				select @msg as Message
			 End
			 Else
				 Begin
					Update PlanMaster set PlanName=@PlanName,Price=@Price,PriceType=@PriceType,PlanType=@PlanType,Ram=@Ram,vCPU=@vCPU,SSD=@SSD,HDD=@HDD,Memory=@Memory,
					Bandwidth=@Bandwidth,Sub_Cat_Id_Fk=@Sub_Cat_Id_Fk,DedicatedIP=@DedicatedIP,OSChoice=@OSChoice,Remark=@Remark,ServerLocation=@ServerLocation,
					NVMe=@NVMe,Bonus=@Bonus,Migration=@Migration,SSL=@SSL,Security=@Security,Monitoring=@Monitoring,Prevention=@Prevention,
					Service_Support=@Service_Support,Support=@Support,Guarantee=@Guarantee,IsActive=@IsActive where Id=@Id
					set @msg='Record Updated Successfully'
					select @msg as Message
				End
		 End
	 Else  If(@Action='Select')
		 Begin
			Select PM.Id,PM.PlanName,(convert(varchar(50),PM.Price)+PM.PriceType) as Price,PM.Bandwidth, SM.SubCategoryName from PlanMaster PM
			left join SubCategoryMaster SM on PM.Sub_Cat_Id_Fk =SM.Id
		 End
	  Else  If(@Action='Edit')
		 Begin
			Select * from PlanMaster where Id=@Id
		 End
	 Else  If(@Action='Delete')
		 Begin
			Delete From PlanMaster where Id=@Id
			set @msg='Record Deleted Successfully'
			select @msg as Message
		 End
	
End


Go
Create Table DiscountMaster(
Id int identity(0,1),
DiscountName varchar(200),
PercentageAmount decimal(18,3),
IsActive bit
)

Go
CREATE proc [dbo].[USP_InsertUpdateDelDiscountMaster]
@Id Int=0,
@DiscountName nvarchar(200)=null ,
@PercentageAmount decimal(18,3)=Null,
@IsActive bit=1,
@Action varchar(20)

As 
Declare @msg varchar(100)
Begin
	 If(@Action='Insert')
		 Begin
		 if Exists(select 1 from DiscountMaster where DiscountName=@DiscountName)
		 Begin
			set @msg='Record Already Exist ..'
			select @msg as Message
		 End
		 Else
			Begin

				Insert Into DiscountMaster(DiscountName,PercentageAmount,IsActive)values(@DiscountName,@PercentageAmount,@IsActive)
				set @msg='Record Inserted Successfully'
				select @msg as Message
			End
		 End
	 Else  If(@Action='Update')
		 Begin
			 if Exists(select 1 from DiscountMaster where DiscountName=@DiscountName and  Id!=@Id)
			 Begin
				set @msg='Record Already Exist ..'
				select @msg as Message
			 End
			 Else
				 Begin
					Update DiscountMaster set DiscountName=@DiscountName,PercentageAmount=@PercentageAmount,IsActive=@IsActive where Id=@Id
					set @msg='Record Updated Successfully'
					select @msg as Message
				End
		 End
	 Else  If(@Action='Select')
		 Begin
			Select Id,DiscountName,PercentageAmount,case when IsActive=1 then'True' else 'false'end as IsActive  from DiscountMaster
		 End
	  Else  If(@Action='Edit')
		 Begin
			Select * from DiscountMaster where Id=@Id
		 End
	 Else  If(@Action='Delete')
		 Begin
			Delete From DiscountMaster where Id=@Id
			set @msg='Record Deleted Successfully'
			select @msg as Message
		 End
	
End



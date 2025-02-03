alter proc [dbo].[USP_InsertUpdateDelPlanMaster]

@Id Int=0,

@PlanName [varchar](100)= NULL,

@Price [decimal](18, 3)= 0,
@PriceYearly [decimal](18, 3)= 0,
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
@Action varchar(20),
@IsFreeSSL VARCHAR(200) = NULL,
@IsFirewallMonitory VARCHAR(200) = NULL,
@Isprevention VARCHAR(200) = NULL,
@Is27Support VARCHAR(200) = NULL,
@IsUpTimeGuarntee VARCHAR(200) = NULL,
@IsAPIIntegration VARCHAR(200) = NULL,
@IsFreeBonuses VARCHAR(200) = NULL,
@IsFreeMigration VARCHAR(200) = NULL,
@IsFullRootAccess VARCHAR(200) = NULL,
@IsFullStackDevelopment VARCHAR(200) = NULL,
@IsMalwareScans VARCHAR(200) = NULL,
@IsMultiplePHPVersion VARCHAR(200) = NULL,
@IsOSChoice VARCHAR(200) = NULL,
@IsPageSpeed VARCHAR(200) = NULL,
@IsPHPVulCheck VARCHAR(200) = NULL,
@IsProtectiveFirewall VARCHAR(200) = NULL,
@IsVul_Scanner VARCHAR(200) = NULL,
@IsWebsiteOptimization VARCHAR(200) = NULL,
@IsWorldwideDataCenters VARCHAR(200) = NULL,
@IsPowerfulControlPanel VARCHAR(200) = NULL,
@IsCSSJSOptimizers VARCHAR(200) = NULL,
@IsOneClickIns VARCHAR(200) = NULL




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

				Remark,ServerLocation,NVMe,Bonus,Migration,SSL,Security,Monitoring,Prevention,Service_Support,Support,Guarantee,IsActive,PriceYearly,
				IsFreeSSL ,
				IsFirewallMonitory,
				Isprevention ,
				Is27Support,
				IsUpTimeGuarntee ,
				IsAPIIntegration ,
				IsFreeBonuses ,
				IsFreeMigration ,
				IsFullRootAccess ,
				IsFullStackDevelopment,
				IsMalwareScans ,
				IsMultiplePHPVersion ,
				IsOSChoice ,
				IsPageSpeed ,
				IsPHPVulCheck ,
				IsProtectiveFirewall ,
				IsVul_Scanner,
				IsWebsiteOptimization ,
				IsWorldwideDataCenters,
				IsPowerfulControlPanel,
				IsCSSJSOptimizers ,
				IsOneClickIns)values

			   (@PlanName,@Price,@PriceType,@PlanType,@Ram,@vCPU,@SSD,@HDD,@Memory,@Bandwidth,@Sub_Cat_Id_Fk,@DedicatedIP,@OSChoice,@Remark,

				@ServerLocation,@NVMe,@Bonus,@Migration,@SSL,@Security,@Monitoring,@Prevention,@Service_Support,@Support,@Guarantee,@IsActive,@PriceYearly,
				@IsFreeSSL ,
				@IsFirewallMonitory,
				@Isprevention ,
				@Is27Support,
				@IsUpTimeGuarntee ,
				@IsAPIIntegration ,
				@IsFreeBonuses ,
				@IsFreeMigration ,
				@IsFullRootAccess ,
				@IsFullStackDevelopment,
				@IsMalwareScans ,
				@IsMultiplePHPVersion ,
				@IsOSChoice ,
				@IsPageSpeed ,
				@IsPHPVulCheck ,
				@IsProtectiveFirewall ,
				@IsVul_Scanner,
				@IsWebsiteOptimization ,
				@IsWorldwideDataCenters,
				@IsPowerfulControlPanel,
				@IsCSSJSOptimizers ,
				@IsOneClickIns 
				
				
				)

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

					Service_Support=@Service_Support,Support=@Support,Guarantee=@Guarantee,IsActive=@IsActive ,PriceYearly=@PriceYearly,
					IsFreeSSL=@IsFreeSSL ,
				IsFirewallMonitory=@IsFirewallMonitory,
				Isprevention=@Isprevention ,
				Is27Support=@Is27Support,
				IsUpTimeGuarntee=@IsUpTimeGuarntee ,
				IsAPIIntegration=@IsAPIIntegration ,
				IsFreeBonuses=@IsFreeBonuses ,
				IsFreeMigration=@IsFreeMigration ,
				IsFullRootAccess=@IsFullRootAccess ,
				IsFullStackDevelopment=@IsFullStackDevelopment,
				IsMalwareScans=@IsMalwareScans ,
				IsMultiplePHPVersion=@IsMultiplePHPVersion ,
				IsOSChoice=@IsOSChoice ,
				IsPageSpeed=@IsPageSpeed ,
				IsPHPVulCheck=@IsPHPVulCheck ,
				IsProtectiveFirewall=@IsProtectiveFirewall ,
				IsVul_Scanner=@IsVul_Scanner,
				IsWebsiteOptimization=@IsWebsiteOptimization ,
				IsWorldwideDataCenters=@IsWorldwideDataCenters,
				IsPowerfulControlPanel=@IsPowerfulControlPanel,
				IsCSSJSOptimizers=@IsCSSJSOptimizers ,
				IsOneClickIns=@IsOneClickIns 
					where Id=@Id

					set @msg='Record Updated Successfully'

					select @msg as Message

				End

		 End

	 Else  If(@Action='Select')

		 Begin

			Select PM.Id,PM.PlanName,case when  PM.Price>0 then(convert(varchar(50),PM.Price)+PM.PriceType) Else
			(convert(varchar(50),PM.PriceYearly)+PM.PriceType) end as Price,PM.Bandwidth, SM.SubCategoryName from PlanMaster PM

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

go

CREATE TABLE [dbo].[PlanMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlanName] [varchar](100) NULL,
	[Price] [decimal](18, 3) NULL,
	[PlanType] [varchar](100) NULL,
	[Ram] [varchar](20) NULL,
	[vCPU] [varchar](20) NULL,
	[SSD] [varchar](20) NULL,
	[HDD] [varchar](20) NULL,
	[Memory] [varchar](100) NULL,
	[Bandwidth] [varchar](20) NULL,
	[Sub_Cat_Id_Fk] [int] NULL,
	[IsActive] [bit] NULL,
	[DedicatedIP] [varchar](20) NULL,
	[OSChoice] [varchar](100) NULL,
	[Remark] [varchar](300) NULL,
	[ServerLocation] [varchar](100) NULL,
	[NVMe] [varchar](100) NULL,
	[Bonus] [varchar](100) NULL,
	[Migration] [varchar](100) NULL,
	[SSL] [varchar](100) NULL,
	[Security] [varchar](100) NULL,
	[Monitoring] [varchar](200) NULL,
	[Prevention] [varchar](200) NULL,
	[Service_Support] [varchar](200) NULL,
	[Support] [varchar](100) NULL,
	[Guarantee] [varchar](200) NULL,
	[PriceType] [varchar](100) NULL,
	[IsAPIIntegration] [varchar](200) NULL,
	[IsFreeBonuses] [varchar](200) NULL,
	[IsFreeMigration] [varchar](200) NULL,
	[IsFullRootAccess] [varchar](200) NULL,
	[IsFullStackDevelopment] [varchar](200) NULL,
	[IsMalwareScans] [varchar](200) NULL,
	[IsMultiplePHPVersion] [varchar](200) NULL,
	[IsOSChoice] [varchar](200) NULL,
	[IsPageSpeed] [varchar](200) NULL,
	[IsPHPVulCheck] [varchar](200) NULL,
	[IsProtectiveFirewall] [varchar](200) NULL,
	[IsVul_Scanner] [varchar](200) NULL,
	[IsWebsiteOptimization] [varchar](200) NULL,
	[IsWorldwideDataCenters] [varchar](200) NULL,
	[IsPowerfulControlPanel] [varchar](200) NULL,
	[IsCSSJSOptimizers] [varchar](200) NULL,
	[IsOneClickIns] [varchar](200) NULL,
	[IsFreeSSL] [varchar](200) NULL,
	[IsFirewallMonitory] [varchar](200) NULL,
	[Isprevention] [varchar](200) NULL,
	[Is27Support] [varchar](200) NULL,
	[IsUpTimeGuarntee] [varchar](200) NULL,
	[PriceYearly] [decimal](3, 0) NULL
	go

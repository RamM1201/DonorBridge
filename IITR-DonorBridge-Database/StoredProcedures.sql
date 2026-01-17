/****** Object:  StoredProcedure [dbo].[sp_adminGetAllDonations]    Script Date: 17-01-2026 16:08:10 ******/

create OR alter procedure [dbo].[sp_adminGetAllDonations]
as

begin
	select 
		d.id as DonationId,
		d.registrationID as UserRegistrationId,
		concat(ur.firstName, ' ', ur.lastName) as Name,
		d.amount as Amount,
		st.name as Status,
		sm.name as State,
		d.updated as Updated

	from tbl_donations as d
	left join tbl_userRegistration as ur
		on ur.id=d.registrationID

	left join tbl_statusMaster as st
		on st.id=d.statusID

	left join tbl_stateMaster as sm
		on sm.id=ur.stateID

	order by d.updated desc;
end;
GO


/****** Object:  StoredProcedure [dbo].[sp_adminGetAllTransactions]    Script Date: 17-01-2026 16:12:01 ******/

create or alter procedure [dbo].[sp_adminGetAllTransactions]
as 

begin
	select 
		t.id as TransactionId,
		concat(ur.firstName, ' ', ur.lastName) as NameofUser,
		d.amount as Amount,
		st.name as Status,
		t.updated as Updated

	from tbl_transactions as t
	inner join tbl_donations as d
		on d.id=t.donationID
	inner join tbl_userRegistration ur
		on ur.id=d.registrationID
	left join tbl_statusMaster as st
		on st.id=t.statusID
	order by t.updated desc;
end;
GO


/****** Object:  StoredProcedure [dbo].[sp_adminGetAllUsers]    Script Date: 17-01-2026 16:12:23 ******/

create or alter procedure [dbo].[sp_adminGetAllUsers]
as

begin
	select 
		CONCAT(ur.firstName, ' ', ur.lastName) as Name,
		ur.email as Email,
		ur.mobile as Mobile,
		sm.name as State,
		ur.created as Created,
		ur.is_verified as isVerified

	from tbl_userRegistration as ur
	left join tbl_stateMaster as sm
		on sm.id=ur.stateID

	order by ur.created desc;
end;
GO


/****** Object:  StoredProcedure [dbo].[sp_donorCreateDonation]    Script Date: 17-01-2026 16:12:37 ******/

create or alter procedure [dbo].[sp_donorCreateDonation]
	@UserRegistrationId INT,
	@Amount INT

as 
begin 
	set nocount on;

	begin try
		begin transaction;

		declare @DonationId INT;
		declare @StatusId INT;

		--Get pending status id
		select @StatusId =id
		from tbl_statusMaster
		where name='pending';

		if @StatusId is NULL
		begin 
			raiserror ('Pending status not found', 16, 1);
			return;
		end

		--validate user registration
		if not exists(
			select 1
			from tbl_userRegistration
			where id=@UserRegistrationId
		)
		begin 
			raiserror ('Invalid UserRegistrationId', 16, 1);
			return;
		end

		--Insert donation 
		insert into tbl_donations (registrationID, amount, statusID, created, updated)
		values (@UserRegistrationId, @Amount, @StatusId, GETDATE(), GETDATE());

		set @DonationId = SCOPE_IDENTITY();

		commit transaction;

		select @DonationId;
	end try
	begin catch
		
		if @@TRANCOUNT>0
			rollback transaction;

		throw;
	end catch
end;
GO


/****** Object:  StoredProcedure [dbo].[sp_donorCreateTransaction]    Script Date: 17-01-2026 16:12:50 ******/

create or alter procedure [dbo].[sp_donorCreateTransaction]
	@DonationId INT,
	@OrderId varchar(100)

as 
begin 
	set nocount on;

	begin try
		begin transaction

		declare @TransactionId INT;
		declare @StatusId INT;

		--Get pending status ID
		select @StatusId=id 
		from tbl_statusMaster 
		where name='pending';

		if @StatusId is NULL
		begin 
			raiserror ('Pending status not found', 16, 1);
			return;
		end

		--validate if donation exists
		if not exists(
			select 1
			from tbl_donations
			where id=@DonationId
		)
		begin 
			raiserror ('Invalid DonationId', 16, 1);
			return;
		end

		--Insert transaction
		insert into tbl_transactions(donationID, orderID, statusID, created, updated)
		values (@DonationId, @OrderId, @StatusId, GETDATE(), GETDATE());

		set @TransactionId = SCOPE_IDENTITY();

		commit transaction 

		select @TransactionId;
	end try
	begin catch 
		if @@TRANCOUNT>0
			rollback transaction;

		throw;
	end catch
end;
GO


/****** Object:  StoredProcedure [dbo].[sp_donorGetAllDonations]    Script Date: 17-01-2026 16:13:03 ******/

create or alter procedure [dbo].[sp_donorGetAllDonations]
	@UserRegistrationId INT
as
begin 

	select 
		d.id as DonationId,
		d.amount as Amount,
		st.name as Status,
		d.updated as Updated
	from tbl_donations as d
	left join tbl_statusMaster as st
		on st.id = d.statusID
	where d.registrationID=@UserRegistrationId
	order by d.updated desc;
end;
GO


/****** Object:  StoredProcedure [dbo].[sp_donorGetAmountForDonationId]    Script Date: 17-01-2026 16:13:17 ******/

create or alter procedure [dbo].[sp_donorGetAmountForDonationId]
	@DonationID INT
as 
begin
	set nocount on;

	declare @Amount INT;

	--validate donation exists 
	select @Amount=amount
	from tbl_donations
	where id=@DonationID;

	if @Amount is nULL
	begin 
		raiserror ('Invalid DonationId', 16, 1);
		return;
	end

	select @Amount as Amount;
end;
GO


/****** Object:  StoredProcedure [dbo].[sp_donorGetTransactionsByDonationId]    Script Date: 17-01-2026 16:13:31 ******/

create or alter procedure [dbo].[sp_donorGetTransactionsByDonationId]
	@DonationId INT
as 
begin
	select
		t.id as TransactionId,
		st.name as Status,
		t.updated as Updated
	from tbl_transactions as t

	left join tbl_statusMaster as st
		on st.id=t.statusID
	where t.donationID = @DonationId
	order by t.updated desc;
end;
GO


/****** Object:  StoredProcedure [dbo].[sp_donorUpdateDonationStatus]    Script Date: 17-01-2026 16:13:45 ******/

create or alter procedure [dbo].[sp_donorUpdateDonationStatus]
	@OrderId varchar(100),
	@Status varchar(50),
	@PaymentId varchar(100)

as 
begin
	set nocount on;

	begin try
		begin transaction

		declare @TransactionId INT;
		declare @StatusId INT;

		--get transaction by OrderId
		select @TransactionId=id
		from tbl_transactions
		where orderID= @OrderId;

		if @TransactionId is NULL
		begin 
			raiserror ('Invalid OrderId', 16, 1);
			return;
		end

		--resolve status
		select @StatusId=id
		from tbl_statusMaster
		where name=@Status;

		if @StatusId is NULL
		begin 
			raiserror ('Invalid status', 16, 1);
			return;
		end

		--update transaction 
		update tbl_transactions
		set 
			statusID=@StatusId,
			paymentID= @PaymentId,
			updated = CAST(GETDATE() as DATE)
		where id=@TransactionId;

		commit transaction;

		--return updated transaction
		select 
			t.id as TransactionId,
			st.name as Status,
			t.updated as Updated
		from tbl_transactions as t
		inner join tbl_statusMaster st
			on st.id=t.statusID
		where t.id=@TransactionId;
		
	end try
	begin catch
		if @@TRANCOUNT>0
			rollback transaction;

		throw;
	end catch
end;
GO


/****** Object:  StoredProcedure [dbo].[sp_GetLogin]    Script Date: 17-01-2026 16:14:00 ******/

create or alter proc [dbo].[sp_GetLogin] @UserID varchar(30),@Password varchar(20)
as
select
ur.id as registrationID,
rm.name as role
from tbl_login l
left join tbl_roleMaster rm on l.roleID=rm.id
left join tbl_userRegistration ur on l.id=ur.loginID
where 
l.userID=@UserID
and l.password=@Password
and l.is_active=1
GO


/****** Object:  StoredProcedure [dbo].[sp_userCreateRegistration]    Script Date: 17-01-2026 16:14:31 ******/

CREATE or alter procedure [dbo].[sp_userCreateRegistration]
	@UserID varchar(30),
	@Password varchar(20),
	@FirstName varchar(50),
	@LastName varchar(50),
	@Email varchar(50),
	@State varchar(50),
	@Mobile varchar(50)

as	

begin
	
	declare @LoginID INT;
	declare @RegistrationID INT;
	declare @StateID INT;
	declare @RoleName varchar(50);
	
	--StateID
	select @StateID= id
	from tbl_stateMaster
	where name=@State;

	--Inserting into login table
	insert into tbl_login(userID, password, roleID, is_active)
	values (@UserID, @Password, 2, 1);

	set @LoginID = SCOPE_IDENTITY();

	--Inserting itno registration table
	insert into tbl_userRegistration(loginID, firstName, lastName, email, mobile, stateID, is_verified)
	values(@LoginID, @FirstName, @LastName, @Email, @Mobile, @StateID, 0);

	set @RegistrationID = SCOPE_IDENTITY();

	--Get Role Name
	select @RoleName=name
	from tbl_roleMaster
	where id=2;


	--return login response
	select 
		@RegistrationID as RegistrationID,
		@RoleName as Role;

END;
GO


/****** Object:  StoredProcedure [dbo].[sp_userGetRegistrationById]    Script Date: 17-01-2026 16:14:44 ******/

create or alter procedure [dbo].[sp_userGetRegistrationById]
	@RegistrationID INT

as
begin

	select
		CONCAT(ur.firstName, ' ', ur.lastName) as Name,
		ur.email as Email,
		ur.mobile as Mobile,
		sm.name as State,
		ur.created as Created,
		ur.is_verified as isVerified

	from tbl_userRegistration as ur
	left join tbl_stateMaster as sm
		on sm.id=ur.stateID
	where ur.id=@RegistrationID;

end;
GO



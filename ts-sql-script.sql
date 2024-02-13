create database today_solution

create table Patient 
(
  id int primary key identity(1,1),
  firstname nvarchar(255) not null,
  lastname nvarchar(255) not null,
  email nvarchar(255) unique not null,
  balance decimal default 0.0,
  lastPayment date,
  deleted int default 0,
  created_on dateTime,
  constraint is_deleted check (deleted in (0,1))
)

create table Payment 
(
	id int primary key identity(1,1),
	amount decimal default (0.0),
	patient_id int foreign key REFERENCES Patient(id),
	balance decimal default (0.0),
	payment_date dateTime
)

Create index ix_username on Patient (firstname, lastname)

/*Select all Patients*/
create procedure sp_get_all_patients @skip int, @take int
AS
	select  id, firstname, lastname, email, balance, lastPayment, created_on 
	from Patient 
	where deleted != 1
	Order By created_on
	OFFSET @skip ROWS
	FETCH NEXT @take ROWS ONLY;
GO
/*Select Patient by Id*/
create procedure sp_get_patient_by_id @id int
AS
select id, firstname, lastname, email, balance, lastPayment, created_on from Patient where id=@id and deleted <> 1;
GO
/*Select Patient by email*/
create procedure sp_get_patient_by_email @email nvarchar(255)
AS
select id, firstname, lastname, email, balance, lastPayment, created_on from Patient where email=@email and deleted <> 1;
GO
/*Select Patient by name*/
create procedure sp_patients_by_name @name nvarchar(255), @skip int, @take int
AS
select id, firstname, lastname, email, balance, lastPayment, created_on 
from Patient 
where firstname + '' + lastname like '%'+@name+'%'
Order By created_on
OFFSET @skip ROWS
FETCH NEXT @take ROWS ONLY;
GO
/*Add new Patient*/
create procedure sp_add_patient 
@firstname nvarchar(255),
@lastname nvarchar(255),
@email nvarchar(255),
@created_on date
AS
insert into Patient (firstname,lastname,email,created_on) values (@firstname,@lastname,@email,@created_on)
GO
/*Update Patient details*/
create procedure sp_update_patient_details @id int, @firstname nvarchar(255),@lastname nvarchar(255),@email nvarchar(255)
AS
update Patient set firstname=@firstname,lastname=@lastname,email=@email where id = @id
GO
/*Delete Patient*/
create procedure sp_deleted @id int
AS
update Patient set deleted=1 where id= @id
GO

create procedure sp_pagination_patients
As 
select COUNT(id) as totalItem from Patient
Go
/*********PAYMENT**************/

/*Select Payment by patient Id*/
create procedure sp_get_payments_by_patient_id @patient_id int, @skip int, @take int
AS
select id , amount, balance, payment_date
from Payment
where patient_id=@patient_id
Order By payment_date
OFFSET @skip ROWS
FETCH NEXT @take ROWS ONLY;
GO

/*Select all Payment in the range of a given date*/
create procedure sp_get_patient_payments_by_date_range @patient_id int,  @start_date dateTime, @end_date dateTime, @skip int, @take int
AS
select id, amount, balance, payment_date
from Payment 
where patient_id = @patient_id and Convert(DateTime, payment_date) between @start_date and @end_date
Order By payment_date
OFFSET @skip ROWS
FETCH NEXT @take ROWS ONLY;
GO

select id, amount, balance, payment_date
from Payment 
where patient_id = 5

/*get balance */
create procedure sp_get_new_balance @user_id int, @amount decimal
AS
select (balance + @amount) as new_balance from Patient where id = @user_id
Go
/*update balance*/
create procedure sp_update_balance @id int, @balance decimal, @payment_date DateTime
AS
update Patient set balance = @balance, lastPayment = @payment_date  where id = @id
GO
/*add new payment*/
create procedure sp_add_payment 
@amount decimal, 
@balance decimal, 
@patient_id int, 
@payment_date date
AS
insert into Payment (amount, balance, patient_id,payment_date) values (@amount, @balance, @patient_id, @payment_date)
GO
/*Payment pagination */
create procedure sp_pagination_payments
As 
select COUNT(id) as totalItem from Payment
Go

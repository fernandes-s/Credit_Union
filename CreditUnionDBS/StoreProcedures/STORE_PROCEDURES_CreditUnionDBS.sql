--STORE PROCEDURES FOR CreditUnionDBS

---------------------------------------------------------------------------------------
-- 1
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspAllAccounts]
AS
BEGIN
	SELECT * FROM Account
END
---------------------------------------------------------------------------------------
-- 2
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspCheckPassword]
	@user nvarchar(30),
	@pass nvarchar(16)
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM LoginDetails WHERE Username = @user AND [Password] = @pass)
		SELECT 'true' AS UserExists 
	ELSE 
		SELECT 'false' AS UserExists
END
---------------------------------------------------------------------------------------
-- 3
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspCollectAccNum]
	@id int
AS
	SELECT AccountNumber, AccountType, Username, InitialBalance From Account Where AccountNumber = @id
RETURN 0
---------------------------------------------------------------------------------------
-- 4
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspCountAccount]
AS
BEGIN
	SELECT COUNT(AccountNumber) FROM Account
END
---------------------------------------------------------------------------------------
-- 5
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspCreateAccount]
	@username nvarchar(30),
	@fn nvarchar(20),
	@sn nvarchar(20),
	@email nvarchar(30),
	@phone nvarchar(10),
	@add1 nvarchar(50),
	@add2 nvarchar(50),
	@city nvarchar(15),
	@county nvarchar(15),
	@accType nvarchar(15),
	@accountNumber int,
	@sortCode int,
	@bal money,
	@overdraft money
AS
BEGIN
	INSERT INTO Account(Username, Firstname, Surname, Email, Phone, AddressLine1, AddressLine2, City, County, AccountType, AccountNumber, SortCode, InitialBalance, OverdraftLimit)
	VALUES(@username, @fn, @sn, @email, @phone, @add1, @add2, @city, @county, @accType, @accountNumber, @sortCode, @bal, @overdraft)
END
---------------------------------------------------------------------------------------
-- 6
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspFilterByAccType]
	@accType nvarchar(10)
AS
BEGIN
	SELECT * FROM Account WHERE AccountType = @accType
END
---------------------------------------------------------------------------------------
-- 7
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspFreeUsername]
	@username nvarchar(30)
AS
BEGIN
	SELECT COUNT(*) FROM LoginDetails WHERE Username = @username
END
---------------------------------------------------------------------------------------
-- 8
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspGetDeposits]
AS
BEGIN
	SELECT *
	FROM Deposits
END
---------------------------------------------------------------------------------------
-- 9
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspGetTransfer]
AS
BEGIN

	SELECT * FROM [Transfer]
END
---------------------------------------------------------------------------------------
-- 10
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspGetWithdrawals]
AS
BEGIN
	SELECT *
	FROM Withdraws
END
---------------------------------------------------------------------------------------
-- 11
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspInsertDeposit]
	@accNum int,
	@accType nvarchar(10),
	@pBalance money,
	@amt money,
	@newBal money
AS
BEGIN
	INSERT INTO Deposits(AccountNumber, AccountType, PreviousBalance, Amount, NewBalance)
	VALUES(@accNum, @accType, @pBalance, @amt, @newBal)
END
---------------------------------------------------------------------------------------
-- 12
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspInsertLoginDetails]
	@username nvarchar(30),
	@password nvarchar(16)
AS
BEGIN TRAN 
	INSERT INTO LoginDetails(Username, [Password])
	VALUES(@username, @password)

	IF @@ERROR <> 0 OR @@ROWCOUNT <> 1
	BEGIN
	PRINT 'ERROR! NO INSERT HAS BEEN DONE!'
	SELECT ERROR_MESSAGE()
	ROLLBACK TRAN
	RETURN -1
	END
COMMIT TRAN
---------------------------------------------------------------------------------------
-- 13
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspInsertTransfer]
	@senderAccNum int,
	@accType nvarchar(10),
	@bal money,
	@toAccNum int,
	@toAccType nvarchar(10),
	@sortCode int,
	@amt money,
	@date datetime
AS
BEGIN
	INSERT INTO [Transfer](AccountNumber, AccountType, Balance, ToAccountNumber, ToAccountType, SortCode, Amount, [DateTime])
	VALUES(@senderAccNum, @accType, @bal, @toAccNum, @toAccType, @sortCode, @amt, @date)
END
---------------------------------------------------------------------------------------
-- 14
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspInsertWithdraw]
	@accNum int,
	@acType nvarchar(10),
	@bal money,
	@amt money,
	@newBal money
AS
BEGIN
	INSERT INTO Withdraws(AccountNumber, AccountType, Balance, Amount, NewBalance)
	VALUES(@accNum, @acType,@bal,@amt,@newBal)
END
---------------------------------------------------------------------------------------
-- 15
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspMyAccountDetails]
	@accNum int
AS
BEGIN
	SELECT * FROM Account WHERE AccountID = @accNum
END
---------------------------------------------------------------------------------------
-- 16
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspSelectAccNum]
AS
BEGIN
	SELECT * FROM Account 
END
---------------------------------------------------------------------------------------
-- 17
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspUpdateAccount]
	@accNum int,
	@email nvarchar(30),
	@phone nvarchar(10),
	@add1 nvarchar(50),
	@add2 nvarchar(50),
	@city nvarchar(50),
	@cy nvarchar(15)
AS
BEGIN
	UPDATE Account SET Email = @email, Phone = @phone, AddressLine1 = @add1, AddressLine2 = @add2, City = @city, County = @cy
	WHERE AccountID = @accNum
END
---------------------------------------------------------------------------------------
-- 18
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspUpdateBalanceAndOverdraft]
	@newBal money,
	@newOverdraft money,
	@accNum int
AS
BEGIN
	UPDATE Account SET InitialBalance = @newBal, OverdraftLimit = @newOverdraft WHERE AccountNumber = @accNum
END
---------------------------------------------------------------------------------------
-- 19
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspValidadeAccountNumber]
	@accountNumber nvarchar (15)
AS
BEGIN
	SELECT COUNT(*) FROM Account WHERE AccountNumber = @accountNumber
END
---------------------------------------------------------------------------------------
-- 20
---------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[uspValidLogin]
	@user nvarchar(30),
	@pass nvarchar (16)
AS
BEGIN
	SELECT COUNT(*) FROM LoginDetails WHERE [Username] = @user and [Password] = @pass
END
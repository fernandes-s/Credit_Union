CREATE TRIGGER trg_BeforeInsertTransaction
ON dbo.[Transaction]
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @next INT;
    SET @next = NEXT VALUE FOR Seq_RefNumber;

    INSERT INTO dbo.[Transaction] (RefNumber, AccNumber, AccType, Amount)
    SELECT 'DBS' + FORMAT(@next, '00000'), AccNumber, AccType, Amount
    FROM inserted;
END;

USE Gram
GO

ALTER TABLE [dbo].[AspNetUsers]
    ADD CONSTRAINT [FK_AspNetUsers_Employee_EmployeeId]
        FOREIGN KEY ([EmployeeId])
        REFERENCES [Subjects].[Employee] ([Id]);
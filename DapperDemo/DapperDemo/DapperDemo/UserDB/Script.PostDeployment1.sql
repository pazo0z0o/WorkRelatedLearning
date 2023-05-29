if not exists (select 1 from dbo.[User]) --see if any record exists
begin
	insert into dbo.[User] (FirstName,LastName)
	values ('Tim','Corey'),
		   ('Sue', 'Storm'),
		   ('John','Smith'),
		   ('Mary', 'Jones');
end

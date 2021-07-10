select p.id as Id, p.name as Name, p.Category as Category,p.pln as PLN, p.Grosz as Grosz, p.Description as Description, m.name as Manufacturer, m.Address as Address, m.City as City, c.name as Country, p.Amount 
from Produkty as p 
INNER join Manufacturers m 
on p.Manufacturer = m.Id 
inner join Countries c 
on m.CountryCode = c.Code
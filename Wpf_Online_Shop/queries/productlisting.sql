select p.name as produkt,(p.pln*100+p.Grosz)/100.0 as cena, m.name as Producent
from Produkty as p
INNER join Manufacturers m
on p.Manufacturer = m.Id;
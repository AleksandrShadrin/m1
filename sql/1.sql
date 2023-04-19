select sellers.name, sellers.surname,
    COALESCE(B.summary, 0) as volume
from sellers
LEFT JOIN
    (SELECT sales.idsel, SUM(sales.quantity) as summary
        from sales
        where sales.date > '01.10.2013' 
        and sales.date < '07.10.2013'
        GROUP BY sales.idsel) as B
on sellers.id = B.idsel
ORDER BY 1, 2
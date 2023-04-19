select productName, name, surname, quantity / summaryVolume as sales_percent
from sellers
RIGHT JOIN
    (SELECT sales.idsel, p.id as productId,
        sales.quantity as quantity, p.name as productName
        from sales
        LEFT JOIN products as p
        on p.id = sales.idprod
        where sales.date > '01.10.2013'
        and sales.date < '07.10.2013'
        ) as B
on sellers.id = B.idsel
RIGHT JOIN
    (SELECT arrivals.date as arrivalDate, 
        arrivals.idprod as idprod
        from arrivals
        where arrivals.date > '07.09.2013'
        and arrivals.date < '07.10.2013'
    ) as C
on B.productId = C.idprod
LEFT JOIN
    (SELECT sales.idprod, SUM(sales.quantity) as summaryVolume
        from sales
        GROUP BY sales.idprod) as D
on B.productId = D.idprod
ORDER BY 1, 3, 2
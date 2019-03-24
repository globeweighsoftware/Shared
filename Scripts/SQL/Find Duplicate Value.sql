




;WITH T AS
(
SELECT *, 
       COUNT(*) OVER (PARTITION BY LocationId) as Cnt
FROM vwStockLocation
)
SELECT * /*TODO: Add column list. Don't use "*"                   */
FROM T
WHERE Cnt > 1
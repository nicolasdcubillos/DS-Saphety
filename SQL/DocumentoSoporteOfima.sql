--
--		Nombre: Generar medios electrónicos Documento Soporte mediante JSON (DTO) via API Rest - Saphety Colombia.
--
--		Autor: Nicolás David Cubillos
--
--		Contenido: Generar medios electrónicos Documento Soporte mediante JSON (DTO) - Saphety Colombia.
--
--		Fecha: 7 de agosto de 2022.
--


USE AJOVECO_NE	
GO

DROP FUNCTION [dbo].[DS_DOCUMENTOSSOPORTEENVIAR]
GO
DROP FUNCTION [dbo].[DS_DATOSPROVEEDOR]
GO
DROP FUNCTION [dbo].[DS_DETALLEDCTO]
GO
DROP FUNCTION [dbo].[SPLITSTRING]
GO

CREATE FUNCTION dbo.splitstring ( @stringToSplit VARCHAR(MAX) )
RETURNS
 @returnList TABLE ([Name] [nvarchar] (500))
AS
BEGIN

 DECLARE @name NVARCHAR(255)
 DECLARE @pos INT

 WHILE CHARINDEX(' ', @stringToSplit) > 0
 BEGIN
  SELECT @pos  = CHARINDEX(' ', @stringToSplit)  
  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

  INSERT INTO @returnList 
  SELECT @name

  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
 END

 INSERT INTO @returnList
 SELECT @stringToSplit

 RETURN
END


/*
	Función DS_DETALLEDCTO
	Retorna la información del proveedor del Documento Soporte.
	
	SELECT * FROM DS_DETALLEDCTO('DI', 43)
	Select xvalorun,valorunit,cantidad from mvtrade where tipodcto='DS' AND NRODCTO='1180'
	XVALORUN = 0 (i.e. documento en pesos)
	XVALORUN != 0 (i.e. documento en dolares convertido a pesos)
*/

GO

CREATE FUNCTION [dbo].[DS_DETALLEDCTO]
(
@TIPODCTO CHAR (50),
@NRODCTO CHAR (50)
)
Returns Table
as
Return
(
SELECT
(SELECT MEUUID FROM TRADE TR WHERE TR.TIPODCTO = TIPODCTONC AND TR.NRODCTO = NUMFACTNC AND TR.ORIGEN = MVTRADE.ORIGEN) CUDS,
NUMFACTNC,
TIPODCTONC,
NIT,
PRODUCTO,
CANTIDAD,
NOMBRE NOMBRE_PRODUCTO,
IVA PORCENTAJE_IVA,
TARIVA TARIFA_IVA,
PORETE PORCENTAJE_RTEFTE,
(SELECT PRETENIVA FROM TRADE WHERE TRADE.TIPODCTO = MVTRADE.TIPODCTO AND TRADE.NRODCTO = MVTRADE.NRODCTO AND ORIGEN = 'COM') 
PORCENTAJE_RTEIVA, 
(SELECT UNIDINT FROM MTUNIDAD WHERE UNIDAD = MVTRADE.UNDVENTA) 
UNIDAD_MEDIDA,
ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT)) 
VALOR_UNITARIO,
ROUND(CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT)), CAST (REDONDEO.VALOR AS INT))
VALOR_PRODUCTOS,
ROUND(CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT)) * DESCUENTO, CAST (REDONDEO.VALOR AS INT))
DESCUENTO,
ROUND((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) - (CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT)) * DESCUENTO), CAST (REDONDEO.VALOR AS INT))
BASE_GRAVABLE,
ROUND(((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) - (CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT)) * DESCUENTO)) * IVA / 100, CAST (REDONDEO.VALOR AS INT))
VALOR_IVA,
ROUND(((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) - (CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT)) * DESCUENTO)) * PORETE / 100, CAST (REDONDEO.VALOR AS INT))
VALOR_RTEFTE,
ROUND(((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) - (CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT)) * DESCUENTO)) * PORICA / 100, CAST (REDONDEO.VALOR AS INT))
VALOR_RTEICA,
ROUND(((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) - (CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT)) * DESCUENTO)) * IVA / 100, CAST (REDONDEO.VALOR AS INT)) * (SELECT T.PRETENIVA / 100 FROM TRADE T WHERE T.NRODCTO = MVTRADE.NRODCTO AND T.TIPODCTO = MVTRADE.TIPODCTO AND T.ORIGEN = MVTRADE.ORIGEN)
VALOR_RTEIVA,
ROUND(((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) 
- ((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) * DESCUENTO)) 
+ (((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) 
- ((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) * DESCUENTO)) * IVA / 100) 
- (((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) 
- ((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) * DESCUENTO)) * PORETE / 100) 
- (((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) 
- ((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) * DESCUENTO)) * PORICA / 1000), CAST (REDONDEO.VALOR AS INT))
- ROUND(((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) - 
(CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT)) * DESCUENTO)) * IVA / 100, CAST (REDONDEO.VALOR AS INT)) 
* (SELECT T.PRETENIVA / 100 FROM TRADE T WHERE T.NRODCTO = MVTRADE.NRODCTO AND T.TIPODCTO = MVTRADE.TIPODCTO AND T.ORIGEN = MVTRADE.ORIGEN)
VALOR_NETO,
ROUND(((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) 
- ((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) * DESCUENTO)) 
+ (((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) 
- ((CANTIDAD * ROUND(VALORUNIT, CAST (REDONDEO.VALOR AS INT))) * DESCUENTO)) * IVA / 100),CAST (REDONDEO.VALOR AS INT))
SUBTOTAL_NETO
FROM
MVTRADE,
MTGLOBAL REDONDEO
WHERE
TIPODCTO = @TIPODCTO AND
NRODCTO = @NRODCTO AND
ORIGEN = 'COM' AND
REDONDEO.CAMPO = 'REDONCOM' 
)

/*
	Función DS_DOCUMENTOSSOPORTEENVIAR
	Retorna la lista de Documento(s) Doporte a enviar en un periodo dado
	
	SELECT * FROM DS_DOCUMENTOSSOPORTEENVIAR('DS DI', '20200901', '20230929')
*/

GO

CREATE FUNCTION [dbo].[DS_DOCUMENTOSSOPORTEENVIAR]
(
@tipodcto char(50),
@fecha1 date,
@fecha2 date
)
Returns Table
AS
Return
(
SELECT
1 AS ENVIO,
CASE WHEN T.OTRAMON = 'S' THEN 'USD' ELSE 'COP' END MONEDA,
T.TIPODCTO, 
T.NRODCTO,
CAST(FORMAT(T.FECHA, 'yyyy-MM-dd') AS CHAR) FECHA,
T.NIT,
T.NOTA,
(SELECT SUM(VALOR_NETO) FROM DS_DETALLEDCTO(T.TIPODCTO, T.NRODCTO)) NETO,
T.PASSWORDIN,
(SELECT SUM(SUBTOTAL_NETO) FROM DS_DETALLEDCTO(T.TIPODCTO, T.NRODCTO)) SUBTOTAL_NETO
FROM
TRADE T
WHERE 
T.TIPODCTO IN (SELECT * FROM dbo.splitstring(@tipodcto)) AND
T.MEUUID = '' AND
T.FECHA BETWEEN @fecha1 AND @fecha2
)

/*
	Función VS_DATOSPROVEEDOR
	Retorna la información del proveedor del Documento Soporte.
	
	SELECT * FROM DS_DATOSPROVEEDOR('444444304-0')
*/

GO

CREATE FUNCTION [dbo].[DS_DATOSPROVEEDOR]
(
@nit char(500)
)
Returns Table
as
Return
(
SELECT 
PROV.NOMBRE,
PROV.CDCIIU,
PROV.DIRECCION,
PROV.EMAILP,
PROV.CODPOSTAL,
PAISES.ISO_3166_1 CODPAIS
FROM
MTPROCLI PROV,
MTPAISES PAISES
WHERE 
NIT = @nit AND
PROV.PAIS = PAISES.CODIGO
)

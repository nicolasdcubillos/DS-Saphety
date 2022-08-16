--
--		Nombre: Generar medios electr�nicos Documento Soporte mediante JSON (DTO) via API Rest - Saphety Colombia.
--
--		Autor: Nicol�s David Cubillos
--
--		Contenido: Generar medios electr�nicos Documento Soporte mediante JSON (DTO) - Saphety Colombia.
--
--		Fecha: 7 de agosto de 2022.
--

DROP FUNCTION [dbo].[DS_DOCUMENTOSSOPORTEENVIAR]
GO
DROP FUNCTION [dbo].[DS_DATOSPROVEEDOR]
GO
DROP FUNCTION [dbo].[DS_DETALLEDCTO]
GO

/*
	Funci�n DS_DOCUMENTOSSOPORTEENVIAR
	Retorna la lista de Documento(s) Doporte a enviar en un periodo dado
	
	SELECT * FROM X_DOCUMENTOSSOPORTEENVIAR('20200101', '20220101')
*/


CREATE FUNCTION [dbo].[DS_DOCUMENTOSSOPORTEENVIAR]
(
@fecha1 date,
@fecha2 date
)
Returns Table
as
Return
(
SELECT
1 AS ENVIO,
T.TIPODCTO, 
T.NRODCTO,
T.FECHA,
T.NIT,
T.NOTA
FROM
TRADE T
WHERE 
T.TIPODCTO = 'DS' AND
T.MEUUID = '' AND
FECHA BETWEEN @fecha1 AND @fecha2
)

/*
	Funci�n VS_DATOSPROVEEDOR
	Retorna la informaci�n del proveedor del Documento Soporte.
	
	SELECT * FROM DS_DATOSPROVEEDOR('1073383868-5')
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

/*
	Funci�n DS_DETALLEDCTO
	Retorna la informaci�n del proveedor del Documento Soporte.
	
	SELECT * FROM DS_DETALLEDCTO('DS', 1)
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
CANTIDAD
FROM
MVTRADE
WHERE
TIPODCTO = @TIPODCTO AND
NRODCTO = @NRODCTO
)

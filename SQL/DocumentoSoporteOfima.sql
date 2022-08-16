--
--		Nombre: Generar medios electrónicos Documento Soporte mediante JSON (DTO) via API Rest - Saphety Colombia.
--
--		Autor: Nicolás David Cubillos
--
--		Contenido: Generar medios electrónicos Documento Soporte mediante JSON (DTO) - Saphety Colombia.
--
--		Fecha: 7 de agosto de 2022.
--

DROP FUNCTION [dbo].[DS_DOCUMENTOSSOPORTEENVIAR]
GO
DROP FUNCTION [dbo].[DS_DATOSPROVEEDOR]
GO

/*
	Función DS_DOCUMENTOSSOPORTEENVIAR
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
	Función VS_DATOSPROVEEDOR
	Retorna la información del proveedor del Documento Soporte.
	
	SELECT * FROM DS_DATOSPROVEEDOR
*/

GO

CREATE VIEW DS_DATOSPROVEEDOR AS 
(
SELECT
VALOR AS NIT
FROM
MTGLOBAL
WHERE
CAMPO = 'NITCIA'
)

SELECT * FROM MTGLOBAL WHERE CAMPO = 'NITCIA'
SELECT * FROM MTPROCLI
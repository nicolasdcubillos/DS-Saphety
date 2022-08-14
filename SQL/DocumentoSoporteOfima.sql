--
--		Nombre: Generar medios electrónicos Documento Soporte mediante JSON (DTO) via API Rest - Saphety Colombia.
--
--		Autor: Nicolás David Cubillos
--
--		Contenido: Generar medios electrónicos Documento Soporte mediante JSON (DTO) - Saphety Colombia.
--
--		Fecha: 7 de agosto de 2022.
--

DROP FUNCTION [dbo].[X_DOCUMENTOSSOPORTEENVIAR]
GO


/*
	Función X_DOCUMENTOSSOPORTEENVIAR
	Retorna la lista de Documento(s) Doporte a enviar en un periodo dado
	
	SELECT * FROM X_DOCUMENTOSSOPORTEENVIAR('20200101', '20220101')
*/

CREATE FUNCTION [dbo].[X_DOCUMENTOSSOPORTEENVIAR]
(
@fecha1 date,
@fecha2 date
)
Returns Table
as
Return
(
SELECT
1 ENVIO,
TIPODCTO, 
NRODCTO,
NIT,
NOTA
FROM
TRADE
WHERE 
TIPODCTO = 'DS' AND
MEUUID = '' AND
FECHA BETWEEN @fecha1 AND @fecha2
)

SELECT 1, BODEGA, ORIGEN, TIPODCTO, NRODCTO, FECHA, NIT, VALORUNIT, XVALORUN, IVA from MVTRADE
WHERE ORIGEN = 'COM' AND TIPODCTO = 'DS'
AND FECHA BETWEEN '20220101' AND '20220731'

/*
	Función X_DETALLEDOCUMENTOSOPORTE
	Retorna el detalle del Documento Soporte a enviar.
*/
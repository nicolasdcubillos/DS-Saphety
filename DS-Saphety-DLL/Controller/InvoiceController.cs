using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DS_Saphety_DLL.Controller
{
    internal class InvoiceController
    {
        private SaphetyController saphetyController = new SaphetyController();
        private PropertiesController properties = new PropertiesController();
        public String enviarDocumentoSoporte ()
        {
            DocumentoSoporteDTO documentoSoporteDTO = new DocumentoSoporteDTO ();
            CreacionDocumentoDTO respuesta = saphetyController.enviarDocumentoSoporte(documentoSoporteDTO);

            /*
             * Lógica de negocio acá
             */

            properties.write("aca jajajaja", JsonConvert.SerializeObject(respuesta));
            return respuesta.ResultData.id;
        }
    }
}

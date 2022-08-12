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
            return respuesta.ResultData.id;
        }
        public Boolean getAccessToken()
        {
            String access_token = null;
            var expirationDate = DateTime.Parse(properties.read("TOKEN_EXPIRATION"));
            DateTime actualDate = DateTime.Now;
            if (expirationDate < actualDate) {
                TokenRequestDTO tokenRequest = new TokenRequestDTO();
                tokenRequest.username = properties.read("USERNAME");
                tokenRequest.password = properties.read("PASSWORD");
                tokenRequest.virtual_operator = properties.read("VIRTUAL_OPERATOR");
                TokenDTO token = saphetyController.getAccessToken(tokenRequest);
                properties.write("TOKEN_EXPIRATION", token.ResultData.expires.ToString());
                properties.write("ACCESS_TOKEN", token.ResultData.access_token);
                return true;
            } else {
                access_token = properties.read("ACCESS_TOKEN");
            }
            return true;
        }
    }
}

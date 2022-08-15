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
        private static SaphetyController saphetyController = new SaphetyController();
        private static PropertiesController properties = new PropertiesController();
        private static List<string> empresasAutorizadas = new List<string>();
        private static String SERIE_EXTERNAL_KEY = properties.read("SERIE_EXTERNAL_KEY");
        public InvoiceController ()
        {
            empresasAutorizadas.Add("860010268-1");
        }
        public String enviarDocumentoSoporte (DocumentoSoporteDTO documentoSoporteDTO)
        {
            documentoSoporteDTO.SerieExternalKey = SERIE_EXTERNAL_KEY;
            CreacionDocumentoDTO respuesta = saphetyController.enviarDocumentoSoporte(documentoSoporteDTO);
            return respuesta.ResultData.id;
        }
        private Boolean getAccessToken()
        {
            String access_token = null;
            DateTime expirationDate;

            try {
                expirationDate = DateTime.Parse(properties.read("TOKEN_EXPIRATION"));
            } catch {
                expirationDate = DateTime.Parse("2000-01-01");
            }
            
            DateTime actualDate = DateTime.Now;
            if (expirationDate < actualDate) {
                TokenRequestDTO tokenRequest = new TokenRequestDTO();
                tokenRequest.username = properties.read("USERNAME");
                tokenRequest.password = properties.read("PASSWORD");
                tokenRequest.virtual_operator = properties.read("VIRTUAL_OPERATOR");
                TokenDTO token = saphetyController.getAccessToken(tokenRequest);
                properties.write("TOKEN_EXPIRATION", token.ResultData.expires);
                properties.write("ACCESS_TOKEN", token.ResultData.access_token);
            } else {
                access_token = properties.read("ACCESS_TOKEN");
            }
            saphetyController.setToken(access_token);
            return true;
        }
        public Boolean auth(string empresa)
        {
            return empresasAutorizadas.Contains(empresa) == true ? getAccessToken() : false;
        }
    }
}

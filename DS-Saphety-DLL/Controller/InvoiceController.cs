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
            empresasAutorizadas.Add("800145400-8");
            empresasAutorizadas.Add("900141348-7");
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

        public Boolean saveConfig (ConfiguracionDTO configuracion)
        {
            try {
                properties.write("PATH", configuracion.PATH);
                properties.write("WS_URL_PRUEBAS", configuracion.WS_URL_PRUEBAS);
                properties.write("WS_URL_PRODUCCION", configuracion.WS_URL_PRODUCCION);
                properties.write("AMBIENTE", configuracion.AMBIENTE);
                properties.write("VIRTUAL_OPERATOR", configuracion.VIRTUAL_OPERATOR);
                properties.write("USERNAME", configuracion.USERNAME);
                properties.write("PASSWORD", configuracion.PASSWORD);
                properties.write("SERIE_EXTERNAL_KEY", configuracion.SERIE_EXTERNAL_KEY);
                return true;
            } catch {
                return false;
            }
        }
        public ConfiguracionDTO loadConfig ()
        {
            ConfiguracionDTO configuracion = new ConfiguracionDTO();
            configuracion.PATH = properties.read("PATH");
            configuracion.WS_URL_PRUEBAS = properties.read("WS_URL_PRUEBAS");
            configuracion.WS_URL_PRODUCCION = properties.read("WS_URL_PRODUCCION");
            configuracion.AMBIENTE = properties.read("AMBIENTE");
            configuracion.VIRTUAL_OPERATOR = properties.read("VIRTUAL_OPERATOR");
            configuracion.USERNAME = properties.read("USERNAME");
            configuracion.PASSWORD = properties.read("PASSWORD");
            configuracion.SERIE_EXTERNAL_KEY = properties.read("SERIE_EXTERNAL_KEY");
            return configuracion;
        }
    }
}

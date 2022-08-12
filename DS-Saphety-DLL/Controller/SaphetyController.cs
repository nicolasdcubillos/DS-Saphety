using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DS_Saphety_DLL.Utils;
using Newtonsoft.Json;

namespace DS_Saphety_DLL.Controller
{
    internal class SaphetyController
    {
        private static HttpClient client = new HttpClient();
        private static PropertiesController properties = new PropertiesController();
        private static String URL_WS = properties.read("AMBIENTE") == "1" ? properties.read("WS_URL_PRUEBAS") : properties.read("WS_URL_PRODUCCION");

        /*
         * TODO JWT Access Token
         */


        /*
         * Enviar documento soporte
         */
        
        public CreacionDocumentoDTO enviarDocumentoSoporte (DocumentoSoporteDTO documentoSoporteDTO)
        {
            try
            {
                var task = enviarDocumentoSoporteAsync(documentoSoporteDTO);
                task.Wait();
                return task.Result;
            } catch (Exception exception) {
                var st = new StackTrace(exception, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new Exception(string.Format("Error en threading al enviar Documento Soporte: ", exception.Message) + "\nStacktrace: [" + st + "]");
            }
        }
        public async Task<CreacionDocumentoDTO> enviarDocumentoSoporteAsync(DocumentoSoporteDTO documentoSoporteDTO)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var requestBody = JsonConvert.SerializeObject(documentoSoporteDTO);
            Uri uri = new Uri(URL_WS + WS_URL.ENVIAR_DOCUMENTO_SOPORTE.getUrl());
            properties.write("uri", uri.ToString());
            try
            { 
                var response = await client.PostAsync(uri,
                                        new StringContent(requestBody, Encoding.UTF8, "application/json"));
                var responseBody = await response.Content.ReadAsStringAsync();
                var respuestaObj = JsonConvert.DeserializeObject<CreacionDocumentoDTO>(responseBody);
                return respuestaObj;
            } catch (Exception ex) {
                properties.write("Exception", ex.ToString());
            }

            //response.EnsureSuccessStatusCode();

            return null;

            
            
        }


    }
}

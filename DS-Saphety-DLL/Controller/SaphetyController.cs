using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private static String URL_WS;
        public SaphetyController ()
        {
            URL_WS = properties.read("AMBIENTE") == "1" ? properties.read("WS_URL_PRUEBAS") : properties.read("WS_URL_PRODUCCION");
        }
        public void setToken(string ACCESS_TOKEN)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);
        }
        public TokenDTO getAccessToken (TokenRequestDTO tokenRequestDTO)
        {
            try {
                var task = postAsync(tokenRequestDTO, WS_URL.SOLICITAR_TOKEN);
                task.Wait();
                var respuestaObj = JsonConvert.DeserializeObject<TokenDTO>(task.Result);
                return respuestaObj;
            } catch (AggregateException exception) {
                var st = new StackTrace(exception, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new Exception("[Threading] " + st);
            } catch { throw; }
        }

        public CreacionDocumentoDTO enviarDocumentoSoporte (DocumentoSoporteDTO documentoSoporteDTO)
        {
            try {
                var task = postAsync(documentoSoporteDTO, WS_URL.ENVIAR_DOCUMENTO_SOPORTE);
                task.Wait();
                var respuestaObj = JsonConvert.DeserializeObject<CreacionDocumentoDTO>(task.Result);
                return respuestaObj;
            } catch (AggregateException exception) { 
                var st = new StackTrace(exception, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new Exception("[Threading] " + st);
            } catch { throw; }
        }

        private async Task<string> postAsync(Object requestBodyDTO, WS_URL requestType)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var requestBody = JsonConvert.SerializeObject(requestBodyDTO);
            Uri uri = new Uri(URL_WS + requestType.getUrl());
            try { 
                var response = await client.PostAsync(uri, new StringContent(requestBody, Encoding.UTF8, "application/json"));
                return await response.Content.ReadAsStringAsync();
            } catch (Exception ex) {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                properties.write("exceptiontask", string.Format("[Peticion POST] ", ex.Message) + "\nStacktrace: [" + st + "]");
                throw new Exception(string.Format("[Peticion POST] ", ex.Message) + "\nStacktrace: [" + st + "]");
            }
        }
    }
}

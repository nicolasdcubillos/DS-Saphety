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
        private static String URL_WS = properties.read("AMBIENTE") == "1" ? properties.read("WS_URL_PRUEBAS") : properties.read("WS_URL_PRODUCCION");
        private static String ACCESS_TOKEN = properties.read("ACCESS_TOKEN");
        public SaphetyController()
        {
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer ");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

        }
        public TokenDTO getAccessToken (TokenRequestDTO tokenRequestDTO)
        {
            try {
                var task = postAsync(tokenRequestDTO, WS_URL.SOLICITAR_TOKEN);
                task.Wait();
                var respuestaObj = JsonConvert.DeserializeObject<TokenDTO>(task.Result);
                validateErrors(respuestaObj);
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
                validateErrors(respuestaObj, documentoSoporteDTO.SerieNumber);
                return respuestaObj;
            } catch (AggregateException exception) { 
                var st = new StackTrace(exception, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new Exception("[Threading] " + st);
            } catch { throw; }
        }

        private void validateErrors(RespuestaSaphetyDTO respuesta, string serieNumber = "-1")
        {
            //TODO Log errors and multiplicity about list of errors
            if (respuesta.errors != null && respuesta.errors.Count > 0)
                throw new Exception("[Respuesta Saphety DS " + serieNumber + "]: " + JsonConvert.SerializeObject(respuesta.errors[0]));
            return;
        }

        private async Task<string> postAsync(Object requestBodyDTO, WS_URL requestType)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var requestBody = JsonConvert.SerializeObject(requestBodyDTO);
            properties.write("poniendoaccesstoken", ACCESS_TOKEN);
            properties.write("last", requestBody);
            Uri uri = new Uri(URL_WS + requestType.getUrl());
            try { 
                var response = await client.PostAsync(uri, new StringContent(requestBody, Encoding.UTF8, "application/json"));
                properties.write("debugresponse", JsonConvert.SerializeObject(response));
                return await response.Content.ReadAsStringAsync();
            } catch (Exception ex) {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new Exception(string.Format("[Peticion POST] ", ex.Message) + "\nStacktrace: [" + st + "]");
            }
        }
    }
}

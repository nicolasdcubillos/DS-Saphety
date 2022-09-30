using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private static String FOLDER_DOCS = "\\DocumentosSoporte\\";
        private static String ERRORS_FILE = "LogErroresDS.txt";
        public InvoiceController () {
            empresasAutorizadas.Add("860010268-1");
            empresasAutorizadas.Add("800145400-8");
            empresasAutorizadas.Add("900141348-7");
        }
        public String enviarDocumentoSoporte(DocumentoSoporteDTO documentoSoporteDTO)
        {
            try
            {
                saveDoc(documentoSoporteDTO);
                CreacionDocumentoDTO respuesta = saphetyController.enviarDocumentoSoporte(documentoSoporteDTO);
                if (respuesta.errors.Count > 0)
                {
                    catchErrors(respuesta.errors, documentoSoporteDTO.CorrelationDocumentId);
                    return "ERROR";
                }
                else return respuesta.ResultData.CUFE;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                writeErrorInLog(documentoSoporteDTO.CorrelationDocumentId, "(" + ex + ") " + st.ToString());
                return "ERROR";
            }
        }

        public String enviarAjusteDocumento(DocumentoSoporteAjusteDTO documentoSoporteAjusteDTO)
        {
            try
            {
                saveDoc(documentoSoporteAjusteDTO);
                CreacionDocumentoDTO respuesta = saphetyController.enviarAjusteDocumento(documentoSoporteAjusteDTO);
                if (respuesta.errors.Count > 0)
                {
                    catchErrors(respuesta.errors, documentoSoporteAjusteDTO.CorrelationDocumentId);
                    return "ERROR";
                }
                else return respuesta.ResultData.CUFE;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                writeErrorInLog(documentoSoporteAjusteDTO.CorrelationDocumentId, "(" + ex + ") " + st.ToString());
                return "ERROR";
            }
        }
        private void catchErrors(List<WarningErrorDTO> errors, String CorrelationDocumentId)
        {
            foreach (WarningErrorDTO error in errors)
            {
                String errorMessage = " - Field: " + error.Field
                                  + "\n - Code: " + error.Code
                                  + "\n - Description: " + error.Description;
                foreach (String st in error.ExplanationValues)
                {
                    errorMessage += "\n - Explanation Value: " + st;
                }
                writeErrorInLog(CorrelationDocumentId, errorMessage);
            }
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
                access_token = token.ResultData.access_token;
            } else {
                access_token = properties.read("ACCESS_TOKEN");
            }
            saphetyController.setToken(access_token);
            return true;
        }
        public Boolean auth(string empresa) { return empresasAutorizadas.Contains(empresa) == true ? getAccessToken() : false; }
        public void writeErrorInLog(String DocumentSerial, String error) {
            String path = properties.read("PATH") + FOLDER_DOCS;
            validatePath(path);
            StreamWriter sw = new StreamWriter(path + ERRORS_FILE, true);
            sw.WriteLine("[Respuesta Saphety " + DocumentSerial + "]\n" + error);
            sw.Close();
        }
        
        public Boolean saveConfig (ConfiguracionDTO configuracion)
        {
            try {
                properties.write("PATH", configuracion.PATH);
                properties.write("AMBIENTE", configuracion.AMBIENTE);
                properties.write("VIRTUAL_OPERATOR", configuracion.VIRTUAL_OPERATOR);
                properties.write("USERNAME", configuracion.USERNAME);
                properties.write("PASSWORD", configuracion.PASSWORD);
                properties.write("TIPO_DCTO", configuracion.TIPO_DCTO);
                properties.write("NOTA_AJUSTE", configuracion.NOTA_AJUSTE);
                properties.write("ACCESS_TOKEN", "");
                properties.write("TOKEN_EXPIRATION", "");
                return true;
            } catch {
                return false;
            }
        }
        public ConfiguracionDTO loadConfig () {
            ConfiguracionDTO configuracion = new ConfiguracionDTO();
            configuracion.PATH = properties.read("PATH");
            configuracion.WS_URL_PRUEBAS = properties.read("WS_URL_PRUEBAS");
            configuracion.WS_URL_PRODUCCION = properties.read("WS_URL_PRODUCCION");
            configuracion.AMBIENTE = properties.read("AMBIENTE");
            configuracion.VIRTUAL_OPERATOR = properties.read("VIRTUAL_OPERATOR");
            configuracion.USERNAME = properties.read("USERNAME");
            configuracion.PASSWORD = properties.read("PASSWORD");
            configuracion.TIPO_DCTO = properties.read("TIPO_DCTO");
            configuracion.NOTA_AJUSTE = properties.read("NOTA_AJUSTE");
            return configuracion;
        }
        private void saveDoc (DocumentoSoporteDTO documento) {
            String path = properties.read("PATH") + FOLDER_DOCS;
            validatePath(path);
            string json = JsonConvert.SerializeObject(documento, Formatting.Indented);
            File.WriteAllText(path + documento.SeriePrefix + documento.SerieNumber + "-" + documento.CorrelationDocumentId + ".json", json);
        }       
        private void validatePath (string path) { if (!Directory.Exists(path)) { Directory.CreateDirectory(path); } }
 
    }
}
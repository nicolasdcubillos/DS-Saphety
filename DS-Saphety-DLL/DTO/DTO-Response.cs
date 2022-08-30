using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_Saphety_DLL
{ 
    internal class WarningErrorDTO
    {
        public string Field { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public List <String> ExplanationValues { get; set; }
    }
    internal class RespuestaSaphetyDTO
    {
        public bool isValid { get; set; }
        public List <WarningErrorDTO> warnings { get; set; }
        public List <WarningErrorDTO> errors { get; set; } 
        public long ResultCode { get; set; }
        public Object ResultData { get; set; }
    }

    /*
     * Token DTO
     */
    internal class DataTokenDTO
    {
        public string access_token { get; set; }
        public string expires { get; set; }
        public string token_type { get; set; }
    }

    internal class TokenDTO : RespuestaSaphetyDTO
    {
        public DataTokenDTO ResultData { get; set; }
    }

    /*
     * CreacionDocumentoDTO
     */
    internal class DataCreacionDocumentoDTO
    {
        public string id { get; set; }
        public string CorrelationDocumentId { get; set; }
        public string CUFE { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
    }

    internal class CreacionDocumentoDTO : RespuestaSaphetyDTO
    {
        public DataCreacionDocumentoDTO ResultData { get; set; } = new DataCreacionDocumentoDTO();
    }

    /*
     * ConsultarDocumentoDTO
     */

    internal class DocumentoConsultaDTO
    {
        public string id { get; set; }
        public string DocumentType { get; set; }
        public string DocumentSubType { get; set; }
        public string DocumentNumber { get; set; }
        public string OriginId { get; set; }
        public string OriginName { get; set; }
        public string OriginCode { get; set; }
        public string DestinationId { get; set; }
        public string DestinationName { get; set; }
        public string DestinationCode { get; set; }
        public string DocumentDate { get; set; }
        public string CreationDate { get; set; }
        public string DocumentStatus { get; set; }
        public string DocumentStatusDate { get; set; }
        public string DocumentStatusApplicationResponseInvalidSignature { get; set; }
        public string DocumentStatusDIANSynchronizationSucessful { get; set; }
        public string CommunicationStatus { get; set; }
        public string CommunicationStatusComments { get; set; }
        public string MainEmailNotification { get; set; }
        public string MainEmailNotificationStatus { get; set; }
        public string MainEmailNotificationStatusReason { get; set; }
        public string Currency { get; set; }
        public string TotalAmount { get; set; }
        public string Cufe { get; set; }
        public string PaymentStatus { get; set; }
    }
    internal class DataConsultarDocumentoDTO
    {
        public List <DocumentoConsultaDTO> items { get; set; }
        public long total { get; set; }
    }
    internal class ConsultarDocumentoDTO : RespuestaSaphetyDTO
    {
        public DataConsultarDocumentoDTO ResultData { get; set; }
    }
}

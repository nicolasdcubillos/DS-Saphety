using System.Runtime.InteropServices;
using DS_Saphety_DLL.Controller;

namespace DS_Saphety_DLL
{
    public class DocumentoSoporte
    {
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        public interface DLLInterface
        {
            [DispId(0)]
            string enviarDocumentoSoporte(DocumentoSoporteDTO documentoSoporteDTO);
            [DispId(1)]
            bool auth(string empresa);
        }

        [ComSourceInterfaces(typeof(DLLInterface))]
        [ClassInterface(ClassInterfaceType.AutoDual)]
        [ProgId("DSSaphety.Class")]
        [ComVisible(true)]
        public class DSSaphety : DLLInterface
        {
            private InvoiceController invoiceController = new InvoiceController();
            public string enviarDocumentoSoporte (DocumentoSoporteDTO documentoSoporteDTO)
            {
                return invoiceController.enviarDocumentoSoporte(documentoSoporteDTO);
            }
            public bool auth(string empresa)
            {
                return invoiceController.auth(empresa);
            }
        }
    }
}

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
            string enviarDocumentoSoporte();
            [DispId(1)]
            bool getAccessToken();
        }

        [ComSourceInterfaces(typeof(DLLInterface))]
        [ClassInterface(ClassInterfaceType.AutoDual)]
        [ProgId("DSSaphety.Class")]
        [ComVisible(true)]
        public class DSSaphety : DLLInterface
        {
            private InvoiceController invoiceController = new InvoiceController();
            public string enviarDocumentoSoporte ()
            {
                return invoiceController.enviarDocumentoSoporte();
            }
            public bool getAccessToken()
            {
                return invoiceController.getAccessToken();
            }
        }
    }
}

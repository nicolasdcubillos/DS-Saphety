using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace DS_Saphety_DLL
{
    public class DocumentoSoporte
    {
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        public interface DLLInterface
        {
            [DispId(0)]
            bool test();
        }

        [ComSourceInterfaces(typeof(DLLInterface))]
        [ClassInterface(ClassInterfaceType.AutoDual)]
        [ProgId("DSSaphety.Class")]
        [ComVisible(true)]
        public class DSSaphety : DLLInterface
        {
            public bool test()
            {
                return true;
            }
        }
    }
}

using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace DS_Saphety_DLL
{
    public class GetTokenDTO
    {
        public string username { get; set; }
        public string password { get; set; }
        public string virtual_operator { get; set; }

    }
    public class DocumentoSoporte
    {
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        public interface DLLInterface
        {
            [DispId(0)]
            string test();
        }

        [ComSourceInterfaces(typeof(DLLInterface))]
        [ClassInterface(ClassInterfaceType.AutoDual)]
        [ProgId("DSSaphety.Class")]
        [ComVisible(true)]
        public class DSSaphety : DLLInterface
        {
            public string test()
            {
                var objPrueba = new GetTokenDTO();
                objPrueba.password = "password321";
                objPrueba.username = "username123";
                objPrueba.virtual_operator = "voperator444";
                return JsonConvert.SerializeObject(objPrueba);
            }
        }
    }
}

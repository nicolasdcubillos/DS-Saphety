using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DS_Saphety_DLL
{
    public class DocumentoSoporte
    {
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        public interface IProbaDll
        {
            [DispId(0)]
            int Add(int a, int b);
            [DispId(1)]
            string HelloWorld();
        }

        [ComSourceInterfaces(typeof(IProbaDll))]
        [ClassInterface(ClassInterfaceType.AutoDual)]
        [ProgId("DSSaphety.Class")]
        [ComVisible(true)]
        public class DSSaphety : IProbaDll
        {
            public int Add(int a, int b)
            {
                return a * b;
            }
            public string HelloWorld()
            {
                return "Hello world";
            }
        }
    }
}

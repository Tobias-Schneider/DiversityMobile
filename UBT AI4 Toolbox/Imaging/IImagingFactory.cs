using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace UBT.AI4.Toolbox.Imaging
{
    // Pulled from imaging.h in the Windows Mobile 5.0 Pocket PC SDK
    [ComImport]
    [Guid("327ABDA7-072B-11D3-9D7B-0000F81EF32E")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IImagingFactory
    {
        uint CreateImageFromStream(); // This is a place holder
        uint CreateImageFromFile(string filename, out IImage image);

        // We need the MarshalAs attribute here to keep COM interop from sending the buffer down as a Safe Array.
        uint CreateImageFromBuffer([MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint size, BufferDisposalFlag disposalFlag, out IImage image);

        uint CreateNewBitmap();            // This is a place holder
        uint CreateBitmapFromImage();      // This is a place holder
        uint CreateBitmapFromBuffer();     // This is a place holder
        uint CreateImageDecoder();         // This is a place holder
        uint CreateImageEncoderToStream(); // This is a place holder
        uint CreateImageEncoderToFile();   // This is a place holder
        uint GetInstalledDecoders();       // This is a place holder
        uint GetInstalledEncoders();       // This is a place holder
        uint InstallImageCodec();          // This is a place holder
        uint UninstallImageCodec();        // This is a place holder
    }   
}

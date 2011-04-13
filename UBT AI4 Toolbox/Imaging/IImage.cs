using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace UBT.AI4.Toolbox.Imaging
{
    // Pulled from imaging.h in the Windows Mobile 5.0 Pocket PC SDK
    [ComImport]
    [Guid("327ABDA9-072B-11D3-9D7B-0000F81EF32E")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IImage
    {
        uint GetPhysicalDimension(out Size size);
        uint GetImageInfo(out ImageInfo info);
        uint SetImageFlags(uint flags);

        // "Correct" declaration: uint Draw(IntPtr hdc, ref Rectangle dstRect, ref Rectangle srcRect);
        uint Draw(IntPtr hdc, ref Rectangle dstRect, IntPtr NULL);

        uint PushIntoSink(); // This is a place holder
        uint GetThumbnail(uint thumbWidth, uint thumbHeight, out IImage thumbImage);
    }
}

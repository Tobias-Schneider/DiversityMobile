using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace UBT.AI4.Toolbox.Imaging.AlphaBlending
{
    public class AlphaBlendAPI
    {
        [DllImport("coredll.dll")]
        extern public static Int32 AlphaBlend(IntPtr hdcDest,
                                              Int32 xDest,
                                              Int32 yDest,
                                              Int32 cxDest,
                                              Int32 cyDest,
                                              IntPtr hdcSrc,
                                              Int32 xSrc,
                                              Int32 ySrc,
                                              Int32 cxSrc,
                                              Int32 cySrc,
                                              BlendFunction blendFunction);
    }
}

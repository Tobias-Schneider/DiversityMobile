//#######################################################################
//Diversity Mobile Synchronization
//Project Homepage:  http://www.diversitymobile.net
//Copyright (C) 2011  Tobias Schneider, Lehrstuhl Angewndte Informatik IV, Universität Bayreuth
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//#######################################################################
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

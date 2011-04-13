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

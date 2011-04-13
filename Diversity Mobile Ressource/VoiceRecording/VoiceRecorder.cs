using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;

namespace UBT.AI4.Bio.DiversityCollection.Ressource.VoiceRecording
{
    public class VoiceRecorder
    {
        #region API prototypes
        [DllImport("voicectl.dll", EntryPoint = "VoiceRecorder_Create")]
         private unsafe static extern IntPtr VoiceRecorder_Create(CM_VOICE_RECORDER* voicerec);
  
         [DllImport("coredll.dll", EntryPoint = "GetForegroundWindow")]
         private unsafe static extern IntPtr GetForegroundWindow();
         #endregion
  
         [StructLayout(LayoutKind.Sequential)]
         public unsafe struct CM_VOICE_RECORDER
         {
             public int cb;
             public wndStyle dwStyle;
             public int xPos;
             public int yPos;
             public IntPtr hwndParent;
             public int id;
             public char* lpszRecordFileName;
         }
  
         public enum wndStyle : uint
         {
             VRS_NO_OKCANCEL = 0x0001, // No OK/CANCLE displayed
             VRS_NO_NOTIFY = 0x0002, // No parent Notifcation
             VRS_MODAL = 0x0004, // Control is Modal                 VRS_NO_OK = 0x0008, // No OK displayed
             VRS_NO_RECORD = 0x0010, // No RECORD button displayed
             VRS_PLAY_MODE = 0x0020, // Immediately play supplied file when launched
             VRS_NO_MOVE = 0x0040, // Grip is removed and cannot be moved around by the user
             VRS_RECORD_MODE = 0x0080, // Immediately record when launched
             VRS_STOP_DISMISS = 0x0100 // Dismiss control when stopped
         }
  
         private unsafe CM_VOICE_RECORDER _VoiceRec;
         private IntPtr _hRecorder;
         private string wavFile = @"\My Documents\VRec_0.wav";
  
         private IntPtr _Hwnd = (IntPtr)0;
  
         public IntPtr Hwnd
         {
             get { return _Hwnd; }
             set
             {
                 _VoiceRec.hwndParent = value;
                 _Hwnd = value;
             }
         }
  
         public unsafe VoiceRecorder(string _audioFile)
         {
             wavFile = _audioFile;
             _hRecorder = new IntPtr();
             char[] temp = new char[200];
  
             this.Hwnd = GetForegroundWindow();
  
             // Populate temp with the file path of the WAV file            
             Buffer.BlockCopy(wavFile.ToCharArray(), 0, temp, 0, 2 * wavFile.Length);
  
             fixed (char* lpszFileName = temp)
             {
                 _VoiceRec = new CM_VOICE_RECORDER();
  
                 _VoiceRec.hwndParent = _Hwnd;
                 _VoiceRec.dwStyle = wndStyle.VRS_NO_MOVE | wndStyle.VRS_MODAL;
                 _VoiceRec.cb = (int)Marshal.SizeOf(_VoiceRec);
                 _VoiceRec.xPos = -1;
                 _VoiceRec.yPos = -1;
                 _VoiceRec.lpszRecordFileName = lpszFileName;
             }
         }
  
         // Show the voice recorder
         public unsafe void Show()
         {
             fixed (CM_VOICE_RECORDER* _VoiceRecPtr = &_VoiceRec)
             {
                 _hRecorder = VoiceRecorder_Create(_VoiceRecPtr);
             }
         }
     }
 }
 

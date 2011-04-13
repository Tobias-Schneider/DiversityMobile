using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xcel2sdf

{
    internal static class ExcelKonstanten
    {
          public enum UpdateLinks{
              DontUpdate=0,
              ExternalOnly = 1,
              RemoteOnly = 2,
              ExternalAndRemote = 3
          }

          public const bool ReadOnly = true;
          public const bool ReadWrite = false;
  
          /// If Microsoft Excel is opening a text file, this argument
          /// specifies the delimiter character, as shown in the following
          /// table. If this argument is omitted, the current delimiter
          /// is used.
     
          public enum Format
          {
              Tabs = 1,
              Commas = 2,
              Spaces = 3,
              Semicolons = 4,
              Nothing = 5,
              CustomCharacter = 6
          };
       
 
          /// True to have Microsoft Excel not display the read-only
          /// recommended message (if the workbook was saved with the
          /// Read-Only Recommended option).
          /// </summary>
          public const bool IgnoreReadOnlyRecommended = true;
          public const bool DontIgnoreReadOnlyRecommended = false;
  
          /// If the file is a Microsoft Excel 4.0 add-in, this argument
          /// is True to open the add-in so that its a visible window.
          /// If this argument is False or omitted, the add-in is opened
          /// as hidden, and it cannot be unhidden. This option doesn't
          /// apply to add-ins created in Microsoft Excel 5.0 or later.
          /// If the file is an Excel template, True to open the specified
          /// template for editing. False to open a new workbook based on
          /// the specified template. The default value is False.
          public const bool Editable = true;
  
          /// If the file cannot be opened in read/write mode, this.
          /// argument is True to add the file to the file notification
          /// list. Microsoft Excel will open the file as read-only, poll
          /// the file notification list, and then notify the user when
          /// the file becomes available. If this argument is False or
          /// omitted, no notification is requested, and any attemptto
          /// open an unavailable file will fail.
          
          public const bool Notify = true;
          public const bool DontNotifiy = false;

          /// The index of the first file converter to try when opening
          /// the file. The specified file converter is tried first; if
          /// this converter doesnt recognize the file, all other converters
          /// are tried. The converter index consists of the row number
          /// of the converters returned by the FileConverters property.

          public enum Converter
          {
              Default = 0
          };
 
          /// True to add this workbook to the list of recently used files.
          /// The default value is False.

          public const bool AddToMru = true;
          public const bool DontAddToMru = false;
  
          /// True saves files against the language of Microsoft Excel
          /// (including control panel settings). False (default) saves
          /// files against the language of Visual Basic for Applications
          /// (VBA) (which is typically US English unless the VBA project
          /// where Workbooks.Open is run from is an old internationalized
          /// XL5/95 VBA project
          public const bool Local = true;
          public const bool NotLocal = false;
          public enum CorruptLoad
          {
              NormalLoad = 0,
              RepairFile = 1,
              ExtractData = 2
          };
      }
 }

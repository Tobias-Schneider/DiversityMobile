using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.SyncBase.Util
{
    public class FieldLock
    {
        private Type _lockedType;
        private Dictionary<FieldInfo, bool> _lockedFields;

        public FieldLock(Type lockedType)
        {
            
            _lockedFields = new Dictionary<FieldInfo, bool>();
            _lockedType = lockedType;
        }

        public void LockField(String fieldName)
        {
            FieldInfo fi = _lockedType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            _lockedFields[fi] = true;
        }

        public void LockField(FieldInfo fi)
        {
            fi = _lockedType.GetField(fi.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            _lockedFields[fi] = true;

        }

        public void UnLockField(String fieldName)
        {
            FieldInfo fi = _lockedType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            _lockedFields[fi] = false;
        }

        public void UnLockField(FieldInfo fi)
        {
            fi = _lockedType.GetField(fi.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            _lockedFields[fi] = false;
        }

        public bool IsLocked(String fieldName)
        {
            try
            {
                FieldInfo fi = _lockedType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                return _lockedFields[fi];
            }
            catch (KeyNotFoundException ex)
            {
                return false;
            }
        }

        public bool IsLocked(FieldInfo fi)
        {
            try
            {
                return _lockedFields[fi];
            }
            catch (KeyNotFoundException ex)
            {
                return false;
            }
        }
    }
}

using System;
using Microsoft.Win32;

namespace Util.Utilities
{
    public static class RegistryUtilities
    {
        /// <summary>
        /// Renames a subkey of the passed in registry key since 
        /// the Framework totally forgot to include such a handy feature.
        /// </summary>
        /// <param name="parentKey">The Parent RegistryKey that contains the subkey 
        /// you want to rename (must be writeable)</param>
        /// <param name="subKeyName">The name of the subkey that you want to rename
        /// </param>
        /// <param name="newSubKeyName">The new name of the RegistryKey</param>
        /// <returns>True if succeeds</returns>
        public static bool RenameSubKeyStringValue(RegistryKey parentKey,

            string subKeyName, string newSubKeyName)
        {
            try
            {
                CopyAndDelteSubKeyStringValue(parentKey, subKeyName, newSubKeyName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool RenameSubKeyStringValueOnDifferentParentKeys(RegistryKey[] parentKey,
            string subKeyName, string newSubKeyName)
        {
            try
            {
                foreach (RegistryKey key in parentKey)
                    CopyAndDelteSubKeyStringValue(key, subKeyName, newSubKeyName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Copy a registry key.  The parentKey must be writeable.
        /// </summary>
        /// <param name="parentKey">parent registry key</param>
        /// <param name="keyNameToCopy">registry key which i want to copy</param>
        /// <param name="newKeyName">new key created with the value of existing key</param>
        /// <returns>True if succeeds</returns>
        public static bool CopyAndDelteSubKeyStringValue(RegistryKey parentKey,
            string keyNameToCopy, string newKeyName)
        {
            try
            {
                //Create new key
                parentKey.SetValue(newKeyName, parentKey.GetValue(keyNameToCopy));

                //Open the sourceKey we are copying from
                //RegistryKey sourceKey = parentKey.OpenSubKey(keyNameToCopy);
                parentKey.DeleteValue(keyNameToCopy);

                //RecurseCopyKey(sourceKey, destinationKey);

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///  Set the value of subkey 
        /// </summary>
        /// <param name="parentKey">Parent key name</param>
        /// <param name="keyName">key name under parent key</param>
        /// <param name="valueContent">value of the key</param>
        /// <returns>Returns true if value successfully set</returns>
        public static bool SetValue(RegistryKey parentKey, string keyName, string valueContent)
        {
            try
            {
                parentKey.SetValue(keyName, valueContent);
            }
            catch (NullReferenceException)
            {

                return false;
            }
            return true;
        }
        /// <summary>
        ///  Get the value of subkey 
        /// </summary>
        /// <param name="parentKey">Parent key name</param>
        /// <param name="keyName">key name under parent key</param>
        /// <returns>Returns the value of registry key</returns>
        public static string GetValue(RegistryKey parentKey, string keyName)
        {
            try
            {
                return parentKey.GetValue(keyName).ToString();
            }
            catch (NullReferenceException)
            {

                return null;
            }

        }
    }
}

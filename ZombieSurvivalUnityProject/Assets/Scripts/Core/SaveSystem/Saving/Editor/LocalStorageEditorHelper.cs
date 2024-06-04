using System.Diagnostics;
using System.IO;
using Core.SaveSystem.Saving.Common.Path;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Core.SaveSystem.Saving.Editor
{
    public class LocalStorageEditorHelper
    {
#if UNITY_EDITOR
        [MenuItem("SaveSystem/Open local folder", false, 0)]
        public static void OpenSaveFolder()
        {
            var localPathProvider = new LocalStoragePathProvider();
            Process.Start(localPathProvider.Path);
        }
        
        [MenuItem("SaveSystem/Clear Save files", false, 1)]
        public static void ClearSaveFiles()
        {
            var localPathProvider = new LocalStoragePathProvider();
            var directory = new DirectoryInfo(localPathProvider.Path);
            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }

            Debug.Log("Save files has been cleared");
        }
#endif
    }
}

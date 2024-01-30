using System.Diagnostics;
using Core.SaveSystem.Saving.Common.Path;
using UnityEditor;

namespace Core.SaveSystem.Saving.Editor
{
    public class LocalStorageEditorHelper
    {
#if UNITY_EDITOR
        [MenuItem("SaveSystem/Open local folder")]
        public static void OpenSaveFolder()
        {
            var localPathProvider = new LocalStoragePathProvider();
            Process.Start(localPathProvider.Path);
        }
#endif
    }
}

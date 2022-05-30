using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;


namespace QiQiuVert
{
  public class DebugFrameSetting : EditorWindow
  {
    int fps = 0;
    [MenuItem("EditorTool/Frame Setting")]
    static void ShowFrameSettingWindow()
    {
      var window = GetWindow<DebugFrameSetting>("System Setting");
      window?.Show();
    }

    void OnGUI()
    {
      GUILayout.Label("Graphic Settings", EditorStyles.boldLabel);
      GUILayout.BeginHorizontal();
      fps = EditorGUILayout.IntField("FPS", fps);
      if (GUILayout.Button("Save", GUILayout.Width(50)))
      {
        SystemHelper.SetFrameLimit(fps);
        UnityEngine.Debug.Log($"Setting {fps} FPS have been saved!");
      }
      GUILayout.EndHorizontal();

    }
  }
}

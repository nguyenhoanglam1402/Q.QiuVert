using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

namespace QiQiuVert
{
  public partial class Debug : EditorWindow
  {
    int fps = 0;
    [MenuItem("Helper/Frame Setting")]
    static void ShowFrameSettingWindow()
    {
      var window = GetWindow<Debug>("System Setting");
      window.Show();
    }

    void OnGUI()
    {
      GUILayout.Label("Graphic Settings", EditorStyles.boldLabel);
      GUILayout.BeginHorizontal("IntField");
      fps = EditorGUILayout.IntField("FPS", fps);
      if (GUILayout.Button("Save", GUILayout.Width(50)))
      {
        SystemHelper.SetFrameLimit(fps);
        UnityEngine.Debug.Log(fps);
      }
      GUILayout.EndHorizontal();
    }
  }
}

using Assets.CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelStaticData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
                levelStaticData.LevelName = SceneManager.GetActiveScene().name;

            EditorUtility.SetDirty(target);
        }
    }
}

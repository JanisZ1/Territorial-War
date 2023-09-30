using Assets.CodeBase.Logic.Spawners;
using Assets.CodeBase.StaticData;
using System.Linq;
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
            {
                levelStaticData.LevelName = SceneManager.GetActiveScene().name;
                //TODO: Make Static Data For All Spawners
                levelStaticData.Spawners = FindObjectsOfType<GreenCommandUnitSpawner>()
                    .Select(x => new SpawnerStaticData(x.transform.position))
                    .ToList();
            }
            EditorUtility.SetDirty(target);
        }
    }
}

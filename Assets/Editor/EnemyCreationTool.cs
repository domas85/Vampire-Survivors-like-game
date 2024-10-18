using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditorInternal;
public class EnemyCreationTool : EditorWindow
{
    string EnemyName = "";
    const string EnemyDataPath = "Assets/GameData/Enemies/";
    const string EnemyPrefabPath = "Assets/Prefabs/Enemies/EnemyPrefabs/";

    public EnemyMovement enemyBehaviour;

    const string TestPath = "Assets/GameData/EditorTesting/";

    public string[] options = new string[] { };

    public string[] behavioursOptions = new string[] { };

    List<MonoScript> enemyBehaviours;
    public int behaviourIndex = 0;

    public int selectedEnemyBehaviour;

    public List<EnemyData> enemySOs;

    public Editor my_ScriptableEnemyEditor;

    public int index = 0;
    public int selectedEnemyData;

    bool showPosition = true;
    


    [MenuItem("Tools/EnemyCreationTool")]
    public static void ShowMyEditor()
    {
        // This method is called when the user selects the menu item in the Editor.
        EditorWindow wnd = GetWindow<EnemyCreationTool>();
        wnd.titleContent = new GUIContent("Enemy Creation Tool");

        // Limit size of the window.
        wnd.minSize = new Vector2(450, 200);
        wnd.maxSize = new Vector2(1920, 720);
    }


    private void OnGUI()
    {
        #region Create Enemy
        //using System.Text.RegularExpressions;
        // var hp = Regex.Replace(hp, @"[^0-9 ]", "");   //change letters to nothing
        GUILayout.Label("Enemy Creator", EditorStyles.boldLabel);

        EditorGUILayout.Space();
        List<MonoScript> _monoScripts = new List<MonoScript>();

        EnemyName = GUILayout.TextField(EnemyName);

        behavioursOptions = LoadBehabiours();

        behaviourIndex = EditorGUILayout.Popup(behaviourIndex, behavioursOptions, GUILayout.Width(200));

        if (EnemyName == "")
        {
            EditorGUILayout.HelpBox("To create a new Enemy please enter a File Name for the Enemy", MessageType.Warning);
        }
        EditorGUILayout.HelpBox("Circle behaviour is not working do not choose it", MessageType.Error); 
        EditorGUILayout.HelpBox("Choose the behaviour you want, this can not be changed later through the tool", MessageType.Error);



        if (CheckIfEnemyDataExists())
        {
            EditorGUILayout.HelpBox("Enemy already exists with this name", MessageType.Error);
            return;
        }

        GUILayout.BeginHorizontal();

        EditorGUI.BeginDisabledGroup(EnemyName == "");

        if (GUILayout.Button("Create Enemy"))
        {

            CreateEnemy();


            EnemyName = "";
        }

        EditorGUI.EndDisabledGroup();

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();



        EditorGUILayout.Space(10);


        #endregion

        #region Enemy Edit


        string status = "Edit enemies";
        showPosition = EditorGUILayout.Foldout(showPosition, status, EditorStyles.boldFont);
        if (showPosition)
        {
            //Enemy Load and Change properties
            GUILayout.BeginHorizontal();
            GUILayout.Label("Enemy Editor", EditorStyles.boldLabel);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            options = Loadnames();

            if (options != null)
            {
                index = EditorGUILayout.Popup(index, options, GUILayout.Width(200));

            }

            GUILayout.EndHorizontal();

            EditorGUILayout.HelpBox("Choose between diffrent enemies data and change their stats", MessageType.None);
            EditorGUILayout.Space(10);

            CreateEnemyEditorWindow();
            if (my_ScriptableEnemyEditor != null)
            {
                my_ScriptableEnemyEditor.OnInspectorGUI();

            }
        }
        #endregion
    }

    private void OnInspectorUpdate()
    {
        this.Repaint();
    }


    private void OnEnable()
    {
        CreateEnemyEditorWindow();
    }

    public void CreateEnemyEditorWindow()
    {
        selectedEnemyData = index;

        if (enemySOs != null && selectedEnemyData >= 0 && selectedEnemyData < enemySOs.Count && enemySOs[selectedEnemyData] != null)
        {
            my_ScriptableEnemyEditor = Editor.CreateEditor(enemySOs[selectedEnemyData]);
        }

    }

    private void GetAllEnemyBehaviours()
    {

        string[] foldersToSearch = { "Assets/Scripts/EnemyScripts/Behaviors/" };
        enemyBehaviours = GetAssets<MonoScript>(foldersToSearch, "t:MonoScript");
    }

    public string[] LoadBehabiours()
    {
        GetAllEnemyBehaviours();

        List<string> list = new List<string>();

        string[] e;

        for (int i = 0; i < enemyBehaviours.Count; i++)
        {
            list.Add(enemyBehaviours[i].name);
        }
        e = list.ToArray();

        return e;
    }




    private void GetAllEnemyData()
    {
        string[] foldersToSearch = { EnemyDataPath };
        enemySOs = GetAssets<EnemyData>(foldersToSearch, "t:EnemyData");
    }

    public string[] Loadnames()
    {
        GetAllEnemyData();

        List<string> list = new List<string>();

        string[] e;

        for (int i = 0; i < enemySOs.Count; i++)
        {
            list.Add(enemySOs[i].name);
        }
        e = list.ToArray();

        return e;
    }

    void CreateEnemyDatatInAssets()
    {
        EnemyData anEnemyData = CreateInstance<EnemyData>();
        AssetDatabase.CreateAsset(anEnemyData, EnemyDataPath + EnemyName + ".asset");


        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }


    void CreateEnemy() //double check paths 
    {
        UnityEngine.Object basePrefabPath = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Base/BaseEnemy.prefab", typeof(GameObject));

        GameObject baseEnemyPrefab = PrefabUtility.SaveAsPrefabAsset((GameObject)basePrefabPath, EnemyPrefabPath + EnemyName + ".prefab");

        Enemy enemyComponent = baseEnemyPrefab.GetComponent<Enemy>();

        baseEnemyPrefab.AddComponent(enemyBehaviours[behaviourIndex].GetClass());



        CreateEnemyDatatInAssets();

        UnityEngine.Object data = AssetDatabase.LoadAssetAtPath(EnemyDataPath + EnemyName + ".asset", typeof(EnemyData));

        //Debug.Log(data);

        enemyComponent.enemyData = (EnemyData)data;
        //PrefabUtility.CreatePrefab("Assets/GameData/EditorTesting/" + EnemyName + ".prefab", (GameObject)basePrefab, ReplacePrefabOptions.ReplaceNameBased);


        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }

    public static List<T> GetAssets<T>(string[] _foldersToSearch, string _filter) where T : UnityEngine.Object
    {
        string[] guids = AssetDatabase.FindAssets(_filter, _foldersToSearch);
        List<T> a = new List<T>();
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            T g = AssetDatabase.LoadAssetAtPath<T>(path);
            a.Add(g);
        }
        return a;
    }

    bool CheckIfEnemyDataExists()
    {
        //string[] someAsset = AssetDatabase.FindAssets("t:UpgradeData");

        //string anAsset = AssetDatabase.GUIDToAssetPath(someAsset[0]);


        if (File.Exists(EnemyDataPath + EnemyName + ".asset"))
        {
            return true;
        }


        return false;
    }

    bool CheckIfEnemyPrefabExists()
    {

        if (File.Exists(EnemyDataPath + EnemyName + ".asset"))
        {
            return true;
        }


        return false;
    }




    //this just hides the default script property that is allways on the top (if you want other script type to hide it then change typeof to other script type like MonoBehaviour)
    //move outside to hide it for every ScriptableObject
    [CustomEditor(typeof(ScriptableObject), true)]
    public class DefaultInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            this.DrawDefaultInspectorWithoutScriptField();
        }
    }

}

public static class DefaultInspector_EditorExtension
{
    public static bool DrawDefaultInspectorWithoutScriptField(this Editor Inspector)
    {
        EditorGUI.BeginChangeCheck();

        Inspector.serializedObject.Update();

        SerializedProperty Iterator = Inspector.serializedObject.GetIterator();

        Iterator.NextVisible(true);

        while (Iterator.NextVisible(false))
        {
            EditorGUILayout.PropertyField(Iterator, true);
        }

        Inspector.serializedObject.ApplyModifiedProperties();

        return (EditorGUI.EndChangeCheck());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.IO;
using System;

public class UpgradeCreationTool : EditorWindow
{
    string UpgradeName = "";

    const string UpgradeDataPath = "Assets/GameData/EditorTesting/";

    public string[] options = new string[] { };

    public string[] createOptions = new string[] { };

    public string[] upgradeOptions = new string[] { };

    public int index = 0;

    public int createIndex = 0;

    public int upgradeIndex = 0;

    public int selectedWeaponData;

    public int selectedUpgradeData;

    bool showPosition = true;

    bool ShowUpgrades = false;

    public List<WeaponData> weapons;

    public List<UpgradeData> myUpgrades;

    public Editor my_ScriptableWeaponEditor;

    public Editor my_ScriptableUpgradeEditor;



    [MenuItem("Tools/UpgradeCreationTool")]
    public static void ShowMyEditor()
    {
        // This method is called when the user selects the menu item in the Editor.
        EditorWindow wnd = GetWindow<UpgradeCreationTool>();
        wnd.titleContent = new GUIContent("Upgrade Creation Tool");

        // Limit size of the window.
        wnd.minSize = new Vector2(450, 200);
        wnd.maxSize = new Vector2(1920, 720);
    }


    private void OnGUI()
    {
        #region Create Upgrade
        //using System.Text.RegularExpressions;
        // var hp = Regex.Replace(hp, @"[^0-9 ]", "");   //change letters to nothing
        GUILayout.Label("Upgrade Creator", EditorStyles.boldLabel);

        EditorGUILayout.Space();
        List<MonoScript> _monoScripts = new List<MonoScript>();

        UpgradeName = GUILayout.TextField(UpgradeName);

        //behavioursOptions = LoadBehabiours();

        //behaviourIndex = EditorGUILayout.Popup(behaviourIndex, behavioursOptions, GUILayout.Width(200));
        GUILayout.BeginHorizontal();
        createOptions = LoadWeaponNames();

        if (createOptions != null)
        {
            createIndex = EditorGUILayout.Popup(createIndex, createOptions, GUILayout.Width(200));
        }
        //Debug.Log(createOptions[createIndex].ToString());
        GUILayout.EndHorizontal();


        if (UpgradeName == "")
        {
            EditorGUILayout.HelpBox("To create a new Upgrade please enter a Name", MessageType.Warning);
        }

        EditorGUILayout.HelpBox("Choose the weapon you want to add a new upgrade for", MessageType.Error);

        if (CheckIfExists())
        {
            EditorGUILayout.HelpBox("Enemy already exists with this name.", MessageType.Error);
            return;
        }

        GUILayout.BeginHorizontal();

        EditorGUI.BeginDisabledGroup(UpgradeName == "");

        if (GUILayout.Button("Create Upgrade"))
        {

            CreateUpgrade();


            UpgradeName = "";
        }

        EditorGUI.EndDisabledGroup();

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();



        EditorGUILayout.Space(10);


        #endregion


        #region Weapon Edit


        string status = "Edit Weapon";
        showPosition = EditorGUILayout.Foldout(showPosition, status, EditorStyles.boldFont);
        if (showPosition)
        {
            EditorGUILayout.Space(5);

            GUILayout.BeginHorizontal();
            options = LoadWeaponNames();

            if (options != null)
            {
                index = EditorGUILayout.Popup(index, options, GUILayout.Width(200));
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.HelpBox("Choose between diffrent Weapons and assign their respective Upgrades and Stats", MessageType.None);
            EditorGUILayout.Space(10);

            CreateWeaponEditorWindow();
            if (my_ScriptableWeaponEditor != null)
            {
                my_ScriptableWeaponEditor.OnInspectorGUI();
            }
            else
            {
                return;
            }

            if (GUILayout.Button("Configure Upgrades for " + options[index], GUILayout.Width(300)))
            {
                GetAllUpgradesData();
                showPosition = false;
                ShowUpgrades = true;
            }
            EditorGUILayout.Space(7);

            if (GUILayout.Button("Click to add avalable Upgrades to " + options[index], GUILayout.Height(50)))
            {
                GetAllUpgradesData();

                weapons[selectedWeaponData].upgrades = myUpgrades;
            }
        }

        #endregion

        EditorGUILayout.Space();

        #region Upgrade Edit


        string regionName = "Edit Upgrades";
        ShowUpgrades = EditorGUILayout.Foldout(ShowUpgrades, regionName, EditorStyles.boldLabel);
        if (ShowUpgrades && showPosition == false)
        {
            EditorGUILayout.HelpBox("Choose and edit the available upgrades", MessageType.Info);
            GUILayout.BeginHorizontal();

            upgradeOptions = LoadUpgradeNames();

           
            if (upgradeOptions != null)
            {
                upgradeIndex = EditorGUILayout.Popup(upgradeIndex, upgradeOptions, GUILayout.Width(200));
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.Space(5);
            CreateUpgradeEditorWindow();
            if (my_ScriptableUpgradeEditor != null)
            {
                my_ScriptableUpgradeEditor.OnInspectorGUI();
            }
            else
            {
                return;
            }

            EditorGUILayout.HelpBox("Choose Stats for the Upgrades", MessageType.None);
            EditorGUILayout.Space(10);

            EditorGUILayout.HelpBox("Go back to Edit Weapon of you want other Weapon Upgrades", MessageType.Warning);

        }
        #endregion
    }

    private void OnInspectorUpdate()
    {
        this.Repaint();
    }


    private void OnEnable()
    {
        CreateWeaponEditorWindow();
        CreateUpgradeEditorWindow();
    }


    public void CreateUpgradeEditorWindow()
    {
        selectedUpgradeData = index;

        if (myUpgrades != null && selectedUpgradeData >= 0 && selectedUpgradeData < myUpgrades.Count && myUpgrades[upgradeIndex] != null)
        {
            my_ScriptableUpgradeEditor = Editor.CreateEditor(myUpgrades[upgradeIndex]);

        }
    }


    public void CreateWeaponEditorWindow()
    {
        selectedWeaponData = index;

        if (weapons != null && selectedWeaponData >= 0 && selectedWeaponData < weapons.Count && weapons[selectedWeaponData] != null)
        {
            my_ScriptableWeaponEditor = Editor.CreateEditor(weapons[selectedWeaponData]);
        }
    }

    public string[] LoadUpgradeNames()
    {
        GetAllUpgradesData();

        List<string> list = new List<string>();

        string[] e;

        for (int i = 0; i < myUpgrades.Count; i++)
        {
            list.Add(myUpgrades[i].name);
        }
        e = list.ToArray();

        return e;
    }


    public string[] LoadWeaponNames()
    {
        GetAllWeaponData();

        List<string> list = new List<string>();

        string[] e;

        for (int i = 0; i < weapons.Count; i++)
        {
            list.Add(weapons[i].name);
        }
        e = list.ToArray();

        return e;
    }

    private void GetAllWeaponData()
    {
        string[] foldersToSearch = { "Assets/GameData/Weapons/" };
        weapons = GetAssets<WeaponData>(foldersToSearch, "t:WeaponData");
    }

    private void GetAllUpgradesData()
    {
        string[] foldersToSearch = { "Assets/GameData/Weapons/" + options[index] + "/" };
        myUpgrades = GetAssets<UpgradeData>(foldersToSearch, "t:UpgradeData");
    }

    void CreateUpgrade()
    {
        UpgradeData anUpgrade = CreateInstance<UpgradeData>();
        AssetDatabase.CreateAsset(anUpgrade, "Assets/GameData/Weapons/" + createOptions[createIndex] + "/" + UpgradeName + ".asset");

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
    bool CheckIfExists()
    {
        //string[] someAsset = AssetDatabase.FindAssets("t:UpgradeData");

        //string anAsset = AssetDatabase.GUIDToAssetPath(someAsset[0]);

        if (File.Exists("Assets/GameData/Weapons/" + createOptions[createIndex] + "/" + UpgradeName + ".asset"))
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

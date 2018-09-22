using UnityEditor;
using UnityEngine;
using System.IO;

public class CreateAssets : EditorWindow
{
    int index = 0;
    [MenuItem("Window/CreatScripts")]
    [MenuItem("Assets/Create/My C# Script/Advanced", false, 0)]
    static void Open()
    {
        GetWindow<CreateAssets>();
    }

    public string scriptName;
    void OnGUI()
    {
        var options = new[] { GUILayout.Width(100), GUILayout.Height(20) };

        GUILayout.Label("CreateScripts");
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("ScriptName", options);
        scriptName = EditorGUILayout.TextArea(scriptName);
        EditorGUILayout.EndHorizontal();

        var opitions = new string[] { "Skill", "Hero", "Mob", "SkillBullet" };


        index = EditorGUILayout.Popup("ClassType", index, opitions);
        if (GUILayout.Button("Create"))
        {
            CreateScript(scriptName,index);
        }
    }
    static void CreateScript(string scriptName, int id)
    {
        string resourceFile = "";
        switch(id)
        {
            case 0:
                CreatNewSubScript.CreateNewSkill(scriptName);
                break;
            case 1:
                CreatNewSubScript.CreatNewHero(scriptName);
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                Debug.Log("Don't have this case");
                break;
        }

        //Texture2D csIcon =
        //    EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;

        //var endNameEditAction =
        //    ScriptableObject.CreateInstance<DoCreateScriptAsset>();

        //ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, endNameEditAction,
        //    scriptName + ".cs", csIcon, resourceFile);
    }

    //新建Material
    //static void CreateMaterial(string materialName)
    //{
    //    var material = new Material(Shader.Find("Standard"));
    //    var instanceID = material.GetInstanceID();
    //    var icon = AssetPreview.GetMiniThumbnail(material);

    //    var endNameEditAction =
    //        ScriptableObject.CreateInstance<DoCreateScriptAsset>();

    //    ProjectWindowUtil.StartNameEditingIfProjectWindowExists(instanceID,
    //        endNameEditAction, materialName + ".mat", icon, "");
    //}

}
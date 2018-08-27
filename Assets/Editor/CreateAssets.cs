using UnityEditor;
using UnityEngine;
using System.IO;

public class CreateAssets : EditorWindow
{

    [MenuItem("Window/CreatAssets")]
    static void Open()
    {
        GetWindow<CreateAssets>();
    }

    public string scriptName, materialName;
    void OnGUI()
    {
        var options = new[] { GUILayout.Width(100), GUILayout.Height(20) };

        GUILayout.Label("CreateScripts");
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("ScriptName", options);
        scriptName = EditorGUILayout.TextArea(scriptName);
        EditorGUILayout.EndHorizontal();


        if (GUILayout.Button("Create"))
        {

            CreateScript(scriptName);
        }

        GUILayout.Label("CreateMaterial");
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("materialName", options);
        materialName = EditorGUILayout.TextArea(materialName);
        EditorGUILayout.EndHorizontal();


        if (GUILayout.Button("Create"))
        {
            CreateMaterial(materialName);
        }

    }
    //  新建自定义脚本
    static void CreateScript(string scriptName)
    {
        var resourceFile = Path.Combine(EditorApplication.applicationContentsPath,
            "Resources/ScriptTemplates/81-C# Script-NewBehaviourScript.cs.txt");

        Debug.Log(resourceFile);
        Texture2D csIcon =
            EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;

        var endNameEditAction =
            ScriptableObject.CreateInstance<DoCreateScriptAsset>();

        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, endNameEditAction,
            scriptName + ".cs", csIcon, resourceFile);
    }

    //新建Material
    static void CreateMaterial(string materialName)
    {
        var material = new Material(Shader.Find("Standard"));
        var instanceID = material.GetInstanceID();
        var icon = AssetPreview.GetMiniThumbnail(material);

        var endNameEditAction =
            ScriptableObject.CreateInstance<DoCreateMaterialAsset>();

        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(instanceID,
            endNameEditAction, materialName + ".mat", icon, "");
    }

}
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using System;

public class DoCreateScriptAsset : EndNameEditAction
{
    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        var text = File.ReadAllText(resourceFile);

        var className = Path.GetFileNameWithoutExtension(pathName);

        //清除空格
        className = className.Replace(" ", "");


        text = "//Programer: " + "KevinX" + "\n//code date:" + DateTime.Now.Date.ToShortDateString() + "\n"
        + text.Replace("#SCRIPTNAME#", className);


        //utf8
        var encoding = new UTF8Encoding(true, false);

        File.WriteAllText(pathName, text, encoding);

        AssetDatabase.ImportAsset(pathName);
        var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(pathName);
        ProjectWindowUtil.ShowCreatedAsset(asset);
    }
}
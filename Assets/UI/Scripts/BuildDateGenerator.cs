#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System;
using System.IO;
using UnityEngine;

public class BuildDateGenerator : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        string buildDate = DateTime.Now.ToString("dd.MM.yyyy");
        string path = "Assets/UI/Scripts/BuildInfo.cs"; //tego nei trzeba commitowac

        string content = $@"// Ten plik jest generowany automatycznie przy buildzie.
public static class BuildInfo
{{
    public const string BuildDate = ""{buildDate}"";
}}";

        File.WriteAllText(path, content);
        AssetDatabase.Refresh();
        Debug.Log($"[BuildDateGenerator] Wygenerowano BuildInfo.cs z dat¹: {buildDate}");
    }
}
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Builder
{
    static string[] SCENES = FindEnabledEditorScenes();

    static string APP_NAME = "YourProject";
    static string TARGET_DIR = "D:/Downloads/";

    [MenuItem("Builder/Build Windows")]
    static void PerformWinBuild()
    {
        GenericBuild(SCENES, GetTargetDir(), BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    private static string GetTargetDir()
    {
        var args = Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].Contains("executeMethod"))
            {
                return args[i + 2];
            }
        }
        
        Debug.LogError("Build path is not specified");

        return null;
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }

        return EditorScenes.ToArray();
    }


    static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
        Debug.Log($"Building to {target_dir}");

        var options = new BuildPlayerOptions()
        {
            scenes = scenes,
            options = build_options,
            target = build_target,
            locationPathName = $"{target_dir}/{PlayerSettings.productName}.exe"
        };

        var res = BuildPipeline.BuildPlayer(options);

        if (res != null)
        {
            
        }
    }
}
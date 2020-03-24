using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEditor.Build.Reporting;
//using System.Environment.CommandLineArgs;

public class BuildProject : MonoBehaviour
{
// Start is called before the first frame update
void Start()
{
}

// Update is called once per frame
void Update()
{

}

/*

   public static void PerformBuild()
   {

           string[] parameters = System.Environment.GetCommandLineArgs();

           BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
           buildPlayerOptions.scenes = GetScenePaths();
           buildPlayerOptions.locationPathName = "%USERPROFILE%\\Desktop\\RogueFrog";
           buildPlayerOptions.target = BuildTarget.Android;
           buildPlayerOptions.options = BuildOptions.AcceptExternalModificationsToPlayer;

           BuildPipeline.BuildPlayer(buildPlayerOptions);

   }

   static string[] GetScenePaths() {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        for(int i = 0; i < scenes.Length; i++) {
                scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        return scenes;
   }

 */

}

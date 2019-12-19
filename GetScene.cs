using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// This will get the save file from disk on start and can be used with a button for GoToLevel().
    /// </summary>
    public class GetScene : MonoBehaviour
    {
      public string SceneName;

        /// <summary>
        /// Loads the save file from disk, save string is not null, set the SceneName.
        /// </summary>
        protected virtual void Start()
        {
            SaveManager.Instance.LoadSavedScene();
            string scenename = (string)SaveManager.Instance.LoadSceneName();
            if (scenename != "") SceneName = scenename;
        }

        /// <summary>
    		/// if the scene name is not null, go to the level
    		/// </summary>
    	    public virtual void GoToLevel()
    	    {
              if (SceneName != "") LevelManager.Instance.GotoLevel(SceneName);
    	    }
    }
}

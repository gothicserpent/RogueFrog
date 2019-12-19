using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// This will set the scene to the save file when a scene is loaded so that it can be loaded later.
    /// </summary>
    public class SetScene : MonoBehaviour
    {
        /// <summary>
        /// Set the savemanager scene to the active scene name
        /// </summary>
        protected virtual void Start()
        {
            SaveManager.Instance.SetCurrentScene(SceneManager.GetActiveScene().name);
        }
    }
}

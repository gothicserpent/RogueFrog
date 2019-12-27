using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using System.Collections.Generic;

namespace MoreMountains.TopDownEngine
{
/// <summary>
/// a class to save game settings (levels. eventually can incorporate inventory saving)
/// </summary>
[Serializable]
public class SaveSettings
{
public string CurrentScene;
}

/// <summary>
/// This persistent singleton handles game saving
/// </summary>
[AddComponentMenu("TopDown Engine/Managers/Save Manager")]
public class SaveManager : PersistentSingleton<SaveManager>    //, MMEventListener<TopDownEngineEvent>, MMEventListener<MMGameEvent>
{
[Header("Save Settings")]
/// the current save settings
public SaveSettings SaveSettings;

protected const string _saveFolderName = "SaveManager/";
protected const string _saveFileName = "save.settings";

/// <summary>
/// Saves the settings to file
/// </summary>
protected virtual void SaveGameSettings()
{
	SaveLoadManager.Save(SaveSettings, _saveFileName, _saveFolderName);
}

/// <summary>
/// Loads the settings from file (if found)
/// </summary>
protected virtual void LoadGameSettings()
{
	SaveSettings settings = (SaveSettings)SaveLoadManager.Load(_saveFileName, _saveFolderName);
	if (settings != null) SaveSettings = settings;

}

/// <summary>
/// Resets the settings by destroying the save file
/// </summary>
protected virtual void ResetGameSettings()
{
	SaveLoadManager.DeleteSave(_saveFileName, _saveFolderName);
}

public virtual void SetCurrentScene(string scene) {
	SaveSettings.CurrentScene = scene; ResetGameSettings(); SaveGameSettings();
}
public virtual void LoadSavedScene() {
	LoadGameSettings();
}
public virtual string LoadSceneName() {
	return SaveSettings.CurrentScene;
}
//public virtual void GetCurrentScene(string scene) { SaveSettings.CurrentScene = scene; SaveGameSettings(); }
}
}

using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using UnityEngine.SceneManagement;

namespace MoreMountains.TopDownEngine
{
/// <summary>
/// a class to save game settings: levels, inventories / deaths (to ini file)
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
public class SaveManager : MMSingleton<SaveManager>    //, MMEventListener<TopDownEngineEvent>, MMEventListener<MMGameEvent>
{
[Header("Inventory items (All the inventory items in Resources/Items. This *MUST* be added!!!)")]
public InventoryItem[] InventoryItems;
[Header("IAP Inventory items (which won't be dropped on player death). This *MUST* be added!!!)")]
public InventoryItem[] PurchasableInventoryItems;

[Header("Save Settings")]
/// the current save settings
public SaveSettings SaveSettings;

public INIParser ini = new INIParser();

private const string _baseFolderName = "/MMData/";
protected const string _saveFolderName = "SaveManager/";
protected const string _saveFileName = "save.settings";
protected const string _iniFileName = "Player.ini";

void Start()
{
	FillInventoryFromINI(); //fill the inventory from the ini file
	PlayerDeathSpawnItemsFromINI(); // eventually spawn items on start each time a level is opened
}

/// <summary>
/// Empty the inventory (respecting IAP) and optionally destroy the weapon (if the player dies)
/// </summary>
public void EmptyInventory(bool DestroyWeapon = false)
{
	ini.Open(DetermineINIPath());
	Inventory FrogInventory = null;
	List<int> FrogInventoryList = null;
	Inventory FrogWeaponInventory = null;
	List<int> FrogWeaponInventoryList = null;

	if (GameObject.Find("FrogInventory") != null) FrogInventory = GameObject.Find("FrogInventory").GetComponent<Inventory>();

	if (GameObject.Find("FrogWeaponInventory") != null) FrogWeaponInventory = GameObject.Find("FrogWeaponInventory").GetComponent<Inventory>();

	//GameObject.Find("FrogInventory").GetComponent<Inventory>().EmptyInventory();
	//if (GameObject.Find("FrogWeaponInventory") != null) GameObject.Find("FrogWeaponInventory").GetComponent<Inventory>().EmptyInventory();

	for (int i = 0; i<InventoryItems.Length; i++)
	{
		if (ini.ReadValue("Inventory",InventoryItems[i].ItemID,0) >= 1)
		{
			//Debug.Log("getting here");

			if(!isPurchasable(InventoryItems[i].ItemID)) //only write in non purchasable items
			{

				FrogInventoryList = FrogInventory.InventoryContains(InventoryItems[i].ItemID);
				//Debug.Log("Frog Inventory IventoryContains count for : " + InventoryItems[i].ItemID + ", is:" + FrogInventoryList.Count);
				if (FrogInventoryList.Count > 0)
				{
					//Debug.Log("Trying to destroy inventory item: " + InventoryItems[i].ItemID);
					FrogInventory.DestroyItem(FrogInventoryList[FrogInventoryList.Count - 1]);
				}

				FrogWeaponInventoryList = FrogWeaponInventory.InventoryContains(InventoryItems[i].ItemID);
				//Debug.Log("Frog Weapon Inventory IventoryContains count for : " + InventoryItems[i].ItemID + ", is:" + FrogInventoryList.Count);
				if (FrogWeaponInventoryList.Count > 0)
				{
					//Debug.Log("Trying to destroy weapon inventory item: " + InventoryItems[i].ItemID);
					FrogWeaponInventory.DestroyItem(FrogWeaponInventoryList[FrogWeaponInventoryList.Count - 1]);
				}
			}
		}
	}
//WeaponID
	if (DestroyWeapon)
	{

		if (GameObject.FindWithTag("Player").GetComponent<CharacterHandleWeapon>().CurrentWeapon != null)
		{
			if(!isPurchasable(GameObject.FindWithTag("Player").GetComponent<CharacterHandleWeapon>().CurrentWeapon.WeaponID))
			{
				//Debug.Log("Trying to destroy current character handle weapon inventory item: " + GameObject.FindWithTag("Player").GetComponent<CharacterHandleWeapon>().CurrentWeapon.WeaponID);
				Destroy(GameObject.FindWithTag("Player").GetComponent<CharacterHandleWeapon>().CurrentWeapon.gameObject);
				//GameObject.FindWithTag("Player").GetComponent<CharacterHandleWeapon>().CurrentWeapon = null;
				//GameObject.FindWithTag("Player").GetComponent<CharacterHandleWeapon>().Setup();
				//GameObject.FindWithTag("Player").GetComponent<CharacterHandleWeapon>().ChangeWeapon(null, null);
			}
		}

	}

	ini.Close();

}

/// <summary>
/// find out if the ItemID is purchasable by iterating through PurchasableInventoryItems
/// </summary>
protected bool isPurchasable(string ItemID)
{
	bool isAPurchasableItem=false;

	for (int j = 0; j<PurchasableInventoryItems.Length; j++)
	{
		if (ItemID==PurchasableInventoryItems[j].ItemID) isAPurchasableItem=true;
	}

	return isAPurchasableItem;
}

/// <summary>
/// Fill the inventory from the INI file specified
/// </summary>
protected void FillInventoryFromINI()
{
	EmptyInventory();

	//INIParser ini = new INIParser();
	ini.Open(DetermineINIPath());
	for (int i = 0; i<InventoryItems.Length; i++)
	{
		if (ini.ReadValue("Inventory",InventoryItems[i].ItemID,0) >= 1)
		{
			GameObject.Find("FrogInventory").GetComponent<Inventory>().AddItem(InventoryItems[i], ini.ReadValue("Inventory",InventoryItems[i].ItemID,0));
			//Debug.Log("Putting Inventory Item: " + InventoryItems[i].ItemID + " with quantity: " + ini.ReadValue("Inventory",InventoryItems[i].ItemID,0));
			//MMInventoryEvent.Trigger(MMInventoryEventType.Pick, null, "FrogInventory", InventoryItems[i], ini.ReadValue("Inventory",InventoryItems[i].ItemID,0), 0); //alternative insertion
		}
	}
	ini.Close();
	//ini.RemoveBlankLines(DetermineINIPath());
}

/// <summary>
/// Fill the level from the Death Sections (called when the player dies or on start() of this manager)
/// </summary>
public void PlayerDeathSpawnItemsFromINI(bool specific = false, int specificDeathSectionNumber = 0)
{
	ini.Open(DetermineINIPath());
	string DeathSection;

	if(specific)
	{
		DeathSection = "Death" + ((specificDeathSectionNumber).ToString());

		if (SceneManager.GetActiveScene().name != ini.ReadValue(DeathSection,"Level","Default")) return; //return if the current death section level does not match the active scene name

		InstantiateInventoryItemsFromDeath(DeathSection);
	}
	else
	{
		for (int DeathSectionNumber=0; ini.IsSectionExists( "Death" + ((DeathSectionNumber).ToString()) ); DeathSectionNumber++) //iterate through all death sections
		{
			DeathSection = "Death" + ((DeathSectionNumber).ToString());

			if (SceneManager.GetActiveScene().name != ini.ReadValue(DeathSection,"Level","Default")) continue; //skip iteration if the current death section level does not match the active scene name

			InstantiateInventoryItemsFromDeath(DeathSection);
		}

	}

	ini.Close();
	//ini.RemoveBlankLines(DetermineINIPath());
}

/// <summary>
/// instantiate objects where the player died
/// </summary>
private void InstantiateInventoryItemsFromDeath(string DeathSection)
{
	for (int i = 0; i<InventoryItems.Length; i++) //iterate through all inventory items
	{

		//need to instantiate objects at the x y position in the death section
		if (ini.ReadValue(DeathSection,InventoryItems[i].ItemID,0) >= 1) // if the object value is nonzero
		{
			//Debug.Log("Trying to instantiate: " + InventoryItems[i].ItemID + "Picker");
			if (InventoryItems[i].ItemID.Contains("Coin"))
			{
				InstantiateResource("Items/ItemPickers/Coins/" + InventoryItems[i].ItemID + "Picker", ini.ReadValue(DeathSection,"x",0.0f), ini.ReadValue(DeathSection,"y",0.0f), ini.ReadValue(DeathSection,InventoryItems[i].ItemID,0));
			}
			else if (InventoryItems[i].ItemID.Contains("Staff"))
			{
				InstantiateResource("Items/ItemPickers/Staves/" + InventoryItems[i].ItemID + "Picker", ini.ReadValue(DeathSection,"x",0.0f), ini.ReadValue(DeathSection,"y",0.0f), ini.ReadValue(DeathSection,InventoryItems[i].ItemID,0));
			}
		}
	}
}

/// <summary>
/// instantiate a resource (when the player dies usually, can also be used when dropping items)
/// </summary>
private void InstantiateResource(string path, float x = 0.0f, float y = 0.0f, int quantity = 1)
{
	GameObject Object = Resources.Load(path) as GameObject;

	GameObject instance = Instantiate(Object, new Vector3 (x,y,0.0f), Quaternion.identity);

	instance.GetComponent<PersistentItemPicker>().Quantity = quantity;
}

/// <summary>
/// called when the player dies at x,y. writes values to the ini
/// </summary>
public int PlayerDeathINI(float x, float y)
{
	EmptyInventory(true); //empty the inventory and destroy the player's weapon

	ini.Open(DetermineINIPath());
	//jump to the current death section
	int DeathSectionNumber = 0;
	while (ini.IsSectionExists( "Death" + ((DeathSectionNumber).ToString()) ))
	{
		DeathSectionNumber++;
	}

	//variables to be used later
	string DeathSection = "Death" + ((DeathSectionNumber).ToString());
	bool InventoryItemsExist = false;

	for (int i = 0; i<InventoryItems.Length; i++)
	{
		if (ini.ReadValue("Inventory",InventoryItems[i].ItemID,0) >= 1)
		{
			//if the ini read value of the InventoryItems iterated item is >=1
			//then we want to write that value to [Death] in the ini file
			//finally, delete the found key in "Inventory" section so the player doesnt have the items until he collects them again
			if(!isPurchasable(InventoryItems[i].ItemID)) //only write in non purchasable items
			{
				if(!InventoryItemsExist) //only write the level and x,y once so that it does not write repeatedly if no items are passed
				{
					InventoryItemsExist=true;
					//write level of death
					ini.WriteValue(DeathSection,"Level",SceneManager.GetActiveScene().name);
					//write X and Y Values
					ini.WriteValue(DeathSection,"x",x);
					ini.WriteValue(DeathSection,"y",y);
				}

				ini.WriteValue(DeathSection,InventoryItems[i].ItemID,ini.ReadValue("Inventory",InventoryItems[i].ItemID,0)); //write the a death key for each inventory item found
				ini.KeyDelete("Inventory", InventoryItems[i].ItemID); // delete the found key so it doesnt spawn with the player again
			}
		}
	}
	ini.Close();
	//ini.RemoveBlankLines(DetermineINIPath());
	//FillInventoryFromINI();
	return DeathSectionNumber;
}

/// <summary>
/// called if the player picks up a persistent item. then the death key must be deleted for it.
/// </summary>
public void PlayerDeathINIDelete(float x, float y, string Item, int Quantity = 1)
{

	ini.Open(DetermineINIPath());
//if the player is near the location of one of his last deaths, then the death section key should lower 1 in value. if the (value-1)==0, then the key should be deleted from the death section. if the death section has no keys left, it should be deleted. this will prevent the death sections from constantly respawning items

	string DeathSection;

	for (int DeathSectionNumber=0; ini.IsSectionExists( "Death" + ((DeathSectionNumber).ToString()) ); DeathSectionNumber++) //iterate through all death sections
	{
		DeathSection = "Death" + ((DeathSectionNumber).ToString());

		if (SceneManager.GetActiveScene().name != ini.ReadValue(DeathSection,"Level","Default")) continue; //skip iteration if the current death section level does not match the active scene name

		if (ini.ReadValue(DeathSection,Item,0) >= 1)         // if the object value is nonzero
		{

//if the player is near the location of one of his last deaths, then the death section key should lower 1 in value. if the (value-1)==0, then the key should be deleted from the death section. if the death section has no keys left, it should be deleted. this will prevent the death sections from constantly respawning items

//	if ((System.Math.Abs(x-ini.ReadValue(DeathSection,"x",0.0f)) <= 0.5f) && (System.Math.Abs(y-ini.ReadValue(DeathSection,"y",0.0f)) <= 0.5f))         //if the distance is close
			if ((x == ini.ReadValue(DeathSection,"x",0.0f)) && (y == ini.ReadValue(DeathSection,"y",0.0f))) //if the distance is close
			{
				//Debug.Log ("attempting to lower section: " + DeathSection + ", key: " + Item + ", current value: " + ini.ReadValue(DeathSection,Item,0) + ", x: " + ini.ReadValue(DeathSection,"x",0.0f) + ", y: " + ini.ReadValue(DeathSection,"y",0.0f));

				//ini.WriteValue(DeathSection,Item,(ini.ReadValue(DeathSection,Item,0)-1));

				if ((ini.ReadValue(DeathSection,Item,0)-Quantity) <= 0) ini.KeyDelete(DeathSection, Item);         //delete the key if it's at 1
				else ini.WriteValue(DeathSection,Item,(ini.ReadValue(DeathSection,Item,0)-Quantity));         //lower the death key by 1

				ShiftDeleteDeathSection(DeathSection, DeathSectionNumber); //delete the death section and shift future ones into its place, to save ini space

				break; //further for loop iterations are not needed since the item was found.
			}
		}

	} //end for

	ini.Close();

	//ini.RemoveBlankLines(DetermineINIPath());
}

/// <summary>
/// clean the death sections in the ini file that have no remaining keys. the death section that is empty is "shifted" and all other sections replace it down the line.
/// </summary>
private void ShiftDeleteDeathSection(string DeathSection, int DeathSectionNumber)
{

	bool DeathKeysExist = false;

	for (int i = 0; i<InventoryItems.Length; i++)                                                 //iterate through all inventory items
	{
		if (ini.ReadValue(DeathSection,InventoryItems[i].ItemID,0) >= 1)                                                 // if the object value is nonzero
		{
			DeathKeysExist = true;                                                 //at least one death key is 1 or above
		}
	}

	if (!DeathKeysExist)
	{
		ini.SectionDelete(DeathSection); // delete the section if none of the death keys are 1 or greater

		for (; ini.IsSectionExists( "Death" + ((DeathSectionNumber+1).ToString()) ); DeathSectionNumber++) //iterate through the remaining sections, re-writing previous ones
		{
			//write level
			ini.WriteValue(DeathSection,"Level",ini.ReadValue("Death" + ((DeathSectionNumber+1).ToString()),"Level","Default"));

			//write X and Y Values
			ini.WriteValue(DeathSection,"x",ini.ReadValue("Death" + ((DeathSectionNumber+1).ToString()),"x",0.0f));
			ini.WriteValue(DeathSection,"y",ini.ReadValue("Death" + ((DeathSectionNumber+1).ToString()),"y",0.0f));

			for (int i = 0; i<InventoryItems.Length; i++)
			{
				if (ini.ReadValue("Death" + ((DeathSectionNumber+1).ToString()),InventoryItems[i].ItemID,0) >= 1)
				{
					ini.WriteValue(DeathSection,InventoryItems[i].ItemID,ini.ReadValue("Death" + ((DeathSectionNumber+1).ToString()),InventoryItems[i].ItemID,0)); //write the a death key for each inventory item found
				}
			}

			ini.SectionDelete("Death" + ((DeathSectionNumber+1).ToString())); //delete remaining section

		}

		ini.SectionDelete("Death" + ((DeathSectionNumber).ToString()));

	}
}

/// <summary>
/// returns the path to the _iniFileName player ini file, to be used with the INIParser class. This can be called from pickable objects.
/// </summary>
public string DetermineINIPath()
{
	string savePath;
	// depending on the device we're on, we assemble the path
	if (Application.platform == RuntimePlatform.IPhonePlayer)
	{
		savePath = Application.persistentDataPath + _baseFolderName;
	}
	else
	{
		savePath = Application.persistentDataPath + _baseFolderName;
	}
#if UNITY_EDITOR
	savePath = Application.dataPath + _baseFolderName;
#endif

	savePath = savePath + _saveFolderName + _iniFileName;
	return savePath;
}

/// <summary>
/// get the int value of a section and key reference. should only be used for quick calls with one read, not more than one read.
/// </summary>
public int INIRead(string section, string key)
{
	//INIParser ini = new INIParser();
	// Open the save file. If the save file does not exist, INIParser automatically create
	// one
	ini.Open(SaveManager.Instance.DetermineINIPath());
	// Read the score. If the section/key does not exist, default score to 0
	//if (_targetInventory.InventoryTypes == IventoryType.Main)
	int value = ini.ReadValue(section,key,0);
	//inivalue += 1;
	//ini.WriteValue("InAppPurchases",removeads,1);  //make the ads be gone!
	//Debug.Log("test: " + _targetInventory.transform.parent.name);
	ini.Close();

	return value;
}

/// <summary>
/// write the int value of a section and key reference. should only be used for quick calls with one write, not more than one write.
/// </summary>
public void INIWrite(string section, string key, int value)
{
	//INIParser ini = new INIParser();
	// Open the save file. If the save file does not exist, INIParser automatically create
	// one
	ini.Open(DetermineINIPath());
	// Read the score. If the section/key does not exist, default score to 0
	//if (_targetInventory.InventoryTypes == IventoryType.Main)
	//int value = ini.ReadValue(section,key,0);
	//inivalue += 1;
	ini.WriteValue(section,key,value);  //write int value to section and key
	//Debug.Log("test: " + _targetInventory.transform.parent.name);
	ini.Close();
}

/// <summary>
/// write the int value of a section and key reference
/// </summary>
public void INIKeyDelete(string section, string key)
{
	//INIParser ini = new INIParser();
	// Open the save file. If the save file does not exist, INIParser automatically create
	// one
	ini.Open(DetermineINIPath());
	// Read the score. If the section/key does not exist, default score to 0
	//if (_targetInventory.InventoryTypes == IventoryType.Main)
	//int value = ini.ReadValue(section,key,0);
	//inivalue += 1;
	ini.KeyDelete(section, key);
	//Debug.Log("test: " + _targetInventory.transform.parent.name);
	ini.Close();
}


/// <summary>
/// Saves the settings to file
/// </summary>
protected virtual void SaveGameSettings()
{
	MMSaveLoadManager.Save(SaveSettings, _saveFileName, _saveFolderName);
}

/// <summary>
/// Loads the settings from file (if found)
/// </summary>
protected virtual void LoadGameSettings()
{
	SaveSettings settings = (SaveSettings)MMSaveLoadManager.Load(typeof(GameObject), _saveFileName, _saveFolderName);
	if (settings != null) SaveSettings = settings;

}

/// <summary>
/// Resets the settings by destroying the save file
/// </summary>
protected virtual void ResetGameSettings()
{
	MMSaveLoadManager.DeleteSave(_saveFileName, _saveFolderName);
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

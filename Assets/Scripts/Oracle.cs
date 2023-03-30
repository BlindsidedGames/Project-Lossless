using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class Oracle : SerializedMonoBehaviour
{
   

    private readonly string fileName = "betaTestTwo";
    private readonly string saveExtension = "beta";

    //public List<int> skillAutoAssignmentList = new();
    public bool Loaded;

    private void Start()
    {
        BsNewsGet();
        Loaded = false;
        Load();
        Loaded = true;
        InvokeRepeating(nameof(Save), 60, 60);
    }


    private void OnApplicationQuit()
    {
        Save();
    }

#if !UNITY_EDITOR
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
#if UNITY_IOS
            Load();
#elif UNITY_ANDROID
            Load();
#endif
        }
        if (!focus)
        {
            Save();
        }
    }
#endif


    #region NewsTicker

    public BsGamesData bsGamesData;
    public bool gotNews;

    [ContextMenu("BsNewsGet")]
    public async void BsNewsGet()
    {
        var url = "https://www.blindsidedgames.com/newsTicker";

        using var www = UnityWebRequest.Get(url);

        www.SetRequestHeader("Content-Type", "application/jason");
        var operation = www.SendWebRequest();

        while (!operation.isDone) await Task.Yield();

        if (www.result == UnityWebRequest.Result.Success)
        {
            bsGamesData = new BsGamesData();

            var newsjson = www.downloadHandler.text;
            //Debug.Log(json);
            bsGamesData = JsonUtility.FromJson<BsGamesData>(newsjson);
            gotNews = true;
        }
        else
        {
            Debug.Log($"error {www.error}");
        }
    }

    [Serializable]
    public class BsGamesData
    {
        public string latestGameName;
        public string latestGameLink;
        public string latestGameAppStore;
        public string newsTicker;
        public string patreons;
        public string idleDysonSwarm;
    }

    #endregion

    #region Oracle

    public SaveData saveData;

    private string _json;

    #region SaveMethods

    public void WipeAllData()
    {
        File.Delete(Application.persistentDataPath + "/" + fileName + saveExtension);
#if UNITY_IOS || UNITY_ANDROID
        SceneManager.LoadScene(Screen.width > Screen.height ? 2 : 1);
#else
        SceneManager.LoadScene(2);
#endif
    }

    [ContextMenu("WipeSaveData")]
    public void WipeSaveData()
    {
        saveData = new SaveData()
        {
            level1 = new Level1()
        };
    }


    [ContextMenu("Save")]
    public void Save()
    {
        saveData.dateQuitString = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        SaveState(Application.persistentDataPath + "/" + fileName + saveExtension);
    }

    public void SaveState(string filePath)
    {
        var bytes = SerializationUtility.SerializeValue(saveData, DataFormat.JSON);
        File.WriteAllBytes(filePath, bytes);
    }

    public void Load()
    {
        Loaded = false;
        WipeSaveData();

        if (File.Exists(Application.persistentDataPath + "/" + fileName + saveExtension))
        {
            LoadState(Application.persistentDataPath + "/" + fileName + saveExtension);
        }

        else
        {
            saveData.dateStarted = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
            Loaded = true;
        }
    }

    public void LoadState(string filePath)
    {
        if (!File.Exists(filePath)) return;

        var bytes = File.ReadAllBytes(filePath);
        saveData = SerializationUtility.DeserializeValue<SaveData>(bytes, DataFormat.JSON);
        Loaded = true;
    }
    

    /*private void AwayForSeconds()
    {
        if (string.IsNullOrEmpty(oracle.saveSettings.dateQuitString)) return;
        var dateStarted = DateTime.Parse(oracle.saveSettings.dateQuitString, CultureInfo.InvariantCulture);
        var dateNow = DateTime.UtcNow;
        var timespan = dateNow - dateStarted;
        var seconds = (float)timespan.TotalSeconds;
        if (seconds < 0) seconds = 0;
        saveSettings.sdPrestige.doubleTime += seconds;
        AwayFor?.Invoke(seconds);
    }*/

    #endregion
    #endregion

    public enum BuyMode
    {
        Buy1,
        Buy10,
        Buy50,
        Buy100,
        BuyMax
    }

    public enum ResearchBuyMode
    {
        Buy1,
        Buy10,
        Buy50,
        Buy100,
        BuyMax
    }

    public enum NumberTypes
    {
        Standard,
        Scientific,
        Engineering
    }

    [Serializable]
    public class SaveData
    {
        public string dateStarted;
        public string dateQuitString;
        public NumberTypes notation;

        public Level1 level1;
    }

    [Serializable]
    public class Level
    {
        public bool unlocked = false;
        public TimeSpan timeSpentInLevel;
    }
    [Serializable]
    public class Level1 : Level
    {
        public double cash = 0;
        public Level1Lures currentLure = Level1Lures.First;

        public float tuna;
        public float salmon;
        public float herring;
        public float sardines;
        public float trout;
        public float halibut;
        public float flounder;
        public float cod;
        public float anchovies;
        public float shrimp;

        public float boot;
        public float can;
        public float tyre;
        public float plasticBottle;
        public float plant;
    }
    
    #region Enums
    
    public enum Level1Lures
    {
        First = 40,
        Second = 35,
        Third = 30,
        Fourth = 20,
        Fifth = 10,
        Sixth = 5
    }
    
    #endregion


    #region Singleton class: Oracle

    public static Oracle oracle;


    private void Awake()
    {
        if (oracle == null)
            oracle = this;
        else
            Destroy(gameObject);
    }

    #endregion

    /*#region PlayfabDataSaving/loading

    public void SaveExpansion()
    {
        saveSettings.dateQuitString = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        saveSettings.dysonVerseSaveData.dysonVerseInfinityData.SkillTreeSaveData = SkillTree;
        var dataString = Encoding.UTF8.GetString(SerializationUtility.SerializeValue(saveSettings, DataFormat.JSON));
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Expansion", dataString }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    public void LoadExpansion()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnLoadDysonVerse, OnError);
    }

    private void OnLoadDysonVerse(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Expansion"))
        {
            Loaded = false;
            var bytes = Encoding.UTF8.GetBytes(result.Data["Expansion"].Value);
            saveSettings = SerializationUtility.DeserializeValue<SaveDataSettings>(bytes, DataFormat.JSON);
            foreach (var variable in SkillTree)
            {
                if (!saveSettings.dysonVerseSaveData.dysonVerseInfinityData.SkillTreeSaveData.ContainsKey(variable.Key))
                    saveSettings.dysonVerseSaveData.dysonVerseInfinityData.SkillTreeSaveData.Add(variable.Key,
                        variable.Value);
                variable.Value.Owned = saveSettings.dysonVerseSaveData.dysonVerseInfinityData
                    .SkillTreeSaveData[variable.Key].Owned;
            }

/*#if UNITY_IOS || UNITY_ANDROID
            SceneManager.LoadScene(Screen.width > Screen.height ? 2 : 1);
#else
        SceneManager.LoadScene(2);
#endif#1#
            Loaded = true;
        }
        else
        {
            Debug.Log("PlayerDataNotComplete");
        }
    }

    public static event Action SuccessfulCloudSave;

    private void OnDataSend(UpdateUserDataResult result)
    {
        SuccessfulCloudSave?.Invoke();
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error);
    }

    #endregion*/
}
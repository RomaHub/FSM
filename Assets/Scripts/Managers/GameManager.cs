using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Linq;
using System.Collections;

public class GameManager : Singleton<GameManager>
{

    public static event Action showMainMenu;
    public static event Action showNewGameMenu;
    public static event Action showLoadGameMenu;
    public static event Action beforeSave;
    public static event Action afterLoad;
    public static event Action beforeChangeScene;
    public static event Action destroyAllMobs;
    public static event Action<MobNames> createNewMob;
    public static event Action<GameData> createMobs;

    public string[] profiles;

    private string _dataPath;
    private bool _IsSceneBeingLoaded;
    private ISerializer _gameSerializer;
    private GameData _gameData;
    private MobNames _mobNames;

    #region Properties

    public GameData Data
    {
        get { return _gameData; }
        set { _gameData = value; }
    }

    #endregion

    #region OnEvents

    public void OnShowMainMenu()
    {
        if (showMainMenu != null)
        {
            showMainMenu();
        }
    }

    public void OnShowNewGameMenu()
    {
        if (showNewGameMenu != null)
        {
            showNewGameMenu();
        }
    }

    public void OnShowLoadGameMenu()
    {
        if (showLoadGameMenu != null)
        {
            showLoadGameMenu();
        }
    }

    public void OnBeforeSave()
    {
        if (beforeSave != null)
        {
            beforeSave();
        }
    }

    public void OnAfterLoad()
    {
        if (afterLoad != null)
            afterLoad();
    }

    public void OnDestroyAllMobs()
    {
        if (destroyAllMobs != null)
            destroyAllMobs();
    }

    public void OnCreateNewMob()
    {
        if (createNewMob != null)
            createNewMob(_mobNames);
    }

    public void OnCreateMobs()
    {
        if (createMobs != null)
            createMobs(Data);
    }

    #endregion

    #region MonoBehaviour

    private void Start()
    {

        _gameSerializer = new XMLSerializer();
        _gameData = new GameData();
        _mobNames = LoadMobNames("MobNames.xml");

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        NewGameMenu.createNewGame += CreateNewGame;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        NewGameMenu.createNewGame -= CreateNewGame;
    }

    #endregion

    #region Private

    private void SceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == "Game")
        {
            if (_IsSceneBeingLoaded)
            {
                ClearSceneFromMobs();
                OnCreateMobs();
                OnAfterLoad();
                _IsSceneBeingLoaded = false;
            }
            else
            {
                SaveGame();
            }

            BulletPool.Instance.InitializePool();
        }

    }

    private void CreateNewGame(string profileName)
    {
        if (profileName == string.Empty)
            return;

        SetProfile(profileName);

        ChangeScene("Game");
    }

    private MobNames LoadMobNames(string fileName)
    {
        string namesPath = Path.Combine(Application.streamingAssetsPath, "Mobs/" + fileName);
        if (File.Exists(namesPath))
        {
            ISerializer nameSerializer = new XMLSerializer();
            return nameSerializer.Deserialize<MobNames>(namesPath);
        }
        else return new MobNames();
    }

    private void ClearSceneFromMobs()
    {
        Unit[] mobs = FindObjectsOfType<Unit>();
        foreach (Unit m in mobs)
        {
            Destroy(m.gameObject);
        }
    }

    private void ChangeScene(string sceneName)
    {
        if (beforeChangeScene != null)
        {
            beforeChangeScene();
            StartCoroutine(SetScene(sceneName, Fader.duration));
        }
        else StartCoroutine(SetScene(sceneName, 0f));
    }

    IEnumerator SetScene(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(sceneName);
    }

    #endregion

    #region Public

    public void LoadAllProfiles()
    {
        var tempVar = from filePath
                      in Directory.GetFiles(Application.streamingAssetsPath, "*.xml")
                      select Path.GetFileNameWithoutExtension(filePath).Split(new string[] { @"\" }, StringSplitOptions.None).Last();

        profiles = tempVar.ToArray();
    }

    public void SetProfile(string profileName)
    {
        _dataPath = Path.Combine(Application.streamingAssetsPath, profileName + ".xml");
    }

    public void CreateNewMob()
    {
        OnCreateNewMob();
    }

    public void MainMenu()
    {
        ChangeScene("MainMenu");
    }

    public void LoadGame()
    {

        if (File.Exists(_dataPath))
        {
            _gameData = _gameSerializer.Deserialize<GameData>(_dataPath);

            _IsSceneBeingLoaded = true;

            ChangeScene(_gameData.sceneName);
        }
        else
        {
            Debug.LogError("Can't load game data");
        }
    }

    public void SaveGame()
    {

        _gameData.mobs.Clear();

        OnBeforeSave();

        _gameData.sceneName = SceneManager.GetActiveScene().name;

        _gameSerializer.Serialize(_dataPath, _gameData);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion

}



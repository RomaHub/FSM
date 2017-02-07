using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    public Button mainMenuButton;
    public Button loadGameButton;
    public Button saveGameButton;
    public Button destroyAllMobsButton;
    public Button createNewMobButton;
    public Button quitGameButton;

    private void OnEnable()
    {
        mainMenuButton.onClick.AddListener(GameManager.Instance.MainMenu);
        loadGameButton.onClick.AddListener(GameManager.Instance.LoadGame);
        saveGameButton.onClick.AddListener(GameManager.Instance.SaveGame);
        destroyAllMobsButton.onClick.AddListener(GameManager.Instance.OnDestroyAllMobs);
        createNewMobButton.onClick.AddListener(GameManager.Instance.CreateNewMob);
        quitGameButton.onClick.AddListener(GameManager.Instance.QuitGame);
    }

    private void OnDisable()
    {
        mainMenuButton.onClick.RemoveListener(GameManager.Instance.MainMenu);
        loadGameButton.onClick.RemoveListener(GameManager.Instance.LoadGame);
        saveGameButton.onClick.RemoveListener(GameManager.Instance.SaveGame);
        destroyAllMobsButton.onClick.RemoveListener(GameManager.Instance.OnDestroyAllMobs);
        createNewMobButton.onClick.RemoveListener(GameManager.Instance.CreateNewMob);
        quitGameButton.onClick.RemoveListener(GameManager.Instance.QuitGame);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button newGameButton;
    public Button loadGameButton;
    public Button quitGameButton;

    private void OnEnable()
    {
        newGameButton.onClick.AddListener(GameManager.Instance.OnShowNewGameMenu);
        loadGameButton.onClick.AddListener(GameManager.Instance.OnShowLoadGameMenu);
        quitGameButton.onClick.AddListener(GameManager.Instance.QuitGame);
    }

    private void OnDisable()
    {
        newGameButton.onClick.RemoveListener(GameManager.Instance.OnShowNewGameMenu);
        loadGameButton.onClick.RemoveListener(GameManager.Instance.OnShowLoadGameMenu);
        quitGameButton.onClick.RemoveListener(GameManager.Instance.QuitGame);
    }
}

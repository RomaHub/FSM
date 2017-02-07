using UnityEngine;
using UnityEngine.UI;
using System;

public class NewGameMenu : MonoBehaviour
{

    public static event Action<string> createNewGame;

    public Button startButton;
    public Button backButton;
    public InputField inputField;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        startButton.onClick.AddListener(OnCreateNewGame);
        backButton.onClick.AddListener(GameManager.Instance.OnShowMainMenu);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(OnCreateNewGame);
        backButton.onClick.RemoveListener(GameManager.Instance.OnShowMainMenu);
    }

    public void OnCreateNewGame()
    {
        if (createNewGame != null)
            createNewGame(inputField.text);
    }
}

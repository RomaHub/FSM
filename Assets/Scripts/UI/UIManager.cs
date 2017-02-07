using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject newGameMenu;
    public GameObject loadGameMenu;

    private void OnEnable()
    {
        GameManager.showMainMenu += GameManager_showMainMenu;
        GameManager.showNewGameMenu += GameManager_showNewGameMenu;
        GameManager.showLoadGameMenu += GameManager_showLoadGameMenu;
    }

    private void OnDisable()
    {
        GameManager.showMainMenu -= GameManager_showMainMenu;
        GameManager.showNewGameMenu -= GameManager_showNewGameMenu;
        GameManager.showLoadGameMenu -= GameManager_showLoadGameMenu;
    }

    private void GameManager_showMainMenu()
    {
        if (mainMenu != null) mainMenu.SetActive(true);
        if (newGameMenu != null) newGameMenu.SetActive(false);
        if (loadGameMenu != null) loadGameMenu.SetActive(false);
    }

    private void GameManager_showNewGameMenu()
    {
        if (mainMenu != null) mainMenu.SetActive(false);
        if (newGameMenu != null) newGameMenu.SetActive(true);
        if (loadGameMenu != null) loadGameMenu.SetActive(false);
    }

    private void GameManager_showLoadGameMenu()
    {
        if (mainMenu != null) mainMenu.SetActive(false);
        if (newGameMenu != null) newGameMenu.SetActive(false);
        if (loadGameMenu != null) loadGameMenu.SetActive(true);
    }

}

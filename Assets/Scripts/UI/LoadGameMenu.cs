using UnityEngine;
using UnityEngine.UI;

public class LoadGameMenu : MonoBehaviour
{

    public Button prefabButton;
    public Button backButton;
    public Transform holder;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateButtons();
    }

    private void OnEnable()
    {
        backButton.onClick.AddListener(GameManager.Instance.OnShowMainMenu);
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveListener(GameManager.Instance.OnShowMainMenu);
    }

    private void CreateButtons()
    {
        GameManager.Instance.LoadAllProfiles();

        for (int i = 0; i < GameManager.Instance.profiles.Length; i++)
        {
            Button b = Instantiate(prefabButton) as Button;
            b.gameObject.transform.SetParent(holder, false);
            Text t = b.transform.GetChild(0).GetComponent<Text>();
            t.text = GameManager.Instance.profiles[i];

            b.onClick.AddListener(() =>
            {
                GameManager.Instance.SetProfile(t.text);
                GameManager.Instance.LoadGame();
            });
        }

    }
}

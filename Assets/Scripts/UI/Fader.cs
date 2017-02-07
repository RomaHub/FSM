using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public static float duration = 1f;

    public GameObject faderPrefab;

    private GameObject _fader;
    private Image _imageFader;
    private float _alpha = 1.0f;


    void Start()
    {
        _fader = Instantiate(faderPrefab) as GameObject;
        _fader.transform.SetParent(gameObject.transform, false);
        _fader.SetActive(true);
        _fader.transform.SetAsLastSibling();
        _imageFader = _fader.GetComponent<Image>();

        FadeOut();
    }

    void OnEnable()
    {
        GameManager.beforeChangeScene += FadeIn;
    }

    void OnDisable()
    {
        GameManager.beforeChangeScene -= FadeIn;
    }

    private void FadeIn()
    {
        _fader.SetActive(true);
        _fader.transform.SetAsLastSibling();
        StartCoroutine(FadeToBlack(duration));
    }

    private void FadeOut()
    {
        StartCoroutine(FadeToClear(duration));
    }

    IEnumerator FadeToBlack(float time)
    {
        _alpha = 0f;

        float _speed = 1f / time;

        while (_alpha < 1.0f)
        {
            _alpha = Mathf.MoveTowards(_alpha, 1, Time.deltaTime * _speed);
            _imageFader.color = new Color(_imageFader.color.r, _imageFader.color.g, _imageFader.color.b, _alpha);
            yield return null;
        }

        _imageFader.color = Color.black;
    }

    IEnumerator FadeToClear(float time)
    {
        _alpha = 1f;

        float _speed = 1f / time;

        while (_alpha > 0.0f)
        {
            _alpha = Mathf.MoveTowards(_alpha, 0, Time.deltaTime * _speed);
            _imageFader.color = new Color(_imageFader.color.r, _imageFader.color.g, _imageFader.color.b, _alpha);
            yield return null;
        }

        _imageFader.color = Color.clear;
        _fader.SetActive(false);
    }
}

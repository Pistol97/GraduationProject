using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeEffect : MonoBehaviour
{
    private Image _panel;

    private float _fadeoutTimer;
    private float _fadeoutTimerMax = 2f;

    private float _alpha;

    public bool _fadeout;

    private void Awake()
    {
        _panel = GetComponent<Image>();
    }

    void Update()
    {
        if(_fadeout)
        {
            if (Color.white != _panel.color)
            {
                _panel.color = Color.white;
            }

            _fadeoutTimer += Time.deltaTime;
            _alpha = Mathf.Lerp(_fadeoutTimer, 1f, _fadeoutTimer / _fadeoutTimerMax);

            _panel.color = new Color(_panel.color.r, _panel.color.g, _panel.color.b, _alpha);
        }
    }
    public IEnumerator GameEnd()
    {
        gameObject.SetActive(true);
        _fadeout = true;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainMenu");
    }
}

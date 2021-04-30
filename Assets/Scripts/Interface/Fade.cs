using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image panel;
    private float time = 0f;
    private float timeMax = 2f;

    private void Start()
    {
        FadeEvent();
    }
    public void FadeEvent()
    {
        StartCoroutine(FadeIn());
    }
    public IEnumerator FadeIn()
    {
        panel.gameObject.SetActive(true);
        time = 0f;
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1);
        Color alpha = panel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / timeMax;
            alpha.a = Mathf.Lerp(1, 0, time);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }
    public IEnumerator FadeOut()
    {
        panel.gameObject.SetActive(true);
        time = 0f;
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
        Color alpha = panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / timeMax;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }
    public IEnumerator FadeFlow()
    {
        panel.gameObject.SetActive(true);
        time = 0f;
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
        Color alpha = panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / timeMax;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;
            yield return null;
        }
        time = 0f;

        yield return new WaitForSeconds(1f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / timeMax;
            alpha.a = Mathf.Lerp(1, 0, time);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }
}

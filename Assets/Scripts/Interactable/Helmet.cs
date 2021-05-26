using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Helmet : MonoBehaviour,IInteractable
{
    [SerializeField]
    private GameObject Bar_Fear;
    [SerializeField]
    private GameObject Bar_Sonar;
    [SerializeField]
    private GameObject SonarDisplay;

    [SerializeField]
    private Image panel;

    private float time = 0f;
    private float timeMax = 0.5f;

    [SerializeField]
    private AudioSource helmetSound;

    private void Start()
    {
        helmetSound = GetComponent<AudioSource>();
    }

    public void ObjectInteract()
    {
        Debug.Log("HELMET");

        Fade();
    }

    private void SonaActive()
    {
        Debug.Log("소나켜짐");
        Bar_Fear.gameObject.SetActive(true);
        Bar_Sonar.gameObject.SetActive(true);
        SonarDisplay.gameObject.SetActive(true);
        Destroy(this.transform.gameObject);
    }

    public void Fade()
    {
        StartCoroutine(FadeFlow());
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

        helmetSound.Play();

        yield return new WaitForSeconds(0.5f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / timeMax;
            alpha.a = Mathf.Lerp(1, 0, time);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);

        SonaActive();
        FindObjectOfType<EventMessage>().DisplayMessage("소나가 장착되었습니다");

        yield return new WaitForSeconds(0.5f);

        yield return null;
    }
}

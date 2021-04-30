using UnityEngine;
using UnityEngine.UI;

public class EventMessage : MonoBehaviour, IDialogueEvent
{
    private bool startBlur;
    private float disappearTimer = 0f;
    private float disappearTimerMax = 5f;

    private Text dialogue;

    private Color alpha;
    private Color origin;

    
    private void Awake()
    {
        dialogue = GetComponent<Text>();
        //원본 텍스트 컬러 저장
        origin = new Color(
            dialogue.color.r,
            dialogue.color.g,
            dialogue.color.b,
            dialogue.color.a);
        alpha = origin;
    }

    private void LateUpdate()
    {
        Blurring();
    }

    public void DisplayMessage(string message)
    {
        dialogue.text = message;
        dialogue.color = origin;
    }

    private void Blurring()
    {
        if (origin.a <= GetComponent<Text>().color.a)
        {
            startBlur = true;
        }

        else if (0f == GetComponent<Text>().color.a)
        {
            startBlur = false;
            disappearTimer = 0f;
        }

        if (startBlur)
        {
            disappearTimer += Time.deltaTime;

            alpha.a = Mathf.Lerp(disappearTimerMax, 0f, disappearTimer / disappearTimerMax);
            dialogue.color = alpha;
        }
    }
}

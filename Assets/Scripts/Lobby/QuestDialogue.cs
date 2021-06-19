using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDialogue : MonoBehaviour
{
    private Text _questText;
    public string _description
    {
        set
        {
            if (_isRunnig)
            {
                StopCoroutine(_coroutine);
                _questText.text = null;
                _coroutine = TextAnimation(value);
                StartCoroutine(_coroutine);
            }

            else
            {
                _coroutine = TextAnimation(value);
                StartCoroutine(_coroutine);
            }
        }
    }
    private IEnumerator _coroutine;
    private bool _isRunnig;
    private void Awake()
    {
        _questText = GetComponent<Text>();
    }


    public IEnumerator TextAnimation(string description)
    {
        _isRunnig = true;
        for (int i = 0; i <= description.Length; i++)
        {
            _questText.text = description.Substring(0, i);
            yield return new WaitForSeconds(0.02f);
        }

        _isRunnig = false;

    }
}

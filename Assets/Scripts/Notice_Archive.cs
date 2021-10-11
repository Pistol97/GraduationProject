using UnityEngine;
using UnityEngine.UI;

public class Notice_Archive : MonoBehaviour
{
    private Color _color;

    private float _timer;
    private readonly float _wait = 4f;

    private bool _isBrighter;

    private void Awake()
    {
        _color = GetComponent<Image>().color;
        _timer = 0f;

        _isBrighter = false;
    }

    void Update()
    {
        _timer += 1f * Time.deltaTime;

        if (_wait <= _timer)
        {
            Destroy(gameObject);
        }

        if (!_isBrighter)
        {
            _color.a = Mathf.MoveTowards(_color.a, 0f, Time.deltaTime);

            if(_color.a <= 0f)
            {
                _isBrighter = true;
            }
        }

        else
        {
            _color.a = Mathf.MoveTowards(_color.a, 1f, Time.deltaTime);

            if(_color.a >= 1f)
            {
                _isBrighter = false;
            }
        }

        GetComponent<Image>().color = _color;

    }
}

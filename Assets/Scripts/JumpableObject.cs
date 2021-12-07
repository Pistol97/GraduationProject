using UnityEngine;

public class JumpableObject : MonoBehaviour
{
    private Transform _jumpPos;

    private Player _player;

    private void Awake()
    {
        _jumpPos = transform.GetChild(0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_player)
            {
                _player = other.GetComponent<Player>();
                _player.JumpPos = _jumpPos;
            }

            _player.GetComponentInChildren<SelectionManager>().ActionText.gameObject.SetActive(true);
            _player.GetComponentInChildren<SelectionManager>().ActionText.text = "<color=yellow>" + " (F) " + "</color>" + "Jump over";


            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("플레이어 점프");
                _player.GetComponent<Animator>().SetBool("IsJump", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _player = null;
    }
}

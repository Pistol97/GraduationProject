using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_PlayerCaught : StateMachineBehaviour
{
    private Player _player;
    private CameraControl _camControl;

    private int _countLeft;
    private int _countRight;
    private readonly int _MaxCount = 20;
    private Slider _progressBar;

    [SerializeField] private float _tickDamage;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioMgr.Instance.PlayRandomSound("PlayerCaught");
        _player = animator.GetComponent<Player>();
        _camControl = animator.transform.GetChild(0).GetComponent<CameraControl>();
        var go = Instantiate(Resources.Load("UI/Bar_Shake"), _player.Hud.transform) as GameObject;
        _progressBar = go.GetComponent<Slider>();
        _progressBar.maxValue = _MaxCount;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player.FearRange += (_tickDamage * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.A))
        {
            _countLeft++;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _countRight++;
        }

        _progressBar.value = _countLeft + _countRight;

        if (_MaxCount <= _countLeft + _countRight || !animator.GetBool("IsCaught"))
        {
            animator.SetBool("IsCaught", false);
            _camControl.enabled = true;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<CapsuleCollider>().isTrigger = true;
        Destroy(_progressBar.gameObject);
        _player.LookTarget = null;
        _countLeft = _countRight = 0;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

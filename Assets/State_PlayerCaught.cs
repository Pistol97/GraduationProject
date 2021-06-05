using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PlayerCaught : StateMachineBehaviour
{
    private Player _player;
    private CameraControl _camControl;

    private int _countLeft;
    private int _countRight;

    [SerializeField] private float _tickDamage;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.GetComponent<Player>();
        _camControl = animator.transform.GetChild(0).GetComponent<CameraControl>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player.FearRange += (2f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.A))
        {
            _countLeft++;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _countRight++;
        }

        if (20 <= _countLeft + _countRight)
        {
            animator.SetBool("IsCaught", false);
            _camControl.enabled = true;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<CapsuleCollider>().isTrigger = true;
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

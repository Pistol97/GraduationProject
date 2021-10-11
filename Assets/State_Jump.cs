using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Jump : StateMachineBehaviour
{
    private Transform _player;
    private Transform _JumpPos;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.GetComponent<Player>().transform;
        _JumpPos = _player.GetComponentInChildren<SelectionManager>().JumpTarget;

        _JumpPos.GetComponent<BoxCollider>().isTrigger = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player.position = Vector3.MoveTowards(_player.position, _JumpPos.position, 0.5f);

        if(_player.position == _JumpPos.position)
        {
            _JumpPos.position = Vector3.zero;

            _JumpPos.GetComponent<BoxCollider>().isTrigger = false;
            animator.SetBool("IsJump", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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

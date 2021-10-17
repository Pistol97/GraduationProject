using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Jump : StateMachineBehaviour
{
    private Transform _player;
    private Transform _JumpPos;

    private float _speed = 0.05f;

    private float _timer = 1f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.GetComponent<Player>().transform;
        _JumpPos = _player.GetComponent<Player>().JumpPos;

        _JumpPos.GetComponentInParent<MeshCollider>().convex = true;
        _JumpPos.GetComponentInParent<MeshCollider>().isTrigger = true;

        _player.GetComponentInChildren<CameraControl>().enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer -= Time.deltaTime;

        _player.position = Vector3.Lerp(_player.position, _JumpPos.position, _speed);

        if (0 >= _timer)
        {
            _JumpPos.GetComponentInParent<MeshCollider>().isTrigger = false;
            _JumpPos.GetComponentInParent<MeshCollider>().convex = false;

            animator.SetBool("IsJump", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player.GetComponentInChildren<CameraControl>().enabled = true;

        _timer = 1f;
    }
}

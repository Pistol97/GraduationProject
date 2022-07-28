using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_EnemyIdle : StateMachineBehaviour
{
    private Enemy enemy;

    private float _timer;
    private readonly int _wait = 10;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;

        if (0 >= (int)_timer % _wait)
        {
            enemy.PlayIdle();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0f;
    }
}

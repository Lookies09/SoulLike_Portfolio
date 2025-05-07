using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{


    // 적의 현재 동작중인 공격 상태 컴포넌트
    [SerializeField] protected AttackPatternState currentState;

    // 적의 모든 공격 패턴 컴포넌트들
    [SerializeField] protected AttackPatternState[] attackPatternState;

    protected Animator animator;

    public AttackPatternState CurrentState { get => currentState; set => currentState = value; }
    public AttackPatternState[] attackPatternState1 { get => attackPatternState; set => attackPatternState = value; }

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }
            

    public void TransactionToState(int state)
    {
        
        CurrentState?.ExitState(); // 이전 상태 정리
        CurrentState = attackPatternState[state]; // 상태 전환 처리
        CurrentState.EnterState(state); // 새로운 상태 전이
    }

}

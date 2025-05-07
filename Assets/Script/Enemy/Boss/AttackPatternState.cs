using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class AttackPatternState : MonoBehaviour
{
    protected EnemyController controller;
    protected AttackController aController;
        
    // 애니메이터 컴포넌트
    protected Animator animator;

    // 네비게이션 컴포넌트
    protected NavMeshAgent navMeshAgent;



    protected virtual void Awake()
    {
        controller = GetComponent<EnemyController>();
        aController = GetComponent<AttackController>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    
    public abstract void EnterState(int state);


    // 몬스터 상태 종료(다른상태로 전이됨) 메소드
    public abstract void ExitState();


}

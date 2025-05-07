using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 적군 컨트롤러 부모
public abstract class EnemyController : MonoBehaviour
{
    // 상태 구현
    
    // 적의 현재 동작중인 상태 컴포넌트
    [SerializeField] protected EnemyState currentState;

    // 적의 모든 상태 컴포넌트들
    [SerializeField] protected EnemyState[] EnemyStates;

    // 플레이어 참조
    protected GameObject player;    

    // 애니메이터
    protected Animator animator;

    // 락온 이미지 위치
    [SerializeField] protected Transform targetDotPos;

    // 히트 이펙트
    [SerializeField] protected GameObject hitEffect;

    // 히트 이펙트 위치
    [SerializeField] protected Transform hitEffectPos;

    // 배회 기준 위치 포인트들
    [SerializeField] protected Transform[] wanderPoints;

    public Transform[] WanderPoints { get => wanderPoints; set => wanderPoints = value; }
    public Transform TargetDotPos { get => targetDotPos; set => targetDotPos = value; }
    public GameObject Player { get => player; set => player = value; }
    public EnemyState[] EnemyStates1 { get => this.EnemyStates; set => this.EnemyStates = value; }
    public EnemyState CurrentState { get => currentState; set => currentState = value; }


    protected void Awake()
    {
        animator = GetComponent<Animator>();        
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Player = GameObject.FindWithTag("Player");

        // 대기 상태로 시작
        TransactionToState(0);
    }

    // Update is called once per frame
    protected void Update()
    {
        // * 현재 설정된 상태의 기능을 동작!
        CurrentState?.UpdateState();
    }

    // * 상태 전환 메소드
    public void TransactionToState(int state)
    {
        Debug.Log($"적군 상태 전환 : {state}");

        CurrentState?.ExitState(); // 이전 상태 정리
        CurrentState = EnemyStates[state]; // 상태 전환 처리
        CurrentState.EnterState(state); // 새로운 상태 전이
    }

    // 플레이어와 적군 간의 거리 측정
    public float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, Player.transform.position);
    }



    

}

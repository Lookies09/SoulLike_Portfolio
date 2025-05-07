using UnityEngine;

public class EnemyAttackAbleState : EnemyState
{
    [SerializeField] protected float attackDistance; // 플레이어 공격 가능 거리
    [SerializeField] protected float detectDistance; // 플레이어 추적 가능 거리

    protected bool isHit;

    [SerializeField] protected float slerpValue = 20;

    // 패링 확인
    protected bool isParry;

    // 리바운드 가능 공격
    protected bool isReboundAttack;

    public bool IsParry { get => isParry; set => isParry = value; }
    public bool IsHit { get => isHit; set => isHit = value; }

    public override void EnterState(int state)
    {

    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }

    // 공격 대상을 주시
    public void LookAtTarget(bool isOn)
    {
        if (isOn)
        {
            // 공격 대상을 향한 방향을 계산
            Vector3 direction = (GameObject.Find("Player").transform.position - transform.position).normalized;

            // 회전 쿼터니언 계산
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

            // 보간 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, slerpValue * Time.deltaTime);
        }


    }



    public void CanParryEvent()
    {
        IsParry = true;
    }

    public void CannotParryEvent()
    {
        IsParry = false;
    }

    public void IsHitEvent()
    {
        isHit = true;        
    }

    public void IsHitEndEvent()
    {
        isHit = false;
    }

    // 패링 가능공격
    public void IsReboundAttack()
    {
        isReboundAttack = true;
    }
}

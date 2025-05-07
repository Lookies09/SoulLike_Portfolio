using UnityEngine;

public class EnemyAttackAbleState : EnemyState
{
    [SerializeField] protected float attackDistance; // �÷��̾� ���� ���� �Ÿ�
    [SerializeField] protected float detectDistance; // �÷��̾� ���� ���� �Ÿ�

    protected bool isHit;

    [SerializeField] protected float slerpValue = 20;

    // �и� Ȯ��
    protected bool isParry;

    // ���ٿ�� ���� ����
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

    // ���� ����� �ֽ�
    public void LookAtTarget(bool isOn)
    {
        if (isOn)
        {
            // ���� ����� ���� ������ ���
            Vector3 direction = (GameObject.Find("Player").transform.position - transform.position).normalized;

            // ȸ�� ���ʹϾ� ���
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

            // ���� ȸ��
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

    // �и� ���ɰ���
    public void IsReboundAttack()
    {
        isReboundAttack = true;
    }
}

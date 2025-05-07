using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectHealth : MonoBehaviour
{
    // 캐릭터 체력
    [SerializeField] protected int health;
    
    // 애니메이터 참조
    protected Animator animator;

    // 캐릭터 죽음 판별
    protected bool isDeath = false;

    // 캐릭터 피격 판별
    protected bool isHit = false;

    // 체간
    [SerializeField] protected float posture;

    // 사망 확인
    protected bool isDead = false;

    protected int startHp;
    public int StartHp { get => startHp; set => startHp = value; }
    public int Health { get => health; set => health = value; }
    public bool IsHit { get => isHit; set => isHit = value; }
    public float Posture { get => posture; set => posture = value; }
    public bool IsDead { get => isDead; set => isDead = value; }

    private void LateUpdate()
    {
        if (posture > 100)
        {
            posture = 100;
        }
        else if(posture < 0)
        {
            posture = 0;
        }                 
    }


    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public abstract void Hit(int DMG);

    public abstract void Death();

    public abstract void postureBreak();


    protected void OnHit()
    {
        IsHit = true;
    }

    protected void ExitHit()
    {
        IsHit = false;
    }
    
}

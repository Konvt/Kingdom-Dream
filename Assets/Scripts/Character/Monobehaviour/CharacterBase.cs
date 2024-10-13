using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public int maxHp;

    public IntVariable hp;

    public int CurrentHp { get => hp.currentValue; set=>hp.SetValue(value);  }

    public int MaxHp { get => hp.maxValue; }

    public bool isDead;

    protected Animator animator;

    protected virtual  void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        isDead = false;
        hp.maxValue = maxHp;
        CurrentHp = maxHp;
    }

    public virtual void TakeDamage(int damage)
    {
        if (CurrentHp > damage)
        {
            CurrentHp -= damage;
        }
        else
        {
            //À¿Õˆ¬ﬂº≠
            isDead = true;
        }
    }
}

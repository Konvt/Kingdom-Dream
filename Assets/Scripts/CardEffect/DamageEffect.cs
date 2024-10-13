using UnityEngine;

[CreateAssetMenu(fileName ="DamageEffect",menuName ="Effects/DamageEffect")]
public class DamageEffect :Effect
{
    public override void Excute(CharacterBase from, CharacterBase Target)
    {
        if(Target==null) return;
        switch (targetType)
        {
            case EffectTargetType.Self:
                break;
            case EffectTargetType.Target:
                Target.TakeDamage(value);
                Debug.Log("����ˣ�"+value+" ���˺�");
                break;
            case EffectTargetType.All:
                //Ⱥ���߼�
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<CharacterBase>().TakeDamage(value);
                }
                break;
        }
    }

  
}

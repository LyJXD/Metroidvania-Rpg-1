using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;    // 距用户上次使用技能时间
    }

    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;       // 重置为设定冷却时间
            return true;
        }

        Debug.Log("Skill is on cooldown.");
        return false;
    }

    public virtual void UseSkill()
    {

    }
}

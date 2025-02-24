using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField]private Skill weaponSkill;

    public virtual void ApplyEffect(SkillManager skilManager)
    {
        skilManager.AddSkill(weaponSkill);
    }
}

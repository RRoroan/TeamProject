using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectimeSkill : Skill
{
    public GameObject projectilePrefab;
    public int projectileCount = 1;

    public override void UseSkill(StatHandler handler)
    {
        // 
        if (Time.time - useTime < cooldown) return;

        useTime = Time.time;

        //���� ����� ���� ã��
        //GameObject targe;
        //if (targe == null) return;

        //for (int i = 0; i < projectileCount; i++)
        //{
        //    ShhotProjectile(player, target); 
        //}
    }


}

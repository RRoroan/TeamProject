using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSkill : BaseSkill
{
    // 사용할 프리팹
    [SerializeField] private GameObject rotatingPrefabs;

    // 스킬이 돌아다닐 범위
    [SerializeField] private float range = 2f;

    // 스킬이 지속될 시간
    [SerializeField] private float lifetime = 3f;


    public override void UseSkill()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSkill : BaseSkill
{
    // ����� ������
    [SerializeField] private GameObject rotatingPrefabs;

    // ��ų�� ���ƴٴ� ����
    [SerializeField] private float range = 2f;

    // ��ų�� ���ӵ� �ð�
    [SerializeField] private float lifetime = 3f;


    public override void UseSkill()
    {
        
    }
}

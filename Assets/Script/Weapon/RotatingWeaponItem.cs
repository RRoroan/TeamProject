using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWeaponItem : WeaponItem
{
    [SerializeField]private GameObject rotatingWeaponPrefab;
    // ��ߵ� ��Ÿ�� 
    [SerializeField]private float effectDurataion = 5f;
    // ��ų ���� �ð�
    [SerializeField]private float effectActivation = 3f;
    // ������ �Ծ�����
    private bool isActive = false;

    public override void ApplyEffect(SkillManager skilManager)
    {
        base.ApplyEffect(skilManager);
        if (!isActive)
        {
            isActive = true;
            skilManager.StartCoroutine(ActivateRotatingWeapon(skilManager));
        }
    }

    private IEnumerator ActivateRotatingWeapon(SkillManager skillManager)
    {
        while (isActive)
        {
            GameObject rotatingWeapon = Instantiate(rotatingWeaponPrefab, skillManager.transform);
            Destroy(rotatingWeapon, effectDurataion);
            yield return new WaitForSeconds(effectActivation);
        }
    }

}

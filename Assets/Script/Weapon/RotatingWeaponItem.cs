using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWeaponItem : WeaponItem
{
    [SerializeField]private GameObject rotatingWeaponPrefab;
    // 재발동 쿨타임 
    [SerializeField]private float effectDurataion = 5f;
    // 스킬 유지 시간
    [SerializeField]private float effectActivation = 3f;
    // 아이템 먹었는지
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // 활성화 된 스킬 목록(먹은 아이템)
    private List<BaseSkill> activeSkill = new List<BaseSkill>();
    private Inventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        StartCoroutine(ActivateSkills());
    }

    private IEnumerator ActivateSkills()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            foreach (BaseSkill skill in activeSkill)
            {
                // 인벤토리 추가 후 인벤토리에 아이템이 있을 시 스킬이 실행되게 설정
                //if (inventory.hasitem(skill.requireditem))  // 인벤토리에 해당 아이템이 있다면 실행
                //{
                //    skill.useskill();
                //}
            }
        }
    }

    public void AddSkill(BaseSkill skill)
    {
        if (!activeSkill.Contains(skill))
        {
            activeSkill.Add(skill);
        }
    }

}

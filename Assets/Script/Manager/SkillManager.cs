using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Ȱ��ȭ �� ��ų ���(���� ������)
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
                // �κ��丮 �߰� �� �κ��丮�� �������� ���� �� ��ų�� ����ǰ� ����
                //if (inventory.hasitem(skill.requireditem))  // �κ��丮�� �ش� �������� �ִٸ� ����
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

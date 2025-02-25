using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Ȱ��ȭ �� ��ų ���(���� ������)
    private List<BaseSkill> activeSkill = new List<BaseSkill>();
    private Inventory inventory;
    public Transform player; // �÷��̾��� Transform

    //�׽�Ʈ�� �ڵ�
    public GameObject testItemPrefab;
    //-----

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        StartCoroutine(ActivateSkills());
        //
        GameObject testItem = Instantiate(testItemPrefab, player);
        testItem.name = "TestProjectile";

        Item testItemComponent = testItem.GetComponent<Item>();
        if (testItemComponent != null)
        {
            
        }
        //
    }

    private IEnumerator ActivateSkills()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            foreach (BaseSkill skill in activeSkill)
            {
                //�κ��丮�� �ִ� �������� �˻�
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

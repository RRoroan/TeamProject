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
    public GameObject testBoombardPrefab;
    //-----

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        StartCoroutine(ActivateSkills());
        // �׽�Ʈ ----
        GameObject testItem = Instantiate(testItemPrefab, player);
        GameObject testBoombard = Instantiate(testBoombardPrefab, player);
        testItem.name = "TestProjectile";

        Item testItemComponent = testItem.GetComponent<Item>();
        if (testItemComponent != null)
        {
            inventory.Additem(testItemComponent);
            Debug.Log("�׽�Ʈ ������ �߰� �Ϸ�");
        }
        else
        {
            Debug.Log("�׽�Ʈ �����ۿ� item������Ʈ�� �����ϴ�.");
        }
        
        Item testBoombardComponent = testBoombard.GetComponent<Item>();
        if (testBoombardComponent != null)
        {
            inventory.Additem(testBoombardComponent);
            Debug.Log("���� ������ �߰� �Ϸ�");
        }
        else
        {
            Debug.Log("���ݾ����ۿ� item ������Ʈ�� ����");
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
                skill.UseSkill();
            }
        }
    }

    public void AddSkill(BaseSkill skill)
    {
        if (!activeSkill.Contains(skill))
        {
            activeSkill.Add(skill);
            Debug.Log("��ų�� Ȱ��ȸ��");
        }
    }

    public void ResgisterSkills(Item item)
    {
        if (item.skill == null) return;

        AddSkill(item.skill);
    }

    public void RemoveSkill(BaseSkill skill)
    {
        if (activeSkill.Contains(skill))
        {
            activeSkill.Remove(skill);
            Debug.Log("��ų�� ���ŵǾ����ϴ�.");
        }
    }

    public void RemoveSkills(Item item)
    {
        BaseSkill skill = item.GetComponent<BaseSkill>();
        if (skill != null)
        {
            RemoveSkill(skill);
        }
    }

}

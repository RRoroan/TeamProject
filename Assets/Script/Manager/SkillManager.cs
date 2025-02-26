using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Ȱ��ȭ �� ��ų ���(���� ������)
    private List<BaseSkill> activeSkill = new List<BaseSkill>();
    private Inventory inventory;
    public Transform player; // �÷��̾��� Transform

    private Dictionary<BaseSkill, Coroutine> skillCoroutines = new Dictionary<BaseSkill, Coroutine>();

    //�׽�Ʈ�� �ڵ�
    public GameObject testItemPrefab;
    public GameObject testBoombardPrefab;
    public GameObject testRotationPrefab;
    //-----

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        // �׽�Ʈ ----
        GameObject testItem = Instantiate(testItemPrefab, player);
        GameObject testBoombard = Instantiate(testBoombardPrefab, player);
        GameObject testRotation = Instantiate(testRotationPrefab, player);
        testItem.name = "TestProjectile";

        Item testItemComponent = testItem.GetComponent<Item>();
        if (testItemComponent != null)
        {
            inventory.Additem(testItemComponent);
            Debug.Log("����ü ������ �߰� �Ϸ�");
        }
        else
        {
            Debug.Log("����ü �����ۿ� item������Ʈ�� �����ϴ�.");
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

        Item testRotationComponent = testRotation.GetComponent<Item>();
        if (testRotationComponent != null)
        {
            inventory.Additem(testRotationComponent);
            Debug.Log("ȸ�������� �߰� �Ϸ�");
        }
        else
        {
            Debug.LogError("ȸ�� �����ۿ� item������Ʈ�� �����ϴ�.");
        }

    }

    private IEnumerator SkillRoutine(BaseSkill skill)
    {
        while (activeSkill.Contains(skill))
        {
            if (skill == null)
            {
                Debug.LogWarning("SkillRoutine : ��ų�� �����ϴ�.");
                yield break;
            }

            StartCoroutine(DelaySkill(skill));

            float cooldown = skill.GetCooldown();
            if (cooldown <= 0)
            {
                Debug.LogError("SkillRoutine ��ų ��Ÿ���� 0�� �����Դϴ�. ��ƾ�� �����մϴ�.");
                yield break;
            }

            yield return new WaitForSeconds(cooldown);
        }
    }

    IEnumerator DelaySkill(BaseSkill skill)
    {
        yield return new WaitForSeconds(3f);
        skill.UseSkill();
    }

    // ��ų�� �߰��ϰ� ��ų�� �߰� ���� �� �ٷ� ��ų��ƾ ����
    public void AddSkill(BaseSkill skill)
    {
        if (!activeSkill.Contains(skill))
        {
            activeSkill.Add(skill);

            if (!skill.gameObject.activeSelf)
            {
                skill.gameObject.SetActive(true);
            }


            // ���� �������� �ڷ�ƾ�� �ִٸ� �����ϰ� ����
            if (skillCoroutines.ContainsKey(skill))
            {
                StopCoroutine(skillCoroutines[skill]);
                skillCoroutines.Remove(skill);
            }

            Coroutine skillRoutine = StartCoroutine(SkillRoutine(skill));

            skillCoroutines[skill] = skillRoutine;
            Debug.Log("��ų�� Ȱ��ȸ��");
        }
        else
        {
            skill.skillLevel++;
        }
    }

    public void ResgisterSkills(Item item)
    {
        if (item.skill == null) return;

        if (!HasSkill(item.skill))
        {
            AddSkill(item.skill);
            Debug.Log($"{item.itemName}�� ��ų�� ���");
        }
        else
        {
            Debug.LogWarning($"{item.itemName}�� ��ų�� �̹� Ȱ��ȭ�� �Ǿ��־��ϴ�.");
        }
    }

    // ��ų�� ���ŵǸ� ��ų��ƾ�� ���߰� ����Ʈ���� ����
    public void RemoveSkill(BaseSkill skill)
    {
        if (activeSkill.Contains(skill))
        {
            activeSkill.Remove(skill);

            if (skillCoroutines.ContainsKey(skill))
            {
                StopCoroutine(skillCoroutines[skill]);
                skillCoroutines.Remove(skill);
            }

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

    public bool HasSkill(BaseSkill skill)
    {
        return activeSkill.Contains(skill);
    }

}

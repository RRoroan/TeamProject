using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // 활성화 된 스킬 목록(먹은 아이템)
    private List<BaseSkill> activeSkill = new List<BaseSkill>();
    private Inventory inventory;
    public Transform player; // 플레이어의 Transform

    private Dictionary<BaseSkill, Coroutine> skillCoroutines = new Dictionary<BaseSkill, Coroutine>();

    //테스트용 코드
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
        // 테스트 ----
        GameObject testItem = Instantiate(testItemPrefab, player);
        GameObject testBoombard = Instantiate(testBoombardPrefab, player);
        GameObject testRotation = Instantiate(testRotationPrefab, player);
        testItem.name = "TestProjectile";

        Item testItemComponent = testItem.GetComponent<Item>();
        if (testItemComponent != null)
        {
            inventory.Additem(testItemComponent);
            Debug.Log("투사체 아이템 추가 완료");
        }
        else
        {
            Debug.Log("투사체 아이템에 item컴포넌트가 없습니다.");
        }
        
        Item testBoombardComponent = testBoombard.GetComponent<Item>();
        if (testBoombardComponent != null)
        {
            inventory.Additem(testBoombardComponent);
            Debug.Log("포격 아이템 추가 완료");
        }
        else
        {
            Debug.Log("포격아이템에 item 컴포넌트가 없음");
        }

        Item testRotationComponent = testRotation.GetComponent<Item>();
        if (testRotationComponent != null)
        {
            inventory.Additem(testRotationComponent);
            Debug.Log("회전아이템 추가 완료");
        }
        else
        {
            Debug.LogError("회전 아이템에 item컴포넌트가 없습니다.");
        }

    }

    private IEnumerator SkillRoutine(BaseSkill skill)
    {
        while (activeSkill.Contains(skill))
        {
            if (skill == null)
            {
                Debug.LogWarning("SkillRoutine : 스킬이 없습니다.");
                yield break;
            }

            StartCoroutine(DelaySkill(skill));

            float cooldown = skill.GetCooldown();
            if (cooldown <= 0)
            {
                Debug.LogError("SkillRoutine 스킬 쿨타임이 0초 이하입니다. 루틴을 종료합니다.");
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

    // 스킬을 추가하고 스킬이 추가 됐을 때 바로 스킬루틴 실행
    public void AddSkill(BaseSkill skill)
    {
        if (!activeSkill.Contains(skill))
        {
            activeSkill.Add(skill);

            if (!skill.gameObject.activeSelf)
            {
                skill.gameObject.SetActive(true);
            }


            // 만약 진행중인 코루틴이 있다면 중지하고 삭제
            if (skillCoroutines.ContainsKey(skill))
            {
                StopCoroutine(skillCoroutines[skill]);
                skillCoroutines.Remove(skill);
            }

            Coroutine skillRoutine = StartCoroutine(SkillRoutine(skill));

            skillCoroutines[skill] = skillRoutine;
            Debug.Log("스킬이 활성회됨");
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
            Debug.Log($"{item.itemName}의 스킬이 등록");
        }
        else
        {
            Debug.LogWarning($"{item.itemName}의 스킬이 이미 활성화가 되어있씁니다.");
        }
    }

    // 스킬이 제거되면 스킬루틴을 멈추고 리스트에서 제거
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

            Debug.Log("스킬이 제거되었습니다.");
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

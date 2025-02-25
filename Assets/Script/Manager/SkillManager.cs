using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // 활성화 된 스킬 목록(먹은 아이템)
    private List<BaseSkill> activeSkill = new List<BaseSkill>();
    private Inventory inventory;
    public Transform player; // 플레이어의 Transform

    //테스트용 코드
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
            inventory.Additem(testItemComponent);
            Debug.Log("테스트 아이템 추가 완료");
        }
        else
        {
            Debug.Log("테스트 아이템에 item컴포넌트가 없습니다.");
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
            Debug.Log("스킬이 활성회됨");
        }
    }

    public void ResgisterSkills(Item item)
    {
        if (item.skill == null) return;

        AddSkill(item.skill);
    }

}

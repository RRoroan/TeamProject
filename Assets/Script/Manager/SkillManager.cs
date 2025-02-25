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
                //인벤토리에 있는 아이템을 검사
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

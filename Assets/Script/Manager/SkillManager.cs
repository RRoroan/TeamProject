using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private List<BaseSkill> activeSkills = new List<BaseSkill>();
    private Dictionary<BaseSkill, Coroutine> skillCoroutines = new Dictionary<BaseSkill, Coroutine>();
    public Transform player; // �÷��̾��� Transform
    public List<SkillReward> availableSkills;



    private void Awake()
    {

    }

    private IEnumerator SkillRoutine(BaseSkill skill)
    {
        while (activeSkills.Contains(skill))
        {
            BaseSkill nowSkill = activeSkills.Find(s => s.SkillName == skill.SkillName);
            if (nowSkill == null)
            {
                Debug.LogWarning("SkillRoutine : ��ų�� �����ϴ�.");
                yield break;
            }

            StartCoroutine(DelaySkill(nowSkill));

            float cooldown = nowSkill.GetCooldown();
            if (cooldown <= 0)
            {
                Debug.LogError("SkillRoutine ��ų ��Ÿ���� 0�� �����Դϴ�. ��ƾ�� �����մϴ�.");
                yield break;
            }

            yield return new WaitForSeconds(nowSkill.GetCooldown());
            if (nowSkill != null) nowSkill.UseSkill();
        }
    }

    private IEnumerator DelaySkill(BaseSkill skill)
    {
        yield return new WaitForSeconds(3f);
        skill.UseSkill();
    }

    public void AddSkill(BaseSkill skill)
    {
        if (HasSkill(skill))
        {
            BaseSkill nowSkill = activeSkills.Find(s => s.SkillName == skill.SkillName);
            nowSkill.SkillLevelUp(); // �̹� �����ϴ� ��� ���� ����
            Debug.Log($"{nowSkill.SkillName} ��ų ���� ��! ���� ����: {nowSkill.skillLevel}");
            //RestartSkill(nowSkill);
            return;
        }

        BaseSkill newSkill = Instantiate(skill, player); // �� �ν��Ͻ� ����
        activeSkills.Add(newSkill);

        if (!newSkill.gameObject.activeSelf)
            newSkill.gameObject.SetActive(true);

        // ���� �ڷ�ƾ�� ������ ���� �� ���� ����
        if (skillCoroutines.ContainsKey(newSkill))
        {
            StopCoroutine(skillCoroutines[newSkill]);
            skillCoroutines.Remove(newSkill);
        }

        Coroutine skillRoutine = StartCoroutine(SkillRoutine(newSkill));
        skillCoroutines[newSkill] = skillRoutine;

        Debug.Log($"{newSkill.SkillName} ��ų�� Ȱ��ȭ��!");
    }


    //public void RestartSkill(BaseSkill skill)
    //{
    //    BaseSkill exisitingSkill = activeSkills.Find(sk => sk.SkillName == skill.SkillName);

    //    if (exisitingSkill == null) return;

    //    if (skillCoroutines.ContainsKey(exisitingSkill))
    //    {
    //        Coroutine routine = skillCoroutines[exisitingSkill];
    //        if (routine != null)
    //        {


    //            RemoveSkill(exisitingSkill);
    //        }
    //        skillCoroutines.Remove(exisitingSkill );
    //    }

    //    BaseSkill newSkill = Instantiate(skill, player); // �� �ν��Ͻ� ����
    //    activeSkills.Add(newSkill);

    //    Coroutine skillRoutine = StartCoroutine(SkillRoutine(newSkill));
    //    if (skillRoutine == null)
    //    {
    //        Debug.LogError($"{newSkill.SkillName} is not a skill");
    //    }
    //    skillCoroutines[newSkill] = skillRoutine;
    //}

    

    public void ApplySkillReward(SkillReward skillReward)
    {
        if (skillReward == null || skillReward.skillPrefab == null) return;

        // �ߺ� üũ �� availableSkills ����Ʈ�� �߰�
        if (!availableSkills.Exists(s => s.skillPrefab.SkillName == skillReward.skillPrefab.SkillName))
        {
            availableSkills.Add(skillReward);
            Debug.Log($"{skillReward.rewardName} ����: {skillReward.skillPrefab.SkillName} ��ų �߰���!");
        }
        AddSkill(skillReward.skillPrefab);
    }

    public void RemoveSkill(BaseSkill skill)
    {
        if (activeSkills.Contains(skill))
        {

            activeSkills.Remove(skill);

            if (skillCoroutines.ContainsKey(skill))
            {
                StopCoroutine(skillCoroutines[skill]);
                skillCoroutines.Remove(skill);
            }

            Destroy(skill.gameObject); // �ν��Ͻ� ����
            Debug.Log($"{skill.SkillName} ��ų�� ���ŵǾ����ϴ�.");
        }
    }

    public bool HasSkill(BaseSkill skill)
    {
        return activeSkills.Exists(s => s.SkillName == skill.SkillName);
    }
}

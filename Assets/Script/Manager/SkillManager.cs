using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private List<BaseSkill> activeSkills = new List<BaseSkill>();
    private Dictionary<BaseSkill, Coroutine> skillCoroutines = new Dictionary<BaseSkill, Coroutine>();
    public Transform player; // 플레이어의 Transform
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
                Debug.LogWarning("SkillRoutine : 스킬이 없습니다.");
                yield break;
            }

            StartCoroutine(DelaySkill(nowSkill));

            float cooldown = nowSkill.GetCooldown();
            if (cooldown <= 0)
            {
                Debug.LogError("SkillRoutine 스킬 쿨타임이 0초 이하입니다. 루틴을 종료합니다.");
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
            nowSkill.SkillLevelUp(); // 이미 존재하는 경우 레벨 증가
            Debug.Log($"{nowSkill.SkillName} 스킬 레벨 업! 현재 레벨: {nowSkill.skillLevel}");
            //RestartSkill(nowSkill);
            return;
        }

        BaseSkill newSkill = Instantiate(skill, player); // 새 인스턴스 생성
        activeSkills.Add(newSkill);

        if (!newSkill.gameObject.activeSelf)
            newSkill.gameObject.SetActive(true);

        // 기존 코루틴이 있으면 중지 후 새로 시작
        if (skillCoroutines.ContainsKey(newSkill))
        {
            StopCoroutine(skillCoroutines[newSkill]);
            skillCoroutines.Remove(newSkill);
        }

        Coroutine skillRoutine = StartCoroutine(SkillRoutine(newSkill));
        skillCoroutines[newSkill] = skillRoutine;

        Debug.Log($"{newSkill.SkillName} 스킬이 활성화됨!");
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

    //    BaseSkill newSkill = Instantiate(skill, player); // 새 인스턴스 생성
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

        // 중복 체크 후 availableSkills 리스트에 추가
        if (!availableSkills.Exists(s => s.skillPrefab.SkillName == skillReward.skillPrefab.SkillName))
        {
            availableSkills.Add(skillReward);
            Debug.Log($"{skillReward.rewardName} 적용: {skillReward.skillPrefab.SkillName} 스킬 추가됨!");
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

            Destroy(skill.gameObject); // 인스턴스 삭제
            Debug.Log($"{skill.SkillName} 스킬이 제거되었습니다.");
        }
    }

    public bool HasSkill(BaseSkill skill)
    {
        return activeSkills.Exists(s => s.SkillName == skill.SkillName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillReward", menuName = "Rewards/Skill")]
public class SkillReward : Reward
{
    public BaseSkill skillPrefab;

    public override void ApplyReward(SkillManager skillManager)
    {
        if (skillManager == null || skillPrefab == null) return;

        if (skillManager.HasSkill(skillPrefab))
        {
            Debug.Log($"{rewardName}: 이미 보유한 스킬입니다.");
            return;
        }

        BaseSkill newSkill = Object.Instantiate(skillPrefab, skillManager.transform);
        skillManager.AddSkill(newSkill);

        Debug.Log($"{rewardName} 적용s: {newSkill.SkillName} 스킬 추가!");
    }
}

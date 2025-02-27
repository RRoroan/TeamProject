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
            Debug.Log($"{rewardName}: �̹� ������ ��ų�Դϴ�.");
            return;
        }

        BaseSkill newSkill = Object.Instantiate(skillPrefab, skillManager.transform);
        skillManager.AddSkill(newSkill);

        Debug.Log($"{rewardName} ����s: {newSkill.SkillName} ��ų �߰�!");
    }
}

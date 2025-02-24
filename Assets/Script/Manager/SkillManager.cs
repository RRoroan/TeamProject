using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private List<Skill> activeSkill = new List<Skill>();

    public void AddSkill(Skill skillName)
    {
        if (!activeSkill.Contains(skillName))
        {
            activeSkill.Add(skillName);
            skillName.Activate();
        }
    }
}

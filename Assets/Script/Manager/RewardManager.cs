using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public List<Reward> availableRewards;
    public GameObject rewardPanel;
    public Button[] rewardButtons;

    private RangeWeaponHandler playerWeaponHandler;
    private SkillManager playerSkillManager;
    

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerWeaponHandler = player.GetComponentInChildren<RangeWeaponHandler>();
            playerSkillManager = player.GetComponentInChildren<SkillManager>();
        }
    }

    public void ShowRewards()
    {
        Time.timeScale = 0f;
        rewardPanel.SetActive(true);
        List<Reward> chosenRewards = GetRandomRewards(3);

        for (int i = 0; i < rewardButtons.Length; i++)
        {
            Reward reward = chosenRewards[i];
            Button button = rewardButtons[i];
            
            rewardButtons[i].GetComponentInChildren<Text>().text = reward.rewardName;
            rewardButtons[i].onClick.RemoveAllListeners();
            rewardButtons[i].onClick.AddListener(() => SelectReward(reward));
        }
        Time.timeScale = 1f;
    }

    private List<Reward> GetRandomRewards(int count)
    {
        List<Reward> shuffled = new List<Reward>(availableRewards);
        shuffled.Sort((a, b) => Random.Range(-1, 2));
        return shuffled.GetRange(0, count);
    }

    public void SelectReward(Reward reward)
    {

        if (reward is StatReward statReward)
        {
            statReward.ApplyReward(playerWeaponHandler);
        }
        else if (reward is SkillReward skillReward)
        {
            playerSkillManager.ApplySkillReward(skillReward);
        }
        rewardPanel.SetActive(false);
    }
}

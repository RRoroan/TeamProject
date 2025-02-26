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

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerWeaponHandler = player.GetComponentInChildren<RangeWeaponHandler>();
        }
    }

    public void ShowRewards()
    {
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
    }

    private List<Reward> GetRandomRewards(int count)
    {
        List<Reward> shuffled = new List<Reward>(availableRewards);
        shuffled.Sort((a, b) => Random.Range(-1, 2));
        return shuffled.GetRange(0, count);
    }

    public void SelectReward(Reward reward)
    {
        Debug.Log($"선택된 보상: {reward.rewardName}");
        reward.ApplyReward(playerWeaponHandler);
        rewardPanel.SetActive(false);
    }
}

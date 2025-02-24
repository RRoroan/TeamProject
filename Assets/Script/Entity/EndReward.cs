using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndReward : MonoBehaviour
{
    public enum RewardStat
    {
        Health,
        Speed,
        Range,
        AttackSpeed,
        Armor,
        ProjectileCount
    }
    public class Reward
    {
        public RewardStat Stat { get; private set; }
        public float Amount { get; set; }

        public Reward(RewardStat stat, float amount)
        {
            Stat = stat;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{Stat}을 {Amount}만큼 얻습니다.";
        }

        private List<Reward> rewards = new List<Reward>();

        public void AddReward(Reward reward)
        {
            rewards.Add(new Reward(Stat, Amount));
        }
    }
    public void ApplyReward(EndReward reward)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{

    [SerializeField] private float healthChangeDelay = .5f;

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; } //현재 체력 수치
    public float CurrentSpeed { get; private set; } //현재 속도 수치
    public float CurrentRange { get; private set; } //현재 사거리 수치
    public float CurrentAttackSpeed { get; private set; } //현재 공격속도 수치
    public float CurrentArmor { get; private set; } //현재 방어력 수치
    public int CurrentProjectileCount { get; private set; } //현재 탄수 수치
    public float MaxHealth => statHandler.Health;

    private void Awake()//이 파일이 실했됐다면 컴포넌트의 데이터를 가져오겠다.
    {
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
        baseController = GetComponent<BaseController>();
    }

    private void Update()//이후 업데이트되며 피격상태의 무적시간을 체크한다.
    {
        if (timeSinceLastChange < healthChangeDelay)//체력 변경의 딜레이가 더 많을땐
        {
            timeSinceLastChange += Time.deltaTime; //
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }
    public bool ChangeHealth(float change) //체력 변경 피해 받을때 불러올 메서드
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (change < 0) // 애니메이션쪽에 정의된 메서드를 불러와 작동시키기
        {
            animationHandler.Damage();

        }

        if (CurrentHealth <= 0f) // 너 죽어있잖아. 현재 체력이 0보다 낮아짐
        {
            Death();
        }

        return true;
    }

    private void Death() // 위에서 보이듯 죽으면 베이스 컨트롤의 데스를 불러옴
    {
        //baseController.Death();
    }

    public void ChangeRange(float change)
    {
        if (change > 0)
            CurrentRange += change;
        else
            CurrentRange -= 0f;
    }

    public void ChangeSpeed(float change)
    {
        if (change > 0)
            CurrentSpeed += change;
        else
            CurrentSpeed -= 0f;
    }

    public void ChangeArmor(float change)
    {
        if (change > 0)
            CurrentArmor += change;
        else
            CurrentArmor -= 0f;
    }

    public void ChangeProjectileCount(int change)
    {
        if (change > 0)
            CurrentProjectileCount += change;
        else
            CurrentProjectileCount -= 0;
    }
}

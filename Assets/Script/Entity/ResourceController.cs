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

    public float CurrentHealth { get; private set; } //���� ü�� ��ġ
    public float CurrentSpeed { get; private set; } //���� �ӵ� ��ġ
    public float CurrentRange { get; private set; } //���� ��Ÿ� ��ġ
    public float CurrentAttackSpeed { get; private set; } //���� ���ݼӵ� ��ġ
    public float CurrentArmor { get; private set; } //���� ���� ��ġ
    public int CurrentProjectileCount { get; private set; } //���� ź�� ��ġ
    public float MaxHealth => statHandler.Health;

    private void Awake()//�� ������ ���ߵƴٸ� ������Ʈ�� �����͸� �������ڴ�.
    {
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
        baseController = GetComponent<BaseController>();
    }

    private void Update()//���� ������Ʈ�Ǹ� �ǰݻ����� �����ð��� üũ�Ѵ�.
    {
        if (timeSinceLastChange < healthChangeDelay)//ü�� ������ �����̰� �� ������
        {
            timeSinceLastChange += Time.deltaTime; //
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }
    public bool ChangeHealth(float change) //ü�� ���� ���� ������ �ҷ��� �޼���
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (change < 0) // �ִϸ��̼��ʿ� ���ǵ� �޼��带 �ҷ��� �۵���Ű��
        {
            animationHandler.Damage();

        }

        if (CurrentHealth <= 0f) // �� �׾����ݾ�. ���� ü���� 0���� ������
        {
            Death();
        }

        return true;
    }

    private void Death() // ������ ���̵� ������ ���̽� ��Ʈ���� ������ �ҷ���
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

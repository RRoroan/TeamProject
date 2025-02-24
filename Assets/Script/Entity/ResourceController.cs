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

        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    private void Death() // ������ ���̵� ������ ���̽� ��Ʈ���� ������ �ҷ���
    {
        //baseController.Death();
    }
}

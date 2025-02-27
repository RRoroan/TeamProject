using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSkill : BaseSkill
{
    // 사용할 프리팹
    [SerializeField] private GameObject rotatingPrefab;

    // 스킬이 돌아다닐 범위
    [SerializeField] private float range = 2f;

    // 스킬이 돌아다닐 속도
    [SerializeField] private float rotationSpeed = 100f;

    // 스킬이 지속될 시간
    [SerializeField] private float lifetime = 3f;

    // 투사채 갯수
    [SerializeField] private int projectileCount = 3;

    // 적에게 피해를 입힐 간격
    [SerializeField] private float hitInterval = 0.5f;
    [SerializeField] private LayerMask enemyLayer;

    // 활성화 될 무기를 넣어줄 리스트(projectileCount 만큼 넣어줄꺼임)
    private List<GameObject> activeWeapon = new List<GameObject>();

    // 중복 공격을 방지
    private HashSet<Collider2D> hitEnemies = new HashSet<Collider2D>();


    protected override void Start()
    {
        base.Start();
        projectileCount += statHandler.GetProjectileCount();
    }

    public override void UseSkill()
    {
        if (!gameObject.activeSelf)
        {
            Debug.LogError($"UseSkill: {SkillName} 오브젝트가 비활성화 상태!");
            return;
        }

        StartCoroutine(SpawnWeapon());

    }

    private IEnumerator SpawnWeapon()
    {


        ClearBeforeWeapon();

        // 간격이 일정하게 투사채를 배치
        float weaponInterval = 360f / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * weaponInterval;
            Vector2 spawnPosition = (Vector2)playerController.transform.position + GetPositionAngle(angle);


            GameObject weapon = Instantiate(rotatingPrefab, spawnPosition, Quaternion.identity);
            weapon.transform.parent = playerController.transform;

            RotatingSkillProjectile controller = weapon.GetComponent<RotatingSkillProjectile>();
            if (controller != null)
            {
                controller.Init(playerController.transform, range, angle, rotationSpeed, hitInterval, enemyLayer);
            }

            activeWeapon.Add(weapon);

        }

        if (lifetime < cooldown)
        {
            yield return new WaitForSeconds(lifetime);
            ClearBeforeWeapon();
        }



    }

    // 플레이어 주변 일정 반경에 생성됨
    private Vector2 GetPositionAngle(float angle)
    {
        float radius = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radius) * range, Mathf.Sin(radius) * range);
    }

    // 기존에 있는 무기 제거
    private void ClearBeforeWeapon()
    {
        foreach (GameObject weapon in activeWeapon)
        {
            Destroy (weapon);
        }
        activeWeapon.Clear();
    }

    public override void SkillLevelUp()
    {
        base.SkillLevelUp();
        
        if (skillLevel % 2 == 1)
        {
            if (lifetime <= cooldown)
            {
                lifetime += 0.5f;

            }
            else
            {
                damage++;
            }
        }
        if (skillLevel % 3 == 0)
        {
            projectileCount++;
        }
        if (skillLevel % 5 == 0)
        {
            rotationSpeed += 10;
            if (hitInterval <= 0.1f)
            {
                hitInterval = Mathf.Max(hitInterval - 0.05f, 0.1f);

            }
            damage += 3;
        }

        damage++;

    }

}

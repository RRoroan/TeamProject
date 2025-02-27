using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoombardEvents : MonoBehaviour
{
    private BoombardmentSkillExplosion explosionScripts;

    // Start is called before the first frame update
    void Start()
    {
        explosionScripts = GetComponentInParent<BoombardmentSkillExplosion>();
    }

    public void TriggerExplosion()
    {
        explosionScripts?.TriggerExplosion();
    }

    public void Attack()
    {
        explosionScripts?.Attack();
    }

    public void ExplosionDestroy()
    {
        explosionScripts?.ExplosionDestroy();
    }

}

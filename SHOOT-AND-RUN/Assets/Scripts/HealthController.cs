using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    float health = 100.0F;

    [SerializeField]
    public GameObject deadVfx;

    public void TakeDamage(float damage)
    {
        health -= Mathf.Abs(damage);
        Debug.Log("Health = " + health);
        if (health <= 0)
        {
            GameObject deathExplision = Instantiate(deadVfx, gameObject.transform.position, Quaternion.identity);
            
            Destroy(gameObject,0.5f);
        }
    }
}

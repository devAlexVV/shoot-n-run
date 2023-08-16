using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class bulletController : MonoBehaviour
{
    [SerializeField]
    LayerMask whatIsEnemy;
    [SerializeField]
    float damage = 50.0f;

    [SerializeField]
    GameObject vfxDamage;


    private void OnCollisionEnter(Collision other)
    {
        GameObject vfx = Instantiate(vfxDamage, other.contacts[0].point, Quaternion.identity);
        if (other.gameObject.layer == (other.gameObject.layer | (1 << whatIsEnemy)))
        {
            HealthController controller = other.gameObject.GetComponent<HealthController>();
            if (controller != null) {
                controller.TakeDamage(damage);
            }
            
        }
        Destroy(gameObject, 0.2f);
        Destroy(vfx,0.2f);

        /*
         * 
       if (collision.gameObject.CompareTag("enemy"))
        {
            GameObject vfx = Instantiate(vfxDamage, collision.contacts[0].point, Quaternion.identity);
            HealthController enemy = collision.gameObject.GetComponent<HealthController>();
            enemy.TakeDamage(damage);
            Destroy(gameObject, 0.2f);
        }
        else if (collision.gameObject.CompareTag("environment")) {
            GameObject vfx = Instantiate(vfxDamage, collision.contacts[0].point, Quaternion.identity);
        }
         
         */
    }


}

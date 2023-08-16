using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Transform attackPoint;

    [SerializeField]
    float attackRadius = 2.0F;

    [SerializeField]
    LayerMask whatIsTarget;

    [SerializeField]
    float damage = 25.0F;

    [SerializeField]
    int attackRate = 2;

    [SerializeField]
    [Range(1, 2)]
    int characterType = 1;

    float _nextAttackTime = 0.0F;


    [SerializeField]
    GameObject attackVfx;

    List<Collider> _attackColliders;

    void Awake()
    {
        _attackColliders = new List<Collider>();
    }

    void Update()
    {
        switch (characterType)
        {
            case 1:
                if (Input.GetButtonDown("Fire1") && Time.time > _nextAttackTime)
                {
                    _nextAttackTime = Time.time + 1.0F / attackRate;
                    animator.SetTrigger("attack");

                    _attackColliders.Clear();
                }
                break;
            case 2:
                PatrolController grandparentObject = transform.parent.parent.gameObject.GetComponent<PatrolController>();

                if (grandparentObject._isAttacking)
                {
                    animator.SetTrigger("attack");

                }
                
                break;
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Sword Attack")) {
            OnAttack();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    void OnAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(attackPoint.position, attackRadius, whatIsTarget);
        
        foreach (Collider collider in colliders)
        {
            if (_attackColliders.Contains(collider)) {
                continue;
            }
            HealthController controller = collider.GetComponent<HealthController>();
            if (controller != null)
            {
                GameObject vfx = Instantiate(attackVfx, attackPoint.position, Quaternion.identity);
                controller.TakeDamage(damage);
                _attackColliders.Add(collider);
            }
            else{
                GameObject vfx = Instantiate(attackVfx, attackPoint.position, Quaternion.identity);
            }
        }
        animator.ResetTrigger("attack");
    }
}

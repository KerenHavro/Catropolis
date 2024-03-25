using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IDamagable
{
    void TakeDamage(int damageAmount);
    void Die();
    void ApplyKnockback(Vector2 damageSourcePosition);
 
}

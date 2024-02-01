using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IDamagable
{
    void TakeDamage(int damageAmount);
    void Die();
}

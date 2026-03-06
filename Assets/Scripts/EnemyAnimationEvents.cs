using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [Header("Weapon")]
    public DamageDealer enemyWeapon;

    public void EnableWeaponHitbox()
    {
        if (enemyWeapon != null)
        {
            enemyWeapon.EnableDamage();
        }
    }

    public void DisableWeaponHitbox()
    {
        if (enemyWeapon != null)
        {
            enemyWeapon.DisableDamage();
        }
    }
}
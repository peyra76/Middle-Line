using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("Weapon Settings")]
    public DamageDealer weaponScript;

    [Header("Combo Settings")]
    public List<string> comboAttacks;
    public float comboResetTime = 1.0f;
    public float maxAttackTime = 1.5f; 

    public bool isAttacking = false;
    private int comboStep = 0;

    private Coroutine resetComboCoroutine;
    private Coroutine failsafeCoroutine; 

    private Animator animator;

    public StaminaSystem staminaSystem;
    public float attackStaminaCost = 15f;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (staminaSystem == null)
        {
            staminaSystem = GetComponent<StaminaSystem>();
        }

        if (staminaSystem == null)
        {
            Debug.LogError($"<color=red>Внимание!</color> На объекте {gameObject.name} отсутствует скрипт StaminaSystem!");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            if (staminaSystem != null)
            {
                if (staminaSystem.UseStamina(attackStaminaCost))
                {
                    PerformAttack();
                }
            }

            else
            {
                Debug.LogError("Забыл привязать StaminaSystem к CombatController!");
                PerformAttack();
            }
        }
    }

    void PerformAttack()
    {
        isAttacking = true;

        if (weaponScript != null) weaponScript.EnableDamage();

        if (comboStep < comboAttacks.Count)
        {
            animator.Play(comboAttacks[comboStep]);
        }

        comboStep++;
        if (comboStep >= comboAttacks.Count) comboStep = 0;

        if (resetComboCoroutine != null) StopCoroutine(resetComboCoroutine);
        resetComboCoroutine = StartCoroutine(ResetComboTimer());

        if (failsafeCoroutine != null) StopCoroutine(failsafeCoroutine);
        failsafeCoroutine = StartCoroutine(AttackFailsafe());
    }

    IEnumerator ResetComboTimer()
    {
        yield return new WaitForSeconds(comboResetTime);

        if (!isAttacking)
        {
            comboStep = 0;
            Debug.Log("Комбо сброшено по таймеру ожидания.");
        }
    }

    IEnumerator AttackFailsafe()
    {
        yield return new WaitForSeconds(maxAttackTime);
        if (isAttacking)
        {
            Debug.LogWarning("Сработал предохранитель! Анимация не прислала Event. Принудительно возвращаем управление.");
            AE_EndAttack(); 
        }
    }

    public void AE_EnableDamage()
    {
        if (weaponScript != null) weaponScript.EnableDamage();
    }

    public void AE_DisableDamage()
    {
        if (weaponScript != null) weaponScript.DisableDamage();
    }

    public void AE_EndAttack()
    {
        isAttacking = false;
        if (weaponScript != null) weaponScript.DisableDamage();
    }
}
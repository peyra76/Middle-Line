using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class SoulsPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3.0f;
    public float runSpeed = 7.0f;
    public float rotationSpeed = 10f; 
    public float gravity = -20.0f;    

    [Header("Roll Settings")]
    public float rollSpeed = 12.0f;
    public float rollDuration = 0.5f; 
    public float invincibilityDuration = 0.5f; 

    [Header("References")]
    public Transform cameraTransform;

    [Header("Stamina Costs")]
    public float rollCost = 25f;
    public float sprintCostPerSecond = 10f;

    private StaminaSystem staminaSystem; 

    private bool isRolling = false;
    private Vector3 rollDir;
    private Vector3 velocity; 

    private CharacterController controller;
    private Animator animator;
    private HealthSystem healthSystem;
    private CombatController combatController;

    private int animIDSpeed;
    private int animIDRoll;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();
        combatController = GetComponent<CombatController>();
        staminaSystem = GetComponent<StaminaSystem>();

        animIDSpeed = Animator.StringToHash("Speed");
        animIDRoll = Animator.StringToHash("Roll");

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        ApplyGravity();

        if (combatController != null && combatController.isAttacking)
        {
            animator.SetFloat(animIDSpeed, 0f);
            return;
        }


        if (isRolling)
        {
            controller.Move(rollDir * rollSpeed * Time.deltaTime);
            return; 
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(h, 0f, v).normalized;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            StartCoroutine(PerformRoll(inputDir));
            return; 
        }

        if (inputDir.magnitude >= 0.1f)
        {
            MoveAndRotate(inputDir);
        }
        else
        {
            animator.SetFloat(animIDSpeed, 0f, 0.1f, Time.deltaTime);
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void MoveAndRotate(Vector3 inputDir)
    {
        float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (isSprinting && staminaSystem != null)
        {
            if (!staminaSystem.UseStamina(sprintCostPerSecond * Time.deltaTime))
            {
                isSprinting = false;
            }
        }

        float currentSpeed = isSprinting ? runSpeed : walkSpeed;

        Vector3 moveDir = targetRotation * Vector3.forward;
        controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);

        float animValue = isSprinting ? 1.0f : 0.5f;
        animator.SetFloat(animIDSpeed, animValue, 0.1f, Time.deltaTime);
    }

    IEnumerator PerformRoll(Vector3 inputDir)
    {
        if (staminaSystem != null && !staminaSystem.UseStamina(rollCost))
            yield break;

        isRolling = true;

        if (healthSystem) healthSystem.isInvulnerable = true;

        animator.SetTrigger(animIDRoll);

        if (inputDir.magnitude < 0.1f)
            rollDir = transform.forward;
        else
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            rollDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.LookRotation(rollDir);
        }

        yield return new WaitForSeconds(invincibilityDuration);

        if (healthSystem) healthSystem.isInvulnerable = false;

        yield return new WaitForSeconds(rollDuration - invincibilityDuration);

        isRolling = false;
    }

    public void MultiplySpeed(float multiplier)
    {
        walkSpeed *= multiplier;
        runSpeed *= multiplier;
    }
}
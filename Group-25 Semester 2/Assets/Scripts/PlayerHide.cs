using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHide : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private Light normalLight;
    [SerializeField] private Light focusLight;

    [Header("Player")]
    [SerializeField] private PlayerSanity playerSanity;
    [SerializeField] private FPController playerController;

    [Header("Input")]
    [SerializeField] private InputActionReference hideAction;

    [Header("Hiding")]
    [SerializeField] private float hideDuration = 5f;

    private bool isHiding = false;
    private float hideTimer;

    private void OnEnable()
    {
        hideAction.action.performed += ToggleHide;
        hideAction.action.Enable();
    }

    private void OnDisable()
    {
        hideAction.action.performed -= ToggleHide;
        hideAction.action.Disable();
    }

    private void Update()
    {
        if (!isHiding)
            return;

        hideTimer -= Time.deltaTime;

        if (hideTimer <= 0f)
        {
            StopHiding();
        }
    }

    private void ToggleHide(InputAction.CallbackContext context)
    {
        if (isHiding)
            return;

        StartHiding();
    }

    private void StartHiding()
    {
        isHiding = true;
        hideTimer = hideDuration;

        normalLight.enabled = false;
        focusLight.enabled = false;

        playerSanity.SetHiding(true);

        playerController.enabled = false;

        AttackEnemyAI[] enemies =
            FindObjectsByType<AttackEnemyAI>(FindObjectsSortMode.None);

        foreach (AttackEnemyAI enemy in enemies)
        {
            enemy.StartPatrol();
        }

        Debug.Log("PLAYER IS HIDING");
    }

    private void StopHiding()
    {
        isHiding = false;

        normalLight.enabled = true;
        focusLight.enabled = false;

        playerSanity.SetHiding(false);

        playerController.enabled = true;

        AttackEnemyAI[] enemies =
            FindObjectsByType<AttackEnemyAI>(FindObjectsSortMode.None);

        foreach (AttackEnemyAI enemy in enemies)
        {
            enemy.StopPatrol();
        }

        Debug.Log("PLAYER IS REVEALED");
    }
}
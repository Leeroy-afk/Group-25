using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private Light normalLight;
    [SerializeField] private Light focusedLight;

    [Header("Player")]
    [SerializeField] private PlayerSanity playerSanity;

    [Header("Input")]
    [SerializeField] private InputActionReference flashlightAction;

    [Header("Enemy Detection")]
    [SerializeField] private float detectionDistance = 15f;
    [SerializeField] private float escapeDistance = 8f;
    [SerializeField] private LayerMask enemyLayer;

    private bool focusedLightOn = false;

    private void OnEnable()
    {
        flashlightAction.action.performed += ToggleFocusedLight;
        flashlightAction.action.Enable();
    }

    private void OnDisable()
    {
        flashlightAction.action.performed -= ToggleFocusedLight;
        flashlightAction.action.Disable();
    }

    private void Start()
    {
        normalLight.enabled = true;

        focusedLightOn = false;
        focusedLight.enabled = false;

        playerSanity.SetFocusedlight(false);
    }

    private void Update()
    {
        if (!focusedLightOn)
            return;

        CheckForEnemy();
    }

    private void ToggleFocusedLight(InputAction.CallbackContext context)
    {
        focusedLightOn = !focusedLightOn;

        if (focusedLightOn)
        {
            normalLight.enabled = false;  // Turn normal light off

            focusedLight.enabled = true; // Turn focus light on

            playerSanity.SetFocusedlight(true); // Tell sanity to drain
        }
        else
        {
            normalLight.enabled = true; // Turn normal light back on

            focusedLight.enabled = false; // Turn focus light off

            playerSanity.SetFocusedlight(false); // Stop sanity drain
        }
    }
    
    private void CheckForEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(
            focusedLight.transform.position,
            detectionDistance,
            enemyLayer
        );

        foreach (Collider hit in hits)
        {
            AttackEnemyAI enemy =
                hit.GetComponentInParent<AttackEnemyAI>();

            if (enemy == null)
                continue;

            Vector3 directionToEnemy =
                (enemy.transform.position - focusedLight.transform.position).normalized;

            float angle = Vector3.Angle(
                focusedLight.transform.forward,
                directionToEnemy
            );

            Debug.Log(
                "Enemy found! Angle: " + angle +
                " | Allowed angle: " + (focusedLight.spotAngle / 2f)
            );

            if (angle <= focusedLight.spotAngle / 2f)
            {
                Debug.Log("ENEMY IS INSIDE FOCUS LIGHT!");

                enemy.EscapeLight(
                    focusedLight.transform.position,
                    escapeDistance
                );
            }
        }
    }
}
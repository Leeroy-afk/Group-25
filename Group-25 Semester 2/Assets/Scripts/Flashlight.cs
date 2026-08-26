using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light flashlight;
    [SerializeField] private PlayerSanity playerSanity;
    [SerializeField] private InputActionReference flashlightAction;

    [Header("Enemy Detection")]
    [SerializeField] private float detectionDistance = 15f;
    [SerializeField] private float escapeDistance = 8f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Battery")]
    [SerializeField] private float maxBattery = 100f;
    [SerializeField] private float batteryDrainRate = 5f;

    [Header("BatteryUI")]
    [SerializeField] private Slider batterySlider;

    private float currentBattery;

    private bool isOn = false;

    private void OnEnable()
    {
        flashlightAction.action.performed += ToggleFlashlight;
        flashlightAction.action.Enable();
    }

    private void OnDisable()
    {
        flashlightAction.action.performed -= ToggleFlashlight;
        flashlightAction.action.Disable();
    }

    private void Start()
    {
        flashlight.enabled = false;
        playerSanity.SetFlashlight(false);

        currentBattery = maxBattery;

        if (batterySlider != null)
        {
            batterySlider.maxValue = maxBattery;
            batterySlider.value = currentBattery;
        }
    }

    private void Update()
    {
        if (!isOn)
            return;

        DrainBattery();
        CheckForEnemy();
    }

    private void ToggleFlashlight(InputAction.CallbackContext context)
    {
        if (isOn)
        {
            TurnOffFlashlight();
        }
        else
        {
            if (currentBattery > 0f)
            {
                TurnOnFlashlight();
            }
        }
    }

    private void DrainBattery()
    {
        currentBattery -= batteryDrainRate * Time.deltaTime;

        currentBattery = Mathf.Clamp(currentBattery, 0f, maxBattery);

        if (batterySlider != null)
        {
            batterySlider.value = currentBattery;
        }

        if (currentBattery <= 0f)
        {
            TurnOffFlashlight();
        }
    }
    private void CheckForEnemy()
    {
        Ray ray = new Ray(
            flashlight.transform.position,
            flashlight.transform.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            detectionDistance,
            enemyLayer))
        {
            AttackEnemyAI enemy =
                hit.collider.GetComponentInParent<AttackEnemyAI>();

            if (enemy != null)
            {
                enemy.EscapeLight(
                    flashlight.transform.position,
                    escapeDistance
                );
            }
        }
    }
    private void TurnOnFlashlight()
    {
        isOn = true;

        flashlight.enabled = true;
        playerSanity.SetFlashlight(true);
    }

    private void TurnOffFlashlight()
    {
        isOn = false;

        flashlight.enabled = false;
        playerSanity.SetFlashlight(false);
    }
    public void ResetFlashlight()
    {
        currentBattery = maxBattery;

        TurnOffFlashlight();

        if (batterySlider != null)
        {
            batterySlider.value = currentBattery;
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SpeedManager : MonoBehaviour
{
    public DynamicMoveProvider moveProvider;
    public InputActionReference runAction;

    public TMP_Dropdown speedDropDown;

    private float runMultiplier = 2f;

    private float baseSpeed;
    private bool isRunning = false;

    void Start()
    {
        baseSpeed = moveProvider.moveSpeed;

        runAction.action.performed += ToggleRun;

        speedDropDown.onValueChanged.AddListener(SetRunMultiplier);
    }

    private void OnDestroy()
    {
        runAction.action.performed -= ToggleRun;

        speedDropDown.onValueChanged.RemoveListener(SetRunMultiplier);
    }

    private void ToggleRun(InputAction.CallbackContext ctx)
    {
        isRunning = !isRunning;
        moveProvider.moveSpeed = isRunning ? baseSpeed * runMultiplier : baseSpeed;
    }

    public void SetRunMultiplier(int index)
    {
        Debug.Log($"Dropdown index: {index}");
        switch (index)
        {
            case 0:
                runMultiplier = 2f;
                break;
            case 1:
                runMultiplier = 4f;
                break;
            case 2:
                runMultiplier = 8f;
                break;
            case 3:
                runMultiplier = 10f;
                break;
        }

        Debug.Log("Run multiplier set to: " + runMultiplier);
    }
}

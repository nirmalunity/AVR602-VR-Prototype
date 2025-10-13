using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CrouchManager : MonoBehaviour
{
    public Transform cameraOffset;
    public InputActionReference crouchAction;

    public TMP_Dropdown CrouchHeight;

    private float crouchHeightOffset = 0.5f;
    private float crouchSpeed = 3f;

    private float originalY;
    private float targetY;
    private bool isCrouching = false;

    void Start()
    {
        originalY = cameraOffset.localPosition.y;
        targetY = originalY;

        crouchAction.action.Enable();
        crouchAction.action.performed += ToggleCrouch;

        CrouchHeight.onValueChanged.AddListener(SetCrouchHeight);
    }

    void OnDestroy()
    {
        crouchAction.action.performed -= ToggleCrouch;

        CrouchHeight.onValueChanged.RemoveListener(SetCrouchHeight);
    }

    private void ToggleCrouch(InputAction.CallbackContext ctx)
    {
        isCrouching = !isCrouching;
        targetY = isCrouching ? originalY - crouchHeightOffset : originalY;
    }

    void Update()
    {
        Vector3 pos = cameraOffset.localPosition;
        pos.y = Mathf.Lerp(pos.y, targetY, Time.deltaTime * crouchSpeed);
        cameraOffset.localPosition = pos;
    }

    public void SetCrouchHeight(int index)
    {
        switch (index)
        {
            case 0:
                crouchHeightOffset = 0.5f;
                break;
            case 1:
                crouchHeightOffset = 0.6f;
                break;
            case 2:
                crouchHeightOffset = 0.7f;
                break;
            case 3:
                crouchHeightOffset = 0.8f;
                break;
        }
    }
}

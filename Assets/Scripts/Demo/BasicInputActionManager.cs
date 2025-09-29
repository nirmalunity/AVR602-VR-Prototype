using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class BasicInputActionManager :  MonoBehaviour
{
    public InputActionAsset InputActionAsset;
    public InputActionReference InputActionReference;
    public UnityEvent actionEvent;

    private void Awake()
    {
        InputActionReference.action.performed +=  OnButtonPressed;

    }

private void OnDestroy()
{
    InputActionReference.action.performed -= OnButtonPressed;
}

private void OnButtonPressed(InputAction.CallbackContext context)
{
    actionEvent.Invoke();
}

}

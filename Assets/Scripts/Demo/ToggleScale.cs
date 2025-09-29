using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleScale : MonoBehaviour
{
 /*    public InputActionAsset InputActionAsset;
    public InputActionReference scaleUp;
    public InputActionReference scaleDown; */

    private Vector3 initialscale;
    private bool isScalingUp;
    private bool isScalingDown;

    void Start()
    {
        initialscale = transform.localScale;
 /*        InputActionAsset.Enable();
        scaleUp.action.performed +=ctx => isScalingUp = true;
        scaleUp.action.performed += ctx => isScalingUp = false;
        scaleUp.action.performed += ctx => isScalingDown = true;
        scaleUp.action.performed += ctx => isScalingDown = false; */
    }

    private void OnDestroy()
    {
     /*    InputActionAsset.Disable(); */
    }

    void Update()
    {
        if(isScalingUp)
        {
            transform.localScale += new Vector3(.01f,.01f,.01f);
        }
        else if(isScalingDown)
        {
            transform.localScale -= new Vector3(.01f,.01f,.01f);
        }
    }

    public void ScaleUp()
    {
        isScalingDown = false;
        isScalingUp = true;
    }
       public void ScaleDown()
    {
        isScalingDown = true;
        isScalingUp = false;
    }

    public void ScaleNull()
    {
           isScalingDown = false;;
        isScalingUp = false; 
    }
    
}

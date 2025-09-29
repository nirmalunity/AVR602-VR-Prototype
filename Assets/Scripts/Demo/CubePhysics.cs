using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverRotate : MonoBehaviour
{
    public float rotationSpeed = 90f; // degrees per second
    private bool isHovered = false;
    private bool rotate = false;

    void Update()
    {
        if (isHovered || rotate)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    // Called when hover starts
    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        isHovered = true;
    }

    // Called when hover ends
    public void OnHoverExit(HoverExitEventArgs args)
    {
        isHovered = false;
    }

    public void ToggleRotate(){

        if(rotate){
            rotate = false;
        }else{
            rotate = true;
        }
    }


    public void ToggleVisibility()
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);

    }
}

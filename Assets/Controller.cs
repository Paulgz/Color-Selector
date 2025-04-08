using UnityEngine;

public class Controller : MonoBehaviour
{
    public Camera myCamera;
    public ColorSelector colorSelector;
    public GameObject   activateButton;

    private Color startColor;

    public void buttonSelectColor()
    {
        startColor = myCamera.backgroundColor;
        colorSelector.gameObject.SetActive(true);
        colorSelector.setColor(startColor);
        activateButton.SetActive(false);
    }
    private void Update()
    {
        if(colorSelector.isActiveAndEnabled) {
            myCamera.backgroundColor = colorSelector.getColor();
            switch(colorSelector.getState()) {
            case ColorSelector.State.cancel:
                myCamera.backgroundColor = startColor;
                activateButton.SetActive(true);
                colorSelector.gameObject.SetActive(false);
                break;
            case ColorSelector.State.okay:
                activateButton.SetActive(true);
                colorSelector.gameObject.SetActive(false);
                break;
            default:
                break;
            }
        }
    }
}

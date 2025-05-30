using ColorSlider;
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
        colorSelector.setTitle("Select Background Color");
        colorSelector.gameObject.SetActive(true);
        colorSelector.setColor(startColor);
        activateButton.SetActive(false);
    }
    public void colorOkay()
    {
        activateButton.SetActive(true);
        colorSelector.gameObject.SetActive(false);
    }
    public void colorCancel()
    {
        myCamera.backgroundColor = startColor;
        activateButton.SetActive(true);
        colorSelector.gameObject.SetActive(false);
    }
    public void colorChange(Color newColor)
    {
        myCamera.backgroundColor = newColor;
    }
}

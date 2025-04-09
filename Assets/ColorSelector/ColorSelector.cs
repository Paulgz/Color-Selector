using SmallTools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour {
    public UnityEvent okayEvent=new();
    public UnityEvent cancelEvent=new();
    public UnityEvent<Color> colorChangeEvent=new();
    public ColorSliders sliders;
    public ColorWheelContainer wheel;
    public Text     title;
    public Image    preview;

    private Color current;
    private Color lastSent;

    public void init()
    {
        sliders.init();
    }
    private void OnEnable()
    {
        init();
    }
    private void Update()
    {
        if( sliders.inUse()) {
            current = sliders.getColor();
            wheel.setColor(current);
        } else {
            current = wheel.getColor();
            sliders.setColor(current);
        }
        preview.color = current;
        if(current != lastSent) {
            lastSent = current;
            colorChangeEvent.Invoke(current);
        }
    }
    public void buttonOkay()
    {
        okayEvent.Invoke();
    }
    public void buttonCancel()
    {
        cancelEvent.Invoke();
    }
    public Color getColor()
    {
        return current;
    }
    public void setColor(Color _color)
    {
        current = _color;
        lastSent = _color;
        sliders.setColor(_color);
        wheel.setColor(_color);
    }
    public void setTitle(string _key)
    {
        title.text = _key;
    }
}

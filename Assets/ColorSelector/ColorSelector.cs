using SmallTools;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour {
    public ColorSliders sliders;
    public ColorWheelContainer wheel;
    public Text      colorName;
    public Image    preview;

    private Color current;
    private State state;

    public void init()
    {
        sliders.init();
        state = State.busy;
    }
    public State getState()
    {
        return state;
    }
    private void OnEnable()
    {
        init();
    }
    private void Update()
    {
        if(state!=State.busy) {
            return;
        }
        if( sliders.inUse()) {
            current = sliders.getColor();
            wheel.setColor(current);
        } else {
            current = wheel.getColor();
            sliders.setColor(current);
        }
        preview.color = current;
    }
    public void buttonOkay()
    {
        state = State.okay;
    }
    public void buttonCancel()
    {
        state = State.cancel;
    }
    public Color getColor()
    {
        return current;
    }
    public void setColor(Color _color)
    {
        current = _color;
        sliders.setColor(_color);
        wheel.setColor(_color);
    }
    public void setName(string _key)
    {
        colorName.text = _key;
    }
    //*******************************************************
    public enum State
    {
        busy,
        okay,
        cancel,
    }
}

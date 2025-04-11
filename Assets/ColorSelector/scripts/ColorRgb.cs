using System.Collections.Generic;
using UnityEngine;

namespace ColorSlider {
    public class ColorRgb : MonoBehaviour
    {
        public ColorSlider slider;

        private readonly string[]    keysRgb={ "R", "G", "B"};
        private readonly Color[]     colorsRgb={Color.red,Color.green,Color.blue};
        private readonly List<ColorSlider>  sliders=new();
        private bool beingUsed;

        public void init()
        {
            if(sliders.Count == 0) {
                for(int i = 0; i < 3; i++) {
                    GameObject go = Instantiate(slider.gameObject, slider.transform.parent);
                    sliders.Add(go.GetComponent<ColorSlider>());
                }
                for(int i = 0; i < 3; i++) {
                    sliders[i].key.text = keysRgb[i];
                    sliders[i].fill.color = colorsRgb[i];
                    sliders[i].slider.onValueChanged.RemoveAllListeners();
                    sliders[i].slider.onValueChanged.AddListener((value)=>onValueChanged(value));

                }
                slider.gameObject.SetActive(false);
            }
        }
        private void onValueChanged(float _)
        {
            beingUsed = true;
        }
        private void OnEnable()
        {
            init();
        }
        public bool inUse()
        {
            bool ret = beingUsed;
            beingUsed = false;
            return ret;
        }
        public void set()
        {
            Color c = getColor();
            setColor(c);
        }
        public Color getColor()
        {
            Color color;
            color.r = sliders[0].slider.value;
            color.g = sliders[1].slider.value;
            color.b = sliders[2].slider.value;
            color.a = 1.0f;
            return color;
        }
        public void setColor(Color _color)
        {
            sliders[0].slider.value = _color.r;
            sliders[1].slider.value = _color.g;
            sliders[2].slider.value = _color.b;
        }
    }
}
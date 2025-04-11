using UnityEngine;

namespace ColorSlider {
    public class ColorWheelContainer : MonoBehaviour
    {
        public ColorWheel wheel;

        public Color getColor()
        {
            return wheel.getColor();
        }
        public void setColor(Color color)
        {
            wheel.setColor(color);
        }
        public bool inUse()
        {
            return wheel.inUse();
        }
    }
}

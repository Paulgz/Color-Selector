using UnityEngine;

namespace SmallTools {
    public class ColorWheelContainer : MonoBehaviour, IColorPicker {
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

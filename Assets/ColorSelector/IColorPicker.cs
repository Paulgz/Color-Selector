using UnityEngine;

namespace SmallTools {
    public interface IColorPicker {
        Color getColor();
        void setColor(Color color);
        bool inUse();
    }
}

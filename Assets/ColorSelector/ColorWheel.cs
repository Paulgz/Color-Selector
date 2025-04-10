using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SmallTools {
    public class ColorWheel : MaskableGraphic, IColorPicker {
        public int segments=12;
        public int rings=2;
        public Image   cursor;
        public Slider slider_v;
        public float outlineSize = 0.1f;

        private bool valueChanged;

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            SetVerticesDirty();
            SetMaterialDirty();
        }
        public void redraw()
        {
            SetVerticesDirty();
            SetMaterialDirty();
        }
        private void Update()
        {
            redraw();
            cursor.color = getColor();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            slider_v.onValueChanged.RemoveAllListeners();
            slider_v.onValueChanged.AddListener((value) => onValueChanged(value));
        }
        public void vChanged(float _)
        {
            redraw();
        }
        private void onValueChanged(float _)
        {
            valueChanged = true;
        }
        public bool inUse()
        {
            bool ret = valueChanged;
            valueChanged = false;
            return ret;
        }
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            UIVertex vert = new ();
            int index=0;
            float colorV=slider_v.value;
            float radius = rectTransform.rect.width*0.5f;
            Color color = Color.white;
            for(int iseg = 0; iseg < segments; iseg++) {
                float h0 = ((float)iseg / (float)segments);
                float h1 = ((float)(iseg+1) / (float)segments);
                float a0 = h0*Tools.twoPi;
                float a1 = h1*Tools.twoPi;
                float r0 = radius;
                float r1 = radius - radius*outlineSize;
                for(int ir = 0; ir < 1; ir++) {
                    Vector2 v0 = new(Mathf.Cos(a0),Mathf.Sin(a0));
                    Vector2 v1 = v0;
                    v0 *= r0;
                    v1 *= r1;
                    Vector2 v2 = new(Mathf.Cos(a1),Mathf.Sin(a1));
                    Vector2 v3 = v2;
                    v2 *= r0;
                    v3 *= r1;

                    vert.position = v0;
                    vert.color = color;
                    vh.AddVert(vert);

                    vert.position = v1;
                    vert.color = color;
                    vh.AddVert(vert);

                    vert.position = v2;
                    vert.color = color;
                    vh.AddVert(vert);

                    vert.position = v3;
                    vert.color = color;
                    vh.AddVert(vert);

                    int i0=index++;
                    int i1=index++;
                    int i2=index++;
                    int i3=index++;

                    vh.AddTriangle(i0, i1, i2);
                    vh.AddTriangle(i2, i1, i3);
                }
            }
            radius -= radius * outlineSize;
            for(int iseg = 0; iseg < segments; iseg++) {
                float h0 = ((float)iseg / (float)segments);
                float h1 = ((float)(iseg+1) / (float)segments);
                float a0 = h0*Tools.twoPi;
                float a1 = h1*Tools.twoPi;
                for(int ir = 0; ir < rings; ir++) {
                    float s0 = ((float)ir / (float)rings);
                    float s1 = ((float)(ir+1) / (float)rings);
                    float r0 = s0*radius;
                    float r1 = s1 * radius;

                    Vector2 v0 = new(Mathf.Cos(a0),Mathf.Sin(a0));
                    Vector2 v1 = v0;
                    v0 *= r0;
                    v1 *= r1;
                    Vector2 v2 = new(Mathf.Cos(a1),Mathf.Sin(a1));
                    Vector2 v3 = v2;
                    v2 *= r0;
                    v3 *= r1;

                    vert.position = v0;
                    vert.color = Color.HSVToRGB(h0, s0, colorV);
                    vh.AddVert(vert);

                    vert.position = v1;
                    vert.color = Color.HSVToRGB(h0, s1, colorV);
                    vh.AddVert(vert);

                    vert.position = v2;
                    vert.color = Color.HSVToRGB(h1, s0, colorV);
                    vh.AddVert(vert);

                    vert.position = v3;
                    vert.color = Color.HSVToRGB(h1, s1, colorV);
                    vh.AddVert(vert);

                    int i0=index++;
                    int i1=index++;
                    int i2=index++;
                    int i3=index++;

                    vh.AddTriangle(i0, i1, i2);
                    vh.AddTriangle(i2, i1, i3);
                }
            }
        }
        public void setColor(Color color)
        {
            Color.RGBToHSV(color, out float h, out float s, out float v);
            slider_v.value = v;
            float angle = h*Tools.twoPi;
            float distance = s*getWheelRadius();
            Vector2 rel = new(Mathf.Cos(angle), Mathf.Sin(angle));
            rel *= distance;
            Vector2 middle = rectTransform.position;
            cursor.rectTransform.position = middle + rel;
        }
        public float getWheelRadius()
        {
            var canvas = Tools.getCanvas(rectTransform);
            var scale = canvas.scaleFactor;
            Vector2 size = Tools.getPixelSize(rectTransform);
            float radius = size.x*scale*0.5f;
            return radius;
        }
        public Vector2 getWheelCenter()
        {
            return rectTransform.position;
        }
        public Color getColor()
        {
            Vector2 middle = getWheelCenter();
            Vector2 where = cursor.rectTransform.position;
            Vector2 rel = where-middle;
            float angle = Mathf.Atan2(rel.y,rel.x);
            angle = Tools.fixAngleRad(angle);
            float size = getWheelRadius();
            float distance = rel.magnitude;
            distance /= size;
            distance = Mathf.Clamp01(distance);
            float h = angle/Tools.twoPi;
            float s = distance;
            float colorV=slider_v.value;
            return Color.HSVToRGB(h, s, colorV);
        }
        void placeCursor(Vector2 where)
        {
            float   r = getWheelRadius();
            Vector2 rel=where-getWheelCenter();
            float d = rel.magnitude;
            RectTransform rt = cursor.rectTransform;
            if(d != 0) {
                float dNew=Mathf.Min(d, r);
                rt.position = getWheelCenter() + rel * (dNew / d);
            } else {
                rt.position = getWheelCenter();
            }
        }
        public void pointerDown(BaseEventData _event)
        {
            PointerEventData    pointer = _event as PointerEventData;
            placeCursor(pointer.position);
            onValueChanged(0);
        }
        public void pointerUp(BaseEventData _event)
        {
            PointerEventData    pointer = _event as PointerEventData;
            placeCursor(pointer.position);
            onValueChanged(0);
        }
        public void pointerDrag(BaseEventData _event)
        {
            PointerEventData    pointer = _event as PointerEventData;
            placeCursor(pointer.position);
            onValueChanged(0);
        }
    }
}
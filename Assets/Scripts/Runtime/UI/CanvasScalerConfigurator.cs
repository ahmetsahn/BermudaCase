using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class CanvasScalerConfigurator : MonoBehaviour
    {
        public CanvasScaler canvasScaler;

        void Start()
        {
            if (canvasScaler == null)
            {
                canvasScaler = GetComponent<CanvasScaler>();
            }

#if UNITY_WEBGL
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;  
#elif UNITY_ANDROID

            canvasScaler.referenceResolution = new Vector2(1080, 1920);  // 16:9 Full HD çözünürlük
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;
#endif
        }
    }
}
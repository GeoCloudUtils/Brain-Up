/*
 Source: https://gist.github.com/bhison/7aa45c7ff56ba692cebf828c374b8192
 Added: Ghercioglo "Romeon0" Roman
 */

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Other
{
    [ExecuteInEditMode]
    public class ScaledGridLayoutGroup : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 1)]
        public float widthPercentage;
        [SerializeField]
        [Range(0, 1)]
        public float heightPercentage;
        private float lWidthPercentage = 0;
        private float lHeightPercentage = 0;
        private Vector2 viewSize = Vector2.zero;

        public Canvas canvas;

        void Start()
        {
            canvas = GetComponentInParent<Canvas>();
            Fix();
        }

        void Update()
        {
#if UNITY_EDITOR
            //This is used to detect whether in editor view resolution has changed
            if (Application.isPlaying) return;
            if (GetMainGameViewSize() != viewSize || widthPercentage != lWidthPercentage || heightPercentage != lHeightPercentage)
            {
                Fix();
                //viewSize = GetMainGameViewSize();
                viewSize = GetGridLayoutSize();//by layout size
                lWidthPercentage = widthPercentage;
                lHeightPercentage = heightPercentage;

                if (canvas != null)
                {
                    Debug.Log(canvas.scaleFactor);
                }
            }
#endif
        }

        private Vector2 GetGridLayoutSize()
        {
            Vector2 size = new Vector2();
            var rect = GetComponent<RectTransform>();

            Vector2 anchorMin = rect.anchorMin;
            Vector2 anchorMax = rect.anchorMax;

            if (anchorMin.x == anchorMax.x)
                size.x = rect.rect.width;
            else
                size.x = (anchorMax.x - anchorMin.x) * (Screen.width / canvas.scaleFactor);
            if (anchorMin.y == anchorMax.y)
                size.y = rect.rect.height;
            else
                size.y = (anchorMax.y - anchorMin.y) * (1080);
            return size;
        }

        public void Fix()
        {
            GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
            //var size = GetMainGameViewSize();//by screen size
            var size = GetGridLayoutSize();//by layout size
            var width = (float)size.x;
            var height = (float)size.y;
            var valWidth = (int)Mathf.Round(width * widthPercentage);
            var valHeight = (int)Mathf.Round(height * heightPercentage);
            grid.cellSize = new Vector2(valWidth, valHeight);
            //Toggle enabled to update screen (is there a better way to do this?)
            grid.enabled = false;
            grid.enabled = true;
        }

        //Thanks to http://kirillmuzykov.com/unity-get-game-view-resolution/
        public static Vector2 GetMainGameViewSize()
        {
            System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
            System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
            Vector2 size = (Vector2)Res;
            return (Vector2)Res;
        }
    }
}
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace cryingpants
{

    public class FSMWindow : EditorWindow
    {
        // Add menu item named "My Window" to the Window menu
        [MenuItem("Window/FSM Graph")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            FSMWindow.GetWindow<FSMWindow>().Init();
        }

        private Rect window1;
        private Rect window2;

        public bool customStyle;
        public GUIStyle myStyle;

        public void Init()
        {
            Debug.Log("FSM initing...");
            window1 = new Rect(10, 10, 100, 100);
            window2 = new Rect(210, 210, 100, 100);
        }



        void OnGUI()
        {
            if (GUILayout.Button("Create Node"))
            {
                Debug.Log("Create Node clicked!");
            }
            DrawNodeCurve(window1, window2); // Here the curve is drawn under the windows

            customStyle = GUILayout.Toggle(customStyle, "Use Custom Style");
            if (customStyle)
            {
                BeginWindows();
                window1 = GUI.Window(1, window1, DrawNodeWindow, "Window 1", myStyle);   // Updates the Rect's when these are dragged
                window2 = GUI.Window(2, window2, DrawNodeWindow, "Window 2", myStyle);
                EndWindows();
            }
            else
            {
                BeginWindows();
                window1 = GUI.Window(1, window1, DrawNodeWindow, "Window 1");   // Updates the Rect's when these are dragged
                window2 = GUI.Window(2, window2, DrawNodeWindow, "Window 2");
                EndWindows();
            }
        }

        void DrawNodeWindow(int id)
        {
            GUI.DragWindow(new Rect(0, 0, 10000, 20));

        }

        void DrawNodeCurve(Rect start, Rect end)
        {
            Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
            Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;
            Color shadowCol = new Color(0, 0, 0, 0.06f);
            for (int i = 0; i < 3; i++) // Draw a shadow
                Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
        }
    }
}
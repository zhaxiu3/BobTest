using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

public class BobTestWindow : EditorWindow
{
    List<Rect> windowRects = new List<Rect>();
    List<String> rectTexts = new List<String>();
    [MenuItem("MyWindow/BobWindow")]
    static void Init() {
        BobTestWindowHelper.OpenBobTestWindow();
    }


    void DoWindow(int id) {
        rectTexts[id] = GUI.TextField(new Rect(0, 30, 100, 50), rectTexts[id]);
        GUI.DragWindow();
    }

    void DrawCurves(Rect wr, Rect wr2)
    {

        Color color = new Color(0.4f, 0.4f, 0.5f, 0.8f);

        Vector3 startPos,endPos;

        Vector3 startTangent, endTangent;
        if (wr.x < wr2.x) {
            startPos = new Vector3(wr.x + wr.width, wr.y + 3 + wr.height / 2, 0);
            endPos = new Vector3(wr2.x, wr2.y + wr2.height / 2, 0);
            startTangent = startPos + Vector3.right * 50.0f;

            endTangent = endPos + Vector3.left * 50.0f;
        }
        else
        {
            startPos = new Vector3(wr.x , wr.y + 3 + wr.height / 2, 0);
            endPos = new Vector3(wr2.x+wr2.width, wr2.y + wr2.height / 2, 0);
            startTangent = startPos - Vector3.right * 50.0f;
            endTangent = endPos - Vector3.left * 50.0f;
        }

        Handles.DrawBezier(startPos, endPos, startTangent, endTangent, color, null, 5f);
    }

    void Callbacks(object obj)
    {
        Debug.Log(obj + " is selected");
        if (obj.Equals("item2"))
        {
            windowRects.Add(new Rect(0, 0, 100, 100));
            rectTexts.Add(String.Empty);
        }
    }
    void OnGUI()
    {

        Event e = Event.current;

        //Handles.DrawCamera(new Rect(0, 0, Screen.width, Screen.height), Camera.current);

        if (e.isKey && EventType.keyDown == e.type&& e.character =='a')
        {
            Debug.Log("Detected character: " + e.character);
            //windowRects.Add(new Rect(0, 0, 100, 100));
            e.Use();
        }

        if (EventType.ContextClick == e.type) {
            //点击鼠标右键弹出右键菜单
            GetContextMenu();

            e.Use();
        }

        if (e.isMouse && e.button == 2) {
            //点击鼠标中键在固定位置出现下拉菜单
            GetDropDownMenu();

            e.Use();
        }

        if (e.Equals(Event.KeyboardEvent("[0]")))
            Debug.Log("pad0 is pressed");



        BeginWindows();
        for (int i = 0; i < windowRects.Count; i++ )
        {
            windowRects[i] = GUI.Window(i, windowRects[i], DoWindow, "hee"+i);
        }
        EndWindows();

        for (int i = 0; i < windowRects.Count - 1; i++)
        {
            DrawCurves(windowRects[i], windowRects[i + 1]);
        }

    }

    private void GetDropDownMenu()
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("hello1"), false, Callbacks, "item1");
        menu.AddItem(new GUIContent("AddItem"), false, Callbacks, "item2");
        menu.AddSeparator("");
        menu.AddItem(new GUIContent("hello3/hello21"), false, Callbacks, "item3");
        menu.DropDown(new Rect(0, 0, 0, 0));
    }
    private void GetContextMenu()
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("hello1"), false, Callbacks, "item1");
        menu.AddItem(new GUIContent("Additem"), false, Callbacks, "item2");
        menu.AddSeparator("我是聪明的分隔符哦亲~~~~~~~~");
        menu.AddItem(new GUIContent("hello3/hello21"), false, Callbacks, "item3");
        menu.ShowAsContext();
    }
    void Update() {
    }

}

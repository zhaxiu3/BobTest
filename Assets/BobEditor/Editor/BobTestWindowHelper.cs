using UnityEngine;
using System.Collections;
using UnityEditor;

public class BobTestWindowHelper : EditorWindow {

    static EditorWindow window = null;
    static BobTestWindow editor = null;

    public static void OpenBobTestWindow() {
        if (editor)
            editor.Close();
        editor = BobTestWindow.GetWindow<BobTestWindow>();
    }
}

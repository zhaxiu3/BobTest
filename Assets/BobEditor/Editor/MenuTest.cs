using UnityEngine;
using UnityEditor;
using System.Collections;

public class MenuTest : MonoBehaviour {

    [MenuItem("MyMenu/Do something")]
    static void DoSomething() {
        Debug.Log("do something");
    }

    //validate menu item
    //在MyMenu中添加名称为“Log Selected Transform Name”的菜单项
    //使用第二个函数来使菜单项合法化
    //这样只有在一个transform被选中时该菜单项可用
    [MenuItem("MyMenu/Log Selected Transform Name")]
    static void LogSelectedTransformName() {
        Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name);
    }

    //使上述函数的菜单项合法化
    //如果下面的函数返回false，则上面的菜单项将被禁用
    [MenuItem("MyMenu/Log Selected Transform Name",true)]
    static bool ValidateLogSelectedTransformName(){
        return Selection.activeTransform != null;
    }

    //在MyMenu中添加名为"Do something with a shortcut key"的菜单项
    //使用ctrl-g来启动
    [MenuItem("MyMenu/Do Something With a Shortcut Key %g")]
    static void DoSomethingWithAShortcutKey() {
        Debug.Log("Doing Something with a shortcut key");
    }

    //在Rigidbody的上下文菜单中添加名为“Double Mass”的菜单项
    [MenuItem("CONTEXT/Rigidbody/Double Mass")]
    static void DoubleMass(MenuCommand command)
    {
        Rigidbody body = (Rigidbody)command.context;
        body.mass *= 2;
        Debug.Log("Doubled Rigidbody's Mass to " + body.mass + "from Context Menu");
    }

    //将选中的物体中的最上级的物体对准相机
    [MenuItem("MyMenu/Selection looks at Main Camera _l")]
    static void Look()
    {
        Camera camera = Camera.main;
        if (camera)
        {
            foreach (Transform transform in Selection.transforms) {
                Undo.RegisterUndo(transform, transform.name + "Look at Main Camera");
                Debug.Log(transform.name);
                transform.LookAt(camera.transform);
            }
        }
    }

    [MenuItem("MyMenu/Selection looks at Main Camera _l", true)]
    static bool ValidateSelection() {
        return Selection.transforms.Length != 0;
    }

    [MenuItem("MyMenu/Duplicate at Origin _d")]
    static void DuplicateSelected() {
        Instantiate(Selection.activeTransform, Vector3.zero, Quaternion.identity);
    }

    [MenuItem("MyMenu/Duplicate at Origin _d", true)]
    static bool ValidateDuplicateSelected() {
        return Selection.activeTransform != null;
    }

    [MenuItem("MyMenu/Hello/Good/Rotate +45 _g")]
    static void RotatePlus45() {
        GameObject obj = Selection.activeGameObject;
        obj.transform.Rotate(Vector3.up * 45);
    }

    [MenuItem("MyMenu/Hello/Good/Rotate +45 _g", true)]
    static bool ValidateRotatePlus45() {
        return Selection.activeGameObject != null;
    }

    [MenuItem("MyMenu/Create Parent for Selection _p")]
    static void MenuInsertParent() {
        Transform[] selection = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.Editable);
        GameObject newObject = new GameObject("parent");
        foreach (Transform t in selection)
        {
            t.parent = newObject.transform;
        }
    }

    [MenuItem("MyMenu/Create Parent for Selection _p", true)]
    static bool ValidateInsertParent() {
        return Selection.activeGameObject != null;
    }

    [MenuItem("MyMenu/Toggle Active Recursively of Selected %i")]
    static void DoToggle() {
        Object[] activeGos = Selection.GetFiltered(typeof(GameObject),
            SelectionMode.Editable | SelectionMode.TopLevel);
        foreach (GameObject activeGo in activeGos)
        {
            activeGo.SetActiveRecursively(!activeGo.active);
        }
    }

}

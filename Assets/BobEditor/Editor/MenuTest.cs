using UnityEngine;
using UnityEditor;
using System.Collections;

public class MenuTest : MonoBehaviour {

    [MenuItem("MyMenu/Do something")]
    static void DoSomething() {
        Debug.Log("do something");
    }

    //validate menu item
    //��MyMenu���������Ϊ��Log Selected Transform Name���Ĳ˵���
    //ʹ�õڶ���������ʹ�˵���Ϸ���
    //����ֻ����һ��transform��ѡ��ʱ�ò˵������
    [MenuItem("MyMenu/Log Selected Transform Name")]
    static void LogSelectedTransformName() {
        Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name);
    }

    //ʹ���������Ĳ˵���Ϸ���
    //�������ĺ�������false��������Ĳ˵��������
    [MenuItem("MyMenu/Log Selected Transform Name",true)]
    static bool ValidateLogSelectedTransformName(){
        return Selection.activeTransform != null;
    }

    //��MyMenu�������Ϊ"Do something with a shortcut key"�Ĳ˵���
    //ʹ��ctrl-g������
    [MenuItem("MyMenu/Do Something With a Shortcut Key %g")]
    static void DoSomethingWithAShortcutKey() {
        Debug.Log("Doing Something with a shortcut key");
    }

    //��Rigidbody�������Ĳ˵��������Ϊ��Double Mass���Ĳ˵���
    [MenuItem("CONTEXT/Rigidbody/Double Mass")]
    static void DoubleMass(MenuCommand command)
    {
        Rigidbody body = (Rigidbody)command.context;
        body.mass *= 2;
        Debug.Log("Doubled Rigidbody's Mass to " + body.mass + "from Context Menu");
    }

    //��ѡ�е������е����ϼ��������׼���
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

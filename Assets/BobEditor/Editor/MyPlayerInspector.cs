#define  useSerializedSystemhh
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MyPlayer))]
[CanEditMultipleObjects]
public class MyPlayerInspector : Editor
{
#if useSerializedSystem
    SerializedProperty damageProp;
    SerializedProperty armorProp;
    SerializedProperty gunProp;
    void OnEnable()
    {
        damageProp = serializedObject.FindProperty("damage");
        armorProp = serializedObject.FindProperty("armor");
        gunProp = serializedObject.FindProperty("gun");
    }
#endif

#if useSerializedSystem
    public override void OnInspectorGUI()
    {
        //更新属性值，总是在OnInspectorGUI的开始
        serializedObject.Update();

        EditorGUILayout.IntSlider(damageProp, 0, 100, new GUIContent("Damage"));
        if (!damageProp.hasMultipleDifferentValues) //当多个物体的值相同时才显示进度条
        {
            ProgressBar(damageProp.intValue / 100.0f, "Damage");
        }
        EditorGUILayout.IntSlider(armorProp, 0, 100, new GUIContent("Armor"));
        if (!armorProp.hasMultipleDifferentValues)
            ProgressBar(armorProp.intValue / 100.0f, "Armor");

        EditorGUILayout.PropertyField(gunProp, new GUIContent("Gun Object"));

        // 对serializedProperty应用改变 - 总是在OnInspectorGUI的末尾.
        serializedObject.ApplyModifiedProperties();

    }
#else
    public override void OnInspectorGUI(){
        MyPlayer _target = target as MyPlayer;
        _target.Damage = EditorGUILayout.IntSlider(new GUIContent("Damage"),_target.Damage, 0, 100);
        ProgressBar(_target.Damage / 100.0f, "Damage");
        _target.Armor = EditorGUILayout.IntSlider( new GUIContent("Armor"),_target.Armor, 0, 100);
        ProgressBar(_target.Armor / 100.0f, "Armor");

        bool allowSceneObjects = !EditorUtility.IsPersistent(target);
        _target.Gun = EditorGUILayout.ObjectField("Gun Object", _target.Gun, typeof(Object), allowSceneObjects) as GameObject;
    }
#endif
    void ProgressBar(float value, string label) {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
}

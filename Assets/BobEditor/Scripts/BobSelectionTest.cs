using UnityEngine;
using System.Collections;
using UnityEditor;

public class BobSelectionTest : MonoBehaviour {

    public SelectionMode[] modes;
    public GameObject WarningObj;
	// Use this for initialization
	void Start () {
        if (modes.Length == 0)
            return;

        SelectionMode mode = modes[0];
        for (int i = 1; i < modes.Length; i++ )
        {
            mode |= modes[i];
        }

        Object[] activeGos = Selection.GetFiltered(typeof(Object), mode);
        foreach (Object activeGo in activeGos)
        {
            Debug.Log(activeGo.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}

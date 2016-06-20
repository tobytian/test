//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform test;
	// Use this for initialization
	void Start () {
	
        Hashtable ht = new Hashtable();

        ht.Add("looktarget", test);

        iTween.LookTo(gameObject,ht);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

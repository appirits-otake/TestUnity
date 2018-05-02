using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowButton : MonoBehaviour {

	public GameObject _window;
	public StackAssetObjects _stackObjects;
	public string _name;

	void Start(){


	}


	public void WindowCreateAndDelete(){

		if (null == _window) {
			WindowIn ();
		}else{
			WindowOut();
		}
	}

	public void WindowIn () {

		//GameObject windowPrefab = StackObjects.GetStackObjects()[_name];

		GameObject windowPrefab = _stackObjects.GetObject(_name);

		_window = Instantiate(windowPrefab) as GameObject;


	}


	public void WindowOut () {
		
		Destroy(_window);
	}
}

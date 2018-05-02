using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StackAssetObjects : MonoBehaviour {

	public Dictionary<string, AssetBundle> _assetBundles = new Dictionary<string, AssetBundle> ();

	public Dictionary<string, GameObject> _stackObjects = new Dictionary<string, GameObject> ();

	public Dictionary<string, string> _nameList = new Dictionary<string, string> ();

	void Start(){

		_nameList.Add (PrefabName.A, AssetBundleName.AB_BUNDLE);
		_nameList.Add (PrefabName.B, AssetBundleName.AB_BUNDLE);
		_nameList.Add (PrefabName.C, AssetBundleName.C_BUNDLE);
		_nameList.Add (PrefabName.D, AssetBundleName.D_BUNDLE);
	}
		
		
	void LoadAssetBundle(string bundleName){
		string bundlePath = Application.streamingAssetsPath + "/" + bundleName;
		var request = AssetBundle.LoadFromFileAsync (bundlePath);

		AssetBundle bundle = request.assetBundle;
		_assetBundles.Add (bundle.name, bundle);
	}


	GameObject GetAssetBundleObject(string bundleName, string objectName){

		AssetBundle assetBundle = _assetBundles[bundleName];

		if (assetBundle == null) return null;

		return assetBundle.LoadAsset<GameObject> (objectName);

	}

	void StackObjectAllSet(){

		_stackObjects.Add (PrefabName.A, 
			GetAssetBundleObject (AssetBundleName.AB_BUNDLE, PrefabName.A));
		_stackObjects.Add (PrefabName.B, 
			GetAssetBundleObject (AssetBundleName.AB_BUNDLE, PrefabName.B));
		_stackObjects.Add (PrefabName.C, 
			GetAssetBundleObject (AssetBundleName.C_BUNDLE, PrefabName.C));
		_stackObjects.Add (PrefabName.D, 
			GetAssetBundleObject (AssetBundleName.D_BUNDLE, PrefabName.D));

	}

	public void BundlesUnload(){

		foreach(KeyValuePair<string, AssetBundle> pair in _assetBundles){
			_assetBundles [pair.Key].Unload (true);
		}

		_assetBundles.Clear ();
	}

	public GameObject GetObject(string name){

		string nameLower = name.ToLower ();

		if (_stackObjects.ContainsKey (nameLower)) {

			Debug.Log ("スタックあり");
			return _stackObjects [nameLower];
		}

		string bundleName = _nameList [nameLower];

		if (!_assetBundles.ContainsKey (bundleName)) {
			Debug.Log("バンドルなし");
			LoadAssetBundle (bundleName);
		}

		_stackObjects.Add (nameLower, GetAssetBundleObject (bundleName, name));
		Debug.Log("スタックなし");
		return  _stackObjects [nameLower];
	}
}

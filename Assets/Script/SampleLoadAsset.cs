using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleLoadAsset : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	AssetBundle prefabAsset = null;
	AssetBundle imageAsset = null;

	public GameObject Canvas;
	
	// Update is called once per frame
	void OnGUI() {

		//アセットバンドルの読み込み
		if (GUI.Button (new Rect (10, 70, 200, 100), "LoadAsset")) {
			Debug.Log("Clicked LoadAsset");
			StartCoroutine (LoadCacheOrDownload ());

		}

		//キャッシュ削除
		if (GUI.Button (new Rect (10, 300, 200, 100), "CleanCache")) {
			Caching.CleanCache();
			Debug.Log("CleanCache");
		}


		if (GUI.Button (new Rect (10, 400, 200, 100), "PrefabAssetUnload")) {
			PrefabAssetUnload ();
			Debug.Log("PrefabAssetUnload");
		}


		if (GUI.Button (new Rect (10, 500, 200, 100), "CreatGameObject")) {
			//事前にLoadCacheOrDownload関数(サンプルソースのLoadAssetボタン)を呼び出してデータを読み込んでおくこと
			CreatGameObject();
			Debug.Log("CreatGameObject");
		}

		if (GUI.Button (new Rect (10, 600, 200, 100), "LoadPrefabBundlePrefabAOnly")) {
			StartCoroutine (LoadPrefabBundlePrefabAOnly ());
			Debug.Log("LoadPrefabBundlePrefabAOnly");
		}

		if (GUI.Button (new Rect (10, 700, 200, 100), "LoadPrefabBundlePrefabABOnly")) {
			StartCoroutine (LoadPrefabBundlePrefabABOnly ());
			Debug.Log("LoadPrefabBundlePrefabABOnly");
		}

	}
	IEnumerator LoadImageAsset(){
		using (var www = WWW.LoadFromCacheOrDownload("http://localhost/assetbundles/image_bundle", 5))
		{
			yield return www;
			//Debug.Log(String.format "LoadFromCacheOrDownload {0}" , www.assetBundle.ToString());


			imageAsset = www.assetBundle;
			//var asset = myLoadedAssetBundle.mainAsset;

		}


	}

	void PrefabAssetUnload(){

		//trueを指定して強制的にメモリ解放したほうが、メモリ節約になる
		prefabAsset.Unload (true);
		imageAsset.Unload (true);
	}

	void CreatGameObject(){
		AssetBundleRequest image = prefabAsset.LoadAssetAsync<GameObject>("PrefabA");
		GameObject obj = (GameObject)Instantiate( image.asset );
		obj.transform.SetParent(Canvas.gameObject.transform, false);

		image = prefabAsset.LoadAssetAsync<GameObject>("PrefabAB");
		obj = (GameObject)Instantiate( image.asset );
		obj.transform.SetParent(Canvas.gameObject.transform, false);

	}


	IEnumerator LoadPrefabBundlePrefabAOnly(){
		Debug.Log("LoadPrefabBundlePrefabAOnly begin");


		using (var www = WWW.LoadFromCacheOrDownload("http://localhost/assetbundles/prefab_bundle_prefab_a_only", 5))
		{
			yield return www;
			if (!string.IsNullOrEmpty(www.error))
			{
				Debug.Log(www.error);
				yield return null;
			}

			//Debug.Log(String.format "LoadFromCacheOrDownload {0}" , www.assetBundle.ToString());


			var myLoadedAssetBundle = www.assetBundle;

			Debug.Log("LoadFromCacheOrDownload OK");

			foreach (string s in myLoadedAssetBundle.GetAllAssetNames()) {
				Debug.Log(s);
			}


			//アセットバンドルデータ
			//prefabAsset = myLoadedAssetBundle;

			AssetBundleRequest assetdata = myLoadedAssetBundle.LoadAssetAsync<GameObject>("PrefabA");
			GameObject obj = (GameObject)Instantiate( assetdata.asset );
			obj.transform.SetParent(Canvas.gameObject.transform, false);


		}
	}



	IEnumerator LoadPrefabBundlePrefabABOnly(){
		Debug.Log("LoadPrefabBundlePrefabAOnly begin");


		using (var www = WWW.LoadFromCacheOrDownload("http://localhost/assetbundles/prefab_bundle_prefab_ab_only", 5))
		{
			yield return www;
			if (!string.IsNullOrEmpty(www.error))
			{
				Debug.Log(www.error);
				yield return null;
			}

			//Debug.Log(String.format "LoadFromCacheOrDownload {0}" , www.assetBundle.ToString());


			var myLoadedAssetBundle = www.assetBundle;

			Debug.Log("LoadFromCacheOrDownload OK");

			foreach (string s in myLoadedAssetBundle.GetAllAssetNames()) {
				Debug.Log(s);
			}


			//アセットバンドルデータ
			//prefabAsset = myLoadedAssetBundle;

			AssetBundleRequest assetdata = myLoadedAssetBundle.LoadAssetAsync<GameObject>("PrefabAB");
			GameObject obj = (GameObject)Instantiate( assetdata.asset );
			obj.transform.SetParent(Canvas.gameObject.transform, false);


		}
	}


	IEnumerator LoadCacheOrDownload(){
		Debug.Log("LoadFromCacheOrDownload begin");

		//さきにimageを読み込んでおく必要がある。でないと、プレファブを表示したときにテクスチャがない状態になる
		using (var www = WWW.LoadFromCacheOrDownload("http://localhost/assetbundles/image_bundle", 5))
		{
			yield return www;
			//Debug.Log(String.format "LoadFromCacheOrDownload {0}" , www.assetBundle.ToString());


			imageAsset = www.assetBundle;
			//var asset = myLoadedAssetBundle.mainAsset;

		}

		//プレファブを読み込む
		using (var www = WWW.LoadFromCacheOrDownload("http://localhost/assetbundles/prefab_bundle", 5))
		{

			Debug.Log("LoadFromCacheOrDownload begin2");
			yield return www;
			Debug.Log("LoadFromCacheOrDownload begin3");
			if (!string.IsNullOrEmpty(www.error))
			{
				Debug.Log(www.error);
				yield return null;
			}

			//Debug.Log(String.format "LoadFromCacheOrDownload {0}" , www.assetBundle.ToString());


			var myLoadedAssetBundle = www.assetBundle;

			Debug.Log("LoadFromCacheOrDownload OK");

			foreach (string s in myLoadedAssetBundle.GetAllAssetNames()) {
				Debug.Log(s);
			}


			//アセットバンドルデータ
			prefabAsset = myLoadedAssetBundle;




		}
	}
}

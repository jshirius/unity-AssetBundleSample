using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildAssetBundlesExample : MonoBehaviour {

	//依存関係
	//PrefabA・・・image1
	//PrefabB・・・image2
	//PrefabAB・・・image1とimage2

	[MenuItem("Assets/Build AssetBundles")]
	static void BuildABs()
	{
		//エディタで選択した範囲のものについてAssetbundleを作成する
		//うまく動作しない？
		BuildPipeline.BuildAssetBundles("Assets/ABs", BuildAssetBundleOptions.None, BuildTarget.iOS);
	}
		


	[MenuItem("Example/Build  prefab_bundle_prefabA_only")]
	static void PrefabBundlePrefabAOnly()
	{
		// Create the array of bundle build details.
		AssetBundleBuild[] buildMap = new AssetBundleBuild[1];


		buildMap[0].assetBundleName = "prefab_bundle_prefab_A_only";
		string[] prefabAssets = new string[1];
		prefabAssets[0] = "Assets/AssetBundle/PrefabAssets/PrefabA.prefab";
		buildMap[0].assetNames = prefabAssets;

		BuildPipeline.BuildAssetBundles("Assets/ABs", buildMap, BuildAssetBundleOptions.None, BuildTarget.iOS);	
	}
		

	[MenuItem("Example/Build prefab_bundle_prefabAB_only")]
	static void PrefabBundlePrefabABOnly()
	{
		// Create the array of bundle build details.
		AssetBundleBuild[] buildMap = new AssetBundleBuild[1];


		buildMap[0].assetBundleName = "prefab_bundle_prefab_AB_only";
		string[] prefabAssets = new string[1];
		prefabAssets[0] = "Assets/AssetBundle/PrefabAssets/PrefabAB.prefab";
		buildMap[0].assetNames = prefabAssets;

		BuildPipeline.BuildAssetBundles("Assets/ABs", buildMap, BuildAssetBundleOptions.None, BuildTarget.iOS);	
	}

	//プレファブとテクスチャを分けたとき、アセットバンドルも分けて作成されるか確認する
	[MenuItem("Example/Build Asset Bundles Using BuildMap")]
	static void BuildMapImageAndPrefabABs()
	{
		// Create the array of bundle build details.
		AssetBundleBuild[] buildMap = new AssetBundleBuild[2];

		buildMap[0].assetBundleName = "image_bundle";

		string[]  imageAssets = new string[2];
		imageAssets[0] = "Assets/AssetBundle/ImageAssets/image1.png";
		imageAssets[1] = "Assets/AssetBundle/ImageAssets/image2.png";

		buildMap[0].assetNames = imageAssets;


		//-------------------------------
		buildMap[1].assetBundleName = "prefab_bundle";
		string[] prefabAssets = new string[4];
		prefabAssets[0] = "Assets/AssetBundle/PrefabAssets/PrefabA.prefab";
		prefabAssets[1] = "Assets/AssetBundle/PrefabAssets/PrefabB.prefab";
		prefabAssets[2] = "Assets/AssetBundle/PrefabAssets/PrefabAB.prefab";
		prefabAssets[3] = "Assets/AssetBundle/PrefabAssets/abc.txt";

		buildMap[1].assetNames = prefabAssets;

		BuildPipeline.BuildAssetBundles("Assets/ABs", buildMap, BuildAssetBundleOptions.None, BuildTarget.iOS);
	}




	//プレファブの依存関係を見てテクスチャを含めるか確認する
	[MenuItem("Example/Build Asset Bundles PrefabOnly")]
	static void BuildMapPrefabOnlyABs()
	{
		// Create the array of bundle build details.
		AssetBundleBuild[] buildMap = new AssetBundleBuild[1];


		//-------------------------------
		buildMap[0].assetBundleName = "prefab_bundle";
		string[] prefabAssets = new string[3];
		prefabAssets[0] = "Assets/AssetBundle/PrefabAssets/PrefabA.prefab";
		prefabAssets[1] = "Assets/AssetBundle/PrefabAssets/PrefabB.prefab";
		prefabAssets[2] = "Assets/AssetBundle/PrefabAssets/PrefabAB.prefab";
		prefabAssets[3] = "Assets/AssetBundle/PrefabAssets/abc.txt";

		buildMap[0].assetNames = prefabAssets;

		BuildPipeline.BuildAssetBundles("Assets/ABs", buildMap, BuildAssetBundleOptions.None, BuildTarget.iOS);
	}



}

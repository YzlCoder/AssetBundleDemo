using UnityEngine;
using System.Collections;
using UnityEditor;
public class CreateAssetBundles {

	[MenuItem("AssetBundle/BuildAll")]
	static void BuildAllAssetBundles()
	{
		BuildPipeline.BuildAssetBundles ("Assets/AssetBundles", 
			BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
	}
}

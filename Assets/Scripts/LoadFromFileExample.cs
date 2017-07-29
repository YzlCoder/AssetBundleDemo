using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;


public class LoadFromFileExample : MonoBehaviour {


	private readonly string matpath = "Assets/AssetBundles/share.unity3d";
	private readonly string wallpath = "Assets/AssetBundles/cubewall.unity3d";

	//1.
	void Start1 () 
	{
		AssetBundle.LoadFromFile (matpath);
		AssetBundle ab = AssetBundle.LoadFromFile (wallpath);
		GameObject wallPrefab = ab.LoadAsset<GameObject> ("cubewall");

		Instantiate (wallPrefab);
	}

	//2.
	IEnumerator Start2()
	{
		AssetBundle.LoadFromFileAsync(matpath);
		AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(wallpath);
		yield return request;
		AssetBundle ab = request.assetBundle;
		GameObject wallPrefab = ab.LoadAsset<GameObject> ("cubewall");
		Instantiate (wallPrefab);
	}

	//3.
	IEnumerator Start3()
	{
		while (!Caching.ready)
			yield return null;
		//WWW.LoadFromCacheOrDownload (@"file://" + Application.dataPath + "/../" + matpath, 1);
		//WWW www = WWW.LoadFromCacheOrDownload (@"file://" + Application.dataPath + "/../" + wallpath, 1);
		//http://123.206.46.146:8080/ecrsys/AssetBundles
		WWW www = WWW.LoadFromCacheOrDownload (@"http://123.206.46.146:8080/ecrsys/AssetBundles/share.unity3d", 2);
		yield return www;
		www = WWW.LoadFromCacheOrDownload (@"http://123.206.46.146:8080/ecrsys/AssetBundles/cubewall.unity3d", 2);
		yield return www;
		if (!string.IsNullOrEmpty (www.error)) 
		{
			Debug.Log (www.error);
			yield break;
		}
		AssetBundle ab = www.assetBundle;
		GameObject wallPrefab = ab.LoadAsset<GameObject> ("cubewall");
		Instantiate (wallPrefab);
	}


	//4.
	IEnumerator Start4()
	{
		//string url = @"http://123.206.46.146:8080/ecrsys/AssetBundles/cubewall.unity3d";
		UnityWebRequest request = UnityWebRequest.GetAssetBundle (@"http://123.206.46.146:8080/ecrsys/AssetBundles/share.unity3d");
		yield return request.Send ();
		AssetBundle ab = null;
		ab = DownloadHandlerAssetBundle.GetContent (request);

		request = UnityWebRequest.GetAssetBundle (@"http://123.206.46.146:8080/ecrsys/AssetBundles/cubewall.unity3d");
		yield return request.Send ();

		//ab = DownloadHandlerAssetBundle.GetContent (request);
		ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;

		GameObject wallPrefab = ab.LoadAsset<GameObject> ("cubewall");
		Instantiate (wallPrefab);
	}


	//5.
	void Start5()
	{
		AssetBundle manifestAB = AssetBundle.LoadFromFile ("Assets/AssetBundles/AssetBundles");
		AssetBundleManifest manifest = manifestAB.LoadAsset<AssetBundleManifest> ("AssetBundleManifest");
		string[] depend = manifest.GetAllDependencies ("cubewall.unity3d");
		for (int i = 0; i < depend.Length; i++)
		{
			Debug.Log ("Assets/AssetBundles/" + depend [i]);
			AssetBundle.LoadFromFile ("Assets/AssetBundles/" + depend [i]);
		}
		AssetBundle ab = AssetBundle.LoadFromFile ("Assets/AssetBundles/cubewall.unity3d");

		Instantiate (ab.LoadAsset<GameObject>("cubewall"));

	}


	void Start()
	{
		Start5 ();
	}

}

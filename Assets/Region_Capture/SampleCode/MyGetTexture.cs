using UnityEngine;
using System.Collections;

public class MyGetTexture : MonoBehaviour {
	public static MyGetTexture instance;

	private Camera RenderTextureCamera;
	public GameObject Region_Capture;

	[SerializeField] Material myMat;

	public bool motion=true;
	void Awake () {
		MyGetTexture.instance = this;
	}
	void Start () {
		RenderTextureCamera = Region_Capture.GetComponentInChildren<Camera>();
	}

	void Update(){
		if (motion) {
			if (RenderTextureCamera.targetTexture) {
				myMat.SetTexture ("_MainTex", RenderTextureCamera.targetTexture);
			}
		}
	}

	public void GetTexture(){
		if (RenderTextureCamera.targetTexture) {
			//将RenderTexture转换成为Texture2D,这样，贴图就不再受到camera影响
			RenderTexture.active = RenderTextureCamera.targetTexture;  
			Rect rect = new Rect (0, 0, RenderTextureCamera.targetTexture.width, RenderTextureCamera.targetTexture.height);
			Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24,false);  
			screenShot.ReadPixels(rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
			screenShot.Apply();  

			myMat.SetTexture ("_MainTex", RenderTextureCamera.targetTexture);
		}
		RenderTextureCamera.enabled = false;
	}

}

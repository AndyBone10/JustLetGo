using UnityEngine;  
using System.Runtime.InteropServices;

public class TwitterScript : MonoBehaviour {  
	public void OpenLinkJSPlugin() {
		#if !UNITY_EDITOR
		openWindow("https://twitter.com/AndyBone10");
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);
}
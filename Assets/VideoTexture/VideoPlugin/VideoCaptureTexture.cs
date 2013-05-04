using UnityEngine;
using System.Collections;

public class VideoCaptureTexture : VideoTextureBase {
	
	public int 	SourceWidthExpectation=1920;
	public int 	SourceHeightExpectation=1080;
	public int 	CaptureID=0;
	public bool AutoStart=true;
	
	// Use this for initialization
	void Start () {
		viInitText(SourceWidthExpectation,SourceHeightExpectation,VideoTextureBase.SourceType.Capture);
		viLinkCapture(CaptureID);
		if(renderer)
		{
            renderer.material.mainTexture = VideoTexture;
		}
		if(AutoStart)
		{
			if(!StartInput())
			{
				//Error report
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		viUpdate();
	}
	
	void OnDestroy()
	{
		viDestroy();
	}
}

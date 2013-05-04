using UnityEngine;
using System.Collections;

public class VideoFileTexture : VideoTextureBase {

	public string 	FilePath="FileLocation";
	public bool 	AutoStart=true;
	public bool		Loop=false;
	// Use this for initialization
	void Start()
	{
		viInitText(0,0,VideoTextureBase.SourceType.File);
		viLinkFile(FilePath);
		viSetLoop(Loop);
		if(AutoStart)
		{
			if(!StartInput())
			{
				print("Faild creating video file input!");
			}
		}
	}
	public void reStart()
	{
		StopInput();
		viGotoTimePos(0.0f);
		StartInput();
	}
	public bool GotoTimePos(float second)
	{
		return viGotoTimePos(second);
	}
	
	public bool EndAtTimePos(float second)
	{
		return viEndAtTimePos(second);
	}
	
	public bool SetLoop(bool loop)
	{
		return viSetLoop(loop);
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

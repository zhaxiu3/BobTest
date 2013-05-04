using UnityEngine;
using System;
using System.Runtime.InteropServices;
	
public class VideoTextureBase: MonoBehaviour{
	
	[DllImport("DiffAREventTool")]
	private static extern void Initialize();
	[DllImport("DiffAREventTool")]
	private static extern void destory();
	[DllImport("DiffAREventTool")]
	private static extern void RegisterTexture(string name, int srcW,int srcH,int destW, int destH,int type, int fillmode, bool alpha);
	[DllImport("DiffAREventTool")]
	private static extern void UnRegisterTexture(string name);
	[DllImport("DiffAREventTool")]
	private static extern bool LinkToFileSource(string name, string url);
	[DllImport("DiffAREventTool")]
	private static extern bool LinkToCaptureSource(string name, int id);
	[DllImport("DiffAREventTool")]
	private static extern int QueryVideoWidth(string name);
	[DllImport("DiffAREventTool")]
	private static extern int QueryVideoHeight(string name);
	[DllImport("DiffAREventTool")]
	private static extern bool UpdateFrame(IntPtr colors,string name);
	[DllImport("DiffAREventTool")]
	private static extern bool PlayVideo(string name);
	[DllImport("DiffAREventTool")]
	private static extern bool StopVideo(string name);
	[DllImport("DiffAREventTool")]
	private static extern bool SeekVideo(string name, float pos);
	[DllImport("DiffAREventTool")]
	private static extern bool SetEndPoint(string name, float pos);
	[DllImport("DiffAREventTool")]
	private static extern bool SetVideoLoop(string name, bool loop);
	
	public enum ImgFillMode
	{
		UpperLeft =0,
		Fill      =1,
		Fit       =2,
		Stretch   =3,
		Tile      =4,
		Center    =5,
	};
	
	public enum SourceType
	{
		Capture    =0,
		File       =1,
	};
	
	public Texture2D 	VideoTexture;
	public int 			width = 1024;
	public int 			height = 1024;	
	public bool			TextureAlpha = true;
	public bool			SourceAlpha = true;
	public bool			FixUV=true;
	public ImgFillMode  FillMode = ImgFillMode.UpperLeft;
	
    private Color32[] 	m_Pixels;
    private GCHandle 	m_PixelsHandle;
	
	private string 		mName;
	private bool		mStart=false;	
	private SourceType 	mType = SourceType.Capture;
	
	protected void setName(string name)
	{
		mName=name;
	}
	// Use this for initialization
	private void viCreateText() 
	{
		VideoTexture = new Texture2D (width, height, TextureFormat.RGBA32, false);
        m_Pixels = VideoTexture.GetPixels32(0);
		long size=width*height;
		for(long i=0; i<size;i++)
		{
			m_Pixels[i].r=0;
			m_Pixels[i].g=0;
			m_Pixels[i].b=0;
			m_Pixels[i].a=255;
		}
		VideoTexture.SetPixels32(m_Pixels);
        VideoTexture.Apply(true);
		
        m_PixelsHandle = GCHandle.Alloc(m_Pixels, GCHandleType.Pinned);
	}
	
	protected void viInitText( int srcWidthExp, int srcHeightExp, SourceType type)
	{
		mName=(string)name.Clone();
		mName+=GetInstanceID();
		viCreateText();
		RegisterTexture(mName,srcWidthExp,srcHeightExp,width,height,(int)type,(int)FillMode,SourceAlpha);
		mType=type;
		mStart=true;
	}
	
	
	private void viFixUV(ref Material mat)
	{
		float SourceWidth=QueryVideoWidth(mName);
		float SourceHeigh=QueryVideoHeight(mName);
		Vector2 vec=new  Vector2((SourceWidth+1)/width, Math.Min(1.0f,(SourceHeigh+1)/height));
		mat.mainTextureScale=vec;
	}
	
	protected bool viLinkFile(string filePath)
	{
		if(!LinkToFileSource(mName,filePath))
		{
			//Error report
			return false;	
		}
		if(renderer)
		{
            renderer.material.mainTexture = VideoTexture;
			if(FixUV)
			{
				float SourceWidth=QueryVideoWidth(mName);
				float SourceHeigh=QueryVideoHeight(mName);
				Vector2 vec=new  Vector2((SourceWidth)/width, Math.Min(1.0f,SourceHeigh/height));
				renderer.material.mainTextureScale=vec;
			}
		}
		return true;
	}
	protected bool viLinkCapture(int id)
	{
		if(!LinkToCaptureSource(mName,id))
		{
			//Error report
			return false;	
		}
		if(renderer)
		{
            renderer.material.mainTexture = VideoTexture;
			if(FixUV)
			{
				float SourceWidth=QueryVideoWidth(mName);
				float SourceHeigh=QueryVideoHeight(mName);
				Vector2 vec=new  Vector2((SourceWidth)/width, Math.Min(1.0f,SourceHeigh/height));
				renderer.material.mainTextureScale=vec;
			}
		}
		return true;	
	}
	public bool StartInput()
	{
		if(mType==SourceType.Capture)
		{
			mStart=true;
		}
		else if(mType==SourceType.File)
		{
			if(!PlayVideo(mName))
			{
				//Error report
				return false;	
			}
		}
		return true;
	}
	
	public bool StopInput()
	{
		if(mType==SourceType.Capture)
		{
			mStart=false;
		}
		else if(mType==SourceType.File)
		{
			if(!StopVideo(mName))
			{
				//Error report
				return false;	
			}
		}
		long size=width*height;
		for(long i=0; i<size;i++)
		{
			m_Pixels[i].r=0;
			m_Pixels[i].g=0;
			m_Pixels[i].b=0;
			m_Pixels[i].a=0;
		}
		VideoTexture.SetPixels32(m_Pixels);
        VideoTexture.Apply(true);
		return true;
	}
	
	protected bool viGotoTimePos(float second)
	{
		if(mType==SourceType.File)
		{
			if(!SeekVideo(mName,second))
			{
				//Error report
				return false;
			}
			return true;
		}
		//Error report
		return false;
	}
	
	protected bool viEndAtTimePos(float second)
	{
		if(mType==SourceType.File)
		{
			if(!SetEndPoint(mName,second))
			{
				//Error report
				return false;
			}
			return true;
		}
		//Error report
		return false;
	}
	
	protected bool viSetLoop(bool loop)
	{
		if(mType==SourceType.File)
		{
			if(!SetVideoLoop(mName,loop))
			{
				//Error report
				return false;
			}
			return true;
		}
		//Error report
		return false;
	}
	
	// Update is called once per frame
	protected void viUpdate()
	{
		if(mStart&&UpdateFrame(m_PixelsHandle.AddrOfPinnedObject(), mName))
		{
			VideoTexture.SetPixels32(m_Pixels, 0);
        	VideoTexture.Apply (false);
		}
	}
	
	private void viUnRegText()
	{
		UnRegisterTexture(mName);
	}
	
	protected void viDestroy()
	{
		UnRegisterTexture(mName);
		m_PixelsHandle.Free();
	}
}

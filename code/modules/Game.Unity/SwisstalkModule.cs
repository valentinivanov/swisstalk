using UnityEngine;
using Swisstalk.Platform.Bootstrapper;
using System;
using Swisstalk.Core.Blocks.Time;

public class SwisstalkModule : MonoBehaviour {

    private CoreBlocksStartupModule _module;

    void ActualizeFrameTime()
    {
        FrameTime.Now = DateTime.Now;
        FrameTime.FixedDelta = TimeSpan.FromSeconds(Time.fixedDeltaTime);
        FrameTime.FrameDelta = TimeSpan.FromSeconds(Time.deltaTime);
    }

	// Use this for initialization
	void Start () 
    {
        ActualizeFrameTime();

        _module = new CoreBlocksStartupModule();
        _module.Start();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        ActualizeFrameTime();

        if (_module != null)
        {
            _module.ActiveServices.Update(FrameTime.FixedDelta);
        }
	}

    void OnDestroy()
    {
        if (_module != null)
        {
            _module.DisposableServices.Dispose();
            _module = null;
        }
    }

    void OnApplicationPause(bool state)
    {
        if (_module != null)
        {
            if (state)
            {
                _module.SuspendableServices.Suspend();
            }
            else
            {
                _module.SuspendableServices.Resume();
            }
        }
    }
}

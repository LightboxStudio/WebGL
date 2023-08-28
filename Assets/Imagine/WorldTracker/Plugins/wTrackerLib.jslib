mergeInto(LibraryManager.library, {
    WebGLPlaceOrigin: function(camPosStr)
    {
        window.wTracker.placeOrigin(UTF8ToString(camPosStr));
    },
    WebGLResetOrigin: function()
    {
        window.wTracker.resetOrigin();
    },
    StartWebGLwTracker: function(name)
	{
    	window.wTracker.startTracker(UTF8ToString(name));
    },
    StopWebGLwTracker: function()
	{
    	window.wTracker.stopTracker();
    },
    IsWebGLwTrackerReady: function()
    {
        return window.wTracker != null;
    },
    SetWebGLwTrackerSettings: function(settings)
	{
    	window.wTracker.setTrackerSettings(UTF8ToString(settings),"1.2.2.335128");
    },
    WebGLSyncSSPos: function(vStr){
        window.wTracker.syncSSPos(UTF8ToString(vStr));
    },
}); 
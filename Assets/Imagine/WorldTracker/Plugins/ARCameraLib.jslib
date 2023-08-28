mergeInto(LibraryManager.library, {
    GetCameraFov: function()
    {
        return window.wTracker.FOV;
    },
    UnpauseWebGLCamera: function()
	{
    	window.wTracker.unpauseCamera();
    },
    PauseWebGLCamera: function()
	{
    	window.wTracker.pauseCamera();
    },
    GetWebGLCameraFrame: function(type)
    {
        var data = window.wTracker.getCameraTexture(UTF8ToString(type));
        var bufferSize = lengthBytesUTF8(data) + 1;
        var buffer =  unityInstance.Module._malloc(bufferSize);
        stringToUTF8(data, buffer, bufferSize);
        return buffer;
    },
    GetWebGLCameraName: function()
    {
        var name = window.wTracker.WEBCAM_NAME;
        var bufferSize = lengthBytesUTF8(name) + 1;
        var buffer =  unityInstance.Module._malloc(bufferSize);
        stringToUTF8(name, buffer, bufferSize);
        return buffer;
    },
    GetWebGLVideoDims: function()
    {
        var data = window.wTracker.getVideoDims();
        var bufferSize = lengthBytesUTF8(data) + 1;
        var buffer =  unityInstance.Module._malloc(bufferSize);
        stringToUTF8(data, buffer, bufferSize);
        return buffer;
    },
    ShowWebGLScreenshot: function(dataUrl)
    {
        window.ShowScreenshot(UTF8ToString(dataUrl));
    }
}); 
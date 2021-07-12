using System;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.TwoDimension;
using CefSharp;
using CefSharp.OffScreen;

public class WebBrowser {

    public bool IsReady = false;
    public bool HasImage => Image != null;
    public Texture Image = null;
    public IBrowserHost Host = null;

    private ChromiumWebBrowser _browser = null;
    private CustomRenderHandler _handler = null;

    public WebBrowser() {
        Cef.Initialize(new CefSettings(), performDependencyCheck: true, browserProcessHandler: null);
        _browser = new ChromiumWebBrowser("www.google.com");
        _browser.Size = new System.Drawing.Size(1920 - 200, 1080 - 200);
        _browser.BrowserInitialized += OnBrowserInitialized;
        _browser.Paint += OnBrowserPaint;
        _handler = new CustomRenderHandler(_browser);
        _browser.RenderHandler = _handler;
    }

    private void OnBrowserInitialized(object sender, EventArgs e) {
        Host = _browser.GetBrowserHost();
        IsReady = true;
    }

    private void OnBrowserPaint(object sender, OnPaintEventArgs e) {
        if (IsReady) {
            lock (_handler.BitmapLock) {
                BitmapBuffer buff = _handler.BitmapBuffer;
                if (buff.Buffer != null) {
                    byte[] dataIn = BGRAToRGBA(buff.Buffer, buff.Width, buff.Height);
                    TaleWorlds.Engine.Texture _engineTexture = TaleWorlds.Engine.Texture.CreateFromByteArray(dataIn, buff.Width, buff.Height);
                    Image = new Texture(new EngineTexture(_engineTexture));
                }
            }
        }
    }

    private byte[] BGRAToRGBA(byte[] input, int width, int height) {
        byte[] outputArr = new byte[width * height * 4];
        int offset = 0;
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                var temp = input[offset];
                outputArr[offset] = input[offset + 2];
                outputArr[offset + 1] = input[offset + 1];
                outputArr[offset + 2] = temp;
                outputArr[offset + 3] = input[offset + 3];
                offset += 4;
            }
        }
        return outputArr;
    }

}

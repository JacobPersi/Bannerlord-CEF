using CefSharp;
using System.Collections.Generic;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;


public class WebBrowserWidget : Widget {

    private WebBrowser Browser;
    private SimpleMaterial _cachedMaterial = null;
    private DrawObject2D _cachedDrawObject = null;
    private IBrowserHost _browserHost = null;

    public WebBrowserWidget(UIContext context) : base(context) {
        Browser = new WebBrowser();

        InputManager.Instance.RegisterKeybind(new KeyBinding(InputKey.LeftMouseButton, KeyBindingType.MouseClick), new System.Action(OnMouseClick));
    }

    protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext) {
        if (IsVisible && Browser.IsReady && Browser.HasImage) {
            float Width = Browser.Image.Width;
            float Height = Browser.Image.Height;

            if (_cachedMaterial == null) {
                _cachedMaterial = drawContext.CreateSimpleMaterial();
                _cachedMaterial.Size = new Vector2(Width, Height);
            }

            if (_cachedDrawObject == null) {
                _cachedDrawObject = DrawObject2D.CreateQuad(_cachedMaterial.Size);
            }

            Sprite sprite = new SpriteTexture(Browser.Image, (int)Width, (int)Height);
            _cachedMaterial.Texture = Browser.Image;

            drawContext.DrawSprite(sprite, _cachedMaterial, 0, 0, 1f, Width, Height, false, false);
        }
    }

    public override void HandleInput(IReadOnlyList<int> lastKeysPressed) {
        if (Browser.IsReady) {
            if (_browserHost == null)
                _browserHost = Browser.Host;

            var keyEvent = default(KeyEvent);
            keyEvent.Type = KeyEventType.Char;
            keyEvent.Modifiers = CefEventFlags.None;

            if (Input.IsKeyDown(InputKey.BackSpace)) {
                var a = true;
            }

            foreach (int keyCode in lastKeysPressed) {
                keyEvent.WindowsKeyCode = keyCode;
                _browserHost.SendKeyEvent(keyEvent);
            }
        }
    }

    private void OnMouseClick() {
        if (Browser.IsReady) {
            if (_browserHost == null)
                _browserHost = Browser.Host;
            Vec2 mousePos = Input.MousePositionPixel;
            MouseButtonType pressType = default(MouseButtonType);

            if (Input.IsKeyPressed(InputKey.LeftMouseButton))
                pressType = MouseButtonType.Left;

            if (Input.IsKeyPressed(InputKey.MiddleMouseButton))
                pressType = MouseButtonType.Middle;

            if (Input.IsKeyPressed(InputKey.RightMouseButton))
                pressType = MouseButtonType.Right;

            _browserHost.SendMouseClickEvent(new MouseEvent((int)mousePos.x, (int)mousePos.y, CefEventFlags.None), pressType, false, 0);
            _browserHost.SendMouseClickEvent(new MouseEvent((int)mousePos.x, (int)mousePos.y, CefEventFlags.None), pressType, true, 0);
        }
    }

    protected override void OnMouseScroll() {
        if (Browser.IsReady) {
            if (_browserHost == null)
                _browserHost = Browser.Host;
            Vec2 mousePos = Input.MousePositionPixel;
            float delta = Input.DeltaMouseScroll;
            _browserHost.SendMouseWheelEvent(new MouseEvent((int)mousePos.x, (int)mousePos.y, CefEventFlags.None), (int)delta, (int)delta);
        }
    }
}

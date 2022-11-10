using System;
using CefSharp;
using CefSharp.Enums;
using CefSharp.OffScreen;
using CefSharp.Structs;
using Point = System.Drawing.Point;
using Range = CefSharp.Structs.Range;
using Size = System.Drawing.Size;

namespace BannerlordCEF {
    public class CustomRenderHandler : IRenderHandler {

        private ChromiumWebBrowser browser;

        private Size popupSize;
        private Point popupPosition;

        public readonly object BitmapLock = new object();

        public bool PopupOpen { get; protected set; }

        public BitmapBuffer BitmapBuffer { get; private set; }

        public BitmapBuffer PopupBuffer { get; private set; }

        public Size PopupSize {
            get { return popupSize; }
        }

        public Point PopupPosition {
            get { return popupPosition; }
        }

        public CustomRenderHandler(ChromiumWebBrowser browser) {
            this.browser = browser;
            popupPosition = new Point();
            popupSize = new Size();
            BitmapBuffer = new BitmapBuffer(BitmapLock);
            PopupBuffer = new BitmapBuffer(BitmapLock);
        }

        public void Dispose() {
            browser = null;
            BitmapBuffer = null;
            PopupBuffer = null;
        }

        public virtual ScreenInfo? GetScreenInfo() {
            return new ScreenInfo { DeviceScaleFactor = 1.0F };
        }

        public virtual Rect GetViewRect() {
            var size = browser.Size;
            var viewRect = new Rect(0, 0, size.Width, size.Height);
            return viewRect;
        }

        public virtual bool GetScreenPoint(int viewX, int viewY, out int screenX, out int screenY) {
            screenX = viewX;
            screenY = viewY;
            return false;
        }

        public virtual void OnAcceleratedPaint(PaintElementType type, Rect dirtyRect, IntPtr sharedHandle) { }

        public virtual void OnPaint(PaintElementType type, Rect dirtyRect, IntPtr buffer, int width, int height) {
            var isPopup = type == PaintElementType.Popup;
            var bitmapBuffer = isPopup ? PopupBuffer : BitmapBuffer;
            bitmapBuffer.UpdateBuffer(width, height, buffer, dirtyRect);
        }

        public virtual void OnCursorChange(IntPtr cursor, CursorType type, CursorInfo customCursorInfo) { }

        public virtual bool StartDragging(IDragData dragData, DragOperationsMask mask, int x, int y) {
            return false;
        }

        public virtual void UpdateDragCursor(DragOperationsMask operation) { }

        public virtual void OnPopupShow(bool show) {
            PopupOpen = show;
        }

        public virtual void OnPopupSize(Rect rect) {
            popupPosition.X = rect.X;
            popupPosition.Y = rect.Y;
            popupSize.Width = rect.Width;
            popupSize.Height = rect.Height;
        }

        public virtual void OnImeCompositionRangeChanged(Range selectedRange, Rect[] characterBounds) { }

        public virtual void OnVirtualKeyboardRequested(IBrowser browser, TextInputMode inputMode) { }
    }

}

using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Engine.Screens;

public class DeveloperConsole {

    private InputManager InputManager;
    private WebBrowserLayer WebLayer = new WebBrowserLayer();
    private bool _isVisible = false;

    public DeveloperConsole() {
        InputManager = InputManager.Instance;
        InputManager.RegisterKeybind(new KeyBinding(InputKey.Tilde, KeyBindingType.KeyPress), new Action(ToggleVisiblity));
    }

    private bool DoesLayerExist() {
        bool screenExists = false;
        if (GameStateManager.Current.ActiveState.Listener is ScreenBase) {
            var screenBase = GameStateManager.Current.ActiveState.Listener as ScreenBase;
            screenExists = screenBase.HasLayer(WebLayer);
        }
        return screenExists;
    }

    private void CreateLayer() {
        if (GameStateManager.Current.ActiveState.Listener is ScreenBase) {
            var screenBase = GameStateManager.Current.ActiveState.Listener as ScreenBase;
            screenBase.AddLayer(WebLayer);
        }
    }

    private void ToggleVisiblity() {
        _isVisible = !_isVisible;
        if (_isVisible && DoesLayerExist() == false) { 
            CreateLayer();
        }
        WebLayer.Widget.IsVisible = _isVisible;
    }
}

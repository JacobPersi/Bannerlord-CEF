using System.Collections.Generic;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library; 

public class WebBrowserLayer : GauntletLayer {

    private UIContext UIContext;
    public WebBrowserWidget Widget;

    public WebBrowserLayer(int localOrder = 1, string categoryId = "GauntletLayer", bool shouldClear = false) : base(localOrder, categoryId, shouldClear) {
        UIContext = this._gauntletUIContext;
        Widget = new WebBrowserWidget(UIContext);
        UIContext.Root.Children.Add(Widget);
        InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
        Input.IsMouseButtonAllowed = true;
        Input.IsMouseWheelAllowed = true;
    }

    protected override void Update(IReadOnlyList<int> lastKeysPressed) {
        Widget.HandleInput(lastKeysPressed);
    }
}

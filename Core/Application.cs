using System;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;

public class Application {

    #region "Singleton"
    private static Application _instance;
    public static Application Instance { 
        get 
        {
            if (_instance == null)
                _instance = new Application();
            return _instance;
        } 
    }
    #endregion

    private InputManager InputManager;

    public DeveloperConsole DevConsole;

    private Application() {
        InputManager = InputManager.Instance;
        DevConsole = new DeveloperConsole();
    }

    public void Tick(float dt) {
        InputManager.Tick(dt);
    }
}

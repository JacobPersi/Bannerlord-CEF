namespace BannerlordCEF {
    public class BrowserManager {

        #region "Singleton"
        private static BrowserManager _instance;
        public static BrowserManager Instance {
            get {
                if (_instance == null)
                    _instance = new BrowserManager();
                return _instance;
            }
        }
        #endregion

        private InputManager InputManager;
        public DeveloperConsole DevConsole;

        private BrowserManager() {
            InputManager = InputManager.Instance;
            DevConsole = new DeveloperConsole();
        }

        public void Tick(float dt) {
            InputManager.Tick(dt);
        }
    }
}

using System;
using System.Collections.Generic;

namespace BannerlordCEF {
    public class InputManager {

        #region "Singleton"
        private static InputManager _instance;
        public static InputManager Instance {
            get {
                if (_instance == null)
                    _instance = new InputManager();
                return _instance;
            }
        }
        #endregion

        private Dictionary<KeyBinding, List<Action>> Keybindings = new Dictionary<KeyBinding, List<Action>>();

        private InputManager() { }

        public void RegisterKeybind(KeyBinding target, Action callback) {
            if (Keybindings.ContainsKey(target) == false) {
                Keybindings[target] = new List<Action>();
            }
            Keybindings[target].Add(callback);
        }

        public void Tick(float dt) {
            foreach (KeyBinding binding in Keybindings.Keys) {
                if (binding.ShouldFire()) {
                    foreach (Action callback in Keybindings[binding]) {
                        callback();
                    }
                }
            }
        }
    }
}

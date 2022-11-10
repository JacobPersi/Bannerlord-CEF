using TaleWorlds.InputSystem;

namespace BannerlordCEF {
    public class KeyBinding {

        public InputKey inputKey { get; private set; }
        public KeyBindingType bindingType { get; private set; }

        public KeyBinding(InputKey inputKey, KeyBindingType bindingType) {
            this.inputKey = inputKey;
            this.bindingType = bindingType;
        }

        public bool ShouldFire() {
            bool returnVal = false;
            switch (this.bindingType) {
                case KeyBindingType.MouseScroll:
                    returnVal = Input.DeltaMouseScroll > 0;
                    break;

                case KeyBindingType.KeyUp:
                    returnVal = Input.IsKeyReleased(this.inputKey);
                    break;

                case KeyBindingType.KeyDown:
                    returnVal = Input.IsKeyDown(this.inputKey);
                    break;

                case KeyBindingType.MouseClick:
                case KeyBindingType.KeyPress:
                    returnVal = Input.IsKeyPressed(this.inputKey);
                    break;

                default:
                    break;
            }
            return returnVal;
        }

    }

}

using TaleWorlds.MountAndBlade;

namespace BannerlordCEF {
    public class SubModule : MBSubModuleBase {
        private BrowserManager app;

        public SubModule() { }

        protected override void OnSubModuleLoad() {
            app = BrowserManager.Instance;
        }

        protected override void OnApplicationTick(float dt) {
            app.Tick(dt);
        }
    }
}
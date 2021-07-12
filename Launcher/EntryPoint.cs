using TaleWorlds.MountAndBlade;

public class EntryPoint : MBSubModuleBase {

    private Application app; 

    protected override void OnSubModuleLoad() {
        app = Application.Instance; 
    }

    protected override void OnApplicationTick(float dt) {
        app.Tick(dt);
    }
}

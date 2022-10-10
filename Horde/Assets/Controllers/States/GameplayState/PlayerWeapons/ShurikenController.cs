using Controllers.States.GameplayState;
using Controllers.States.GameplayState.PlayerWeapons;
using Data;
using UnityEngine;
using Views.States.GameplayState;

public class ShurikenController : GameplayControllerBase
{

    public ShurikenView ShurikenView;

    private const float ShootDelay = 0.5f;
    private float currentDelay = ShootDelay;
    
    public ShurikenController(Context context, PlayerController playerController) : base(context, playerController)
    {
        
    }

    public override void Init(GameplayView gameplayView)
    {
        ShurikenView = gameplayView as ShurikenView;
    }

    public override void OnUpdate()
    {
        ShurikenView.OnUpdate();

        currentDelay -= Time.deltaTime;
        if (currentDelay <= 0)
        {
            ShootShuriken();
            currentDelay = ShootDelay;
        }
    }

    public override void OnDestroy()
    {
        
    }

    private void ShootShuriken()
    {
        var shurikenBulletController = ControllerFactory.CreateController<ShurikenBulletController>(Context, PlayerController);
        
    }
}

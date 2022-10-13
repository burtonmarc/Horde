using Catalogs.Scripts;

namespace Data
{
    public class ShurikenModel : IModel
    {
        public float BaseDamage;

        public float HitSpeed;

        public bool AutoAim;

        public ShurikenModel(WeaponConfig weaponConfig)
        {
            BaseDamage = weaponConfig.BaseDamage;
            
            HitSpeed = weaponConfig.HitSpeed;
            
            AutoAim = weaponConfig.AutoAim;
        }
    }
}
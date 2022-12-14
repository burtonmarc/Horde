using System;
using Catalogs.Scripts.Configs;

namespace Data.Models
{
    [Serializable]
    public class ShurikenModel : IModel
    {
        public float BaseDamage;

        public float HitSpeed;

        public float ProjectileSpeed;

        public bool AutoAim;

        public ShurikenModel(WeaponConfig weaponConfig)
        {
            BaseDamage = weaponConfig.BaseDamage;
            
            HitSpeed = weaponConfig.HitSpeed;

            ProjectileSpeed = weaponConfig.ProjectileSpeed;
            
            AutoAim = weaponConfig.AutoAim;
        }
    }
}
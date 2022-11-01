using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Controllers.States.GameplayState
{
    public class EntityArgs
    {
        public EntityType EntityType;
        public GameplayControllerBase Entity;
    }
    public enum EntityType
    {
        GeneralBehaviour,
        Pickable,
        Weapon,
        Projectile,
        Enemy
    }
    public class EntitiesContainerController : GameplayControllerBase
    {
        private Dictionary<EntityType, List<GameplayControllerBase>> EntitiesContainer;

        private List<GameplayControllerBase> EntitiesToDestroyAtEndOfFrame; 

        public EntitiesContainerController(Context context) : base(context)
        {
            EntitiesContainer = new Dictionary<EntityType, List<GameplayControllerBase>>
            {
                {EntityType.Pickable, new List<GameplayControllerBase>(256)},
                {EntityType.Weapon, new List<GameplayControllerBase>(16)},
                {EntityType.Projectile, new List<GameplayControllerBase>(32)},
                {EntityType.Enemy, new List<GameplayControllerBase>(256)},
            };
            
            EntitiesToDestroyAtEndOfFrame = new List<GameplayControllerBase>(16);
            
            OnGameplayEvent += GameplayEvent;
        }

        public override void OnUpdate()
        {
            foreach (var valuePair in EntitiesContainer)
            {
                foreach (var entity in valuePair.Value)
                {
                    entity.OnUpdate();
                }
            }
        }

        public override void OnFixedUpdate()
        {
            foreach (var valuePair in EntitiesContainer)
            {
                foreach (var entity in valuePair.Value)
                {
                    if(entity.MarkedToDestroy) continue;
                    
                    entity.OnFixedUpdate();

                    if (valuePair.Key == EntityType.Enemy)
                    {
                        CheckEnemyPlayerCollision(entity);
                        CheckEnemyProjectilesCollision(entity);
                    }
                }
            }
        }

        public override void OnLateUpdate()
        {
            base.OnLateUpdate();

            for (var index = EntitiesToDestroyAtEndOfFrame.Count - 1; index >= 0; index--)
            {
                var entity = EntitiesToDestroyAtEndOfFrame[index];
                EntitiesContainer.TryGetValue(entity.EntityType, out var entityList);
                entityList?.Remove(entity);
                entity.DestroyCompletely();
                EntitiesToDestroyAtEndOfFrame.RemoveAt(index);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            
            foreach (var valuePair in EntitiesContainer)
            {
                foreach (var entity in valuePair.Value)
                {
                    entity.OnDestroy();
                }
            }
        }

        private void GameplayEvent(GameplayEvent gameplayEvent, object arg)
        {
            if (gameplayEvent == GameplayState.GameplayEvent.AddEntity)
            {
                if (arg is EntityArgs addEntityArgs)
                {
                    EntitiesContainer.TryGetValue(addEntityArgs.EntityType, out var entityList);
                    entityList?.Add(addEntityArgs.Entity);
                    addEntityArgs.Entity.EntityType = addEntityArgs.EntityType;
                }
            }

            if (gameplayEvent == GameplayState.GameplayEvent.RemoveEntity)
            {
                if (arg is EntityArgs addEntityArgs)
                {
                    EntitiesToDestroyAtEndOfFrame.Add(addEntityArgs.Entity);
                }
            }
        }

        private void CheckEnemyPlayerCollision(GameplayControllerBase enemy)
        {
            
        }
        
        private void CheckEnemyProjectilesCollision(GameplayControllerBase enemy)
        {
            EntitiesContainer.TryGetValue(EntityType.Projectile, out var projectiles);

            if (projectiles == null) return;

            foreach (var projectile in projectiles)
            {
                var distanceVector = projectile.Transform.position - enemy.Transform.position;
                if (distanceVector.sqrMagnitude < 0.03)
                {
                    projectile.OnDestroy();
                    enemy.OnDestroy();
                    return;
                }
            }
        }

        public void RemoveRandomEnemy()
        {
            EntitiesContainer.TryGetValue(EntityType.Enemy, out var enemies);
            if (enemies == null) return;
            var index = Random.Range(0, enemies.Count - 1);
            enemies[index].OnDestroy();
        }

        public void RemoveAllEnemies()
        {
            EntitiesContainer.TryGetValue(EntityType.Enemy, out var enemies);
            if (enemies == null) return;
            for (var index = enemies.Count - 1; index >= 0; index--)
            {
                var enemy = enemies[index];
                enemy.OnDestroy();
            }
        }
    }
}
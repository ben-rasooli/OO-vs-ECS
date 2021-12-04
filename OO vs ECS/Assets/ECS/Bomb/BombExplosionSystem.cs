using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class BombExplosionSystem : SystemBase
{
  protected override void OnCreate()
  {
    _ECBSys = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    _crates_entityQuery = GetEntityQuery(typeof(Crate), typeof(Translation));
  }

  protected override void OnUpdate()
  {
    var ECB = _ECBSys.CreateCommandBuffer();
    var crateEntities = _crates_entityQuery.ToEntityArray(Allocator.TempJob);
    var crateTranslations = _crates_entityQuery.ToComponentDataArray<Translation>(Allocator.TempJob);

    Entities
      .WithAll<Ready_tag>()
      .ForEach((Entity entity, in Bomb bomb, in Translation translation) =>
      {
        ECB.DestroyEntity(entity);
        for (int i = 0; i < crateTranslations.Length; i++)
        {
          var distanceToCrate = math.distance(translation.Value, crateTranslations[i].Value);
          if (distanceToCrate <= bomb.ExplosionRadius)
          {
            if (bomb.Type == BombType.Chilly)
              ECB.AddComponent(crateEntities[i], new AffectedByChillyBomb { Damage = bomb.Damage });
            else if (bomb.Type == BombType.Pepper)
              ECB.AddComponent(crateEntities[i], new AffectedByPepperBomb { Damage = bomb.Damage });
          }
        }
      })
      .WithDisposeOnCompletion(crateEntities)
      .WithDisposeOnCompletion(crateTranslations)
      .WithoutBurst().Schedule();

    _ECBSys.AddJobHandleForProducer(Dependency);
  }

  EntityCommandBufferSystem _ECBSys;
  EntityQuery _crates_entityQuery;
}
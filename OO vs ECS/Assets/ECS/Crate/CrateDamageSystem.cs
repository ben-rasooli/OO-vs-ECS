#region usings
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
#endregion

public class CrateDamageSystem : SystemBase
{
  protected override void OnCreate()
  {
    _ECBSys = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
  }

  protected override void OnUpdate()
  {
    var ECB = _ECBSys.CreateCommandBuffer();
    Entities
      .ForEach((Entity entity, in Crate crate, in AffectedByChillyBomb bomb) =>
      {
        switch (crate.Type)
        {
          case CrateType.Yellow:
            if (HasComponent<AffectedByPepperBomb_tag>(entity))
            {
              ECB.RemoveComponent<AffectedByPepperBomb_tag>(entity);
              ECB.AddSingleFrameComponent(entity, new Damage { Value = bomb.Damage });
              Debug.Log($"Yellow crate took {bomb.Damage} damage. Actual bomb's damage was {bomb.Damage}");
            }
            break;
          case CrateType.Blue:
            ECB.AddSingleFrameComponent(entity, new Damage { Value = bomb.Damage * 2 });
            Debug.Log($"Blue crate took {bomb.Damage * 2} damage. Actual bomb's damage was {bomb.Damage}");
            break;
          case CrateType.Red:
            ECB.AddComponent<AffectedByChillyBomb_tag>(entity);
            Debug.Log("Red crate got a sticky Chilly bomb");
            break;
        }
        ECB.RemoveComponent<AffectedByChillyBomb>(entity);
      })
      .WithoutBurst().Schedule();

    Entities
      .ForEach((Entity entity, in Crate crate, in AffectedByPepperBomb bomb) =>
      {
        switch (crate.Type)
        {
          case CrateType.Yellow:
            ECB.AddComponent<AffectedByPepperBomb_tag>(entity);
            Debug.Log("Yellow crate got a sticky Pepper bomb");
            break;
          case CrateType.Blue:
            ECB.AddSingleFrameComponent(entity, new Damage { Value = bomb.Damage * 2 });
            Debug.Log($"Blue crate took {bomb.Damage * 2} damage. Actual bomb's damage was {bomb.Damage}");
            break;
          case CrateType.Red:
            if (HasComponent<AffectedByChillyBomb_tag>(entity))
            {
              ECB.RemoveComponent<AffectedByChillyBomb_tag>(entity);
              ECB.AddSingleFrameComponent(entity, new Damage { Value = (int)math.ceil(bomb.Damage / 2f) });
              Debug.Log($"Red crate took {(int)math.ceil(bomb.Damage / 2f)} damage. Actual bomb's damage was {bomb.Damage}");
            }
            break;
        }
        ECB.RemoveComponent<AffectedByPepperBomb>(entity);
      })
      .WithoutBurst().Schedule();

    Entities
      .ForEach((Entity entity, ref Health health, in Damage damage) =>
      {
        health.Value -= damage.Value;
        if (health.Value <= 0)
          ECB.AddComponent<Destroy_tag>(entity);
      }).WithoutBurst().Schedule();
    _ECBSys.AddJobHandleForProducer(Dependency);
  }
  EntityCommandBufferSystem _ECBSys;
}

public struct AffectedByChillyBomb : IComponentData
{
  public int Damage;
}
public struct AffectedByChillyBomb_tag : IComponentData { }

public struct AffectedByPepperBomb : IComponentData
{
  public int Damage;
}
public struct AffectedByPepperBomb_tag : IComponentData { }
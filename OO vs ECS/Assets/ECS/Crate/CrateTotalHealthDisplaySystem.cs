using Unity.Entities;
using Unity.Jobs;

public class CrateTotalHealthDisplaySystem : SystemBase
{
  protected override void OnCreate()
  {
    _ECBSys = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
  }

  protected override void OnUpdate()
  {
    var ECB = _ECBSys.CreateCommandBuffer();
    int totalHealthOfYellowCrates = 0;
    int totalHealthOfRedCrates = 0;
    int totalHealthOfBlueCrates = 0;
    Entities
    .WithChangeFilter<Health>()
      .ForEach((in Crate crate, in Health health) =>
      {
        switch (crate.Type)
        {
          case CrateType.Yellow:
            totalHealthOfYellowCrates += health.Value;
            break;
          case CrateType.Red:
            totalHealthOfRedCrates += health.Value;
            break;
          case CrateType.Blue:
            totalHealthOfBlueCrates += health.Value;
            break;
        }
      }).Run();
    string textToDispaly = $"Yellow: {totalHealthOfYellowCrates}\nRed: {totalHealthOfRedCrates}\nBlue: {totalHealthOfBlueCrates}";
    UIManager.Instance.SetTotalHealth(textToDispaly);
    _ECBSys.AddJobHandleForProducer(Dependency);
  }
  EntityCommandBufferSystem _ECBSys;
}
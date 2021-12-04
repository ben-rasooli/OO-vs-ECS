using Unity.Entities;
using UnityEngine;

public class SingleFrameComponent_Authoring : MonoBehaviour, IConvertGameObjectToEntity
{
  public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
  {
    dstManager.AddBuffer<SingleFrameComponent>(entity);
  }
}
public struct SingleFrameComponent : IBufferElementData
{
  public ComponentType TargetComponent;
}

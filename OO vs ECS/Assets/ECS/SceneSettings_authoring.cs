using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
public class SceneSettings_authoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
  [SerializeField] GameObject _bombPrefab;
  [SerializeField] GameObject _yellowCratePrefab;
  [SerializeField] GameObject _redCratePrefab;
  [SerializeField] GameObject _blueCratePrefab;

  public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
  {
    dstManager.AddComponentData(entity, new SceneSettings
    {
      BombPrefab = conversionSystem.GetPrimaryEntity(_bombPrefab),
      YellowCratePrefab = conversionSystem.GetPrimaryEntity(_yellowCratePrefab),
      RedCratePrefab = conversionSystem.GetPrimaryEntity(_redCratePrefab),
      BlueCratePrefab = conversionSystem.GetPrimaryEntity(_blueCratePrefab)
    });
  }

  public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
  {
    referencedPrefabs.Add(_bombPrefab);
    referencedPrefabs.Add(_yellowCratePrefab);
    referencedPrefabs.Add(_redCratePrefab);
    referencedPrefabs.Add(_blueCratePrefab);
  }
}

public struct SceneSettings : IComponentData
{
  public Entity BombPrefab;
  public Entity YellowCratePrefab;
  public Entity RedCratePrefab;
  public Entity BlueCratePrefab;
}

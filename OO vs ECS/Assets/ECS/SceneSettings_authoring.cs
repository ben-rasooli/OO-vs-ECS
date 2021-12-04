using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
public class SceneSettings_authoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
  [SerializeField] GameObject _bombPrefab;
  [SerializeField] GameObject _cratePrefab;

  public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
  {
    dstManager.AddComponentData(entity, new SceneSettings
    {
      BombPrefab = conversionSystem.GetPrimaryEntity(_bombPrefab),
      CratePrefab = conversionSystem.GetPrimaryEntity(_cratePrefab)
    });
  }

  public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
  {
    referencedPrefabs.Add(_bombPrefab);
    referencedPrefabs.Add(_cratePrefab);
  }
}

public struct SceneSettings : IComponentData
{
  public Entity BombPrefab;
  public Entity CratePrefab;
}

using UnityEngine;

public class CrateSpawner : MonoBehaviour
{
  [SerializeField] GameObject _cratePrefab;

  public void Spawn(Vector3 position)
  {
    var crateController = Instantiate(_cratePrefab).GetComponent<CrateController>();
    crateController.Init(Random.Range(4, 10));
    crateController.SetPosition(position);
  }
}

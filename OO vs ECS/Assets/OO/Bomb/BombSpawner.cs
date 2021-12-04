using UnityEngine;

public class BombSpawner : MonoBehaviour
{
  [SerializeField] GameObject _bombPrefab;

  public void Spawn(Vector3 position, float explosionDelay)
  {
    float explosionRadius = UnityEngine.Random.Range(3f, 5f);
    int damage = UnityEngine.Random.Range(1, 4);
    var bombController = Instantiate(_bombPrefab, position, Quaternion.identity).GetComponent<BombController>();
    bombController.Init(explosionDelay, explosionRadius, damage);
    bombController.SetPosition(position);
  }
}

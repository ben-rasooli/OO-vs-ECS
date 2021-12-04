using UnityEngine;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
  [SerializeField] Button _chillyBombButton;
  [SerializeField] Button _pepperBombButton;
  [SerializeField] Button _yellowCrateButton;
  [SerializeField] Button _redCrateButton;
  [SerializeField] Button _blueCrateButton;

  void OnEnable()
  {
    _chillyBombButton.onClick.AddListener(() => spawnBomb(BombType.Chilly));
    _pepperBombButton.onClick.AddListener(() => spawnBomb(BombType.Pepper));
    _yellowCrateButton.onClick.AddListener(() => spawnCrate(CrateType.Yellow));
    _redCrateButton.onClick.AddListener(() => spawnCrate(CrateType.Red));
    _blueCrateButton.onClick.AddListener(() => spawnCrate(CrateType.Blue));
  }

  void OnDisable()
  {
    _chillyBombButton.onClick.RemoveAllListeners();
    _pepperBombButton.onClick.RemoveAllListeners();
    _yellowCrateButton.onClick.RemoveAllListeners();
    _redCrateButton.onClick.RemoveAllListeners();
    _blueCrateButton.onClick.RemoveAllListeners();
  }

  void Awake()
  {
    _bombSpawner = FindObjectOfType<BombSpawner>();
    _crateSpawner = FindObjectOfType<CrateSpawner>();
  }

  void spawnBomb(BombType bombType)
  {
    Vector2 randomPosition = Random.insideUnitCircle * _groundExtend;
    _bombSpawner.Spawn(
      position: new Vector3(randomPosition.x, 0, randomPosition.y),
      explosionDelay: 2f,
      bombType);
  }

  void spawnCrate(CrateType crateType)
  {
    Vector2 randomPosition = Random.insideUnitCircle * _groundExtend;
    _crateSpawner.Spawn(new Vector3(randomPosition.x, 0, randomPosition.y), crateType);
  }

  float _groundExtend = 7f;
  BombSpawner _bombSpawner;
  CrateSpawner _crateSpawner;
}

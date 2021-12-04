using UnityEngine;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
  [SerializeField] Button _bombButton;
  [SerializeField] Button _yellowCrateButton;
  [SerializeField] Button _redCrateButton;
  [SerializeField] Button _blueCrateButton;

  void OnEnable()
  {
    _bombButton.onClick.AddListener(spawnBomb);
    _yellowCrateButton.onClick.AddListener(() => spawnCrate(CrateType.Yellow));
    _redCrateButton.onClick.AddListener(() => spawnCrate(CrateType.Red));
    _blueCrateButton.onClick.AddListener(() => spawnCrate(CrateType.Blue));
  }

  void OnDisable()
  {
    _bombButton.onClick.RemoveAllListeners();
    _yellowCrateButton.onClick.RemoveAllListeners();
    _redCrateButton.onClick.RemoveAllListeners();
    _blueCrateButton.onClick.RemoveAllListeners();
  }

  void Awake()
  {
    _bombSpawner = FindObjectOfType<BombSpawner>();
    _crateSpawner = FindObjectOfType<CrateSpawner>();
  }

  void spawnBomb()
  {
    Vector2 randomPosition = Random.insideUnitCircle * _groundExtend;
    _bombSpawner.Spawn(
      position: new Vector3(randomPosition.x, 0, randomPosition.y),
      explosionDelay: 2f);
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

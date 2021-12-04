using UnityEngine;

public class CrateController : MonoBehaviour
{
  [SerializeField] Vector3 _infoTextOffset;

  public void TakeDamage(int value)
  {
    setHealth(_health - value);
    if (_health <= 0)
      Destroy(gameObject);
  }

  public Vector3 Position => transform.position;

  public void Init(int health)
  {
    setHealth(health);
  }

  public void SetPosition(Vector3 value)
  {
    transform.position = value;
    _UIManager.SetUITextPosition(gameObject.GetInstanceID(), _mainCamera.WorldToScreenPoint(value) + _infoTextOffset);
  }

  void OnEnable()
  {
    _UIManager = FindObjectOfType<UIManager>();
    _mainCamera = Camera.main;
  }

  void OnDisable()
  {
    _UIManager.RemoveUIText(gameObject.GetInstanceID());
  }

  void setHealth(int value)
  {
    _health = value;
    _UIManager.SetUIText(gameObject.GetInstanceID(), _health.ToString());
  }

  UIManager _UIManager;
  Camera _mainCamera;
  int _health;
}

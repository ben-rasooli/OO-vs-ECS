using UnityEngine;

public abstract class CrateController : MonoBehaviour
{
  [SerializeField] Vector3 _infoTextOffset;

  public abstract void TakeDamage(BombType _myType, int value);

  public Vector3 Position => transform.position;

  public void Init(int health, CrateType type)
  {
    setHealth(health);
    _myType = type;
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

  protected void setHealth(int value)
  {
    _health = value;
    _UIManager.SetUIText(gameObject.GetInstanceID(), _health.ToString());
  }

  UIManager _UIManager;
  Camera _mainCamera;
  protected int _health;
  protected CrateType _myType;
}

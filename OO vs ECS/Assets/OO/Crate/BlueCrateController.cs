using UnityEngine;

public class BlueCrateController : CrateController
{
  public override void TakeDamage(BombType bombType, int value)
  {
    var damage = value * 2;
    setHealth(_health - damage);
    if (_health <= 0)
      Destroy(gameObject);
    Debug.Log($"{_myType} crate took {damage} damage. Actual bomb's damage was {value}");
  }
}
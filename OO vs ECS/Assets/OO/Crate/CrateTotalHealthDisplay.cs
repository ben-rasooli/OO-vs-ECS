using System.Collections.Generic;
using UnityEngine;

public class CrateTotalHealthDisplay : MonoBehaviour
{
  public void AddCrateController(CrateController crateController)
  {
    if (crateControllers.IndexOf(crateController) < 0)
      crateControllers.Add(crateController);
  }

  public void RemoveCrateController(CrateController crateController)
  {
    crateControllers.Remove(crateController);
  }

  void Update()
  {
    int totalHealthOfYellowCrates = 0;
    int totalHealthOfRedCrates = 0;
    int totalHealthOfBlueCrates = 0;
    foreach (var crate in crateControllers)
    {
      switch (crate.Type)
      {
        case CrateType.Yellow:
          totalHealthOfYellowCrates += crate.Health;
          break;
        case CrateType.Red:
          totalHealthOfRedCrates += crate.Health;
          break;
        case CrateType.Blue:
          totalHealthOfBlueCrates += crate.Health;
          break;
      }
      string textToDispaly = $"Yellow: {totalHealthOfYellowCrates}\nRed: {totalHealthOfRedCrates}\nBlue: {totalHealthOfBlueCrates}";
      UIManager.Instance.SetTotalHealth(textToDispaly);
    }
  }

  List<CrateController> crateControllers = new List<CrateController>();
}

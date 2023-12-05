using UnityEngine;

public class FighterMoney : MonoBehaviour
{
  public static int deathEnemyCount = 0;

  // шанс випадіння монети    
  public static int GetChance()
  {
    return 100;
  }

  // кількість монет яка випадає за раз
  public static int GetCoinCount()
  {
    return 1 + deathEnemyCount / 4;
  }

  private void Awake()
  {
    deathEnemyCount = 0;
  }
}

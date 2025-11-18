using UnityEngine;

public abstract class EnemyBaseState 
{
  public abstract  void EnterState(EnemyStateManager Emeny);
  public abstract  void UpdateState(EnemyStateManager Emeny);
  public abstract  void OnCollisionEnter(EnemyStateManager Emeny);

    
}

using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour {
    public delegate void EnemyDeathHandler(AbstractEnemy enemy);
    public event EnemyDeathHandler OnDeath;
    protected void DeathEvent() {
        if (OnDeath != null) {
            OnDeath(this);
        }
    }

    public delegate void EnemyAttackHandler(AbstractEnemy enemy, System.Type enemyType);
    public event EnemyAttackHandler OnAttack;
    protected void AttackEvent() {
        if (OnAttack != null) {
            OnAttack(this, this.GetType());
        }
    }

    public delegate bool PerformanceEvaluator();
    public abstract bool EvaluatePerformance();

    public Hero Player { get; set; }
}

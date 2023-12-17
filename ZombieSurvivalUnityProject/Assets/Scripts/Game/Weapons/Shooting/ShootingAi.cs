using UnityEngine;

// TODO: temp class for tests, refactor to full health/enemy system
public class ShootingAi : MonoBehaviour
{
    public void TakeDamage(int damage)
    {
        print($"{gameObject.name} took {damage} damage");
    }
}

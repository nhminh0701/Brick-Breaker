using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private bool isDamagable;
    private int maxHp;
    [SerializeField] private int hp;

    [SerializeField] private IntReference currentNumberOfBlock;

    private void Start()
    {
        maxHp = hp;
    }

    private void OnEnable()
    {
        if (isDamagable) currentNumberOfBlock.Value++;
    }

    public void TakeDamage()
    {
        if (!isDamagable) return;

        hp--;
        if (hp < 0)
        {
            KillBlock();
        }
    }

    private void KillBlock()
    {
        currentNumberOfBlock.Value--;
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        hp = maxHp;
    }
}

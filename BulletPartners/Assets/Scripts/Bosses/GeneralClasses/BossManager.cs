using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> gunDrops;

    public void Death()
    {
        if(gunDrops.Count < 0)
        {
            Instantiate(gunDrops[Random.Range(0, gunDrops.Count)]);
        }
        Destroy(gameObject);
        FindAnyObjectByType<S_GameManager>().FinishRound();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private S_Camera cam;

    private void Awake()
    {
        cam = FindAnyObjectByType<S_Camera>();
    }
    void Update()
    {
        
    }

    public void Die()
    {
        cam.playerList.Remove(gameObject);
        Destroy(gameObject);
    }
}

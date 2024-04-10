using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobObject : MonoBehaviour
{
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    [SerializeField] private Transform middlePoint;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalOffset = Mathf.PingPong(Time.time * frequency, amplitude);

        transform.position = new Vector3(transform.position.x, middlePoint.position.y + verticalOffset, transform.position.z);
    }
}

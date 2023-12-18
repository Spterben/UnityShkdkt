using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;
    private void Awake()
    {
        if(!player)
            player = FindObjectOfType<Cat>().transform;
    }

    // Update is called once per frame
    private void Update()
    {
        pos = player.position;
        pos.z = -10f;
        pos.y += 1f;
        pos.x += 2f;

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToPlayer : DAS_Action
{
    public bool lookToPlayer = false;
    public Transform player;
    public float rotationSpeed = 4f;

    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    public override void execute()
    {
        lookToPlayer = !lookToPlayer;
    }

    void Update()
    {
        if (lookToPlayer)
        {
            Quaternion targetDir = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetDir, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

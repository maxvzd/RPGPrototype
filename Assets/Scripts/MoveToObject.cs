using UnityEngine;

public class MoveToObject : MonoBehaviour
{
    [SerializeField] private bool moveX;
    [SerializeField] private bool moveY;
    [SerializeField] private bool moveZ;
    [SerializeField] private Transform moveToTarget;
    [SerializeField] private float yOffset;

    public void Update()
    {
        var position = transform.position;
        if (moveX) position.x = moveToTarget.transform.position.x;
        if (moveY) position.y = moveToTarget.transform.position.y + yOffset;
        if (moveZ) position.z = moveToTarget.transform.position.z;

        transform.position = position;
    }
}

using UnityEngine;

public class MatchRotation : MonoBehaviour
{
    [SerializeField] private Transform objectToMatch;
    [SerializeField] private bool matchX;
    [SerializeField] private bool matchY;
    [SerializeField] private bool matchZ;

    private void Update()
    {
        var rotation = transform.eulerAngles;
        var objectsRotation = objectToMatch.eulerAngles;

        if (matchX) rotation.x = objectsRotation.x;
        if (matchY) rotation.y = objectsRotation.y;
        if (matchZ) rotation.z = objectsRotation.z;
        
        transform.rotation = Quaternion.Euler(rotation);
    }
}
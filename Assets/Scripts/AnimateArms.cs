using Constants;
using UnityEngine;

public class AnimateArms : MonoBehaviour
{
    [SerializeField] private Animator mainAnimator; 
    private Animator _fpArmAnimator; 
    
    public void Start()
    {
        _fpArmAnimator = GetComponent<Animator>();
    }

    public void Update()
    {
        var horizontalKey = AnimatorConstants.Horizontal;
        var verticalKey = AnimatorConstants.Vertical;
        
        _fpArmAnimator.SetFloat(horizontalKey, mainAnimator.GetFloat(horizontalKey));
        _fpArmAnimator.SetFloat(verticalKey, mainAnimator.GetFloat(verticalKey));
    }
}

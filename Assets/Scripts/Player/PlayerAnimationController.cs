using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private string runAnimationParameterName;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }


    public void SetSpeed(float speed)
    {
        _animator.SetFloat(runAnimationParameterName, Mathf.Abs(speed));
    }
}
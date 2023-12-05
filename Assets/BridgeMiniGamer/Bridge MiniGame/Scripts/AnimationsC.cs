using Pixelplacement;
using UnityEngine;

public class AnimationsC : Singleton<AnimationsC>
{
    [HideInInspector]
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimatorBool(string name, bool state)
    {
        if (name == "onBridge")
            transform.rotation = Quaternion.identity;
        animator.SetBool(name, state);
    }

}

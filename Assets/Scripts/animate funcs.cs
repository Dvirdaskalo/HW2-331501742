using UnityEngine;

public interface IAnimated
{
    public bool IsAnimationFinised(Animator animator, string AnimationName)
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        
        if (!info.IsName(AnimationName)) return true;
        
        if (info.normalizedTime >= 1)
            return true;
        
        return false;
    }
}

using UnityEngine;
using System.Collections;

public class ChangeEmotionalState : MonoBehaviour {
    public int state=4;
    private Animator animator;
    public bool changeEmotionalStateFear=false;
    public bool changeEmotionalStateNormal = false;
	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (changeEmotionalStateFear)
        {
            animator.SetInteger("emotion", state);
            changeEmotionalStateFear = false;
        }
        if(changeEmotionalStateNormal)
        {
            animator.SetInteger("emotion", 0);
            changeEmotionalStateNormal = false;
        }
	}
}

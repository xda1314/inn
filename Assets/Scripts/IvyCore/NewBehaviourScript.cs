using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    Spine.Unity.SkeletonAnimation anim_;
	// Use this for initialization
	void Start () {
        anim_ = this.GetComponent<Spine.Unity.SkeletonAnimation>();

    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Q))
        {
            anim_.Skeleton.SetSkin("brick_color");
            anim_.Skeleton.SetToSetupPose();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim_.Skeleton.SetSkin("bomb_cross");
            anim_.Skeleton.SetToSetupPose();
        }
    }
}

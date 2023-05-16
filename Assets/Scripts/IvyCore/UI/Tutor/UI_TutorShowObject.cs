using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using DG.Tweening;

namespace IvyCore
{
    [System.Serializable, ShowOdinSerializedPropertiesInInspector]
    public class UI_TutorShowObject : SerializedMonoBehaviour
    {
        public void RunEnterAction()
        {
            if (EnterAction_ != null)
            {
                EnterTween_ = EnterAction_.Run(this.gameObject);
            }
        }
        public void RunOutAction()
        {
            if(EnterTween_!=null)
            {
                EnterTween_.Kill();
            }
            if(OutAction_!=null)
            {
                OutAction_.Run(this.gameObject);
            }
        }
        Tween EnterTween_ = null;
        [LabelText("进场动作"),OdinSerialize,System.NonSerialized,SerializeField,ShowInInspector]
        ActionSequence EnterAction_=null;
        [LabelText("出场动作"), OdinSerialize, System.NonSerialized, SerializeField, ShowInInspector]
        ActionSequence OutAction_=null;
    }
}
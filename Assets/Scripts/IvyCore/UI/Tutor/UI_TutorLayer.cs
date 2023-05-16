using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IvyCore
{
    public class UI_TutorLayer : MonoBehaviour
    {
        public Image MaskLayer_;
        [HideInInspector]
        public UI_RayIgnore UI_RayIgnore_ { get; set; }

        [SerializeField]
        public GameObject DialogObj;
        [SerializeField]
        private GameObject HeadObj;
        [SerializeField]
        private TextMeshProUGUI NameText;
        [SerializeField]
        private TextMeshProUGUI DialogText;
        public void Start()
        {
            if (MaskLayer_ != null)
            {
                UI_RayIgnore_ = MaskLayer_.GetComponent<UI_RayIgnore>();
            }
            if (DialogObj != null)
                DialogObj.SetActive(false);
        }

        public void SetDialogByContent(string strDialog, float posy)
        {
            if (DialogObj != null)
            {
                DialogObj.SetActive(true);
                if (NameText != null)
                    NameText.text = I2.Loc.ScriptLocalization.Get("Obj/Role/Name/C1");
                var pos = DialogObj.transform.localPosition;
                DialogObj.transform.localPosition = new Vector3(pos.x, posy, 0);
                DialogText.text = strDialog;
            }
        }

        public void SetDialogByKey(string strKey, float posy)
        {
            if (DialogObj != null)
            {
                DialogObj.SetActive(true);
                if (NameText != null)
                    NameText.text = I2.Loc.ScriptLocalization.Get("Obj/Role/Name/C1");
                var pos = DialogObj.transform.localPosition;
                DialogObj.transform.localPosition = new Vector3(pos.x, posy, 0);
                DialogText.text = I2.Loc.ScriptLocalization.Get(strKey);
            }
        }

        public void HideDialog()
        {
            if (DialogObj != null)
                DialogObj.SetActive(false);
        }
    }
}

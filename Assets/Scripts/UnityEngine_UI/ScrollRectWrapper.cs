// ILSpyBased#2
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class ScrollRectWrapper : ScrollRect
    {
        private bool _disabled;

        private int touchNum;

        private int touchIdx;

        private int[] touchData = new int[10];

        private PointerEventData[] lastEventData = new PointerEventData[10];

        public bool disabled
        {
            set
            {
                this._disabled = value;
            }
        }

        private ScrollRectWrapper()
        {
            for (int i = 0; i < 10; i++)
            {
                this.touchData[0] = 0;
            }
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (this.touchNum == 0)
            {
                this.touchIdx = 0;
                base.OnBeginDrag(eventData);
            }
            this.touchNum++;
            if (eventData.pointerId >= 0 && eventData.pointerId < 10)
            {
                this.touchData[eventData.pointerId] = 1;
            }
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (eventData.pointerId >= 0 && eventData.pointerId < 10)
            {
                this.lastEventData[eventData.pointerId] = eventData;
            }
            if (eventData.pointerId == this.touchIdx)
            {
                base.OnDrag(eventData);
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerId >= 0 && eventData.pointerId < 10)
            {
                this.touchData[eventData.pointerId] = 0;
            }
            bool flag = false;
            int num = this.touchIdx;
            if (this.touchIdx == eventData.pointerId)
            {
                int num2 = 0;
                while (num2 < 10)
                {
                    if (this.touchData[num2] != 1)
                    {
                        num2++;
                        continue;
                    }
                    flag = true;
                    num = this.touchIdx;
                    this.touchIdx = num2;
                    break;
                }
            }
            if (flag)
            {
                if (this.lastEventData[num] != null)
                {
                    base.OnEndDrag(this.lastEventData[num]);
                }
                if (this.lastEventData[this.touchIdx] != null)
                {
                    base.OnBeginDrag(this.lastEventData[this.touchIdx]);
                }
            }
            else
            {
                base.OnEndDrag(eventData);
            }
            this.lastEventData[num] = null;
            this.touchNum--;
        }
    }
}



// ILSpyBased#2
using BDUnity.Utils;
using System;
using System.Collections.Generic;

namespace ivy.game {
[Serializable]
public class ElementConfig
{
    public ElementType type;

    public int subType;

    public ElementColor color;

    public int hp;

    public int rowSpan;

    public int colSpan;

    public ElementColor targetColor;

    public int targetNum;

    public int colorGroupId;

    public Direction direction;

    public ElementConfig()
    {
        this.type = ElementType.None;
        this.subType = 0;
        this.color = ElementColor.None;
        this.hp = 1;
        this.rowSpan = 1;
        this.colSpan = 1;
        this.targetColor = ElementColor.None;
        this.targetNum = 0;
        this.colorGroupId = 0;
        this.direction = Direction.None;
    }

    public ElementConfig(ElementType type, ElementColor color, int hp = 1, int subType = 0)
    {
        this.subType = subType;
        this.hp = hp;
        this.rowSpan = 1;
        this.colSpan = 1;
        this.targetColor = ElementColor.None;
        this.targetNum = 0;
        this.colorGroupId = 0;
        this.type = type;
        this.color = color;
        this.direction = Direction.None;
    }

    public ElementConfig(BombType bombType, ElementColor color)
    {
        this.type = ElementType.Bomb;
        this.hp = ((bombType != BombType.Square) ? 1 : 2);
        this.rowSpan = 1;
        this.colSpan = 1;
        this.targetColor = ElementColor.None;
        this.targetNum = 0;
        this.colorGroupId = 0;
        this.subType = (int)bombType;
        this.color = color;
        this.direction = Direction.None;
    }

    public ElementConfig(Dictionary<string, CustomJSONObject> json)
    {
        this.UpdateDateByJson(json);
    }

    public void UpdateDateByJson(Dictionary<string, CustomJSONObject> json)
    {
        this.type = (ElementType)json["type"].i;
        if (json.ContainsKey("subType"))
        {
            this.subType = json["subType"].i;
        }
        else
        {
            this.subType = 0;
        }
        if (json.ContainsKey("color"))
        {
            this.color = (ElementColor)json["color"].i;
        }
        else
        {
            this.color = ElementColor.None;
        }
        if (json.ContainsKey("hp"))
        {
            this.hp = json["hp"].i;
        }
        else
        {
            this.hp = ((this.type != ElementType.Bomb || this.subType != 3) ? 1 : 2);
        }
        if (json.ContainsKey("rowSpan"))
        {
            this.rowSpan = json["rowSpan"].i;
        }
        else
        {
            this.rowSpan = 1;
        }
        if (json.ContainsKey("colSpan"))
        {
            this.colSpan = json["colSpan"].i;
        }
        else
        {
            this.colSpan = 1;
        }
        if (json.ContainsKey("targetColor"))
        {
            this.targetColor = (ElementColor)json["targetColor"].i;
        }
        else
        {
            this.targetColor = ElementColor.None;
        }
        if (json.ContainsKey("targetNum"))
        {
            this.targetNum = json["targetNum"].i;
        }
        else
        {
            this.targetNum = 0;
        }
        if (json.ContainsKey("colorGroupId"))
        {
            this.colorGroupId = json["colorGroupId"].i;
        }
        else
        {
            this.colorGroupId = 0;
        }
        if (json.ContainsKey("direction"))
        {
            this.direction = (Direction)json["direction"].i;
        }
        else
        {
            this.direction = Direction.None;
        }
    }

    public bool IsSameType(ElementConfig config)
    {
        if (config != null && this.type == config.type && this.subType == config.subType && this.color == config.color)
        {
            return true;
        }
        return false;
    }

    public string GetResource()
    {
        string text = null;
        ElementType elementType = this.type;
        if (elementType == ElementType.Jelly)
        {
            text = "jelly";
        }
        if (text != null)
        {
            text = ((this.color > ElementColor.None) ? (text + (int)this.color) : (text + (int)(this.color + 1)));
        }
        return text;
    }

    public CustomJSONObject GetJsonData()
    {
        CustomJSONObject mTJSONObject = CustomJSONObject.CreateDict();
        mTJSONObject.Add("type", (int)this.type);
        mTJSONObject.Add("color", (int)this.color);
        if (this.subType > 0)
        {
            mTJSONObject.Add("subType", this.subType);
        }
        if (this.hp > 1)
        {
            mTJSONObject.Add("hp", this.hp);
        }
        if (this.rowSpan > 1)
        {
            mTJSONObject.Add("rowSpan", this.rowSpan);
        }
        if (this.colSpan > 1)
        {
            mTJSONObject.Add("colSpan", this.colSpan);
        }
        if (this.targetColor != 0)
        {
            mTJSONObject.Add("targetColor", (int)this.targetColor);
            mTJSONObject.Add("targetNum", this.targetNum);
        }
        if (this.type == ElementType.Jelly && this.subType == 1)
        {
            mTJSONObject.Add("colorGroupId", this.colorGroupId);
        }
        if (this.direction != Direction.None)
        {
            mTJSONObject.Add("direction", (int)this.direction);
        }
        return mTJSONObject;
    }
}


}

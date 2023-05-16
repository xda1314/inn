using UnityEngine;

public class ImageSpritesContainer : MonoBehaviour
{
    [SerializeField] private Sprite[] img_sprites;
    public Sprite GetSprite(string sprite_name) 
    {
        foreach (var item in img_sprites)
        {
            if (sprite_name == item.name)
                return item;
        }
        return null;
    }
}

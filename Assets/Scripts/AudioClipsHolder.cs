// ILSpyBased#2
using System.Collections.Generic;
using UnityEngine;

namespace ivy.game {
public class AudioClipsHolder : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<string, AudioClip> audioPool;

    public List<AudioClip> soundList = new List<AudioClip>();

    private void Start()
    {
        this.audioPool = new Dictionary<string, AudioClip>();
        for (int i = 0; i < this.soundList.Count; i++)
        {
            if ((Object)this.soundList[i] != (Object)null)
            {
                this.audioPool.Add(this.soundList[i].name, this.soundList[i]);
            }
        }
    }

    public AudioClip TryGetAudioClip(string name)
    {
        AudioClip result = default(AudioClip);
        if (this.audioPool.TryGetValue(name, out result))
        {
            return result;
        }
        return null;
    }
}


}

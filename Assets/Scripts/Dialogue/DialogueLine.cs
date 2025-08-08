using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public CharacterInfo speaker;
    public AudioClip audioClip; 
    [TextArea(3, 10)] public string dialogueText;
}

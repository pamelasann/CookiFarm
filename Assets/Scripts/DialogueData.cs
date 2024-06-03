using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "DialogueData", order = 51)]
public class DialogueData : ScriptableObject
{
    [TextArea]
    public List<string> dialogueMessages;
}


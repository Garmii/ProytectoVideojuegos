using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue/DialogueObject")]

public class DialogueObject : ScriptableObject //No es un script para gameObject
{
    [SerializeField] [TextArea] private string[] dialogue;

    public string[] Dialogue => dialogue;

}

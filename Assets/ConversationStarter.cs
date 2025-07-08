using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myconversation;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (Input.GetKeyDown(KeyCode.F)) 
            {
                ConversationManager.Instance.StartConversation(myconversation);
            }
        }
    }
}

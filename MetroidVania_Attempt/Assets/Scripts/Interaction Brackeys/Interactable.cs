using UnityEngine;

public class Interactable : MonoBehaviour
{
    //open a door, pick an item, go through a fog gate
    public Transform player;
    public float radius = 3f;

    bool hasInteracted;

    public virtual void Interact()
    {
        // this method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }
    private void Update()
    {

        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= radius && Input.GetKeyDown(KeyCode.W) && !hasInteracted)
        {
            Debug.Log("Interact");
            Interact();
            hasInteracted = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

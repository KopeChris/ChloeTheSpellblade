using UnityEngine;
using TMPro;

public class Indicator : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI indicator;
    public float radius = 3f;
    public float startRadius;

    private void Start()
    {
        startRadius = radius;
    }
    private void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= radius)  //indication
        {
            indicator.enabled = true;
            radius = 2 * startRadius;
        }
        if (distance <= radius && Input.GetButtonDown("Interact"))  //action
        {
            Interact();
        }
        if(distance>radius)
        {
            radius = startRadius;
            indicator.enabled = false;
        }
    }
    public virtual void Interact()
    {
        // this method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

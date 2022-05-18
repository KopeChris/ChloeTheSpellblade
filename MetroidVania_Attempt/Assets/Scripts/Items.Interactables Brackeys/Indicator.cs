using UnityEngine;
using TMPro;

public class Indicator : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI text;
    public float radius = 3f;
    float startRadius;


    private void Awake()
    {
        startRadius = radius;
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {

        float distance = Vector2.Distance(player.position, transform.position);


        if (distance <= radius)  //indication
        {
            text.enabled = true;
            radius = 2 * startRadius;
        }
        if (distance <= radius && Input.GetKeyDown(KeyCode.Y))  //action
        {
            Interact();
        }
        if(distance>radius)
        {
            text.enabled = false;
            radius = startRadius;
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

using UnityEngine;
using TMPro;

public class Indicator : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI indicator;
    public float radius = 4f;

    
    private void Update()
    {
        float distance = Vector2.Distance(new Vector2(PlayerBasic.positionX,PlayerBasic.positionY), transform.position);

        if (distance <= radius)  //indication
        {
            indicator.enabled = true;
            radius = 8;
        }
        if (distance <= radius && Input.GetButtonDown("Interact"))  //action
        {
            Interact();
        }
        if(distance>radius)
        {
            radius = 4;
            indicator.enabled = false;
        }
    }
    public virtual void Interact()
    {
        // this method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);

        indicator.gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

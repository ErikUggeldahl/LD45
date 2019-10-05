using UnityEngine;

public class Accretion : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.tag == "Brush" && collision.transform.parent == null)
        {
            transform.root.GetComponent<PlayerAccretion>().AddBrush(collision.transform.transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    
    public virtual void GetDamage()
    {
        
    }

    public virtual void Win()
    {
        Invoke("SetWinPanel", 1.1f);
        Time.timeScale = 0;
    }


    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Entity>())
            collision.gameObject.GetComponent<Entity>().Die();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaktus : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision) // метод, который при столкновении Kolision моделей кота и кактуса отнимает 1HP у кота
    {
        if (collision.gameObject == Cat.Instance.gameObject)
        {
            Cat.Instance.GetDamage();
        } 
        
    }

}

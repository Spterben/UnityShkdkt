using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaktus : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision) // �����, ������� ��� ������������ Kolision ������� ���� � ������� �������� 1HP � ����
    {
        if (collision.gameObject == Cat.Instance.gameObject)
        {
            Cat.Instance.GetDamage();
        } 
        
    }

}

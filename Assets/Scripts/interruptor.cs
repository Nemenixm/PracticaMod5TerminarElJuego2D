using UnityEngine;
using UnityEngine.InputSystem;

public class interruptor : MonoBehaviour
{
public LightController controller;
public bool apagarAlEntrar = true;

private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        if (apagarAlEntrar) controller.ApagarLuz();
        else controller.EncenderLuz();
    }
}
   
}

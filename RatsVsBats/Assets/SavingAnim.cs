using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingAnim : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        // Desactiva el objeto al inicio
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // Activa el objeto
        gameObject.SetActive(true);
        // Activa la animación de entrada automáticamente
        if (anim != null)
        {
            anim.Play("Saving", 0, 0); // Reemplaza "NombreDeTuAnimacion" con el nombre de tu animación
        }
    }
}

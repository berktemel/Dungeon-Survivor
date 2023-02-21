using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public static GameObject damageNumber;
    private TextMeshPro textMesh;
    private const float Y_SPEED = 0.2f;
    private void Awake()
    {
        textMesh = damageNumber.GetComponent<TextMeshPro>();
    }

    public void Setup(float damage)
    {
        textMesh.SetText(damage.ToString());
    }

    public static DamagePopup Create(Vector3 pos, float damage)
    {
        /*GameObject popup = Instantiate(damageNumber, pos, Quaternion.identity);
        DamagePopup damagePopup = popup.GetComponent<DamagePopup>();
        damagePopup.Setup(damage);*/
        damageNumber.GetComponent<TextMeshPro>().SetText(damage.ToString());
        GameObject popupGameObject = Instantiate(damageNumber, pos, Quaternion.identity);
        DamagePopup damagePopup = popupGameObject.GetComponent<DamagePopup>();
        Destroy(damageNumber, 2f);
        return damagePopup;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, Y_SPEED, 0) * Time.deltaTime;
    }
}

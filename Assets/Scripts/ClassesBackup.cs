using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ClassesBackup : MonoBehaviour
{
    // Start is called before the first frame update\\
    private void Awake()
    {
        Debug.Log("(Awk) Bacon egg and cheesse");
    }
    private void Start()
    {
        Debug.Log("(Str) Supa");
    }

    private void Update()
    {
        Debug.Log("(Up) Sea Salt!");
    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        Debug.Log("(LatUp) Are you sure?");
    }

    private void OnEnable()
    {
        Debug.Log("(OnEn) Coming in hot!");
    }

    private void OnDisable()
    {
        Debug.Log("(OnDis) He's behind me, isn't he?");
    }

    private void OnDestroy()
    {
        Debug.Log("(OnDes) ");
    }
}

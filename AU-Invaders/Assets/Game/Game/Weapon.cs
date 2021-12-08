using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;
    test t1 = new test();

    public Button gun1;
    public Button gun2;
    public Button gun3;

    public int lastGunSelected;

    //scattergun
    public int pelletamount = 7;
    public int spread = 1;

    //fire rate
    public struct FireRate
    {
        public static bool canShoot1 = true;
        public static bool canShoot2 = true;
        public static bool canShoot3 = true;
        public static float delayInSeconds1 = 1;
        public static float delayInSeconds2 = 5;
        public static float delayInSeconds3 = 10;
    };

    private void Start()
    {
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gun1.gameObject, pointer, ExecuteEvents.pointerDownHandler);
        changeLastGun(1, pointer);
    }
    // Update is called once per frame
    void Update()
    {
        var pointer = new PointerEventData(EventSystem.current);

        //weapon selector
        if (Input.GetKeyDown("1"))
        {
            ExecuteEvents.Execute(gun1.gameObject, pointer, ExecuteEvents.pointerDownHandler);
            changeLastGun(1, pointer);
        }
        else if (Input.GetKeyDown("2"))
        {
            ExecuteEvents.Execute(gun2.gameObject, pointer, ExecuteEvents.pointerDownHandler);
            changeLastGun(2, pointer);
        }
        else if (Input.GetKeyDown("3"))
        {   
            ExecuteEvents.Execute(gun3.gameObject, pointer, ExecuteEvents.pointerDownHandler);
            changeLastGun(3, pointer);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    void changeLastGun(int gun, PointerEventData pointer)//Stops the last weapon's button from being pressed
    {
        if (lastGunSelected == 1 && gun!= 1)
        {
            ExecuteEvents.Execute(gun1.gameObject, pointer, ExecuteEvents.pointerUpHandler);
        }
        else if (lastGunSelected == 2 && gun != 2)
        {
            ExecuteEvents.Execute(gun2.gameObject, pointer, ExecuteEvents.pointerUpHandler);
        }
        else if (lastGunSelected == 3 && gun != 3)
        {
            ExecuteEvents.Execute(gun3.gameObject, pointer, ExecuteEvents.pointerUpHandler);
        }
        lastGunSelected = gun;
        
    }
    void Shoot()
    {
        if (lastGunSelected == 1 && FireRate.canShoot1)//laser gun
        {
            FireRate.canShoot1 = false;
            StartCoroutine(t1.ShootDelay1());

            Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);

        }
        else if (lastGunSelected == 2 && FireRate.canShoot2)//scattergun
        {
            Quaternion newRot = FirePoint.rotation;
            for (int i = 0; i <= pelletamount; i++)
            {
                float addedOffset = (i - (pelletamount / 2) * spread);

                // Then add "addedOffset" to whatever rotation axis the player must rotate on
                newRot = Quaternion.Euler(FirePoint.transform.localEulerAngles.x,
                FirePoint.transform.localEulerAngles.y,
                FirePoint.transform.localEulerAngles.z + addedOffset);

                Instantiate(BulletPrefab, FirePoint.position, newRot);


                // Then add "addedOffset" to whatever rotation axis the player must rotate on
                newRot = Quaternion.Euler(FirePoint.transform.localEulerAngles.x,
                FirePoint.transform.localEulerAngles.y,
                FirePoint.transform.localEulerAngles.z - addedOffset);
                Instantiate(BulletPrefab, FirePoint.position, newRot);


            }
            FireRate.canShoot2 = false;
            StartCoroutine(t1.ShootDelay2());
        }
        else if (lastGunSelected == 3 && FireRate.canShoot3)//railgun
        {
            FireRate.canShoot3 = false;
            StartCoroutine(t1.ShootDelay3());

        }
    }

    public class test
    {

        public IEnumerator ShootDelay1()
        {
            yield return new WaitForSeconds(FireRate.delayInSeconds1);
            FireRate.canShoot1 = true;
        }
        public IEnumerator ShootDelay2()
        {
            yield return new WaitForSeconds(FireRate.delayInSeconds2);
            FireRate.canShoot2 = true;
        }
        public IEnumerator ShootDelay3()
        {
            yield return new WaitForSeconds(FireRate.delayInSeconds3);
            FireRate.canShoot3 = true;
        }
    }
}


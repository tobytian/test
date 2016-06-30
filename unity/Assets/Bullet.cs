//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************


using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Networking;


public class Bullet : MonoBehaviour
{
    public const float g = 9.8f;

    public GameObject target;
    public float speed = 10;
    private float verticalSpeed;
    private Vector3 moveDirection;

    private float angleSpeed;
    private float angle;
    void Start()
    {
       float tmepDistance = Vector3.Distance(transform.position, target.transform.position);
        float tempTime = tmepDistance / speed;
        float riseTime, downTime;
        riseTime = downTime = tempTime / 2;
         verticalSpeed = g * riseTime;
        transform.LookAt(target.transform.position);

        float tempTan = verticalSpeed / speed;
        double hu = Math.Atan(tempTan);
        angle = (float)(180 / Math.PI * hu);
        transform.eulerAngles = new Vector3(-angle, transform.eulerAngles.y, transform.eulerAngles.z);
        angleSpeed = angle / riseTime;

        moveDirection = target.transform.position - transform.position;
    }
    private float time;
    void Update()
    {
        if (transform.position.y < target.transform.position.y)
        {
            //finish
            return;
        }
        time += Time.deltaTime;
        float test = verticalSpeed - g * time;
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.up * test * Time.deltaTime,Space.World);
        float testAngle = -angle + angleSpeed * time;
        transform.eulerAngles = new Vector3(testAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}












//public class ProjectileTest : MonoBehaviour
//{
//    public GameObject target;
//    public float speed = 10;
//    private float distanceToTarget;
//    private bool move = true;

//    void Start()
//    {
//        distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);
//        StartCoroutine(Shoot());
//    }

//    IEnumerator Shoot()
//    {

//        while (move)
//        {
//            Vector3 targetPos = target.transform.position;
//            this.transform.LookAt(targetPos);
//            float angle = Mathf.Min(1, Vector3.Distance(this.transform.position, targetPos) / distanceToTarget) * 45;
//            this.transform.rotation = this.transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);
//            float currentDist = Vector3.Distance(this.transform.position, target.transform.position);
//            print("currentDist" + currentDist);
//            if (currentDist < 0.5f)
//                move = false;
//            this.transform.Translate(Vector3.forward * Mathf.Min(speed * Time.deltaTime, currentDist));
//            yield return null;
//        }
//    }


//}















//using UnityEngine;
//using System.Collections;
///// <summary>
///// 弓箭轨迹模拟
///// 阿亮设计，欢迎交流经验
///// </summary>
//public class TestRay : MonoBehaviour
//{

//    public float Power = 10;//这个代表发射时的速度/力度等，可以通过此来模拟不同的力大小
//    public float Angle = 45;//发射的角度，这个就不用解释了吧
//    public float Gravity = -10;//这个代表重力加速度



//    private Vector3 MoveSpeed;//初速度向量
//    private Vector3 GritySpeed = Vector3.zero;//重力的速度向量，t时为0
//    private float dTime;//已经过去的时间
//    // Use this for initialization
//    void Start()
//    {
//        //通过一个公式计算出初速度向量
//        //角度*力度
//        MoveSpeed = Quaternion.Euler(new Vector3(-Angle, 0, 0)) * Vector3.forward * Power;

//    }

//    // Update is called once per frame
//    void FixedUpdate()
//    {

//        //计算物体的重力速度
//        //v = at ;
//        GritySpeed.y = Gravity * (dTime += Time.fixedDeltaTime);
//        //位移模拟轨迹
//        transform.Translate(MoveSpeed * Time.fixedDeltaTime);
//        transform.Translate(GritySpeed * Time.fixedDeltaTime);

//    }
//}
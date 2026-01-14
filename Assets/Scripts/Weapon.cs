using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;

    //bắn
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //burt
    public int bulletsPerBurt = 3;
    public int burstBulletLeft;

    //Spread
    public float spreadIntensity;

    //Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;
    public Animator animator;




    //enum tạo danh sách có thể gọi bằng cách :
    //ShootingMode.
    public enum ShootingMode
    {
        Single,
        Burt,
        Auto

    }

    //curentShootingMode giữ 1 trong các enum
    public ShootingMode curentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletLeft = bulletsPerBurt;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //nếu curentShootingMode là Auto
        if (curentShootingMode == ShootingMode.Auto)
        {
            //Giữ chuột bắn liên tục
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }

        //nếu curentShootingMode là Single hoặc Burt
        else if (curentShootingMode == ShootingMode.Single || curentShootingMode == ShootingMode.Burt)
        {
            //bấm chuột mới bắn
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && isShooting)
        {
            burstBulletLeft = bulletsPerBurt;
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        muzzleEffect.GetComponent<ParticleSystem>().Play();

        animator.SetTrigger("RECOIL");

        SoundManager.Instance.shootingAk47.Play();

        readyToShoot = false;
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;


        //tạo viên đạn gồm: clone viên đạn,vị trí viên đạn,rotation của viên đạn(0,0,0)
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        //Hướng đạn bắn về phía trước
        bullet.transform.forward = shootingDirection;

        //tạo lực bắn cho viên đạn
        //shootingDirection : đạn bắn về phía trước
        //bulletVelocity : vận tốc của viên đạn
        //ForceMode.Impulse : Lực tát động tức thời(có xét mass)
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        //xóa viên đạn
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        if (allowReset)
        {
            //đặt lịch Resetshot sau 2 giây
            Invoke("ResetShot", shootingDelay);

            //sau khi bắn thành false,đợi 2 giây chạy ResetShot
            allowReset = false;
        }
        //Burt Mode
        if (curentShootingMode == ShootingMode.Burt && burstBulletLeft > 1)
        {
            burstBulletLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;

    }


    public Vector3 CalculateDirectionAndSpread()
    {
        //ray: là 1 tia tưởng tượng kh có trong sence...
        //tia từ camera người chơi(playerCamera) tới giữa màn hình(ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));)
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        //chứa kết quả khi raycast trúng vật thể
        //hit.point:tọa độ va chạm
        //hit.collider:collider bị trúng
        //hit.transform: transform của GameObject
        //hit.distance : khoảng cách từ ray đến origin
        RaycastHit hit;


        //vật thể bị raycast trúng
        Vector3 targetPoint;

        // nếu bắn trúng vật thể
        // lưu ý:Raycast không phải đạn mà là 1 tia vô hình
        if (Physics.Raycast(ray, out hit))
        {
            //hit.pont: tọa độ va chạm (x,y,z)
            targetPoint = hit.point;
        }
        else
        {
            //tạo điểm giả khi Raycast kh dính gì
            targetPoint = ray.GetPoint(100);
        }

        //khoảng cách từ điểm xuất phát --> điểm đến
        //Vector hướng=điểm đến - điểm xuất phát || A->B = B-A
        Vector3 direction = targetPoint - bulletSpawn.position;

        //tạo độ lệch cho X(trái-phải) và Y(trên-dưới)
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        //trả về hướng bắn + độ lệch
        return direction + new Vector3(x, y, 0);



    }


    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        Destroy(bullet);
    }
}

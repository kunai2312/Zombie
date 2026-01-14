using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision objectWeHit)
    {
        if (objectWeHit.gameObject.CompareTag("Target"))
        {
            Debug.Log("hit" + objectWeHit.gameObject.name + "!");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
        if (objectWeHit.gameObject.CompareTag("Wall"))
        {

            Debug.Log("hit Wall");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
    }
    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        //điểm va chạm đầu tiên
        //contact gồm:
        //+ contact.point:vị trí va chạm
        //+ contact.normal:hướng vuông góc bề mặt
        ContactPoint contact = objectWeHit.contacts[0];

        //truy cập vào Singleton để lấy prefab vết đạn
        GameObject hole = Instantiate(GobalRefences.Instance.bulletImpactEffectPrefab,
        contact.point,   //contact.point: vị trí sinh prefab
        Quaternion.LookRotation(contact.normal)); //xoay vết đạn ra hướng mặt bị bắn

        //SetParent:gán 1 object thành con của 1 object khác
        //hole sẽ thành con của objectWeHit, di chuyển theo objectWeHit
        // cú pháp: child.transform.SetParent(parent.transform);
        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}


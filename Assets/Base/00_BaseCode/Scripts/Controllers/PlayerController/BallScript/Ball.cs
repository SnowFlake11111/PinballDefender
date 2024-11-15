using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Public Variables
    #endregion

    #region Private Variables
    Vector3 lastVelocity;
    Vector3 bounceDirection;

    PlayerController owner;

    [SerializeField] int ownerId = 0;
    int bounceLimit = 0;
    int ballDamage = 0;

    float velocityStrength;
    #endregion

    #region Functions
    public void ShootBall(Vector3 shootDirection)
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 300);
    }

    //Gán sát thương cho bóng
    public void SetDamage(int damage)
    {
        ballDamage = damage;
    }

    //Gán số lần nảy cho bóng
    public void SetBounceLimit(int bounce)
    {
        bounceLimit = bounce;
    }

    //Gán id của người bắn quả bóng này vào id người sở hữu của quả bóng này, id sẽ được sử dụng để kiểm tra va chạm với quân địch và đồng đội
    public void SetOwner(int id, PlayerController ballOwner)
    {
        ownerId = id;
        owner = ballOwner;
        gameObject.layer = id;
    }
    #endregion

    #region Start, Update
    private void LateUpdate()
    {
        lastVelocity = GetComponent<Rigidbody>().velocity;
    }
    #endregion

    #region Functions
    //----------Public----------
    //----------Private----------
    #endregion

    #region Collision Functions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<GameUnitBase>() != null)
        {
            HitUnit(collision);
        }
        else if (collision.collider.GetComponent<GameWall>() != null)
        {
            HitWall(collision);
        }
        else if (collision.collider.GetComponent<UnitHittableProjectile>() != null)
        {
            HitProjectile(collision);
        }
        else if (collision.collider.GetComponent<Ball>() != null)
        {
            BounceBall(collision);
        }
    }

    void HitUnit(Collision unit)
    {
        //Ý tưởng ở đây là kiểm tra xem unit va phải có là đồng đội hay không, nếu là đồng đội thì sẽ không gây sát thương, nếu không phải thì ngược lại
        //UPDATE: Sau khi áp dụng việc sử dụng danh sách đồng đội và danh sách bóng bắn ra thì không cần phải sử dụng ownerId để kiểm tra phe nữa mà bóng sẽ tự bỏ qua va chạm đối với các unit cùng phe
        //UPDATE 2: Đã đổi sang sử dụng Collision Layer
        unit.collider.GetComponent<GameUnitBase>().TakeDamage(ballDamage);

        if (unit.collider.GetComponent<GameUnitBase>().GetBouncePermission())
        {
            if (bounceLimit - unit.collider.GetComponent<GameUnitBase>().GetBounceCost() <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                bounceLimit -= unit.collider.GetComponent<GameUnitBase>().GetBounceCost();
                BounceBall(unit);
            }
        }
    }

    void HitWall(Collision wall)
    {
        //Đối với các chướng ngại vật thì bản thân chúng không thuộc về ai cả, nên va chạm sẽ luôn được xử lý
        if (wall.collider.GetComponent<GameWall>().GetBouncePermission())
        {
            if (bounceLimit - wall.collider.GetComponent<GameWall>().GetBounceCost() <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                bounceLimit -= wall.collider.GetComponent<GameWall>().GetBounceCost();
                BounceBall(wall);
            }
        }
    }

    void HitProjectile(Collision projectile)
    {
        projectile.collider.GetComponent<UnitHittableProjectile>().TakeDamage(ballDamage);

        if (projectile.collider.GetComponent<UnitHittableProjectile>().GetBouncePermission())
        {
            if (bounceLimit - projectile.collider.GetComponent<UnitHittableProjectile>().GetBounceCost() <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                bounceLimit -= projectile.collider.GetComponent<UnitHittableProjectile>().GetBounceCost();
                BounceBall(projectile);
            }
        }
    }

    void BounceBall(Collision collision)
    {
        velocityStrength = lastVelocity.magnitude;
        bounceDirection = Vector3.Reflect(lastVelocity.normalized, collision.GetContact(0).normal);

        GetComponent<Rigidbody>().velocity = bounceDirection * Mathf.Max(velocityStrength, 0);
    }
    #endregion
}

/*
 * Danh sách Layer:
 * 6: Director
 * 7: Player_1
 * 8: Player_2
 * 
 * NOTE: 3 layer này đã được thiết lập để không xử lý vật lý (như va chạm collider) đối với các object có cùng layer với chúng
 */
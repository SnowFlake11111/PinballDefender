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

    int ownerId = 0;
    int bounceLimit = 5;
    int ballDamage = 0;

    float velocityStrength;
    #endregion

    #region Functions
    public void ShootBall(Vector3 shootDirection)
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 200);
    }

    //Gán sát thương cho bóng
    public void SetDamage(int damage)
    {
        ballDamage = damage;
    }

    //Gán id của người bắn quả bóng này vào id người sở hữu của quả bóng này, id sẽ được sử dụng để kiểm tra va chạm với quân địch và đồng đội
    public void SetOwner(int id)
    {
        ownerId = id;
    }
    #endregion

    #region Start, Update
    private void LateUpdate()
    {
        lastVelocity = GetComponent<Rigidbody>().velocity;
    }
    #endregion

    #region Collision Functions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<GameUnit>() != null)
        {
            HitUnit(collision);
        }
        else if (collision.collider.GetComponent<GameWall>() != null)
        {
            HitWall(collision);
        }
        else if (collision.collider.GetComponent<Ball>() != null)
        {
            HitBall(collision);
        }
    }

    void HitUnit(Collision unit)
    {
        //Ý tưởng ở đây là kiểm tra xem unit va phải có là đồng đội hay không, nếu là đồng đội thì sẽ không gây sát thương, nếu không phải thì ngược lại
        if (unit.collider.GetComponent<GameUnit>().GetOwnerId() != ownerId)
        {
            unit.collider.GetComponent<GameUnit>().TakeDamage(ballDamage);

            if (unit.collider.GetComponent<GameUnit>().GetBouncePermission())
            {
                if (bounceLimit - unit.collider.GetComponent<GameUnit>().GetBounceCost() <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    bounceLimit -= unit.collider.GetComponent<GameUnit>().GetBounceCost();
                    BounceBall(unit);
                }
            }
        }
        else
        {
            if (unit.collider.GetComponent<GameUnit>().GetBouncePermission())
            {
                if (bounceLimit - unit.collider.GetComponent<GameUnit>().GetBounceCost() <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    bounceLimit -= unit.collider.GetComponent<GameUnit>().GetBounceCost();
                    BounceBall(unit);
                }
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

    void HitBall(Collision ball)
    {
        //Do là tất cả các object trong game không sử dụng IsTrigger, nên là sẽ thường xuyên xảy ra trường hợp bóng va vào nhau, hàm này sẽ xử lý việc đó
        if (bounceLimit - 1 <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            bounceLimit--;
            BounceBall(ball);
        }
    }

    void BounceBall(Collision collision)
    {
        velocityStrength = lastVelocity.magnitude;
        bounceDirection = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        GetComponent<Rigidbody>().velocity = bounceDirection * Mathf.Max(velocityStrength, 0);
    }
    #endregion
}

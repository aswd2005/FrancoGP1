using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChase : StateMachineBehaviour
{
    
    Transform player;
    Rigidbody2D rb;
    BossPosition BossPosition;
    

    override public void OnStateEnter(Animator animtor, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animtor.GetComponent<Rigidbody2D>();
        BossPosition = animtor.GetComponent<BossPosition>();
    }
    override public void OnStateUpdate(Animator animtor, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossPosition.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, BossPosition.speed * Time.deltaTime);
        rb.MovePosition(newPosition);
        float distance = Vector2.Distance(player.position, rb.position);

        if (distance<BossPosition.attackRange&&!BossPosition.phase2 && !BossPosition.phase3 && !BossPosition.isDead)
        {
            animtor.SetTrigger("MeleeAttack");
        }
        else if (distance < BossPosition.attackRange && BossPosition.phase2 && !BossPosition.phase3 && !BossPosition.isDead)
        {
            animtor.SetTrigger("phase2");
        }
        else if (distance < BossPosition.attackRange && !BossPosition.phase2 && BossPosition.phase3 && !BossPosition.isDead)
        {
            animtor.SetTrigger("phase3");
        }
        else if (distance < BossPosition.attackRange && !BossPosition.phase2 && !BossPosition.phase3 && BossPosition.isDead)
        {
            animtor.SetTrigger("Death");
        }
    }
    override public void OnStateExit(Animator animtor, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}

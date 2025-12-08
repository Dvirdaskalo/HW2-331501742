using UnityEngine;

public interface IHpManager
{
    int hp { get; set; }
    int Damage { get; set; }
    void lose_hp(int damage, string type)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0; 
            Died();
        }
        Debug.Log($"The new hp of {type} is: {hp}");
    }
    
    void Died();
    
}

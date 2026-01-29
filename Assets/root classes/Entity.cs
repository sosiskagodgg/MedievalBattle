using System;
using System.Collections.Generic;
using System.Linq;

public class Entity 
{
    public string Name { get;private set; }





    public event EventHandler<Entity> EntityDead;
    private Entity _lastDamager;

    public List<BodyPart> LowerBody { get; private set; }
    public List<BodyPart> MiddleBody { get; private set; }
    public List<BodyPart> UpperBody { get; private set; }

    private float _bloodVolume;
    protected float bloodVolume { get => _bloodVolume; set { _bloodVolume = value; if (_bloodVolume <= 0) EntityDead?.Invoke(this, _lastDamager); } }
    public float bloodLossMLPerSecond { get; private set; }

    public float Speed { get; set; }


    public virtual void TakeDamage(Entity lastDamager, BodySection bodySection,float damage)
    {
        List<BodyPart> bodyParts = LowerBody.Concat(MiddleBody.Concat(UpperBody)).Where(BS=>BS.bodySection == bodySection).ToList();
        //находим все BodyPart где bodySection == BodyPart.bodySection

        BodyPart targetBodyPart = RandomEventGeneration.GetRandomBodyPart(bodyParts);
        //получаем случайную часть тела

        _lastDamager = lastDamager;


        bloodLossMLPerSecond += targetBodyPart.TakeDamage(damage);
        //наносим урон этой части тела + получаем кровопотерю

        
    }
    
}

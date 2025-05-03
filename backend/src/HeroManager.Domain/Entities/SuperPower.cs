namespace HeroManager.Domain.Entities;

public class SuperPower
{
    public int Id {get ; set;}

    public string Name {get ; set;}

    public string Description {get ; set;}

    public List<SuperPowerHero> heroSuperPowers {get ; set;} = new ();

    public SuperPower(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}
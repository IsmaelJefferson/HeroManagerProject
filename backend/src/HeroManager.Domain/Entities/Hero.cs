namespace HeroManager.Domain.Entities;

public class Hero
{
    public int Id {get ; private set;}

    public string Name {get ; private set;}

    public string HeroName {get; private set;}

    public DateTime BirthDate {get ; private set;}

    public float Height {get ; private set;}

    public float Weight {get ; private set;}

    public List<SuperPowerHero> SuperPowers {get ; private set;} = new ();

    public Hero(string name, string heroName, DateTime birthDate, float height, float weight)
    {
        Name = name;
        HeroName = heroName;
        BirthDate = birthDate;
        Height = height;
        Weight = weight;
    }

    public void UpdateDate(string name, string heroName, DateTime birthDate, float height, float weight)
    {
        Name = name;
        HeroName = heroName;
        BirthDate = birthDate;
        Height = height;
        Weight = weight;
    }
    
    public void AssignSuperPower(List<SuperPower> superPowers)
    {
        SuperPowers.Clear();

        foreach(var power in superPowers)
        {
            SuperPowers.Add(new SuperPowerHero
            {
                HeroId = this.Id,
                SuperPowerId = power.Id,
                SuperPower = power,
            });
        }

    }

}
namespace HeroManager.Domain.Entities;

public class SuperPowerHero
{
    public int HeroId {get ; set;}

    public Hero Hero {get; set;}

    public int SuperPowerId {get ; set;}

    public SuperPower SuperPower {get ; set;}
}
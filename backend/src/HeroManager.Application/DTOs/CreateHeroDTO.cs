namespace HeroManager.Application.DTOs;

public class CreateHeroDTO
{
    public string Name {get ; set;}

    public string HeroName {get; set;}

    public DateTime BirthDate {get; set;}

    public float Height {get; set;}

    public float Weight {get; set;}

    public List<int> SuperPowersId {get; set;}
}
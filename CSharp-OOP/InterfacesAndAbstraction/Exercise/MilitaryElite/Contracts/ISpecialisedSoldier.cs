namespace MilitaryElite.Contracts
{
    public enum Corps
    {
        Airforces, 
        Marines
    }

    public interface ISpecialisedSoldier
    {
        Corps Corps { get; }
    }
}

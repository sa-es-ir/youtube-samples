namespace ManageConcurrencyWays;

public class ModelToCheck
{
    public ModelToCheck()
    {
        AccessMe = Guid.NewGuid();
    }

    public Guid AccessMe { get; set; }
}

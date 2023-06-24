namespace VendingMachine.Protect
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class AdminKeyAttribute : System.Attribute
    {
       public static string Key = "secret";

       public AdminKeyAttribute(){}
    }
}

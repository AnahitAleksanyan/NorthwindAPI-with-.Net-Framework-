namespace NorthwindAPI_with_Framework_.ContextService
{
    //NorthwindContextFactory is designed to get the NorthwindContext object to avoid creating many instances.
    public class NorthwindContextFactory 
    {
       
            public static readonly NorthwindContext INSTANCE;

            static NorthwindContextFactory()
            {
                INSTANCE = new NorthwindContext();
            }
    }
}
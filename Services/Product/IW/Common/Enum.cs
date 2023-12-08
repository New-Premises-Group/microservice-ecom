namespace IW.Common
{
    public enum VALIDATOR_ERROR_CODE
    {
        NotEmpty,
        Match,
        Length,
        GreaterThan
    }
    public enum PAGINATING
    {
        OffsetDefault=0,
        AmountDefault=10
    }
    public enum ROLE
    {
        User,
        Admin,
        Seller
    }
    public enum QUEUE_NAME
    {
        Order_Placed,
        Order_Confirmed,
        Order_Delivered,
        Order_Shipped,
        Items
    }

    public enum RATING
    {
        Very_Good=5,
        Good=4,
        Normal=3,
        Bad=2,
        VeryBad=1
    }
}

namespace IW.Common
{
    public enum VALIDATOR_ERROR_CODE
    {
        NotEmpty,
        Match,
        Length,
        GreaterThan,
        BeAValidDate,
        LessThanOrEqualTo,
        IsInEnum
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
    public enum ORDER_STATUS
    {
        Pending,
        Proccessing,
        Shipping,
        Delivered,
        Confirmed,
        Created,
        Cancelled,
    }
    public enum QUEUE_NAME
    {
        Order_Placed,
        Order_Confirmed,
        Order_Delivered,
        Order_Shipped,
        Items
    }
}

namespace IW.Common
{
    public enum VALIDATOR_ERROR_CODE
    {
        NotEmpty,
        Match,
        Length,
        GreaterThan,
        GreaterThanOrEqualTo,
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
        Done
    }
    public enum QUEUE_NAME
    {
        Order_Placed,
        Order_Cancelled,
        Order_Confirmed,
        Order_Delivered,
        Order_Shipped,
        Items
    }

    public enum PAYLOAD_TYPE
    {
        Create,
        Update,
        Delete,
    }

    public enum DISCOUNT_TYPE
    {
        None,
        Percent,
        Fixed,
        Tier
    }

    public enum DISCOUNT_CONDITION
    {
        None,
        Total,
        Birthday,
        SpecialDay
    }
}

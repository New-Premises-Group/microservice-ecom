namespace IW.Common
{
    public enum VALIDATOR_ERROR_CODE
    {
        NotEmpty,
        Match,
        Length,
        MaxLength,
        GreaterThan,
        BeAValidDate,
        LessThanOrEqualTo,
        IsInEnum,
        MustBeBoolean
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
    public enum TRANSACTION_TYPE
    {
        Sale,
        Return,
        Restock,
        Adjustment
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

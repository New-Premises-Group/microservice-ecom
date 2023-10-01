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
        Confirm
    }
}

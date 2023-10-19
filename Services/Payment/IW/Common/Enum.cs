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
        OffsetDefault = 0,
        AmountDefault = 10
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
    public enum PAYMENT_STATUS
    {
        Pending,
        Successful,
        Failed,
        Cancelled
    }
    public enum CURRENCY
    {
        VND,
        USD
    }
}

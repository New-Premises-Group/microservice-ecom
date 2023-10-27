using IW.Models.DTOs.PaymentDto;

namespace IW.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<PaymentDto> GetPayment(int id);

        Task<IEnumerable<PaymentDto>> GetPayments(int offset, int amount);

        Task<IEnumerable<PaymentDto>> GetPayments(GetPayment query, int offset, int amount);

        Task UpdatePayment(int id, UpdatePayment input);

        Task DeletePayment(int id);

        Task CreatePayment(CreatePayment input);
    }
}
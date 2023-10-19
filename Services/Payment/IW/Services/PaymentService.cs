using IW.Exceptions.ReadPaymentError;
using IW.Interfaces;
using IW.Interfaces.Services;
using IW.Models.DTOs.PaymentDto;
using IW.Models.Entities;
using MapsterMapper;

namespace IW.Services
{
    public class PaymentService : IPaymentService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreatePayment(CreatePayment input)
        {
            Payment payment = _mapper.Map<Payment>(input);

            PaymentValidator validator = new();
            validator.ValidateAndThrowException(payment);

            _unitOfWork.Payments.Add(payment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<PaymentDto> GetPayment(int id)
        {
            var payment = await PaymentExist(id);
            if (Equals(payment, null))
            {
                throw new PaymentNotFoundException(id);
            }
            var order = await _unitOfWork.Payments.GetById(payment.ID);
            PaymentDto result = _mapper.Map<PaymentDto>(order);
            return result;
        }

        public async Task<IEnumerable<PaymentDto>> GetPayments(int offset, int amount)
        {
            var payments = await _unitOfWork.Payments.GetAll(offset, amount);
            ICollection<PaymentDto> result = _mapper.Map<List<PaymentDto>>(payments);
            return result;
        }

        public async Task<IEnumerable<PaymentDto>> GetPayments(GetPayment query, int offset, int amount)
        {
            var payments = await _unitOfWork.Payments.FindByConditionToList(
                p => p.ID == query.ID
                , offset, amount);
            ICollection<PaymentDto> result = _mapper.Map<List<PaymentDto>>(payments);
            return result;
        }

        public async Task UpdatePayment(int id, UpdatePayment model)
        {
            var payment = await PaymentExist(id);
            if (Equals(payment, null)) throw new PaymentNotFoundException(id);

            payment.Amount = model.Amount;
            payment.Status = model.Status;

            PaymentValidator validator = new();
            validator.ValidateAndThrowException(payment);

            _unitOfWork.Payments.Update(payment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeletePayment(int id)
        {
            var payment = await PaymentExist(id);
            if (Equals(payment, null)) throw new PaymentNotFoundException(id);

            _unitOfWork.Payments.Remove(payment);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<Payment?> PaymentExist(int id)
        {
            if (id.ToString() == String.Empty) return null;
            var result = await _unitOfWork.Payments.GetById(id);
            return result;
        }
    }
}
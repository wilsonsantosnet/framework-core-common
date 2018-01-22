using System.Collections.Generic;

namespace Common.Domain
{
    public interface IPaymentTransaction
    {
        dynamic ExecutePaymentCreditCardDefault(string email, dynamic credit_card, decimal total, string description);
        dynamic ExecutePayment(dynamic data);
        dynamic GetPayments(object data);
        dynamic GetPaymentsDetails();
    }
}

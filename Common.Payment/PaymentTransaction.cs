
using System;
using Common.Domain;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Common.Domain.Base;

namespace Common.Payment
{
    public class PaymentTransaction : PaymentBase, IPaymentTransaction
    {

        private string _transaction_resource;
        public PaymentTransaction(IRequest request, ConfigPaymentBase config, string transactionId = null) : base(request, config)
        {
            if (!string.IsNullOrEmpty(transactionId))
                this._transaction_resource = $"payments/payment/{transactionId}";
            else
                this._transaction_resource = "payments/payment";

            this._request = request;
            this._request.SetAddress(this._endpoint);

        }
        public PaymentTransaction(IRequest request, IOptions<ConfigPaymentBase> config, string transactionId = null) :
            this(request, config.Value, transactionId)
        { }

        public dynamic ExecutePaymentCreditCardDefault(string email, dynamic credit_card, decimal total, string description)
        {
            return this.ExecutePayment(new
            {
                intent = "sale",
                payer = new
                {
                    payment_method = "credit_card",
                    payer_info = new
                    {
                        email = email
                    },
                    funding_instruments = new List<dynamic> {
                        new {
                            credit_card = credit_card
                        }
                    }
                },
                transactions = new List<dynamic>
                {
                    new {
                        amount = new {
                            total= total,
                            currency= "USD",
                            details = new {
                                subtotal= total
                            }
                        },
                        description = description,
                        invoice_number = DateTime.Now.ToString("yyMMddHHmms"),
                        item_list = new
                        {
                            items = new List<dynamic> {
                            new {
                                    name= description,
                                    description= description,
                                    quantity= "1",
                                    price= total,
                                    currency= "USD"
                                }
                            }
                        }
                    }
                }
            });
        }

        public dynamic ExecutePayment(dynamic data)
        {

            var result = this._request.Post<dynamic, dynamic>(this._transaction_resource, data);
            return result;
        }

        public dynamic GetPayments(object data)
        {
            var result = this._request.Get<dynamic>(this._transaction_resource, data.ToDictionary());
            return result;
        }

        public dynamic GetPaymentsDetails()
        {
            var result = this._request.Get<dynamic>(this._transaction_resource);
            return result;
        }

    }
}
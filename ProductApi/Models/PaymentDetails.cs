using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class PaymentDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        public string PaymentMethod { get; set; }

        // Credit Card Details
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }

        // PayPal Details
        public string PayPalEmail { get; set; }

        // Net Banking Details
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSCCode { get; set; }

    }
}

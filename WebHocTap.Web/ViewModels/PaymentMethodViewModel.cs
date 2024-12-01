namespace WebHocTap.Web.ViewModels
{
    public class PaymentMethodViewModel
    {
        public int CategorySubId { get; set; }
        public string SelectedMethod { get; set; }
        public List<string> PaymentMethods { get; set; } = new List<string> { "PayPal", "CreditCard" };
    }
}

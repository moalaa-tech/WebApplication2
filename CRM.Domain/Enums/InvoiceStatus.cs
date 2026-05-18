namespace CRM.Domain.Enums
{
    public enum InvoiceStatus
    {   /// <summary>
        /// The order has been placed but not yet processed.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// The order has been confirmed and is ready to be processed.
        /// </summary>
        Confirmed = 2,

        /// <summary>
        /// The order is awaiting payment confirmation.
        /// </summary>
        PendingPayment = 3,

        /// <summary>
        /// The payment for the order has been received.
        /// </summary>
        Paid = 4,

        /// <summary>
        /// The payment attempt for the order has failed.
        /// </summary>
        PaymentFailed = 5,

        /// <summary>
        /// The order is being prepared for shipment.
        /// </summary>
        Processing = 6,

        /// <summary>
        /// The order has been processed and is awaiting pickup.
        /// </summary>
        WaitingForPickup = 7,

        /// <summary>
        /// The order has been shipped and is in transit.
        /// </summary>
        InDelivery = 8,

        /// <summary>
        /// The order has been successfully delivered to the customer.
        /// </summary>
        Delivered = 9,

        /// <summary>
        /// The order has been canceled.
        /// </summary>
        Canceled = 10,

        /// <summary>
        /// The order has been returning from delivery.
        /// </summary>
        ReturningFromDelivery = 11,

        /// <summary>
        /// The customer has requested a refund for the order.
        /// </summary>
        RequestRefund = 12,

        /// <summary>
        /// The refund for the order is in progress.
        /// </summary>
        RefundInProgress = 13,

        /// <summary>
        /// The refund for the order has been completed.
        /// </summary>
        Refunded = 14
    }
}

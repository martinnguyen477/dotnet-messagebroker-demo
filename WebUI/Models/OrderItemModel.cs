﻿namespace WebUI.Models
{
    public record OrderItemModel
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public string? ProductName { get; init; }
        public double ProductPrice { get; init; }
        public ushort Quantity { get; set; }
    }
}

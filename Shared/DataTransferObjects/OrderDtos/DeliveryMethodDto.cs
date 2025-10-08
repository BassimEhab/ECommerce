namespace Shared.DataTransferObjects.OrderDtos
{
    public class DeliveryMethodDto
    {
        public int Id { get; set; }
        public string ShortName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string DeliveryTime { get; set; } = default!;
        public decimal cost { get; set; }
    }
}
/*
  shortName: string;
    deliveryTime: string;
    description: string;
    cost: number;
    id: number;
 */

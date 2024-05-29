namespace EFC_WMP_Asset_Tracking
{
    internal class Asset
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Office { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public decimal PriceUSD { get; set; }
        public string Currency { get; set; }
    }

    internal class Computer : Asset
    { }

    internal class Phone : Asset
    { }
}
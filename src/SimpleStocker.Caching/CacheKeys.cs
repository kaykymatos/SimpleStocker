namespace SimpleStocker.Caching
{
    public class CacheKeys
    {
        public const string GetOneProduct = "PRODUCT_API_GET_ONE_PRODUCT_{0}";
        public const string GetAllProducts = "PRODUCT_API_GET_ALL_PRODUCTS";

        public const string GetOneCategory = "CATEGORY_API_GET_ONE_CATEGORY_{0}";
        public const string GetAllCategories = "CATEGORY_API_GET_ALL_CATEGORIES";

        public const string GetOneClient = "CLIENT_API_GET_ONE_CLIENT_{0}";
        public const string GetAllClients = "CLIENT_API_GET_ALL_CLIENTS";

        public const string GetOneInventory = "INVENTORY_API_GET_ONE_INVENTORY_{0}";
        public const string GetAllInventories = "INVENTORY_API_GET_ALL_INVENTORIES";
        public const string GetAllProductInventories = "INVENTORY_API_GET_ALL_PRODUCT_INVENTORIES";
    }
}

namespace DayanaWeb.Shared.Basic.Classes;

public record CRUDRouts
{
    public const string Create = "create";
    public const string ReadOneById = "read-one-by-id";
    public const string ReadAll = "read-all";
    public const string ReadListByFilter = "read-list-by-filter";
    public const string Update = "update";
    public const string Delete = "delete";
}
public record ShopRoutes
{
    public const string ShopApi = "shop-api/";
    public const string Product = ShopApi + "product/";
    public const string ProductCategory = ShopApi + "product-category/";
}

public record BlogRoutes
{
    public const string BlogApi = "blog-api/";
    public const string PostCategory = BlogApi + "blog-post-category/";
    public const string Post = BlogApi + "blog-post/";
}

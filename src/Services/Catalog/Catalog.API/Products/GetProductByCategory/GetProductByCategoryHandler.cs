
namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductCategoryQuery(string Category):IQuery<GetProductCategoryResult>;
    public record GetProductCategoryResult(IEnumerable<Product> Products);


    public class GetProductByCategoryHandler
        (IDocumentSession session, ILogger<GetProductByCategoryHandler> logger)
        : IQueryHandler<GetProductCategoryQuery, GetProductCategoryResult>
    {
        public async Task<GetProductCategoryResult> Handle(GetProductCategoryQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryHandler.Handle called with {@Request}", request);

            var products = await session.Query<Product>()
                .Where(x => x.Category.Contains(request.Category))
                .ToListAsync(cancellationToken);

            return new GetProductCategoryResult(products);
        }
    }
}

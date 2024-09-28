


namespace Catalog.Api.Product.CreateProduct
{

    public record CreateProductCommand(
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price):ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);


    public class CreateProductCommandHandler(IDocumentSession session):ICommandHandler<CreateProductCommand,CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var Product = new Models.Product
            {
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
                Name = command.Name
            };

            session.Store(Product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(Product.Id);
        }
    }
}

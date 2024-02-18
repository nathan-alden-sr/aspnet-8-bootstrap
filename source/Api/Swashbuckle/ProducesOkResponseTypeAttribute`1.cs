using Microsoft.AspNetCore.Mvc;

namespace Company.Product.WebApi.Api.Swashbuckle;

public class ProducesOkResponseTypeAttribute<T> : ProducesResponseTypeAttribute
    where T : notnull
{
    public ProducesOkResponseTypeAttribute() : base(StatusCodes.Status200OK)
    {
        Type = typeof(T);
    }
}

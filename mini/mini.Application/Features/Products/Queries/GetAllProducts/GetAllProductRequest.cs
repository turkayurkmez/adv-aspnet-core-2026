using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Application.Features.Products.Queries.GetAllProducts
{

    //handler'a göndereceğimiz request'in yapısını tanımlayalım:
    //analoji: request ise bir uçaktır.
    public record GetAllProductRequest() : IRequest<GetAllProductResponse>;


    //istemciye dönecek cevabın yapısını tanımlayalım:
    public record ProductDisplayDto(int Id,
        string Name,
        string Description,
        decimal Price
    );


    //handler'dan dönecek cevabın yapısını tanımlayalım:
    public record GetAllProductResponse(List<ProductDisplayDto> Products);

}

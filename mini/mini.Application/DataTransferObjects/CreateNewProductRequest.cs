using System;
using System.Collections.Generic;
using System.Text;

namespace mini.Application.DataTransferObjects
{
   public record CreateNewProductRequestDto(string Name, string Description, decimal Price);
    
}

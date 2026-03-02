namespace mini.API.REPR
{
    /*
     * REPR (Request-Endpoint-Response-Pipeline) için bir arayüz tanımlıyoruz. Bu arayüz, her bir endpoint'in nasıl haritalanacağını belirlemek için kullanılacak. Her endpoint sınıfı bu arayüzü implement ederek kendi endpoint'ini tanımlayacak ve Program.cs içerisinde bu endpoint'leri kolayca ekleyebileceğiz.
     */
    public interface IEndpoint
    {
        // void MapEndpoint(IEndpointRouteBuilder app);

        //Gruplama yapabilmek için RouteGroupBuilder kullanarak MapEndpoint metodunu güncelledik.
        void MapEndpoint(RouteGroupBuilder group);
    }
}

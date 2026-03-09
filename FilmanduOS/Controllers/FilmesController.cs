

using FilmanduOS.Data;
using FilmanduOS.models;
using Microsoft.AspNetCore.Mvc;

namespace FilmanduOS.Controllers;

[ApiController] //isso diz que a classe e um controlador de API, o que significa que ela pode lidar com requisições HTTP e retornar respostas HTTP.
[Route("[controller]")] // isso define a rota para o controlador. O [controller] é um placeholder que será substituído pelo nome do controlador, ou seja, "Filmes". Portanto, as requisições para este controlador devem ser feitas para a rota "/Filmes".
public class FilmesController: ControllerBase //esse ControllerBase é uma classe base para controladores de API no ASP.NET Core. Ele fornece métodos e propriedades úteis para lidar com requisições HTTP e retornar respostas HTTP.
{
    /* 
    nao precisa mais porque agr com banco de dados, mas vou deixar para exemplificar como funiona o controller sem banco de dados, ou seja, sem o Entity Framework Core. O controller vai armazenar os filmes em uma lista em memória, e cada filme terá um id gerado automaticamente a partir de um contador.
    private static List<Filme> filmes = new List<Filme>();
    private static int id = 0;
    */
    private BancoDados _dados;

    public FilmesController(BancoDados dados)  
    {
        _dados = dados;
    }


    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] Filme filme) //esse fromBody indica que o filme será enviado no corpo da requisição HTTP, geralmente em formato JSON. O método AdicionarFilme é um endpoint HTTP POST que recebe um objeto do tipo Filme e o adiciona à lista de filmes, no caso quando eu passar pelo metodo post do postman ele vai armazenar aqui ou em outro local tipo o swagger
    {
        /*
        filme.Id = id++;
        filmes.Add(filme);
        */
        _dados.Filmes.Add(filme);//aqui no caso estou adicionando o filme no banco de dados
        _dados.SaveChanges();
        return CreatedAtAction(nameof(filtrarPorId), new { filme.Id}, filme);
        //nameof() quer dizer que eu estou passando o nome do método filtrarPorId para o CreatedAtAction, que é um método que retorna uma resposta HTTP 201 Created, indicando que um novo recurso foi criado com sucesso. O primeiro parâmetro é o nome do método de ação para o qual a resposta deve apontar, ou seja, o método filtrarPorId. O segundo parâmetro é um objeto anônimo que contém os valores dos parâmetros necessários para chamar o método de ação, neste caso, o id do filme recém-criado. O terceiro parâmetro é o conteúdo da resposta, ou seja, o filme recém-criado. Portanto, quando um novo filme é adicionado com sucesso, a resposta HTTP 201 Created incluirá um cabeçalho Location que aponta para a URL do filme recém-criado e o corpo da resposta conterá os dados do filme.



        /*
        subistituido pela dataAnottations mas esta na classe Filme
        if(!string.IsNullOrEmpty(filme.Nome) && //esse string.IsNullOrEmpty é um método que verifica se uma string é nula ou vazia. Ele retorna true se a string for nula ou vazia, e false caso contrário. Então, nesse código, ele está verificando se o nome do filme não é nulo ou vazio, e se a descrição do filme não é nula ou vazia, e se o gênero do filme não é nulo ou vazio, e se a duração do filme está entre 0 e 300 minutos, e se a lista de atores do filme não é nula e contém pelo menos um ator. Se todas essas condições forem verdadeiras, o filme será adicionado à lista de filmes.
            !string.IsNullOrEmpty(filme.Descricao) && //no caso tbm ele tem o ! para inverter apos a verificação, ou seja, ele vai verificar se retornou vazio, se sim ele vai retornar true o ! inverte para false, se vier os dados ele vem false ! transforma em true, ou seja, ele só vai adicionar o filme se os dados estiverem preenchidos, caso contrário ele não vai adicionar
            !string.IsNullOrEmpty(filme.Genero) &&
            filme.Duracao >= 0 && filme.Duracao <= 300  &&
            filme.Atores != null && filme.Atores.Any() && //esse filme.Atores.Any() é um método de extensão do LINQ que verifica se a coleção de atores contém pelo menos um elemento. Ele retorna true se houver pelo menos um ator na lista, e false se a lista estiver vazia ou nula. Então, nesse código, ele está verificando se a lista de atores do filme não é nula e contém pelo menos um ator antes de adicionar o filme à lista de filmes.
            filme.Nome.Length < 50)//aqui estou passando o .length que é o tamanho do texto para verificar se o nome do filme tem menos de 50 caracteres, caso contrário ele não vai adicionar o filme 
            filmes.Add(filme);
        Console.WriteLine(filme.Nome);
        foreach(var item in filme.Atores!)
        {
            Console.WriteLine($"-ator: {item}");
        }
        */
    }
    [HttpGet]
    public IEnumerable<Filme> exibirFilmes([FromQuery] int skip = 0, [FromQuery] int take = 10)//esse IEnumerable é uma interface que representa uma coleção de elementos que podem ser enumerados e percorridos e so para para executar quando toda o objeto e percorrido. Ele é usado para retornar uma sequência de objetos do tipo Filme. O método exibirFilmes é um endpoint HTTP GET que retorna a lista de filmes armazenada no controlador.
    {//e skip = 0 e take = 10, fica com valores default como no python
        return _dados.Filmes.Skip(skip).Take(take);

        //esse skip() é um método de extensão do LINQ que pula um número especificado de elementos em uma sequência e retorna os elementos restantes. No caso, ele está pulando os primeiros 10 filmes da lista e retornando os próximos 20 filmes. O take() é outro método de extensão do LINQ que retorna um número especificado de elementos do início de uma sequência. Então, nesse código, ele está retornando os próximos 20 filmes após pular os primeiros 10 filmes da lista.
    }

    [HttpGet("{id}")] //aqui no caso o [id] é um placeholder que indica que o valor do id será passado como parte da rota da requisição HTTP. Por exemplo, se a rota do controlador é "/Filmes", uma requisição para "/Filmes/1" passaria o valor 1 como id para o método filtrarPorId e se não passar ele vai parar o get acima porque não tem nada
    public IActionResult filtrarPorId(int id)//esse IActionResult é uma interface que representa o resultado de uma ação em um controlador de API. Ele permite retornar diferentes tipos de respostas HTTP, como Ok(), NotFound(), BadRequest(), entre outros. O método filtrarPorId é um endpoint HTTP GET que recebe um id como parâmetro e retorna o filme correspondente a esse id, ou uma resposta NotFound() se o filme não for encontrado.
    {
        var resposta = _dados.Filmes.FirstOrDefault(idFilme => idFilme.Id == id);
        if(resposta == null) { return NotFound(); } // o notFound() é um método que retorna uma resposta HTTP 404 Not Found, indicando que o recurso solicitado não foi encontrado. No caso, ele está sendo usado para indicar que o filme com o id especificado não foi encontrado na lista de filmes, e posso colocar parametos dentro da função que vai retornar o valor que estiver dentro do parenteses, por exemplo: return NotFound($"O filme com id {id} não foi encontrado."); para retornar uma mensagem personalizada junto com a resposta HTTP 404 Not Found.
        return Ok(resposta); //o ok() é um método que retorna uma resposta HTTP 200 OK, indicando que a solicitação foi bem-sucedida. No caso, ele está sendo usado para indicar que o filme com o id especificado foi encontrado e retornado com sucesso, e passando o filme o ok, eu esotu passando a resposta do filme encontrado para o cliente que fez a requisição HTTP, ou seja, o cliente receberá os dados do filme encontrado como parte da resposta HTTP 200 OK.
    }
}

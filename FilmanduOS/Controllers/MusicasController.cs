using AutoMapper;
using FilmanduOS.DTO;
using FilmanduOS.models;
using FilmanduOS.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmanduOS.Controllers;

[ApiController]
[Route("[controller]")]
public class MusicasController : ControllerBase
{
    private MusicasService _service;
    private IMapper _mapper;//e o  objeto autoMapper e vou adicionar no construtor para injeção de dependencias

    public MusicasController(MusicasService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }


    ///<summary>
    /// adicionando novas musicas ao banco de dados musicas
    /// </summary>
    /// <param name="musicaDTO">Objeto com os campos necessarios para criação de Musica</param>
    ///<returns>IActionResult</returns>
    ///<response code ="201"> caso a insercao seja feita com sucesso</response>
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AdicionarMusica([FromBody] CreateDTOMusicas musicaDTO)
    {
        Musica musica = _mapper.Map<Musica>(musicaDTO);
        await _service.AdicionarMusica(musica);
        return CreatedAtAction(nameof(GetMusicaId), new { musica.Id }, musica);
    }


    ///<summary>
    /// adicionando novas musicas ao banco de dados musicas por uma api publica mas feito filtros antes para não salvar tudo
    /// </summary>
    ///<returns>IActionResult</returns>
    ///<response code ="200"> caso a insercao seja feita com sucesso</response>

    [HttpPost("ApiPublica")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> adiconarMusicaDaApiPublica()
    {
        await _service.AdicionarMusicasPorApiPublica();
        return Ok();
    }

    ///<summary>
    /// puxando uma musica salva no banco de dados por id
    /// </summary>
    ///<returns>IActionResult</returns>
    ///<response code ="200"> caso a insercao seja feita com sucesso</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetMusicaId(int id)
    {
        var idMusica = _service.BucarMusicaId(id);
        if (idMusica == null)
        {
            return NotFound();
        }
        return Ok(idMusica);

    }


    ///<summary>
    /// puxando todas as musicas do banco de dados filtrando em .skip() e .take() |  utilizar: ?skip=int,take=int
    /// </summary>

    ///<returns>Musicas</returns>
    ///<response code ="201"> caso a insercao seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]//devolve para o swagger o tipo de resposta
    public IEnumerable<Musica> GetAllMusicasBySkipTake([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _service.GetMusicas(skip, take);
    }



    ///<summary>
    /// puxando todas as musicas salvas no banco de dados
    /// </summary>
    ///<returns>IActionResult</returns>
    ///<response code ="200"> caso a insercao seja feita com sucesso</response>
    [HttpGet("AllMusics")]
    public async Task<IEnumerable<Musica>> exibirTodasMusicas()
    {
        return _service.GetAllMusics();
    }



    ///<summary>
    /// atualizando todos os valores de um campo com o metodo request put
    /// </summary>
    /// <param name="updateMusica">Objeto com os campos necessarios para criação de Musica</param>
    ///<returns>IActionResult</returns>
    ///<response code ="201"> caso a insercao seja feita com sucesso</response>
    
    [HttpPut("{id:int}")]//pense no typeScript variavel e tipo, kkk não sei porque e invertido aqui mas e assim
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AtualizarFilme(int id, [FromBody] UpdateDTOMusicas updateMusica)
    {
        var respostaFilmeUpdate = await _service.AtualizarMusica(id, updateMusica);
        if (respostaFilmeUpdate)
        {
            return NoContent();
        }
        return NotFound();
    }



    ///<summary>
    /// atulizando valor usando o metodo request patch
    ///</summary>
    /// <param name="patch">Objeto com os campos necessarios para criação de Musica</param>
    ///<returns>IActionResult</returns>
    ///<response code ="201"> caso a insercao seja feita com sucesso</response>
    
    [HttpPatch("{id:int}")]//pense no typeScript variavel e tipo, kkk não sei porque é invertido aqui mas e assim
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AtualizarFilmeParcial(int id, JsonPatchDocument<UpdateDTOMusicas> patch)
    {
        Musica respostaFilmeUpdate = await _service.mandarParaControllerMusicaIdPatch(id);
        if (respostaFilmeUpdate == null)
        {
            return NotFound();
        }

        UpdateDTOMusicas transfDto = _mapper.Map<UpdateDTOMusicas>(respostaFilmeUpdate);

        patch.ApplyTo(transfDto, ModelState);//no caso aqui ele esta aplicando as mudanças aonde exatamente queremos mudar, op, path e value e esta tentando fazer isso no transfDto e se der erro ele salva em ModelState

        if (!TryValidateModel(transfDto))//ele valida se tudo deu certo, senão ele mostra o erro de validação com o erro que foi salvo em ModelState
        {
            return ValidationProblem(ModelState);
        }

        Musica ValidadoMusica = _mapper.Map(transfDto, respostaFilmeUpdate);
        await _service.MandarPatchParaBanco();
        return NoContent();
    }

    ///<summary>
    /// deletando uma musica por id
    /// </summary>
    ///<returns>IActionResult</returns>
    ///<response code ="201"> caso a insercao seja feita com sucesso</response>
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> deletandoMusicaPorId(int id)
    {
        bool valor = await _service.DeletandoMusica(id);

        if(valor)
        {
            return NotFound();
        }
        return NoContent();
    }
}


/*
 * 
 * PATCH (modifica só partes)

    Você manda operações:

    [
        { "op": "replace", "path": "/nome", "value": "Nova Música" }
    ]
 * 
 * 
 * 
 * 
 * 
 * 
 * "op": "replace"
 * 
 * Define qual operação será feita.

    Exemplos:

op	            significado
replace	        substituir valor
add	            adicionar valor
remove	        remover
copy	        copiar
move	        mover
test	        testar valor
 * 
 * 
 * Por que precisa de "path"
"path": "/nome"

Isso diz qual campo será alterado.

O / indica o caminho dentro do objeto.

Exemplo:

Objeto:

{
  "nome": "Musica",
  "artista": {
    "nome": "João"
  }
}

Patch:

{
  "op": "replace",
  "path": "/artista/nome",
  "value": "Maria"
}

Resultado:

{
  "nome": "Musica",
  "artista": {
    "nome": "Maria"
  }
}
 * 
 * 
 *
 *
 *
 *Por que "value"

    orque você precisa informar o novo valor.

    "value": "Nova Música"
    5️ Por que é um array

Porque você pode aplicar várias mudanças de uma vez.

[
  { "op": "replace", "path": "/nome", "value": "Nova Música" },
  { "op": "replace", "path": "/duracao", "value": 250 }
 * 
 * 
 * 
 */
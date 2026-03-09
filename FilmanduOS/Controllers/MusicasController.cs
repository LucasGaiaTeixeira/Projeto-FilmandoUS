using FilmanduOS.models;
using FilmanduOS.Service;
using Microsoft.AspNetCore.Mvc;

namespace FilmanduOS.Controllers;

    [ApiController]
    [Route("[controller]")]
public class MusicasController: ControllerBase
{
    private MusicasService _service;

    public MusicasController(MusicasService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarMusica([FromBody] Musica musica)
    {
        await _service.AdicionarMusica(musica);
        return CreatedAtAction(nameof(GetMusicaId), new { musica.Id }, musica);
    }

    [HttpPost("ApiPublica")]
    public async Task<IActionResult> adiconarMusicaDaApiPublica()
    {
        await _service.AdicionarMusicasPorApiPublica();
        return Ok();
    }


    [HttpGet("{id:int}")]
    public IActionResult GetMusicaId(int id)
    {
        var idMusica = _service.BucarMusicaId(id);
        if(idMusica == null)
        {
            return NotFound();
        }
        return Ok(idMusica);

    }

    [HttpGet]
    public IEnumerable<Musica> GetAllMusicasBySkipTake([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _service.GetMusicas(skip, take);
    }

    [HttpGet("AllMusics")]
    public async Task<IEnumerable<Musica>> exibirTodasMusicas()
    {
        return _service.GetAllMusics();
    }
}

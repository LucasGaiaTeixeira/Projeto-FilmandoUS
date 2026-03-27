using AutoMapper;
using FilmanduOS.Data;
using FilmanduOS.DTO;
using FilmanduOS.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;


namespace FilmanduOS.Service;

public class MusicasService
{
    private BancoDados _dados;
    private HttpClient _httpClient;
    private IMapper _mapper;

    public MusicasService(BancoDados dados, IMapper mapper)
    {
        _dados = dados;
        _httpClient = new HttpClient();
        _mapper = mapper;
    }

    public async Task AdicionarMusica(Musica musica)
    {
        _dados.Musicas.Add(musica);
        await _dados.SaveChangesAsync();

    }

    public async Task AdicionarMusicasPorApiPublica()
    {
        var musicasSalvas = await getApiMusicas();
        var musicasTake = musicasSalvas.Take(30);
        _dados.Musicas.AddRange(musicasTake);
        await _dados.SaveChangesAsync();
    }


    public Musica? BucarMusicaId(int id)
    {
        return _dados.Musicas.FirstOrDefault(item => item.Id == id);
    }

    public IEnumerable<Musica> GetAllMusics()
    {
        return _dados.Musicas;
    }

    public IEnumerable<Musica> GetMusicas(int skip, int take)
    {
        return _dados.Musicas.Skip(skip).Take(take);
    }

    private async Task<List<Musica>> getApiMusicas()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Musica>>("https://guilhermeonrails.github.io/api-csharp-songs/songs.json");
        return response ??  new List<Musica>();// se o valor da esquerda for nulo, uso o da direita, que dizer esses ??
    }

    
    public async Task<bool> AtualizarMusica(int id, UpdateDTOMusicas DTOmusica)
    {
        var respostaMusicaBanco = await _dados.Musicas.FirstOrDefaultAsync(musica => musica.Id == id);     
        if(respostaMusicaBanco == null || DTOmusica == null)
        {
            return false;
        }
        _mapper.Map(DTOmusica, respostaMusicaBanco);//desde quando em respostaBanco os objetos que foram rastreados no banco ja estão salvos os que foram ou foi rastreano no caso pelo id da musica na variavel, então ja esta rastreado então
        //quando passamos _mapper.Map(), estamos dizendo que quero trasnformar esse objeto DtoMusica em uma Musica, nisso o asp.net ja viu que quero atualizar essa valor do campo, então quando uso ao final _dados.saveChangesAsync()
        //ele ja le no o objeto do banco, salvo esse objeto, mapeado em _mapper.Map() então, quando uso _dados.... ele ja cria o sql dizendo aonde tem que mudar e muda de fato
        //obs: esse _mapper.map() dessa forma quier dizer oque tem em DTOmusica, copie e cole dentro de respostaMusicaBanco, não crie um outro objeto
        
        await _dados.SaveChangesAsync();
        return true;
    }


    public async Task<Musica?> mandarParaControllerMusicaIdPatch(int id)
    {
        var musicaBanco = await _dados.Musicas.FirstOrDefaultAsync(musica=> musica.Id == id);
        return musicaBanco;
    }

    public async Task MandarPatchParaBanco()
    {
        await _dados.SaveChangesAsync();
    }   

    public async Task<bool> DeletandoMusica(int id)
    {
        Musica musica = await _dados.Musicas.FirstOrDefaultAsync(musi => musi.Id == id);

        if (musica == null)
        {
            return true;
        }

        _dados.Remove(musica);
        await _dados.SaveChangesAsync();

        return false;
    }

}

using FilmanduOS.Data;
using FilmanduOS.models;


namespace FilmanduOS.Service;

public class MusicasService
{
    private BancoDados _dados;
    private HttpClient _httpClient;


    public MusicasService(BancoDados dados)
    {
        _dados = dados;
        _httpClient = new HttpClient();
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

    


}

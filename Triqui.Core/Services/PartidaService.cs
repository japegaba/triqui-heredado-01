using Triqui.Core.Models;

namespace Triqui.Core.Services;

public class PartidaService
{
    private readonly EvaluadorVictoria _evaluadorVictoria;

    public PartidaService()
    {
        _evaluadorVictoria = new EvaluadorVictoria();
    }

    public ResultadoPartida EvaluarEstado(Tablero tablero, char jugador)
    {
        if (tablero is null)
        {
            throw new ArgumentNullException(nameof(tablero));
        }

        var resultado = new ResultadoPartida
        {
            HayGanador = _evaluadorVictoria.HayGanador(tablero, jugador),
            Ganador = null,
            Empate = false
        };

        if (resultado.HayGanador)
        {
            resultado.Ganador = jugador;
            return resultado;
        }

        resultado.Empate = tablero.Casillas.All(casilla => casilla != ' ');
        return resultado;
    }
}

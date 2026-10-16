using Triqui.Core.Models;
using Triqui.Core.Validators;

namespace Triqui.Core.Services;

/// <summary>
/// Coordina la validación de jugadas y la evaluación del resultado de una partida.
/// </summary>
public class PartidaService
{
    private readonly EvaluadorVictoria _evaluadorVictoria;

    public PartidaService()
    {
        _evaluadorVictoria = new EvaluadorVictoria();
    }

    /// <summary>
    /// Valida una jugada antes de permitir que el tablero sea actualizado.
    /// </summary>
    /// <param name="tablero">Tablero de la partida actual.</param>
    /// <param name="posicion">Posición elegida por el jugador.</param>
    /// <returns>El motivo de aceptación o rechazo de la jugada.</returns>
    public EstadoValidacionJugada ValidarJugada(Tablero tablero, int posicion)
    {
        if (tablero is null)
        {
            throw new ArgumentNullException(nameof(tablero));
        }

        ResultadoPartida estado = EvaluarEstado(tablero);
        if (estado.HayGanador || estado.Empate)
        {
            return EstadoValidacionJugada.PartidaFinalizada;
        }

        return ValidadorJugada.Validar(tablero, posicion);
    }

    /// <summary>
    /// Evalúa el estado completo del tablero, incluyendo victorias de ambos jugadores.
    /// </summary>
    /// <param name="tablero">Tablero que se debe evaluar.</param>
    /// <returns>El ganador, un empate o un resultado de partida en curso.</returns>
    public ResultadoPartida EvaluarEstado(Tablero tablero)
    {
        if (tablero is null)
        {
            throw new ArgumentNullException(nameof(tablero));
        }

        if (_evaluadorVictoria.HayGanador(tablero, 'X'))
        {
            return new ResultadoPartida { HayGanador = true, Ganador = 'X' };
        }

        if (_evaluadorVictoria.HayGanador(tablero, 'O'))
        {
            return new ResultadoPartida { HayGanador = true, Ganador = 'O' };
        }

        return new ResultadoPartida
        {
            Empate = tablero.Casillas.All(casilla => casilla != ' ')
        };
    }

    /// <summary>
    /// Evalúa si el jugador indicado completó una línea o si el tablero terminó en empate.
    /// </summary>
    /// <param name="tablero">Tablero que se debe evaluar.</param>
    /// <param name="jugador">Símbolo del jugador que acaba de realizar la jugada.</param>
    /// <returns>El resultado de la evaluación para el jugador indicado.</returns>
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

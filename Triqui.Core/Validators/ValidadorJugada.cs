using Triqui.Core.Models;

namespace Triqui.Core.Validators;

/// <summary>
/// Valida que una posición exista y que su casilla esté disponible.
/// </summary>
public static class ValidadorJugada
{
    /// <summary>
    /// Determina el resultado de validar una posición en el tablero.
    /// </summary>
    /// <param name="tablero">Tablero donde se intentará jugar.</param>
    /// <param name="posicion">Posición elegida, entre 1 y 9.</param>
    /// <returns>El motivo de aceptación o rechazo de la posición.</returns>
    public static EstadoValidacionJugada Validar(Tablero tablero, int posicion)
    {
        if (tablero is null)
        {
            throw new ArgumentNullException(nameof(tablero));
        }

        if (!Tablero.EsPosicionValida(posicion))
        {
            return EstadoValidacionJugada.PosicionFueraDeRango;
        }

        return tablero.EstaDisponible(posicion)
            ? EstadoValidacionJugada.Valida
            : EstadoValidacionJugada.CasillaOcupada;
    }
}

using Triqui.Core.Models;

namespace Triqui.Core.Validators;

public static class ValidadorJugada
{
    public static bool EsCasillaValida(Tablero tablero, int posicion)
    {
        if (tablero is null)
        {
            throw new ArgumentNullException(nameof(tablero));
        }

        return Tablero.EsPosicionValida(posicion) && tablero.EstaDisponible(posicion);
    }
}

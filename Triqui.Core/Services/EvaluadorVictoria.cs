using Triqui.Core.Models;

namespace Triqui.Core.Services;

public class EvaluadorVictoria
{
    public bool HayGanador(Tablero tablero, char jugador)
    {
        if (tablero is null)
        {
            throw new ArgumentNullException(nameof(tablero));
        }

        for (int fila = 0; fila < Tablero.Dimension; fila++)
        {
            if (tablero.Casillas[fila * 3] == jugador && tablero.Casillas[fila * 3 + 1] == jugador && tablero.Casillas[fila * 3 + 2] == jugador)
            {
                return true;
            }
        }

        for (int columna = 0; columna < Tablero.Dimension; columna++)
        {
            if (tablero.Casillas[columna] == jugador && tablero.Casillas[columna + 3] == jugador && tablero.Casillas[columna + 6] == jugador)
            {
                return true;
            }
        }

        if (tablero.Casillas[0] == jugador && tablero.Casillas[4] == jugador && tablero.Casillas[8] == jugador)
        {
            return true;
        }

        if (tablero.Casillas[2] == jugador && tablero.Casillas[4] == jugador && tablero.Casillas[6] == jugador)
        {
            return true;
        }

        return false;
    }
}

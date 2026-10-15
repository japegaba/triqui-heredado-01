namespace Triqui.Core.Models;

public class Tablero
{
    public const int Dimension = 3;
    public const int CantidadCasillas = Dimension * Dimension;

    public char[] Casillas { get; } = new char[CantidadCasillas];

    public void Reiniciar()
    {
        Array.Fill(Casillas, ' ');
    }

    public bool EstaDisponible(int posicion)
    {
        return EsPosicionValida(posicion) && Casillas[posicion - 1] == ' ';
    }

    public char ObtenerCasilla(int posicion)
    {
        if (!EsPosicionValida(posicion))
        {
            throw new ArgumentOutOfRangeException(nameof(posicion), "La posición debe estar entre 1 y 9.");
        }

        return Casillas[posicion - 1];
    }

    public void Marcar(int posicion, char jugador)
    {
        if (!EsPosicionValida(posicion))
        {
            throw new ArgumentOutOfRangeException(nameof(posicion), "La posición debe estar entre 1 y 9.");
        }

        if (!EstaDisponible(posicion))
        {
            throw new InvalidOperationException("La casilla ya está ocupada.");
        }

        Casillas[posicion - 1] = jugador;
    }

    public static bool EsPosicionValida(int posicion)
    {
        return posicion >= 1 && posicion <= CantidadCasillas;
    }
}

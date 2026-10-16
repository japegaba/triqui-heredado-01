using Triqui.Core.Models;
using Triqui.Core.Services;
using Triqui.Core.Validators;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var tablero = new Tablero();
var partidaService = new PartidaService();
int ganX = 0, ganO = 0, empates = 0;
bool seguir = true;

while (seguir)
{
    tablero.Reiniciar();
    int turno = 0;
    bool fin = false;

    while (!fin)
    {
        Console.WriteLine();
        Console.WriteLine("=== TRIQUI ===");
        Console.WriteLine($"X: {ganX}   O: {ganO}   Empates: {empates}");
        Console.WriteLine();
        ImprimirTablero(tablero, mostrarNumeros: true);
        Console.WriteLine();

        char jugador = turno % 2 == 0 ? 'X' : 'O';
        int? posicion = LeerCasillaValida(jugador, tablero, partidaService);

        if (posicion is null)
        {
            fin = RegistrarResultado(tablero, partidaService, ref ganX, ref ganO, ref empates);
            if (!fin)
            {
                throw new InvalidOperationException("La partida se marcó como finalizada sin un resultado.");
            }

            continue;
        }

        tablero.Marcar(posicion.Value, jugador);
        turno++;
        fin = RegistrarResultado(tablero, partidaService, ref ganX, ref ganO, ref empates);
    }

    Console.Write("Otra partida? (s/n): ");
    string? respuesta = Console.ReadLine();
    if (respuesta != "s" && respuesta != "S")
    {
        seguir = false;
    }
}

Console.WriteLine($"Marcador final -> X: {ganX}  O: {ganO}  Empates: {empates}");
Console.WriteLine("Chao!");

static int? LeerCasillaValida(char jugador, Tablero tablero, PartidaService partidaService)
{
    while (true)
    {
        Console.Write($"Turno de {jugador}. Elige una casilla (1-9): ");
        if (!TryLeerPosicion(out int posicion))
        {
            continue;
        }

        EstadoValidacionJugada validacion = partidaService.ValidarJugada(tablero, posicion);
        if (validacion == EstadoValidacionJugada.Valida)
        {
            return posicion;
        }

        if (validacion == EstadoValidacionJugada.PosicionFueraDeRango)
        {
            Console.WriteLine("La casilla debe estar entre 1 y 9.");
            continue;
        }

        if (validacion == EstadoValidacionJugada.CasillaOcupada)
        {
            Console.WriteLine("La casilla está ocupada. Elija otra.");
            continue;
        }

        if (validacion == EstadoValidacionJugada.PartidaFinalizada)
        {
            return null;
        }

        MostrarErrorJugada(validacion);
    }
}

static bool TryLeerPosicion(out int posicion)
{
    string? entrada = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(entrada))
    {
        Console.WriteLine("La entrada está vacía. Introduce un número del 1 al 9.");
        posicion = 0;
        return false;
    }

    if (!int.TryParse(entrada, out posicion))
    {
        Console.WriteLine("La entrada no es válida. Debe ser un número del 1 al 9.");
        return false;
    }

    return true;
}

static void MostrarErrorJugada(EstadoValidacionJugada validacion)
{
    switch (validacion)
    {
        case EstadoValidacionJugada.PosicionFueraDeRango:
            Console.WriteLine("La casilla debe estar entre 1 y 9.");
            return;
        case EstadoValidacionJugada.CasillaOcupada:
            Console.WriteLine("La casilla está ocupada. Elija otra.");
            return;
        default:
            throw new InvalidOperationException("El estado de validación de la jugada no es válido.");
    }
}

static bool RegistrarResultado(
    Tablero tablero,
    PartidaService partidaService,
    ref int ganX,
    ref int ganO,
    ref int empates)
{
    ResultadoPartida resultado = partidaService.EvaluarEstado(tablero);
    if (!resultado.HayGanador && !resultado.Empate)
    {
        return false;
    }

    ImprimirTablero(tablero, mostrarNumeros: false);
    ActualizarMarcador(resultado, ref ganX, ref ganO, ref empates);
    return true;
}

static void ActualizarMarcador(ResultadoPartida resultado, ref int ganX, ref int ganO, ref int empates)
{
    if (resultado.HayGanador)
    {
        Console.WriteLine($"Gana {resultado.Ganador}!!!");
        if (resultado.Ganador == 'X')
        {
            ganX++;
        }
        else
        {
            ganO++;
        }
    }
    else
    {
        Console.WriteLine("Empate");
        empates++;
    }
}

static void ImprimirTablero(Tablero tablero, bool mostrarNumeros)
{
    for (int fila = 0; fila < Tablero.Dimension; fila++)
    {
        Console.WriteLine($" {ObtenerTextoCasilla(tablero, fila * 3 + 1, mostrarNumeros)} | {ObtenerTextoCasilla(tablero, fila * 3 + 2, mostrarNumeros)} | {ObtenerTextoCasilla(tablero, fila * 3 + 3, mostrarNumeros)}");
        if (fila < Tablero.Dimension - 1)
        {
            Console.WriteLine("---+---+---");
        }
    }
}

static string ObtenerTextoCasilla(Tablero tablero, int posicion, bool mostrarNumeros)
{
    char contenido = tablero.ObtenerCasilla(posicion);
    return contenido != ' ' ? contenido.ToString() : mostrarNumeros ? posicion.ToString() : " ";
}

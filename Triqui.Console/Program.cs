// TRIQUI v1 - version estable
// lo hice rapido para la feria de la empresa, funciona bien
// TODO: organizar esto algun dia

Console.OutputEncoding = System.Text.Encoding.UTF8;

char[] b = new char[9];
int t = 0; // turno
int ganX = 0, ganO = 0, emp = 0;
bool seguir = true;

while (seguir)
{
    for (int i = 0; i < 9; i++) b[i] = ' ';
    t = 0;
    bool fin = false;

    while (!fin)
    {
        // mostrar tablero
        Console.WriteLine();
        Console.WriteLine("=== TRIQUI ===");
        Console.WriteLine("X: " + ganX + "   O: " + ganO + "   Empates: " + emp);
        Console.WriteLine();
        Console.WriteLine(" " + (b[0] == ' ' ? "1" : b[0].ToString()) + " | " + (b[1] == ' ' ? "2" : b[1].ToString()) + " | " + (b[2] == ' ' ? "3" : b[2].ToString()));
        Console.WriteLine("---+---+---");
        Console.WriteLine(" " + (b[3] == ' ' ? "4" : b[3].ToString()) + " | " + (b[4] == ' ' ? "5" : b[4].ToString()) + " | " + (b[5] == ' ' ? "6" : b[5].ToString()));
        Console.WriteLine("---+---+---");
        Console.WriteLine(" " + (b[6] == ' ' ? "7" : b[6].ToString()) + " | " + (b[7] == ' ' ? "8" : b[7].ToString()) + " | " + (b[8] == ' ' ? "9" : b[8].ToString()));
        Console.WriteLine();

        char jug = t % 2 == 0 ? 'X' : 'O';
        int pos = LeerCasillaValida(jug, b);

        b[pos - 1] = jug;
        t++;

        // verifica empate
        if (CheckWinner(jug))
        {
            printBoard();
            Console.WriteLine("Gana " + jug + "!!!");
            if (jug == 'X') ganX++;
            else ganO++;
            fin = true;
        }
        else if (t == 9)
        {
            printBoard();
            Console.WriteLine("Empate");
            emp++;
            fin = true;
        }

        // int pc = new Random().Next(9); // la compu jugaba sola pero se dano, despues lo arreglo
    }

    Console.Write("Otra partida? (s/n): ");
    string? r = Console.ReadLine();
    if (r != "s" && r != "S") seguir = false;
}

Console.WriteLine("Marcador final -> X: " + ganX + "  O: " + ganO + "  Empates: " + emp);
Console.WriteLine("Chao!");

bool CheckWinner(char p)
{
    // filas
    if (b[0] == p && b[1] == p && b[2] == p) return true;
    if (b[3] == p && b[4] == p && b[5] == p) return true;
    if (b[6] == p && b[7] == p && b[8] == p) return true;
    // columnas
    if (b[0] == p && b[3] == p && b[6] == p) return true;
    if (b[1] == p && b[4] == p && b[7] == p) return true;
    if (b[2] == p && b[5] == p && b[8] == p) return true;
    // diagonales
    if (b[0] == p && b[4] == p && b[8] == p) return true;
    if (b[2] == p && b[4] == p && b[6] == p) return true;
    return false;
}

void printBoard()
{
    Console.WriteLine();
    for (int f = 0; f < 3; f++)
    {
        Console.WriteLine(" " + b[f * 3] + " | " + b[f * 3 + 1] + " | " + b[f * 3 + 2]);
        if (f < 2) Console.WriteLine("---+---+---");
    }
    Console.WriteLine();
}

int LeerCasillaValida(char jug, char[] tablero)
{
    while (true)
    {
        Console.Write("Turno de " + jug + ". Elige una casilla (1-9): ");
        string? entrada = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(entrada))
        {
            Console.WriteLine("La entrada está vacía. Introduce un número del 1 al 9.");
            continue;
        }

        if (!int.TryParse(entrada, out int pos))
        {
            Console.WriteLine("La entrada no es válida. Debe ser un número del 1 al 9.");
            continue;
        }

        if (pos < 1 || pos > 9)
        {
            Console.WriteLine("La casilla debe estar entre 1 y 9.");
            continue;
        }

        if (tablero[pos - 1] != ' ')
        {
            Console.WriteLine("La casilla está ocupada. Elija otra.");
            continue;
        }

        return pos;
    }
}

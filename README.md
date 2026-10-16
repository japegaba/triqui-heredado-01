# Triqui

Juego de triqui (tres en línea) para dos jugadores humanos en la consola.

## Requisitos

- SDK de .NET 10.

## Compilar y ejecutar

Desde la carpeta raíz del repositorio:

```powershell
dotnet build
dotnet run --project Triqui.Console/Triqui.Console.csproj
```

## Reglas y flujo de la partida

- El tablero tiene 3 filas y 3 columnas, para un total de 9 casillas numeradas de 1 a 9 en orden de lectura:

  ```text
  1 | 2 | 3
  ---+---+---
  4 | 5 | 6
  ---+---+---
  7 | 8 | 9
  ```

- Participan dos jugadores: X empieza y los jugadores alternan entre X y O.
- En cada turno se elige una casilla disponible. Una jugada válida marca la casilla y avanza el turno.
- Gana el primer jugador que complete una línea de tres marcas: una fila, una columna o cualquiera de las dos diagonales (1-5-9 y 3-5-7).
- Si las nueve casillas quedan ocupadas y nadie ha completado una línea, la partida termina en empate.
- Al terminar una partida, se actualiza el marcador de la sesión y se pregunta si se desea jugar otra. Una nueva partida limpia el tablero, pero conserva el marcador mientras la aplicación siga abierta.

## Validación de jugadas

La consola solicita una posición entera entre 1 y 9. Las entradas vacías, no numéricas o decimales se rechazan y se vuelve a solicitar la casilla. El dominio comprueba, antes de modificar el tablero, que la posición esté dentro del rango, que la casilla esté libre y que la partida no haya terminado. Una casilla ocupada o una posición fuera de rango no cambia el tablero ni consume el turno.

## Depuración

En Visual Studio Code, abre la carpeta raíz, selecciona el proyecto `Triqui.Console` para ejecutar o depurar y coloca puntos de interrupción en `Triqui.Console/Program.cs` o en las clases de `Triqui.Core`. La configuración de compilación también puede iniciarse desde la terminal integrada con `dotnet run --project Triqui.Console/Triqui.Console.csproj`.

Para comprobar los datos de entrada de las partidas de referencia, están los archivos de `pruebas/`: `entrada-invalida.txt`, `dos-partidas.txt` y `x-gana-diagonal-357.txt`. Sus jugadas cubren entradas rechazadas, partidas repetidas y la diagonal secundaria.

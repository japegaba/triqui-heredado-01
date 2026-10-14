# Diagnóstico del programa

## Resumen
- El programa implementa un juego de Triqui en consola para dos jugadores humanos.
- Recorre partidas en bucle, valida casillas, lleva marcador y permite iniciar otra partida.
- Tiene fallas de entrada y de lógica que pueden cerrar la aplicación o dejar una victoria válida sin detectar.

## Hallazgos

| ID | Hallazgo | Dónde | Tipo | Impacto | Cómo se reproduce o se ve | Decisión del equipo |
| --- | --- | --- | --- | --- | --- | --- |
| D-01 | La lectura de la casilla usa `int.Parse(Console.ReadLine())` sin ninguna validación ni manejo de excepciones. El programa puede cerrarse por una entrada no numérica o vacía. | Program.cs, bloque de lectura de la jugada dentro del bucle principal | Falla | Alto | 1. Ejecutar el programa. 2. En cualquier turno, escribir una cadena no numérica como `abc`, un valor vacío o una entrada con decimales como `1.5`. 3. La ejecución falla con una excepción de formato o de argumento antes de llegar a la validación de rango. |  |
| D-02 | La comprobación de ganador omite la diagonal secundaria y no reconoce una línea válida en la forma 3-5-7. Esto provoca un resultado incorrecto en la partida. | `CheckWinner(char p)` | Falla | Alto | 1. Iniciar una nueva partida. 2. Colocar las jugadas para X en las casillas 3, 5 y 7, dejando una formación de diagonal secundaria (`3-5-7`). 3. El juego no declara ganador aunque la línea es válida. La partida sigue adelante y puede terminar en empate o con un resultado inconsistente. |  |
| D-03 | La lógica del juego, la entrada del usuario, el render del tablero y la condición de fin están mezclados en el mismo bucle de juego. No se cumple el principio de responsabilidad única. | Bucle principal del juego (`while (seguir)`, `while (!fin)`) | Diseño | Medio | El mismo bloque controla el tablero, lectura de movimientos, validación, actualización del estado, comprobación de fin de partida y flujo de repetición. Cualquier cambio en la interfaz o en las reglas obliga tocar el mismo bloque, y la complejidad crece sin un límite claro de responsabilidades. |  |
| D-04 | La validación de casillas reconoce solo rango numérico y ocupación, pero no trata entradas de consola no controladas de forma robusta. El programa asume que la entrada siempre es válida y la ruptura ocurre antes de controlar el error. | Bloque `if (pos < 1 || pos > 9)` y lectura de `Console.ReadLine()` | Calidad | Medio | En una ejecución real, una entrada como `x` o una línea en blanco provoca una excepción antes de la validación de casilla. La aplicación se cierra sin ningún manejo de error, lo que hace que la experiencia sea frágil y dependiente del usuario. |  |
| D-05 | El programa no documenta el flujo ni la lógica del juego en comentarios ni en artefactos de documentación, y los comentarios existentes son muy generales y contradictorios. La documentación es insuficiente para mantener el código. | Encabezado del archivo y comentarios de TODO | Documentación | Bajo | El archivo incluye comentarios generales como `TODO: organizar esto algun dia` y una nota de versión, pero no explica cómo funciona el tablero, la lógica del ganador ni el comportamiento esperado de las partidas. No hay un documento de uso ni de reglas del juego. |  |

## Observaciones adicionales

- La comprobación de victoria solo evalúa filas, columnas y la diagonal principal; no contempla la diagonal secundaria, por lo que una combinación válida puede quedar sin detectar.
- El flujo principal depende del valor devuelto por `Console.ReadLine()` sin ninguna protección frente a entradas no válidas o nulas, lo que convierte la interacción con consola en un punto crítico de cierre del programa.
- No se presenta un diseño modular ni una separación clara entre reglas del juego, visualización y control del ciclo de ejecución.
